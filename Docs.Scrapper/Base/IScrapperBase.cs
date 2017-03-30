using Docs.Model.DB.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Scrapper.Base
{
    public interface IScrapperBase
    {
        /// <summary>
        /// Тип используемого ядра парсера
        /// </summary>
        ScrapperType ScrapperType { get; }
        /// <summary>
        /// Информация о суде
        /// </summary>
        Court Court { get; }
        /// <summary>
        /// Запуск парсера
        /// </summary>
        void DoScrap();
        /// <summary>
        /// Запуск парсера за период
        /// </summary>
        /// <param name="Start">Начальная дата</param>
        /// <param name="End">Конечная дата</param>
        void DoScrap(DateTime Start, DateTime End);
    }
}
