using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public class UniversityContext : DbContext
    {
        public DbSet<Reader> Readers => Set<Reader>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookCard> BookCards => Set<BookCard>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // база данных сохранится в локальный файл library.db
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }
}