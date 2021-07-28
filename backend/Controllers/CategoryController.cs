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
    public class CategoryController : ControllerBase
    {


        private readonly ICategoryService _Service;
        private readonly IUserService _UserService;
        public CategoryController(ICategoryService Service, IUserService UserService)
        {
            _Service = Service;
            _UserService = UserService;
        }

        [HttpGet("/api/category/{id}")]

        public Category Get(int id)
        {
            var result = _Service.GetCategory(id);

            return result;
        }

        [HttpGet("/api/categories")]

        public List<Category> Get()
        {
            var result = _Service.GetCategories();

            return result;
        }
        [HttpPost("/api/category")]
        public ActionResult Post(Category category)
        {
            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         var existCategory = _Service.Add(category);
            //         if (existCategory > 0)
            //         {
            //             return Ok(existCategory);
            //         }
            //         else
            //         {
            //             return NotFound();
            //         }
            //     }
            //     catch (Exception)
            //     {

            //         return BadRequest();
            //     }

            // }

            // return BadRequest();
            int token = int.Parse(Request.Headers["Token"]);

            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);
            if (user == null)
            {
                return Unauthorized();
            }
            else if (user.Role == Role.Admin)
            {
                _Service.Add(category);
                return Ok();
            }
            else
            {
                return StatusCode(403);
            }
        }
        [HttpPut("/api/category")]
        public ActionResult Put(Category category)
        {
            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         _Service.Edit(category);

            //         return Ok();
            //     }
            //     catch (Exception ex)
            //     {
            //         if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
            //         {
            //             return NotFound();
            //         }

            //         return BadRequest();
            //     }
            // }

            // return BadRequest();
             if (!ModelState.IsValid) return null;

            int token = int.Parse(Request.Headers["Token"]);

            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);
            if (user == null)
            {
                return Unauthorized();
            }
            else if (user.Role == Role.Admin)
            {
                _Service.Edit(category);
                return Ok();
            }
            else
            {
                return StatusCode(403);
            }
        }
        [HttpDelete("/api/category/{id}")]
        public IActionResult Delete(int id)
        {
            int token = int.Parse(Request.Headers["Token"]);
            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);
            if (user == null)
            {
                return Unauthorized();
            }
            else if (user.Role == Role.Admin)
            {
                _Service.Delete(id);

                return Ok();
            }
            else
            {
                return StatusCode(403);
            }


        }


    }
}
