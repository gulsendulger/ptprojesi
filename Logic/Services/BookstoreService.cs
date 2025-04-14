using System;
using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Interfaces;
using Task1Project.Data.Models;
using Task1Project.Logic.Interfaces;

namespace Task1Project.Logic.Services
{
    public class BookstoreService : IBookstoreService
    {
        private readonly IDataRepository _repo;

        public BookstoreService(IDataRepository repo)
        {
            _repo = repo;
        }

        public void PlaceOrder(int userId, List<int> bookIds)
        {
            ValidateUser(userId);
            foreach (var bookId in bookIds)
                ValidateBook(bookId);

            var order = new Order
            {
                Id = _repo.GetOrders().Count + 1,
                UserId = userId,
                BookIds = bookIds
            };
            _repo.AddOrder(order);
            _repo.AddEvent(new Event { Timestamp = DateTime.Now, Description = $"Order {order.Id} placed by User {userId}" });
        }

        public IReadOnlyList<Book> BrowseBooks()
        {
            return _repo.GetCatalog();
        }

        public void BorrowBook(int userId, int bookId)
        {
            ValidateUser(userId);
            ValidateBook(bookId);

            _repo.AddLoan(new Loan
            {
                UserId = userId,
                BookId = bookId,
                BorrowedAt = DateTime.Now,
                ReturnedAt = null
            });
            _repo.AddEvent(new Event { Timestamp = DateTime.Now, Description = $"Book {bookId} borrowed by User {userId}" });
        }

        public void ReturnBook(int userId, int bookId)
        {
            ValidateUser(userId);
            ValidateBook(bookId);

            _repo.UpdateLoan(new Loan
            {
                UserId = userId,
                BookId = bookId,
                ReturnedAt = DateTime.Now
            });
            _repo.AddEvent(new Event { Timestamp = DateTime.Now, Description = $"Book {bookId} returned by User {userId}" });
        }

        public List<Loan> GetActiveLoans()
        {
            return _repo.GetLoans().Where(l => l.ReturnedAt == null).ToList();
        }

        private void ValidateUser(int userId)
        {
            if (!_repo.GetUsers().Any(u => u.Id == userId))
                throw new InvalidOperationException($"User ID {userId} does not exist.");
        }

        private void ValidateBook(int bookId)
        {
            if (!_repo.GetCatalog().Any(b => b.Id == bookId))
                throw new InvalidOperationException($"Book ID {bookId} does not exist.");
        }
    }
}