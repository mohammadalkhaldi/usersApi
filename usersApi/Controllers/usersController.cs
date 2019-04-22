using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using usersApi.Models;
using usersApi.Services;

namespace usersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly UserService _userService;

        public usersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("addUser")]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.id }, user);
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, User updatedUser)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.id);

            return NoContent();
        }
        [HttpPost("searchUser")]
        public ActionResult<List<User>> Search(IFormCollection data)
        {

           return _userService.Search(data);
                  
        }

        [HttpGet("AddAllUsers")]
        public ActionResult<List<User>> AddAllUsers()
        {
            return _userService.AddAllUsers();
        }
    }
}