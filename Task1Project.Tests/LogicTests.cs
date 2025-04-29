using Xunit;
using System.Collections.Generic;
using Task1Project.Data;
using Task1Project.Logic;

namespace Task1Project.Tests
{
    public class LogicTests
    {
        [Fact]
        public void PlaceOrder_ShouldAddOrder()
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            service.PlaceOrder(1, new List<int> { 101, 102 });

            Assert.Single(repo.GetOrders());
        }

        [Fact]
        public void BorrowAndReturnBook_ShouldManageLoans()
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            service.BorrowBook(1, 101);

            Assert.Single(repo.GetLoans());

            service.ReturnBook(1, 101);

            Assert.Empty(repo.GetLoans());
        }

        [Fact]
        public void BorrowAlreadyBorrowedBook_ShouldThrowException()
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            service.BorrowBook(1, 101);

            var ex = Assert.Throws<System.Exception>(() => service.BorrowBook(2, 101));
            Assert.Contains("already borrowed", ex.Message);
        }
    }
}
