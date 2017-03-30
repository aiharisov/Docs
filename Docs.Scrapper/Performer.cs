using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Scrapper.Base;
using Docs.Scrapper.VSRF;
using Docs.Scrapper.SudRF;

namespace Docs.Scrapper
{
    public class Performer
    {
        #region Singleton, init
        private static readonly object padlock = new object();
        private static Performer _provider;
        public static Performer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_provider == null)
                        _provider = new Performer();
                    return _provider;
                }
            }
        }
        #endregion
        private List<IScrapperBase> ScrapperList { get; set; }
        public List<IScrapperBase> GetAvalibleScrappers()
        {
            return ScrapperList;
        }
        private Performer()
        {
            InitScrappers();
        }
        private void InitScrappers()
        {
            ScrapperList = new List<IScrapperBase>();
            foreach (var court in DataProvider.Instance.DocsProvider.CourtRepository.Get(x => x.CourtId > 1))
            {
                switch((ScrapperType)court.TypeSiteId)
                {
                    case ScrapperType.VSRF: { ScrapperList.Add(new VSRFEngine(court)); break; }
                    case ScrapperType.SUDRF: { ScrapperList.Add(new SudRFEngine(court)); break; }
                }
            }
        }
    }
}
