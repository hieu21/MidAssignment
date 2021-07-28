using System;
using backend.Models;
using System.Collections.Generic;

namespace backend.Services{
    public interface ICategoryService{
        List<Category> GetCategories(); 
        Category GetCategory(int id);
        int Add(Category category);
        void Edit(Category category);  
        int Delete(int id);
    }
}