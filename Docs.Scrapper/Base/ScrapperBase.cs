using System;
using System.Collections.Generic;
using System.Linq;
using Docs.Model.DB.Docs;
using Docs.Model.DB.Log;
using Docs.Model.ES;

namespace Docs.Scrapper.Base
{
    public enum ScrapperType { VSRF = 1, SUDRF }
    public class ScrapperBase: IScrapperBase
    {
        /// <summary>
        /// Тип движка
        /// </summary>
        public ScrapperType ScrapperType { get; internal set; }
        /// <summary>
        /// Выбранный суд
        /// </summary>
        public Court Court { get; internal set; }
        /// <summary>
        /// Дата последнего запуска
        /// </summary>
        internal DateTime LastLaunchDate { get; set; }
        /// <summary>
        /// Дает время случайной задержки в сек
        /// </summary>
        internal int RandomTime
        {
            get { return new Random().Next(1000, 3000); }
        }
        /// <summary>
        /// Пары значение в "HTML/поле в модели" для парсера
        /// </summary>
        internal Dictionary<string, KeyParam> ScrapKeys { get; set; }
        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="court"></param>
        public ScrapperBase(Court court)
        {
            Court = court;
            ScrapperType = (ScrapperType)court.TypeSiteId;
            using (var provider = DataProvider.Instance.LogProvider)
            {
                DateTime? date = provider.ScrapperLogRepository.Get(x => x.SudId == (int)ScrapperType).Select(x => x.DateOperation).FirstOrDefault();
                LastLaunchDate = date ?? DateTime.Now.AddDays(-1);
            }
        }
        #region Scrap Methods
        /// <summary>
        /// Начать сбор данных со значениями по умолчанию
        /// </summary>
        public void DoScrap()
        {
            DateTime End = DateTime.Now;
            Scrap(LastLaunchDate.AddDays(-7), End);
            LastLaunchDate = End;
            ScrapperLog log = new ScrapperLog()
            {
                DateOperation = End,
                SudId = Court.CourtId
            };
            using (var provider = DataProvider.Instance.LogProvider)
            {
                provider.ScrapperLogRepository.Insert(log);
                provider.Save();
            }

        }
        /// <summary>
        /// Начать сбор данных
        /// </summary>
        /// <param name="Start">Начальная дата</param>
        /// <param name="End">Конечная дата</param>
        public void DoScrap(DateTime Start, DateTime End)
        {
            Scrap(Start, End);
        }
        /// <summary>
        /// Метод предоставляющий логику парсера
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        internal virtual void Scrap(DateTime Start, DateTime End) { }
        #endregion
        #region DB
        /// <summary>
        /// Сохраняет модель в БД
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        internal Doc SaveInDB(Doc doc)
        {
            using (var dp = DataProvider.Instance.DocsProvider)
            {
                dp.DocRepository.Insert(doc);
                dp.Save();
            }
            return doc;
        }
        #region ES
        /// <summary>
        /// Индекс в ElasticSearch
        /// </summary>
        private const string Index = "Docs";

        /// <summary>
        /// Сохраняет модель в ES
        /// </summary>
        /// <param name="data"></param>
        internal void SaveInES(DataDoc data)
        {
            DataProvider.Instance.ESProvider.IndexData<DataDoc>(data, Index);
        }
        #endregion
        #endregion
    }
    internal class KeyParam
    {
        /// <summary>
        /// Имя поля
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Выражение
        /// </summary>
        public string Expression { get; set; }
    }
}
