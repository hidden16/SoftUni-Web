using Library.Data.Entities;
using Library.Models.Book;

namespace Library.Contracts
{
    public interface IBookService
    {
        public Task<IEnumerable<BookViewModel>> AllBooksAsync();
        public Task AddBookAsync(AddBookViewModel model);
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<IEnumerable<BookViewModel>> ReadBooksAsync(string userId);
        public Task AddToCollection(int bookId, string userId);
        public Task RemoveFromCollection(int bookId, string userId);
    }
}
