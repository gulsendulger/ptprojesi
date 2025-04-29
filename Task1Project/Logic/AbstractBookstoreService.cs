using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Logic
{
    public abstract class AbstractBookstoreService
    {
        public abstract List<Book> BrowseBooks();
        public abstract void PlaceOrder(int userId, List<int> bookIds);
        public abstract void BorrowBook(int userId, int bookId);
        public abstract void ReturnBook(int userId, int bookId);
        public abstract List<Loan> GetActiveLoans();
    }
}