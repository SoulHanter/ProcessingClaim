using ProcessingClaim.DAL.Logical;
using ProcessingClaim.DAL.Models;
using ProcessingClaim.DAL.Repositories;
using ProcessingClaim.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClaim.DAL.Extensions
{
    /// <summary>
    /// Оболочка для приведения пользователя
    /// </summary>
    public static class PeopleExtension
    {
        public static Person ConvertToPerson(this IRoleRepository<Role, string> roleRepository, ApplicationUser applicationUser)
        {
            return new Person
            {
                Id = applicationUser.Id,
                Name = applicationUser.UserName,
                RoleId = roleRepository.GetRoleForUserId(applicationUser)?.Id
            };
        }

        /// <summary>
        /// Поиск роли пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static Role GetRoleForUserId(this IRoleRepository<Role, string> roleRepository, ApplicationUser user)
        {
            var roleId = user
                .Roles?
                .FirstOrDefault()?
                .RoleId;

            return roleId != null
                ? roleRepository.GetRole(roleId)
                : null;
        }
    }
}
