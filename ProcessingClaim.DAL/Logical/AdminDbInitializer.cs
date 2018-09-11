using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProcessingClaim.DAL.Models;
using ProcessingClaim.DAL.Repositories;
using ProcessingClaim.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace ProcessingClaim.DAL.Logical
{
    /// <summary>
    /// Инициализируем пользователя админ
    /// Создаем роли
    /// </summary>
    public class AdminDbInitializer : DropCreateDatabaseAlways<ProcessingClaimDbContext>
    {
        private string[] rolesName = { "administration", "operation", "worker" };
        protected override void Seed(ProcessingClaimDbContext context)
        {
            //var userManager = new UserManager(new UserStore<ApplicationUser>(context));
            var userRepository = new UserRepository(context);
            var roleManager = new RoleRepository(context);

            //Создаем роли
            var roles = rolesName
                .Select(x => new Role { Title = x });

            foreach (var role in roles)
            {
                roleManager.Create(role);
            }

            //Создаем админа
            AddUser(userRepository, roleManager, "AdminUserName", "AdminPassword", "administration");
            //Создаем оператора
            AddUser(userRepository, roleManager, "OperationUserName", "OperationPassword", "operation");
            //Создаем исполнителя
            AddUser(userRepository, roleManager, "WorkerUserName", "WorkerPassword", "worker");

            base.Seed(context);
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="userManager">Менеджер пользователей</param>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="roleNams">Роль</param>
        private void AddUser(UserRepository userManager, RoleRepository roleManager, string login, string password, string roleName)
        {
            var userName = ConfigurationManager.AppSettings[login];
            var userPassword = ConfigurationManager.AppSettings[password];

            userManager.Create(new Person
            {
                Name = userName,
                Password = userPassword,
                RoleId = roleManager.GetRoleName(roleName)?.Id
            });
        }
    }
}