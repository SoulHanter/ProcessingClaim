using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    /// <typeparam name="TUser">Тип сущности</typeparam>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public interface IUserRepository<TUser, TId>
    {
        List<TUser> Users();
        TUser GetUser(TId id);
        TId Create(TUser currentUser);
        void Delete(TId id);
        void Edit(TUser currentUser);
    }
}
