using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data
{
    public abstract class AbstractDataRepository
    {
        public abstract List<User> GetUsers();
        public abstract List<Book> GetBooks();
        public abstract List<Order> GetOrders();
        public abstract List<Loan> GetLoans();
        public abstract List<Event> GetEvents();

        public abstract void AddUser(User user);       
        public abstract void AddBook(Book book);
        public abstract void AddLoan(Loan loan);
        public abstract void RemoveLoan(Loan loan);
        public abstract void AddOrder(Order order);
        public abstract void AddEvent(Event evt);

        public abstract void GenerateSampleUsers();
        public abstract void GenerateSampleCatalog();
    }
}
