using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheNote.Model;
using TheNote.DataLayer.Sql;
using TheNote.DataLayer;

namespace TheNote.DataLayer.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private const string ConnectionString = @"Data Source=DESKTOP-T2Q9DN8\MYMSSQLSERVER;Initial Catalog=THENOTE";
        private readonly List<Guid> _tempUsers = new List<Guid>();

        [TestMethod]
        public void ShouldCreateUser()
        {
            //arrange
            var user = new User
            {
                FirstName = "test"
            };

            //act
            var repository = new UsersRepository(ConnectionString, new CategoriesRepository(ConnectionString));
            var result = repository.Create(user);

            _tempUsers.Add(user.ID);

            var userFromDb = repository.Get(result.ID);

            //asserts
            Assert.AreEqual(result.FirstName, userFromDb.FirstName);
        }

        [TestCleanup]
        public void CleanData()
        {
            foreach (var id in _tempUsers)
                new UsersRepository(ConnectionString, new CategoriesRepository(ConnectionString)).Delete(id);
        }
    }
}
