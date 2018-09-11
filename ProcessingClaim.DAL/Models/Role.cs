using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Models
{
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Title { get; set; }
    }
}
