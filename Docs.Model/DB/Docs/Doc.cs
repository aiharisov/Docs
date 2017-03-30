using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docs.Model.DB.Docs
{
    /// <summary>
    /// Документ
    /// </summary>
    public class Doc
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int DocId { get; set; }
        /// <summary>
        /// Наименование документа
        /// </summary>
        [MaxLength(300), Required, Index]
        public string DocName { get; set; }
        /// <summary>
        /// Дата документа
        /// </summary>
        [Required, Index]
        public DateTime DocDate { get; set; }
        /// <summary>
        /// Примечание к документу
        /// </summary>
        [Required]
        public string DocComment { get; set; }
        /// <summary>
        /// URL к файлу документа, на первоначальном сервере
        /// </summary>
        [Required]
        public string OriginalDocURL { get; set; }
        /// <summary>
        /// URL к карточке документа, на первоначальном сервере
        /// </summary>
        [Required]
        public string DocCardURL { get; set; }
        /// <summary>
        /// Дата операции
        /// </summary>
        [Required]
        public DateTime DateOp { get; set; }
        /// <summary>
        /// Суд
        /// </summary>
        [Required, Index]
        public virtual Court Court { get; set; }
        /// <summary>
        /// Инстанция
        /// </summary>
        [Required, Index]
        public virtual Instance Instance { get; set; }
        /// <summary>
        /// Тип дела
        /// </summary>
        [Required, Index]
        public virtual Case Case { get; set; }
        /// <summary>
        /// Вид документа
        /// </summary>
        [Required, Index]
        public virtual DocType TypeDoc { get; set; }
    }
}
