using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class BookService : IBookService
    {
        BackendContext db;
        public BookService(BackendContext _db)
        {
            db = _db;
        }
        public int Add(Book book)
        {
            if (db != null)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return book.Id;

            }

            return 0;
        }


        public int Delete(int id)
        {
            int result = 0;

            if (db != null)
            {

                var exitsingBook = db.Books.Find(id);

                if (exitsingBook != null)
                {

                    db.Books.Remove(exitsingBook);


                    result = db.SaveChanges();
                }
                return result;
            }

            return result;
        }

        public void Edit(Book book)
        {
              if (db != null)
            {
                //Delete that post
                db.Books.Update(book);

                //Commit the transaction
                db.SaveChanges();
            }
        }

        public Book GetBook(int id)
        {
            return db.Books.SingleOrDefault(b=>b.Id == id);
        }

        public List<Book> GetBooks()
        {
            if (db != null)
            {
                return db.Books.ToList();
            }
            return null;
        }

        
    }
}