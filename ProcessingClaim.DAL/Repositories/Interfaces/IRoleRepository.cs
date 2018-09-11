using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс работы с ролями
    /// </summary>
    /// <typeparam name="TRole">Тип сущности</typeparam>
    /// <typeparam name="TId">Тип идентификатора</typeparam>
    public interface IRoleRepository<TRole, TId>
    {
        List<TRole> Roles();
        TRole GetRole(TId id);
        TId Create(TRole currentRole);
        void Delete(TId id);
        void Edit(TRole currentRole);
    }
}
