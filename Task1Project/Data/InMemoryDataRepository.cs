using System.Collections.Generic;
using System.Linq;
using Task1Project.Data.Models;

namespace Task1Project.Data
{
    internal class InMemoryDataRepository : AbstractDataRepository
    {
        private List<User> users = new();
        private List<Book> books = new();
        private List<Order> orders = new();
        private List<Loan> loans = new();
        private List<Event> events = new();

        public override List<User> GetUsers() => users;
        public override List<Book> GetBooks() => books;
        public override List<Order> GetOrders() => orders;
        public override List<Loan> GetLoans() => loans;
        public override List<Event> GetEvents() => events;

        public override void AddUser(User user) => users.Add(user);      
        public override void AddBook(Book book) => books.Add(book);
        public override void AddLoan(Loan loan) => loans.Add(loan);
        public override void RemoveLoan(Loan loan) => loans.Remove(loan);
        public override void AddOrder(Order order) => orders.Add(order);
        public override void AddEvent(Event evt) => events.Add(evt);

        public override void GenerateSampleUsers()
        {
            users.Add(new User { Id = 1, Name = "Alice" });
            users.Add(new User { Id = 2, Name = "Bob" });
        }

        public override void GenerateSampleCatalog()
        {
            books.Add(new Book { Id = 101, Title = "C# Basics", Author = "John Doe" });
            books.Add(new Book { Id = 102, Title = "Advanced .NET", Author = "Jane Smith" });
        }
    }
}
