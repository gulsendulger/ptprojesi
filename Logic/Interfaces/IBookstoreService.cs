using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Logic.Interfaces
{
    public interface IBookstoreService
    {
        void PlaceOrder(int userId, List<int> bookIds);
        IReadOnlyList<Book> BrowseBooks();
        void BorrowBook(int userId, int bookId);
        void ReturnBook(int userId, int bookId);
        List<Loan> GetActiveLoans();
    }
}