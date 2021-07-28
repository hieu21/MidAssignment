using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections;
namespace backend.Models{
    [Table("User")]
    public class User{
        [Key]
        public int Id{ get; set;}
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public ICollection<BookBorrowingRequest> BorrowRequests { get; set; } 

    }
}