using System.Collections.Generic;
using Task1Project.Data.Models;

namespace Task1Project.Data.Interfaces
{
    public interface IDataRepository
    {
        IReadOnlyList<User> GetUsers();
        IReadOnlyList<Book> GetCatalog();
        IReadOnlyList<Order> GetOrders();
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Loan> GetLoans();

        void AddOrder(Order order);
        void AddEvent(Event evt);
        void AddLoan(Loan loan);
        void UpdateLoan(Loan loan);
        void AddUser(User user);
        void AddBook(Book book);
    }
}