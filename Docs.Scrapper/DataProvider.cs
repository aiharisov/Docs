using Docs.DAL.UOW;
using Docs.DAL.ES;
using Docs.DAL;

namespace Docs.Scrapper
{
    public class DataProvider
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static DataProvider _provider;
        public static DataProvider Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_provider == null)
                        _provider = new DataProvider();
                    return _provider;
                }
            }
        }
        #endregion
        /// <summary>
        /// Предоставляет доступ к документам в БД, IDisposable - обязательно!
        /// </summary>
        internal UOWDocs DocsProvider { get { return new UOWDocs(); } }
        /// <summary>
        /// Предоставляет доступ к логу, IDisposable - обязательно!
        /// </summary>
        internal UOWLog LogProvider { get { return new UOWLog(); } }
        /// <summary>
        /// Предоставляет доступ к ElasticSearch
        /// </summary>
        internal ElasticSearchEngine ESProvider
        {
            get { return ElasticSearchEngine.Instance; }
        }
        /// <summary>
        /// Предоставляет доступ к справочникам
        /// </summary>
        internal SPRRepository SPRProvider
        {
            get { return SPRRepository.Instance; }
        }
    }
}
