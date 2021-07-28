using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections;
namespace backend.Models{
    public class BookBorrowingRequestDetails{
        public int BorrowRequestId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public BookBorrowingRequest BorrowRequest { get; set; }
    }
}