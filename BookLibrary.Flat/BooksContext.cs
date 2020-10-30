using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibrary.Flat
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BooksContext()
        {

        }

        public BooksContext(DbContextOptions<BooksContext> options)
            :base (options)
        {

        }
    }
}
