using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections;
namespace backend.Models{
    [Table("BookBorrowingRequest")]
    public class BookBorrowingRequest{
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime BorrowDate { get; set; }
        public Status Status { get; set; }
        public User User{ get; set;}
        public ICollection<BookBorrowingRequestDetails> BorrowRequestDetails { get; set; }

    }
}