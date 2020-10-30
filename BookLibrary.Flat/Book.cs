using System;

namespace BookLibrary.Flat
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public string Author { get; set; }
        public DateTime AuthorDateOfBirth { get; set; }
    }
}
