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
    public class BookController : ControllerBase
    {


        private readonly IBookService _Service;
         private readonly IUserService _UserService;
        public BookController(IBookService Service, IUserService UserService)
        {
            _Service = Service;
            _UserService = UserService;
        }

        [HttpGet("/api/book/{id}")]
        public Book Get(int id)
        {
            var result = _Service.GetBook(id);

            return result;
        }

        [HttpGet("/api/books")]
        public List<Book> Get()
        {
            var result = _Service.GetBooks();

            return result;
        }
        [HttpPost("/api/book")]
        public ActionResult Post(Book book)
        {
            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //         var existBook =  _Service.Add(book);
            //         if (existBook > 0)
            //         {
            //             return Ok(existBook);
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
            int token = 1;//int.Parse(Request.Headers["Token"]);

            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);
            if (user == null)
            {
                return Unauthorized();
            }
            else if (user.Role == Role.Admin)
            {
                _Service.Add(book);
                return Ok();
            }
            else
            {
                return StatusCode(403);
            }
        }
        [HttpPut("/api/book")]
        public ActionResult Put(Book book)
        {
            // if (ModelState.IsValid)
            // {
            //     try
            //     {
            //          _Service.Edit(book);

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
                _Service.Edit(book);
                return Ok();
            }
            else
            {
                return StatusCode(403);
            }
        }
        [HttpDelete("/api/book/{id}")]
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
