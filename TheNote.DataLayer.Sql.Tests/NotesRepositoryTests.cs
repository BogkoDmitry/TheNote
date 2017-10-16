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
    public class NotesRepositoryTests
    {
        private const string ConnectionString = @"Data Source = DESKTOP-T2Q9DN8\MYMSSQLSERVER;Initial Catalog=THENOTE";
        private readonly List<Guid> _tempNote = new List<Guid>();

        [TestMethod]
        public void ShouldCreateNote()
        {
            //arrange
            var note = new Note
            {
                Title = "test"
            };

            //act
            var repository = new NotesRepository(ConnectionString);
            var result = repository.Create(note);

            _tempNote.Add(note.ID);

            var noteFromDb = repository.Get(result.ID);

            //asserts
            Assert.AreEqual(result.Title, noteFromDb.Title);
        }

        [TestCleanup]
        public void CleanData()
        {
            foreach (var id in _tempNote)
                new NotesRepository(ConnectionString).Delete(id);
        }
    }
}

