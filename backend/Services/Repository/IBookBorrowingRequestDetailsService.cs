using System;
using backend.Models;
using System.Collections.Generic;

namespace backend.Services{
    public interface IBookBorrowingRequestDetailsService{
        List<BookBorrowingRequestDetails> GetRequestDetails(); 
        int Add(BookBorrowingRequestDetails bookBorrowingRequestDetails);
        void Edit(BookBorrowingRequestDetails bookBorrowingRequestDetails);  
        int Delete(int id);
    }
}