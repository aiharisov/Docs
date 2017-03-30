using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Model.DB.Docs;
using Docs.DAL.Repository;
using Docs.DAL.Context;

namespace Docs.DAL.UOW
{
    public class UOWDocs :IDisposable
    {
        private DocsContext context = new DocsContext();
        private GRepository<Doc> _DocRepository;
        /// <summary>
        /// Документ
        /// </summary>
        public GRepository<Doc> DocRepository
        {
            get
            {
                if (this._DocRepository == null)
                {
                    this._DocRepository = new GRepository<Doc>(context);
                }
                return _DocRepository;
            }
        }

        private GRepository<Case> _CaseRepository;
        /// <summary>
        /// Тип дела
        /// </summary>
        public GRepository<Case> CaseRepository
        {
            get
            {
                if (this._CaseRepository == null)
                {
                    this._CaseRepository = new GRepository<Case>(context);
                }
                return _CaseRepository;
            }
        }

        private GRepository<Court> _CourtRepository;
        /// <summary>
        /// Суды
        /// </summary>
        public GRepository<Court> CourtRepository
        {
            get
            {
                if (this._CourtRepository == null)
                {
                    this._CourtRepository = new GRepository<Court>(context);
                }
                return _CourtRepository;
            }
        }

        private GRepository<Instance> _InstanceRepository;
        /// <summary>
        /// Инстанция
        /// </summary>
        public GRepository<Instance> InstanceRepository
        {
            get
            {
                if (this._InstanceRepository == null)
                {
                    this._InstanceRepository = new GRepository<Instance>(context);
                }
                return _InstanceRepository;
            }
        }

        private GRepository<DocType> _TypeDocRepository;
        /// <summary>
        /// Вид документа
        /// </summary>
        public GRepository<DocType> TypeDocRepository
        {
            get
            {
                if (this._TypeDocRepository == null)
                {
                    this._TypeDocRepository = new GRepository<DocType>(context);
                }
                return _TypeDocRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void InitDB()
        {
            if (CaseRepository.GetAll().Count() == 0)
            {
                CaseRepository.Insert(new Case() { Caseid = 1, Name = "Все" });
                CaseRepository.Insert(new Case() { Caseid = 2, Name = "Уголовное" });
                CaseRepository.Insert(new Case() { Caseid = 3, Name = "Гражданское" });
                CaseRepository.Insert(new Case() { Caseid = 4, Name = "Административное" });
                CaseRepository.Insert(new Case() { Caseid = 5, Name = "Экономическое" });
                CaseRepository.Insert(new Case() { Caseid = 6, Name = "Дисциплинарный спор" });
                this.Save();
            }
            if (CourtRepository.GetAll().Count() == 0)
            {
                CourtRepository.Insert(new Court() { CourtId = 1, Name = "Все", BaseURL = "none", URLToDoc = "none", TypeSite = "none", TypeSiteId = 0, Encoding = "none" });
                CourtRepository.Insert(new Court() { CourtId = 2, Name = "Верховный Суд", BaseURL = "http://www.vsrf.ru", URLToDoc = "indexA.php?", TypeSite = "vsrf", TypeSiteId = 1, Encoding = "windows-1251" });
                CourtRepository.Insert(new Court() { CourtId = 3, Name = "Архангельский областной суд", BaseURL = "http://oblsud.arh.sudrf.ru", URLToDoc = "modules.php?", TypeSite = "sudrf", TypeSiteId = 2, Encoding = "windows-1251" });
                this.Save();
            }
            if (InstanceRepository.GetAll().Count() == 0)
            {
                InstanceRepository.Insert(new Instance() { InstanceId = 1, Name = "Все" });
                InstanceRepository.Insert(new Instance() { InstanceId = 2, Name = "Первая инстанция" });
                InstanceRepository.Insert(new Instance() { InstanceId = 3, Name = "Апелляция" });
                InstanceRepository.Insert(new Instance() { InstanceId = 4, Name = "Кассация" });
                InstanceRepository.Insert(new Instance() { InstanceId = 5, Name = "Надзор" });
                this.Save();
            }
            if (TypeDocRepository.GetAll().Count() == 0)
            {
                TypeDocRepository.Insert(new DocType() { DocTypeId = 1, Name = "Все" });
                TypeDocRepository.Insert(new DocType() { DocTypeId = 2, Name = "Решение" });
                TypeDocRepository.Insert(new DocType() { DocTypeId = 3, Name = "Приговор" });
                TypeDocRepository.Insert(new DocType() { DocTypeId = 4, Name = "Определение" });
                TypeDocRepository.Insert(new DocType() { DocTypeId = 5, Name = "Постановление" });
                this.Save();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
