using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProcessingClaim.DAL.Extensions;
using ProcessingClaim.DAL.Logical;
using ProcessingClaim.DAL.Models;
using ProcessingClaim.DAL.Properties;
using ProcessingClaim.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProcessingClaim.DAL.Repositories
{
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    public class UserRepository : IUserRepository<Person, string>
    {
        private UserManager _userStore;
        private IRoleRepository<Role, string> _roleRepository;
        public UserRepository(ProcessingClaimDbContext dbContext = null)
        {
            _roleRepository = new RoleRepository(dbContext);
            _userStore = new UserManager(
                new UserStore<ApplicationUser>(dbContext ?? new ProcessingClaimDbContext()));
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public string Create(Person currentUser)
        {
            var user = new ApplicationUser
            {
                Email = currentUser.Name,
                UserName = currentUser.Name
            };
            var manager = _userStore.Create(user, currentUser.Password);

            if (manager.Succeeded)
            {
                var roleName = _roleRepository
                    .GetRole(currentUser.RoleId)?
                    .Title;
                _userStore.AddToRole(user.Id, roleName);
            }
            return manager.Succeeded
                ? user.Id
                : null;
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var entityUser = GetUserId(id);
            _userStore?.Delete(entityUser);
        }
        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="currentUser"></param>
        public virtual void Edit(Person currentUser)
        {
            var entityUser = GetUserId(currentUser.Id);
            entityUser.Email = currentUser.Name;
            entityUser.UserName = currentUser.Name;
            var manager = _userStore.Update(entityUser);

            if (manager.Succeeded)
            {
                var roles = entityUser
                    .Roles?
                    .Select(x => _roleRepository.GetRole(x.RoleId).Title)?
                    .ToArray() ?? new string[] { };

                _userStore.RemoveFromRoles(entityUser.Id, roles);

                var roleName = _roleRepository
                    .GetRole(currentUser.RoleId)?
                    .Title;

                _userStore.AddToRole(entityUser.Id, roleName);
            }            
        }
        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetUser(string id)
        {
            var entityUser = GetUserId(id);

            return _roleRepository.ConvertToPerson(entityUser);
        }

        /// <summary>
        /// Получить пользователей
        /// </summary>
        /// <returns></returns>
        public List<Person> Users()
        {
            var users = _userStore?.Users?.ToList() ?? new List<ApplicationUser>();

            return users.Any()
                ? users
                .Select(x => _roleRepository.ConvertToPerson(x))
                .ToList()
                : new List<Person>();
        }            

        /// <summary>
        /// Ищем пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ApplicationUser GetUserId(string id) =>
            _userStore?
                .Users?
                .FirstOrDefault(x => x.Id.Equals(id)) 
                ?? throw new Exception(string.Format(Errors.NotExistsUserId, id));


    }
}
