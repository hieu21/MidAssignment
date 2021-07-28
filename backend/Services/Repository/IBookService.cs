using System;
using backend.Models;
using System.Collections.Generic;

namespace backend.Services{
    public interface IBookService{
        List<Book> GetBooks(); 
        Book GetBook(int id);
        int Add(Book book);
        void Edit(Book book);  
        int Delete(int id);
    }
}