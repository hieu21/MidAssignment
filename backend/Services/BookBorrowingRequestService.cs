using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class BookBorrowingRequestService : IBookBorrowingRequestService
    {
        BackendContext db;
        public BookBorrowingRequestService(BackendContext _db)
        {
            db = _db;
        }
        public int Add(BookBorrowingRequest bookBorrowingRequest)
        {
            if (db != null)
            {
                db.BookBorrowingRequests.Add(bookBorrowingRequest);
                db.SaveChanges();
                return bookBorrowingRequest.Id;

            }

            return 0;
        }


        public int Delete(int id)
        {
            int result = 0;

            if (db != null)
            {

                var exitsingRequest = db.BookBorrowingRequests.Find(id);

                if (exitsingRequest != null)
                {

                    db.BookBorrowingRequests.Remove(exitsingRequest);


                    result = db.SaveChanges();
                }
                return result;
            }

            return result;
        }

        public void Edit(BookBorrowingRequest bookBorrowingRequest)
        {
              if (db != null)
            {
                //Delete that post
                db.BookBorrowingRequests.Update(bookBorrowingRequest);

                //Commit the transaction
                db.SaveChanges();
            }
        }

        public BookBorrowingRequest GetRequest(int id)
        {
            return db.BookBorrowingRequests.SingleOrDefault(b => b.Id == id);
        }

        public List<BookBorrowingRequest> GetRequests()
        {
            if (db != null)
            {
                return db.BookBorrowingRequests.ToList();
            }
            return null;
        }

        
    }
}