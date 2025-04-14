using Xunit;
using Task1Project.Data.Repositories;
using Task1Project.Logic.Services;
using Task1Project.Logic.Interfaces;
using Task1Project.Data.Models;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Task1Project.Tests
{
    public class LoanLogicTests
    {
        [Fact]
        public void BorrowBook_ShouldAddLoanAndLogEvent()
        {
            var repo = new InMemoryDataRepository();
            IBookstoreService service = new BookstoreService(repo);

            service.BorrowBook(1, 1);
            var loan = repo.GetLoans().FirstOrDefault(l => l.UserId == 1 && l.BookId == 1);
            Assert.NotNull(loan);
            Assert.True(loan.BorrowedAt <= DateTime.Now);
            Assert.Null(loan.ReturnedAt);

            var lastEvent = repo.GetEvents().Last();
            Assert.Contains("borrowed", lastEvent.Description);
        }

        [Fact]
        public void ReturnBook_ShouldUpdateLoanAndLogEvent()
        {
            var repo = new InMemoryDataRepository();
            IBookstoreService service = new BookstoreService(repo);

            service.BorrowBook(1, 1);
            service.ReturnBook(1, 1);

            var loan = repo.GetLoans().FirstOrDefault(l => l.UserId == 1 && l.BookId == 1);
            Assert.NotNull(loan);
            Assert.NotNull(loan.ReturnedAt);
            Assert.True(loan.ReturnedAt <= DateTime.Now);

            var lastEvent = repo.GetEvents().Last();
            Assert.Contains("returned", lastEvent.Description);
        }

        [Fact]
        public void PlaceOrder_ShouldAddOrderAndLogEvent()
        {
            var repo = new InMemoryDataRepository();
            IBookstoreService service = new BookstoreService(repo);

            var bookIds = new List<int> { 1 };
            service.PlaceOrder(1, bookIds);

            var order = repo.GetOrders().FirstOrDefault(o => o.UserId == 1);
            Assert.NotNull(order);
            Assert.Equal(bookIds, order.BookIds);

            var lastEvent = repo.GetEvents().Last();
            Assert.Contains("Order", lastEvent.Description);
        }

        [Fact]
        public void BrowseBooks_ShouldReturnAllBooks()
        {
            var repo = new InMemoryDataRepository();
            IBookstoreService service = new BookstoreService(repo);

            var books = service.BrowseBooks();
            Assert.NotEmpty(books);
            Assert.True(books.Any(b => b.Title == "Clean Code"));
        }
    }
}