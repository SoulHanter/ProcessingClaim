using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProcessingClaim.DAL.Models;
using ProcessingClaim.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingClaim.DAL.Properties;
using System.Data.Entity;
using ProcessingClaim.DAL.Logical;

namespace ProcessingClaim.DAL.Repositories
{
    /// <summary>
    /// Репоситорий ролей
    /// </summary>
    public class RoleRepository : IRoleRepository<Role, string>
    {
        private RoleManager<IdentityRole, string> _roleManager;
        public RoleRepository(ProcessingClaimDbContext dbContext = null)
        {
            _roleManager = new RoleManager<IdentityRole, string>(
                new RoleStore<IdentityRole>(dbContext ?? new ProcessingClaimDbContext()));
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="currentRole"></param>
        /// <returns></returns>
        public string Create(Role currentRole)
        {
            var role = new IdentityRole { Name = currentRole.Title };
            var manager = _roleManager?.Create(role);

            return manager.Succeeded
                ? role.Id
                : null;
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var role = GetEntityRoleId(id);

            _roleManager.Delete(role);
        }

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="currentRole"></param>
        public void Edit(Role currentRole)
        {
            var role = GetEntityRoleId(currentRole.Id);

            role.Name = currentRole.Title;

            _roleManager.Update(role);
        }

        /// <summary>
        /// Поиск роли по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRole(string id)
        {
            var role = GetEntityRoleId(id);

            return new Role
            {
                Title = role.Name,
                Id = role.Id
            };
        }

        /// <summary>
        /// Поиск роли по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRoleName(string name)
        {
            var role = GetEntityRoleName(name);

            return new Role
            {
                Title = role.Name,
                Id = role.Id
            };
        }

        /// <summary>
        /// Получить список ролей
        /// </summary>
        /// <returns></returns>
        public List<Role> Roles() =>
            _roleManager?
                .Roles?
                .Select(x => new Role
                {
                    Title = x.Name,
                    Id = x.Id
                })
                .ToList() ?? new List<Role>();      

        /// <summary>
        /// Получаем роль 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        private IdentityRole GetEntityRoleId(string id)
        {
            var role = _roleManager?
            .Roles?
            .FirstOrDefault(x => x.Id.Equals(id));

            return role != null
                ? role
                : throw new Exception(string.Format(Errors.NotExistsRoleId, id));
        }

        /// <summary>
        /// Получаем роль 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns></returns>
        private IdentityRole GetEntityRoleName(string name)
        {
            var role = _roleManager?
            .Roles?
            .FirstOrDefault(x => x.Name.Equals(name));

            return role != null
                ? role
                : throw new Exception(string.Format(Errors.NotExistsRoleName, name));
        }
    }
}
