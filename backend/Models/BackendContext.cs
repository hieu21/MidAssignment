using System;
using Microsoft.EntityFrameworkCore;
namespace backend.Models
{
    public class BackendContext : DbContext
    {
        
        public BackendContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
              .HasOne(b => b.Category)
              .WithMany(c => c.Books)
              .HasForeignKey(c => c.CategoryId)
              .IsRequired();
            modelBuilder.Entity<Book>()
              .HasMany(b => b.BorrowRequestDetails)
              .WithOne(b => b.Book)
              .HasForeignKey(b => b.BookId)
              .IsRequired();
            modelBuilder.Entity<Category>()
              .HasMany(c => c.Books)
              .WithOne(b => b.Category)
              .HasForeignKey(c => c.CategoryId)
              .IsRequired();
            modelBuilder.Entity<BookBorrowingRequest>()
              .HasOne(b => b.User)
              .WithMany(u => u.BorrowRequests)
              .HasForeignKey(u => u.UserId)
              .IsRequired();
            modelBuilder.Entity<BookBorrowingRequest>()
                .HasMany(b => b.BorrowRequestDetails)
                .WithOne(brd => brd.BorrowRequest)
                .HasForeignKey(brd => brd.BorrowRequestId)
                .IsRequired();

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasKey(b => new { b.BorrowRequestId, b.BookId });
            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasOne(br => br.Book)
                .WithMany(b => b.BorrowRequestDetails)
                .HasForeignKey(b => b.BookId)
                .IsRequired();

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                .HasOne(brd => brd.BorrowRequest)
                .WithMany(br => br.BorrowRequestDetails)
                .HasForeignKey(br => br.BorrowRequestId)
                .IsRequired();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }

    }
}