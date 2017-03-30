using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Docs.Core;
using System.Configuration;
using Nest;

namespace Docs.DAL.ES
{
    public class ElasticSearchEngine
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static ElasticSearchEngine _provider;
        public static ElasticSearchEngine Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_provider == null)
                        _provider = new ElasticSearchEngine();
                    return _provider;
                }
            }
        }
        #endregion
        public ElasticClient Repository { get; internal set; }
        private Uri ServiceURL { get; set; }
        private ElasticSearchEngine()
        {
            LoadSettings();
            Repository = new ElasticClient(ServiceURL);
        }
        private void LoadSettings()
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");
                ServiceURL = new Uri(ConfigurationManager.AppSettings["ESURL"]);
            }
            catch { throw new ApplicationException("Docs.DAL.ES|LoadSettings|Could not load app.config");  }
        }
        public bool IndexData<T>(T data, string indexName = null) where T : class, new()
        {
            if (Repository == null)
            {
                throw new ArgumentNullException("Docs.DAL.ES|IndexData|Repository not init");
            }
            var result = this.Repository.Index<T>(data, x=>x.Index(indexName.ToLower()));
            return result.IsValid;
        }
    }
}
