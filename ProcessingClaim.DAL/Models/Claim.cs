using ProcessingClaim.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Models
{
    /// <summary>
    /// Заявка
    /// </summary>
    [Table("td_claims")]
    public class Claim
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Тема
        /// </summary>
        [MaxLength(50)]
        public string Title { get; set; }
        /// <summary>
        /// ФИО заявителя
        /// </summary>
        public string FIO { get; set; }
        /// <summary>
        /// Телефонный номер
        /// </summary>
        [MaxLength(12, ErrorMessage = "Номер телефона не должен привышать 12 символов.")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        [MaxLength(400, ErrorMessage = "Текст сообщения не должен привышать 400 символов.")]
        public string Text { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationOn { get; set; }
        /// <summary>
        /// Статус заявки
        /// </summary>
        public StatusType Status { get; set; }
        /// <summary>
        /// id Категории
        /// </summary>
        public Guid? CategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        /// <summary>
        /// id Автора
        /// </summary>
        public string AuthorId { get; set; }
    }
}
