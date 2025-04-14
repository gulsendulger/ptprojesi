using System;
using System.Collections.Generic;
using Task1Project.Data.Interfaces;
using Task1Project.Data.Repositories;
using Task1Project.Logic.Interfaces;
using Task1Project.Logic.Services;
using Task1Project.Data.Models;
using System.Linq;


namespace Task1Project
{
    class Program
    {
        static void Main()
        {
            IDataRepository repo = new InMemoryDataRepository();
            IBookstoreService service = new BookstoreService(repo);

            while (true)
            {
                Console.WriteLine("\n--- BOOKSTORE MENU ---");
                Console.WriteLine("   Users:");
                foreach (var user in repo.GetUsers())
                {
                    Console.WriteLine($"   - {user.Id}: {user.Name}");
                }
                

                    Console.WriteLine("\n1. Show catalog");
                Console.WriteLine("2. Add new book");
                Console.WriteLine("3. Place an order");
                Console.WriteLine("4. Borrow a book");
                Console.WriteLine("5. Return a book");
                Console.WriteLine("6. View active loans");
                Console.WriteLine("7. View all orders");
                Console.WriteLine("8. View event log");
                Console.WriteLine("9. Add new user");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("📚 Catalog:");
                            foreach (var book in service.BrowseBooks())
                            {
                                Console.WriteLine($"- {book.Id}: {book.Title} by {book.Author}");
                            }
                            break;

                        case "2":
                            Console.Write("Enter book ID: ");
                            int bookId = int.Parse(Console.ReadLine());
                            Console.Write("Enter book title: ");
                            string title = Console.ReadLine();
                            Console.Write("Enter book author: ");
                            string author = Console.ReadLine();
                            repo.AddBook(new Book { Id = bookId, Title = title, Author = author });
                            Console.WriteLine("✅ Book added to catalog.");
                            break;

                        case "3":
                            Console.Write("Enter your user ID: ");
                            int userId = int.Parse(Console.ReadLine());
                            Console.Write("Enter book IDs to order (comma separated): ");
                            string orderInput = Console.ReadLine();
                            List<int> orderIds = orderInput.Split(',').Select(id => int.Parse(id.Trim())).ToList();
                            service.PlaceOrder(userId, orderIds);
                            Console.WriteLine("🛒 Order placed successfully.");
                            break;

                        case "4":
                            Console.Write("Enter your user ID: ");
                            int borrowerId = int.Parse(Console.ReadLine());
                            Console.Write("Enter book ID to borrow: ");
                            int borrowBookId = int.Parse(Console.ReadLine());
                            service.BorrowBook(borrowerId, borrowBookId);
                            Console.WriteLine("✅ Book borrowed.");
                            break;

                        case "5":
                            Console.Write("Enter your user ID: ");
                            int returnUserId = int.Parse(Console.ReadLine());
                            Console.Write("Enter book ID to return: ");
                            int returnBookId = int.Parse(Console.ReadLine());
                            service.ReturnBook(returnUserId, returnBookId);
                            Console.WriteLine("✅ Book returned.");
                            break;

                        case "6":
                            Console.WriteLine("📖 Active Loans:");
                            foreach (var loan in service.GetActiveLoans())
                            {
                                Console.WriteLine($"- Book {loan.BookId} borrowed by User {loan.UserId} on {loan.BorrowedAt}");
                            }
                            break;

                        case "7":
                            Console.WriteLine("📦 Orders:");
                            foreach (var order in repo.GetOrders())
                            {
                                Console.WriteLine($"- Order {order.Id} by User {order.UserId} for books: {string.Join(", ", order.BookIds)}");
                            }
                            break;

                        case "8":
                            Console.WriteLine("📝 Event Log:");
                            foreach (var evt in repo.GetEvents())
                            {
                                Console.WriteLine($"- [{evt.Timestamp}] {evt.Description}");
                            }
                            break;

                        case "9":
                            Console.Write("Enter new user ID: ");
                            int newUserId = int.Parse(Console.ReadLine());
                            Console.Write("Enter user name: ");
                            string newUserName = Console.ReadLine();
                            repo.AddUser(new User { Id = newUserId, Name = newUserName });
                            Console.WriteLine("✅ New user added.");
                            break;

                        case "0":
                            Console.WriteLine("👋 Goodbye!");
                            return;

                        default:
                            Console.WriteLine("❌ Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
        }
    }
}