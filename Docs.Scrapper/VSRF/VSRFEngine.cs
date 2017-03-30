using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Scrapper.Base;
using Docs.Model.DB.Docs;
using Docs.Model.ES;
using AngleSharp.Parser.Html;
using Docs.Core;
using System.Threading;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using AngleSharp.Dom;

namespace Docs.Scrapper.VSRF
{
    public class VSRFEngine: ScrapperBase, IScrapperBase
    {
        /// <summary>
        /// Page - страница
        /// iDateB - Начало
        /// iDateE - Конец
        /// iDoc - Вид документа - опеределяем из текста, что бы снизить к-во запросов
        /// delo_type - Тип дела
        /// 
        /// </summary>
        private const string FormatURL = "page={0}&i1p1=1&i1text=&number=&iDateB={1}&iDateE={2}&email=&iDoc=0&delo_type={3}&iPhase={4}&iSpeaker=0&Frash=0&search.x=56&search.y=15";
        private Dictionary<int, int> deloType;
        /// <summary>
        /// Ключ - ключ в БД
        /// Значение - ключ на сайте
        /// </summary>
        private Dictionary<int, int> DeloType
        {
            get
            {
                if (deloType == null)
                {
                    deloType = new Dictionary<int, int>();
                    deloType.Add(2, 1);//Уголовное
                    deloType.Add(3, 2);//Гражданское
                    deloType.Add(4, 3);//Административное
                    deloType.Add(5, 4);//Экономическое
                    deloType.Add(6, 5);//Дисциплинарный спор
                }
                return deloType;
            }
        }
        private Dictionary<int, int> iPhase;
        /// <summary>
        /// Ключ - ключ в БД
        /// Значение - ключ на сайте
        /// </summary>
        private Dictionary<int, int> IPhase
        {
            get
            {
                if (iPhase == null)
                {
                    iPhase = new Dictionary<int, int>();
                    iPhase.Add(2, 3);//Первая инстанция
                    iPhase.Add(3, 6);//Апелляция
                    iPhase.Add(4, 4);//Кассация
                    iPhase.Add(5, 5);//Надзор
                }
                return iPhase;
            }
        }
        public VSRFEngine(Court court) : base(court) { }
        internal override void Scrap(DateTime Start, DateTime End)
        {
            foreach (var dt in DeloType.Keys)
            {
                foreach (var p in IPhase.Keys.Where(x => x != 1))
                {
                    ScrapHTML(dt, p, Start, End);
                    Thread.Sleep(RandomTime);
                }
            }
        }
        private void ScrapHTML(int DType, int Phase, DateTime Start, DateTime End)
        {
            int page = 1;
            bool IsExit = false;
            while (!IsExit)
            {
                string URL = $"{Court.BaseURL}/{Court.URLToDoc}/{string.Format(FormatURL, page, Start.ToString("dd.MM.yyyy"), End.ToString("dd.MM.yyyy"), DeloType[DType], iPhase[Phase])}";
                string HTML = HTTPHelper.Instance.HTTPGet(URL, Encoding.GetEncoding(Court.Encoding));
                var htmlDoc = (new HtmlParser()).Parse(HTML);
                if (!HTML.Contains("Последняя")) IsExit = true;
                //Получаем искомую таблицу
                var table = htmlDoc.QuerySelector("table.q_t_form");
                if (table != null)
                {
                    //Забираем все блоки
                    var bloks = table.QuerySelectorAll("tr.resultRow");
                    if (bloks.Length > 0)
                    {
                        foreach (var block in bloks)
                        {
                            //формируем модель для БД
                            var doc = FillDBModel(block, DType, Phase);
                            //Сохраняем в БД
                            doc = SaveInDB(doc);
                            //формируем модель для ElasticSearch
                            //т.к. будем делать еще один запрос к сайту, то спим
                            Thread.Sleep(RandomTime);
                            var dataDoc = FillESModel(doc);
                            //Сохраняем в ElasticSearch
                            SaveInES(dataDoc);
                        }
                        if (!IsExit) Thread.Sleep(RandomTime);
                    }
                    else IsExit = true;
                }
                else IsExit = true;
                page++;
            }
        }
        private Doc FillDBModel(IElement block, int DType, int Phase)
        {
            var doc = new Doc();
            doc.Court = Court;
            var inst = DataProvider.Instance.SPRProvider.InstanceList.Where(x => x.InstanceId == Phase).FirstOrDefault();
            if (inst == null) DataProvider.Instance.SPRProvider.InstanceList.First();
            doc.Instance = inst;
            var dcase = DataProvider.Instance.SPRProvider.CaseList.Where(x => x.Caseid == DType).FirstOrDefault();
            if (dcase == null) dcase = DataProvider.Instance.SPRProvider.CaseList.First();
            doc.Case = dcase;
            doc.DateOp = DateTime.Now;
            doc.OriginalDocURL = Court.BaseURL + @"/" + block.QuerySelector($"a:contains('Документ в формате PDF')").Attributes["href"].Value;
            var cardUrl = block.QuerySelector($"a:contains('Полная информация о производстве')").Attributes["href"].Value;
            if (!cardUrl.ToLower().Contains("http")) cardUrl = Court.BaseURL + @"/" + cardUrl;
            doc.DocCardURL = cardUrl;
            doc.DocComment = block.QuerySelectorAll("td")[1].TextContent;
            doc.DocComment = doc.DocComment.Replace("Докладчик", " Докладчик");
            doc.DocComment = doc.DocComment.Remove(doc.DocComment.IndexOf("Документ"));

            var typeDoc = DataProvider.Instance.SPRProvider.DocTypeList.Where(x => x.Name.ToLower().Contains(doc.DocComment.Split(' ')[0].ToLower())).FirstOrDefault();
            if (typeDoc == null) typeDoc = DataProvider.Instance.SPRProvider.DocTypeList.Where(x => x.DocTypeId == 4).First();
            doc.TypeDoc = typeDoc;
            DateTime date = new DateTime();
            if (DateTime.TryParse(doc.DocComment.Split(' ')[2].Remove(doc.DocComment.Split(' ')[2].Length - 1), out date))
                doc.DocDate = date;
            else
                doc.DocDate = DateTime.MinValue;
            doc.DocName = block.QuerySelector("a").TextContent;
            return doc;
        }
        private DataDoc FillESModel(Doc doc)
        {
            var dataDoc = new DataDoc();
            dataDoc.Id = doc.DocId;
            dataDoc.CaseId = doc.Case.Caseid;
            dataDoc.CourtId = doc.Court.CourtId;
            dataDoc.DateDoc = doc.DocDate;
            dataDoc.DateOp = doc.DateOp;
            dataDoc.InstanceId = doc.Instance.InstanceId;
            dataDoc.TypeDocId = doc.TypeDoc.DocTypeId;
            dataDoc.SearchText = ReadPDFDocToText(HTTPHelper.Instance.HTTPGetFile(doc.OriginalDocURL, Encoding.GetEncoding(Court.Encoding)));
            dataDoc.HTML = TextToHTML(dataDoc.SearchText);
            return dataDoc;
        }
        private string ReadPDFDocToText(byte[] DocStream)
        {
            var txt = string.Empty;
            using (PdfReader reader = new PdfReader(DocStream))
            {
                for (int countPdfPage = 1; countPdfPage <= reader.NumberOfPages; countPdfPage++)
                {
                    txt += PdfTextExtractor.GetTextFromPage(reader, countPdfPage, new LocationTextExtractionStrategy());
                }
            }
            return txt;
        }
        private string TextToHTML(string data)
        {
            var result = "<html><body><div>";
            var temp = data.Split('\n');
            int i = 0;
            foreach (string row in temp)
            {
                if (i < 6)
                    result += $"<div style=\"text-align: center;margin: 0 auto;\">{row}</div>";
                if (i == 6)
                    result += "<br><br><div style=\"text-align: justify;margin: 0 auto;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                if (i >= 6)
                    if (!row.ToLower().Contains("установил"))
                        result += "" + row;
                    else
                        result += $"</div><div style=\"text-align: center;margin: 0 auto;\"><br>{row}</div><br><div style=\"text-align: justify;margin: 0 auto;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                i++;
            }
            result += "</div></div></body></html>";
            return result;
        }
    }
}
