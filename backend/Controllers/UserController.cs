using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;

namespace Library.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly IUserService _Service;
        public UserController(IUserService Service)
        {
            _Service = Service;
        }
        [HttpGet("/api/user/{id}")]
        public User Get(int id)
        {
            var result = _Service.GetUser(id);

            return result;
        }

        [HttpGet("/api/users")]
        public List<User> Get()
        {
            var result = _Service.GetUsers();

            return result;
        }
        [HttpPost("/api/user")]
        public ActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existUser =  _Service.Add(user);
                    if (existUser > 0)
                    {
                        return Ok(existUser);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
        [HttpPost("Login")]
        public IActionResult Login(User user){
            var dbUser = _Service.GetUsers().SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
            if (dbUser != null)
            {
                return Ok(dbUser);
            }

            return BadRequest("Ten dang nhap hoac mat khau khong chinh xac!");
        }

        [HttpPut("/api/user")]
        public ActionResult Put(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _Service.Edit(user);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }
        [HttpDelete("/api/user/{id}")]
        public IActionResult Delete(int id)
        {

            _Service.Delete(id);

            return NoContent();
        }
       

    }
}
