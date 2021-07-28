using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections;
namespace backend.Models{
    [Table("Category")]
    public class Category{
        [Key]
        public int Id{get; set;}
        public string Name{get; set;}
        public ICollection<Book> Books { get; set; } 
    }
}