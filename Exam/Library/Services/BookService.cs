using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models.Book;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;
        public BookService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            await context.Books.AddAsync(new Book()
            {
                Title = model.Title,
                Author = model.Author,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                Rating = model.Rating,
            });
            await context.SaveChangesAsync();
        }

        public async Task AddToCollection(int bookId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u=>u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User.");
            }

            var book = await context.Books
                .Where(b => b.Id == bookId)
                .Include(b => b.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new ArgumentException("Invalid Book.");
            }

            if (!user.ApplicationUsersBooks.Any(u=>u.BookId == bookId))
            {
                user.ApplicationUsersBooks.Add(new ApplicationUserBook()
                {
                    ApplicationUser = user,
                    Book = book
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookViewModel>> AllBooksAsync()
        {
            var books = await context.Books
                .Include(b => b.Category)
                .ToListAsync();

            return books
                .Select(e => new BookViewModel()
                {
                    Category = e.Category,
                    Author = e.Author,
                    Description = e.Description,
                    Id = e.Id,
                    ImageUrl = e.ImageUrl,
                    Rating = e.Rating,
                    Title = e.Title
                });
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<BookViewModel>> ReadBooksAsync(string userId)
        {
            var appUser = await context.Users
                 .Where(u => u.Id == userId)
                 .Include(u => u.ApplicationUsersBooks)
                 .ThenInclude(u => u.Book)
                 .ThenInclude(u => u.Category)
                 .FirstOrDefaultAsync();

            if (appUser == null)
            {
                throw new ArgumentException("Invalid User.");
            }

            var result = appUser.ApplicationUsersBooks.Where(x => x.ApplicationUserId == userId).ToList();
            List<BookViewModel> books = new List<BookViewModel>();
            foreach (var book in result)
            {
                books.Add(new BookViewModel()
                {
                    Author = book.Book.Author,
                    Category = book.Book.Category,
                    Description = book.Book.Description,
                    Id = book.Book.Id,
                    ImageUrl = book.Book.ImageUrl,
                    Rating = book.Book.Rating,
                    Title = book.Book.Title
                });
            }
            return books;
        }

        public async Task RemoveFromCollection(int bookId, string userId)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User.");
            }

            var book = await context.Books
                .Where(b => b.Id == bookId)
                .Include(b => b.ApplicationUsersBooks)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new ArgumentException("Invalid Book.");
            }

            var appUserBook = user.ApplicationUsersBooks.Where(x => x.BookId == bookId).FirstOrDefault();
            user.ApplicationUsersBooks.Remove(appUserBook);
            await context.SaveChangesAsync();
        }
    }
}
