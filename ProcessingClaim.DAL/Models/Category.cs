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
    /// Категории заявок
    /// </summary>
    [Table("td_categories")]
    public class Category
    {
        public Category()
        {
            Claims = new List<Claim>();
        }
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Заявки
        /// </summary>
        public virtual ICollection<Claim> Claims { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
