using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNote.DataLayer;
using TheNote.DataLayer.Sql;
using TheNote.Model;

namespace TheNote.Api.Controllers
{
    public class CategoriesController : ApiController
    {
        private const string ConnectionString = @"Data Source=AGATA\SQLEXPRESS;Initial Catalog=db; Integrated Security=True;";
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController()
        {
            _categoriesRepository = new CategoriesRepository(ConnectionString);
        }

        [HttpPost]
        [Route("api/categories")]

        public Category Create(User user, string name)
        {
            return _categoriesRepository.Create(user.ID, name);
        }

        [HttpDelete]
        [Route("api/categories/{id}")]

        public void Delete(Guid id)
        {
            _categoriesRepository.Delete(id);
        }

        [HttpGet]
        [Route("api/users/{id}/categories")]

        public IEnumerable<Category> GetUsersCategories(User user)
        {
            return _categoriesRepository.GetUsersCategories(user.ID);
        }
    }
}
