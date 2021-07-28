using System;
using backend.Models;
using System.Collections.Generic;

namespace backend.Services{
    public interface IBookBorrowingRequestService{
        List<BookBorrowingRequest> GetRequests(); 
        BookBorrowingRequest GetRequest(int id);
        int Add(BookBorrowingRequest bookBorrowingRequest);
        void Edit(BookBorrowingRequest bookBorrowingRequest);  
        int Delete(int id);
    }
}