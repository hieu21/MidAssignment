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
    public class BookBorrowingRequestController : ControllerBase
    {


        private readonly IBookBorrowingRequestService _Service;
        private readonly IUserService _UserService;
        BackendContext db;
        public BookBorrowingRequestController(IBookBorrowingRequestService Service, IUserService UserService,BackendContext _db)
        {
            _Service = Service;
            _UserService = UserService;
            db = _db;
        }
        [HttpGet("/api/BorrowRequest/{id}")]
        public BookBorrowingRequest Get(int id)
        {
            var result = _Service.GetRequest(id);

            return result;
        }
        
        
        [HttpGet("/api/BorrowRequests")]
        public List<BookBorrowingRequest> Get()
        {
            var result = _Service.GetRequests();

            return result;
        }
        [HttpGet("/api/BorrowRequests/{UserId}")]
        public IEnumerable<BookBorrowingRequest> GetUserId(int UserId)
        {
            var result = _Service.GetRequests().Where(br=>br.UserId == UserId);

            return result;
        }
        
       
        [HttpDelete("/api/BorrowRequest/{id}")]
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
        [HttpPut("{borrowRequestId}/approve")]
        public IActionResult ApproveBorrowRequest(int borrowRequestId)
        {
            if (!ModelState.IsValid) return BadRequest("Co loi xay ra!");

            int token = int.Parse(Request.Headers["Token"]);

           

            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);

            if (user == null) return Unauthorized();

            if (user.Role == Role.User) return StatusCode(403);

            var entity = _Service.GetRequest(borrowRequestId);

            if (entity != null)
            {
                entity.Status = (Status)1;
                _Service.Edit(entity);
                return Ok(entity);
            }

            return BadRequest("Khong tim thay book co id la " + borrowRequestId!);
        }
       [HttpPut("{borrowRequestId}/reject")]
        public IActionResult RejectBorrowRequest(int borrowRequestId)
        {
            if (!ModelState.IsValid) return BadRequest("Co loi xay ra!");

            int token = int.Parse(Request.Headers["Token"]);

            

            var user = _UserService.GetUsers().SingleOrDefault(u => u.Id == token);

            if (user == null) return Unauthorized();

            if (user.Role == Role.User) return StatusCode(403);

            var entity = _Service.GetRequest(borrowRequestId);

            if (entity != null)
            {   
                // var result = new BookBorrowingRequest{

                // };
                entity.Status = (Status)2;
                db.SaveChanges();
                return Ok(entity);
            }

            return BadRequest("Khong tim thay book co id la " + borrowRequestId!);
        }
        [HttpPost("{userId}")]
        public IActionResult Post(BookBorrowingRequest borrowRequest, int userId)
        {
            var checkBorrowInMonth = _Service.GetRequests().Count(br => br.UserId == userId && br.BorrowDate.Month == DateTime.Now.Month);

            if (checkBorrowInMonth < 3)
            {
                if (borrowRequest.BorrowRequestDetails.Count <= 5)
                {
                    borrowRequest.BorrowDate = DateTime.Now;
                    borrowRequest.Status = (Status)0;
                    borrowRequest.UserId = userId;
                    borrowRequest.User = null;
                    borrowRequest.BorrowRequestDetails= null;

                    _Service.Add(borrowRequest);
                    return Ok(borrowRequest);
                }
                return BadRequest("Ban ko the muon 5 cuon sach 1 luc");
            }
            return BadRequest("Ban ko the muon qua 3 lan trong 1 thang");
        }

    }
}
