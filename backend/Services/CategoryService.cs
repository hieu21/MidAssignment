using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class CategoryService : ICategoryService
    {
        BackendContext db;
        public CategoryService(BackendContext _db)
        {
            db = _db;
        }
        public int Add(Category category)
        {
            if (db != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.Id;

            }

            return 0;
        }

        public int Delete(int id)
        {
            int result = 0;

            if (db != null)
            {

                var exitsingCategory = db.Categories.Find(id);

                if (exitsingCategory != null)
                {

                    db.Categories.Remove(exitsingCategory);


                    result = db.SaveChanges();
                }
                return result;
            }

            return result;
        }

        public void Edit(Category category)
        {
              if (db != null)
            {
                //Delete that post
                db.Categories.Update(category);

                //Commit the transaction
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            if (db != null)
            {
                return db.Categories.ToList();
            }
            return null;
        }

        public Category GetCategory(int id)
        {
            return db.Categories.SingleOrDefault(c=>c.Id == id);
        }
    }
}