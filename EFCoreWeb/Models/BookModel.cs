using BookLibrary;
using System.Linq;

namespace EFCoreWeb.Models
{
    public static class BookModelExtensions
    {
        public static IQueryable<BookModel> ToModel (this IQueryable<Book> source)
        {
            return source.Select(b => new BookModel
            {
                Id = b.Id,
                Title = b.Title,
                YearOfPublication = b.YearOfPublication,
                Author = b.Author.Name
            });
        }
    }

    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public string Author { get; set; }

        public BookModel()
        {

        }

        public BookModel(Book entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            YearOfPublication = entity.YearOfPublication;
            Author = entity.Author.Name;
        }
    }
}
