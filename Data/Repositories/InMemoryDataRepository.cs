using System;
using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Interfaces;
using Task1Project.Data.Models;

namespace Task1Project.Data.Repositories
{
    public class InMemoryDataRepository : IDataRepository
    {
        private readonly List<User> _users = new()
{
    new User { Id = 1, Name = "Alice" },
    new User { Id = 2, Name = "Bob" },
    new User { Id = 3, Name = "Charlie" },
    new User { Id = 4, Name = "Diana" }
};

        private readonly List<Book> _catalog = new()
{
    new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin" },
    new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt, David Thomas" },
    new Book { Id = 3, Title = "Design Patterns", Author = "Erich Gamma et al." },
    new Book { Id = 4, Title = "Refactoring", Author = "Martin Fowler" }
};
        private readonly List<Order> _orders = new();
        private readonly List<Event> _events = new();
        private readonly List<Loan> _loans = new();

        public IReadOnlyList<User> GetUsers() => _users.AsReadOnly();
        public IReadOnlyList<Book> GetCatalog() => _catalog.AsReadOnly();
        public IReadOnlyList<Order> GetOrders() => _orders.AsReadOnly();
        public IReadOnlyList<Event> GetEvents() => _events.AsReadOnly();
        public IReadOnlyList<Loan> GetLoans() => _loans.AsReadOnly();

        public void AddOrder(Order order)
        {
            if (_orders.Any(o => o.Id == order.Id))
                throw new InvalidOperationException("Order ID already exists.");

            foreach (var bookId in order.BookIds)
            {
                // 1. Kitap zaten aktif bir sipariþte mi?
                bool isAlreadyOrdered = _orders.Any(o =>
                    o.IsActive &&
                    o.BookIds?.Contains(bookId) == true);
                if (isAlreadyOrdered)
                    throw new InvalidOperationException($"Book with ID {bookId} is already ordered by someone else.");

                // 2. Kitap ödünçte mi?
                bool isBookBorrowed = _loans.Any(l =>
                    l.BookId == bookId &&
                    l.ReturnedAt == null);
                if (isBookBorrowed)
                    throw new InvalidOperationException($"Book with ID {bookId} is currently borrowed and cannot be ordered.");
            }

            _orders.Add(order);
        }

        public void AddEvent(Event evt)
        {
            _events.Add(evt);
        }

        public void AddLoan(Loan loan)
        {
            // 1. Kitap hâlâ bir sipariþ olarak iþaretliyse (yani raftan alýnmýþsa), ödünç verilemez
            bool isOrdered = _orders.Any(o => o.IsActive && o.BookIds?.Contains(loan.BookId) == true);

            if (isOrdered)
                throw new InvalidOperationException("This book has been ordered and is currently unavailable for borrowing.");

            // 2. Kitap hâlâ baþka biri tarafýndan ödünçte mi?
            bool isBookLoanedByAnyone = _loans.Any(l =>
            l.BookId == loan.BookId &&
            l.ReturnedAt == null);
            if (isBookLoanedByAnyone)
                throw new InvalidOperationException("This book is already borrowed and has not yet been returned.");


            // 3. Kitap katalogda var mý kontrolü istersen buraya eklenebilir
            bool isInCatalog = _catalog.Any(b => b.Id == loan.BookId);
            if (!isInCatalog)
                throw new InvalidOperationException("This book does not exist in the catalog.");

            _loans.Add(loan);
        }




        public void UpdateLoan(Loan loan)
        {
            var existing = _loans.FirstOrDefault(l =>
                l.UserId == loan.UserId &&
                l.BookId == loan.BookId &&
                l.ReturnedAt == null);

            if (existing == null)
                throw new InvalidOperationException("This book has already been returned.");

            existing.ReturnedAt = loan.ReturnedAt;
        }


        public void AddUser(User user)
        {
            if (_users.Any(u => u.Id == user.Id))
                throw new InvalidOperationException("User ID already exists.");
            _users.Add(user);
        }

        public void AddBook(Book book)
        {
            if (_catalog.Any(b => b.Id == book.Id))
                throw new InvalidOperationException("Book ID already exists.");
            _catalog.Add(book);
        }
    }
}