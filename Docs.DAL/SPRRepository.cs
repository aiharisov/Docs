using Docs.Model.DB.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.DAL
{
    public class SPRRepository
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static SPRRepository _provider;
        public static SPRRepository Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_provider == null)
                        _provider = new SPRRepository();
                    return _provider;
                }
            }
        }
        #endregion
        private UOW.UOWDocs Provider
        {
            get { return new UOW.UOWDocs(); }
        }
        private List<Case> caseList;
        /// <summary>
        /// Справочник - тип дела
        /// </summary>
        public List<Case> CaseList
        {
            get
            {
                if (caseList == null)
                {
                    caseList = new List<Case>();
                    using (var prov = Provider)
                    {
                        caseList = prov.CaseRepository.GetAll().ToList();
                    }
                }
                return caseList;
            }
        }
        private List<Court> courtList;
        /// <summary>
        /// Справочник - суды
        /// </summary>
        public List<Court> CourtList
        {
            get
            {
                if (courtList == null)
                {
                    courtList = new List<Court>();
                    using (var prov = Provider)
                    {
                        courtList = prov.CourtRepository.GetAll().ToList();
                    }
                }
                return courtList;
            }
        }
        private List<Instance> instanceList;
        /// <summary>
        /// Справочник - инстанции
        /// </summary>
        public List<Instance> InstanceList
        {
            get
            {
                if (instanceList == null)
                {
                    instanceList = new List<Instance>();
                    using (var prov = Provider)
                    {
                        instanceList = prov.InstanceRepository.GetAll().ToList();
                    }
                }
                return instanceList;
            }
        }
        private List<DocType> docTypeList;
        /// <summary>
        /// Справочник - вид документа
        /// </summary>
        public List<DocType> DocTypeList
        {
            get
            {
                if (docTypeList == null)
                {
                    docTypeList = new List<DocType>();
                    using (var prov = Provider)
                    {
                        docTypeList = prov.TypeDocRepository.GetAll().ToList();
                    }
                }
                return docTypeList;
            }
        }
    }
}
