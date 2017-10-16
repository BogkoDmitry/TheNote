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
    class CategoriesRepositoryTests
    {
        private const string ConnectionString = @"Data Source=DESKTOP-T2Q9DN8\MYMSSQLSERVER;Initial Catalog=THENOTE";
        private readonly List<Guid> _tempCategories = new List<Guid>();

        [TestMethod]
        public void ShouldCreateCategory()
        {
            //arrange
            var category = new Category
            {
                Name = "test"
            };

            //act
            var repository = new CategoriesRepository(ConnectionString);
            var result = repository.Create(category);

            _tempCategories.Add(category.ID);

            var noteFromDb = repository.Get(result.ID);

            //asserts
            Assert.AreEqual(result.Name, noteFromDb.Name);
        }

        [TestCleanup]
        public void CleanData()
        {
            foreach (var id in _tempCategories)
                new NotesRepository(ConnectionString).Delete(id);
        }
    }
}
