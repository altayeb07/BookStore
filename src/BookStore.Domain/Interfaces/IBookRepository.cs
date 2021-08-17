using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces 
{
    public interface IBookRepository : IRepository<Book>
    {
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(int id);
        Task<IEnumerable<Book>> GetBooksbyCategory(int categoryId);
        Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue);
    }
}
