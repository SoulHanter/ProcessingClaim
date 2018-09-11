using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Enums
{
    /// <summary>
    /// Статусы заявок
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// Зарегистрированно
        /// </summary>
        Registred = 0,
        /// <summary>
        /// Исполнено
        /// </summary>
        Executed = 1,
        /// <summary>
        /// Не исполнено
        /// </summary>
        NotExecuted = 2
    }
}
