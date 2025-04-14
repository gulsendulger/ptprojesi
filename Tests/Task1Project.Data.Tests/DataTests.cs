using Xunit;
using Task1Project.Data.Models;
using Task1Project.Data.Repositories;
using System.Collections.Generic;
using System;

namespace Task1Project.Tests
{
    public class DataTests
    {
      

        [Fact]
        public void CanAddBook_WithValidData()
        {
            var repo = new InMemoryDataRepository();
            var bookId = new Random().Next(1000, 9999);
            var book = new Book { Id = bookId, Title = "Refactoring", Author = "Martin Fowler" };
            repo.AddBook(book);

            Assert.Contains(repo.GetCatalog(), b => b.Id == bookId && b.Title == "Refactoring" && b.Author == "Martin Fowler");
        }

        [Fact]
        public void CanLogEvent_CorrectTimestamp()
        {
            var repo = new InMemoryDataRepository();
            var now = DateTime.Now;
            var evt = new Event { Description = "Logged in", Timestamp = now };
            repo.AddEvent(evt);

            Assert.Contains(repo.GetEvents(), e => e.Description == "Logged in" && e.Timestamp == now);
        }
    }
}
