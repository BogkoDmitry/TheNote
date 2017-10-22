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
    public class UsersController : ApiController
    {
        private const string ConnectionString = @"Data Source=106sv0013\sql2012;Initial Catalog=evernote;User ID=sa";
        private readonly IUsersRepository _usersRepository;

        public UsersController()
        {
            _usersRepository = new UsersRepository(ConnectionString, new CategoriesRepository(ConnectionString));
        }

        [HttpGet]
        [Route("api/users")]

        public User Get(Guid ID)
        {
            return _usersRepository.Get(ID);
        }

        [HttpPost]
        [Route("api/users")]

        public User Post([FromBody] User user)
        {
            return _usersRepository.Create(user);
        }

        [HttpDelete]
        [Route("api/users/{id}")]

        public void Delete(Guid id)
        {
            _usersRepository.Delete(id);
        }

        [HttpGet]
        [Route("api/users/{id}/categories")]

        public IEnumerable<Category> GetUsersCategories(Guid id)
        {
            return _usersRepository.Get(id).Categories;
        }
    }
}
