using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Model.DB.Log;
using Docs.DAL.Repository;
using Docs.DAL.Context;

namespace Docs.DAL.UOW
{
    public class UOWLog :IDisposable
    {
        private LogContext context = new LogContext();
        private GRepository<Error> _errorRepository;
        /// <summary>
        /// Лог ошибок
        /// </summary>
        public GRepository<Error> errorRepository
        {
            get
            {
                if (this._errorRepository == null)
                {
                    this._errorRepository = new GRepository<Error>(context);
                }
                return _errorRepository;
            }
        }
        private GRepository<ScrapperLog> _ScrapperLogRepository;
        /// <summary>
        /// Лог парсера
        /// </summary>
        public GRepository<ScrapperLog> ScrapperLogRepository
        {
            get
            {
                if (this._ScrapperLogRepository == null)
                {
                    this._ScrapperLogRepository = new GRepository<ScrapperLog>(context);
                }
                return _ScrapperLogRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
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
