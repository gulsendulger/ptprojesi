using System;
using System.Linq;
using System.Collections.Generic;
using Task1Project.Data;
using Task1Project.Data.Models;
using Task1Project.Logic;

namespace Task1Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();
            repo.GenerateSampleCatalog();
            var service = new BookstoreService(repo);

            while (true)
            {
                ShowMenu();

                Console.Write("Your choice: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowCatalog(service);
                            break;
                        case "2":
                            AddNewBook(repo);
                            break;
                        case "3":
                            AddNewUser(repo);
                            break;
                        case "4":
                            PlaceOrder(service);
                            break;
                        case "5":
                            BorrowBook(service);
                            break;
                        case "6":
                            ReturnBook(service);
                            break;
                        case "7":
                            ViewActiveLoans(service);
                            break;
                        case "8":
                            ViewEventLog(repo);
                            break;
                        case "9":
                            ViewUsers(repo);
                            break;
                        case "0":
                            Console.WriteLine("Exiting...");
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. View catalog");
            Console.WriteLine("2. Add new book");
            Console.WriteLine("3. Add new user");
            Console.WriteLine("4. Place an order");
            Console.WriteLine("5. Borrow a book");
            Console.WriteLine("6. Return a book");
            Console.WriteLine("7. View active loans");
            Console.WriteLine("8. View event log");
            Console.WriteLine("9. View users");
            Console.WriteLine("0. Exit");
        }

        static void ShowCatalog(BookstoreService service)
        {
            foreach (var book in service.BrowseBooks())
                Console.WriteLine($"- {book.Id}: {book.Title} by {book.Author}");
        }

        static void AddNewBook(InMemoryDataRepository repo)
        {
            Console.Write("Book title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();

            int newBookId = GenerateNewId(repo.GetBooks().Select(b => b.Id));

            repo.AddBook(new Book { Id = newBookId, Title = title, Author = author });
            Console.WriteLine($"Book added. Book ID: {newBookId}");
        }

        static void AddNewUser(InMemoryDataRepository repo)
        {
            Console.Write("User name: ");
            string name = Console.ReadLine();

            int newUserId = GenerateNewId(repo.GetUsers().Select(u => u.Id));

            repo.AddUser(new User { Id = newUserId, Name = name });
            Console.WriteLine($"User added. User ID: {newUserId}");
        }

        static void PlaceOrder(BookstoreService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userIdOrder)) throw new Exception("Invalid user ID");
            Console.Write("Book IDs (comma-separated): ");
            var bookIds = Console.ReadLine().Split(',').Select(id => int.Parse(id.Trim())).ToList();
            service.PlaceOrder(userIdOrder, bookIds);
            Console.WriteLine("Order placed.");
        }

        static void BorrowBook(BookstoreService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userIdBorrow)) throw new Exception("Invalid user ID");
            Console.Write("Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookIdBorrow)) throw new Exception("Invalid book ID");
            service.BorrowBook(userIdBorrow, bookIdBorrow);
            Console.WriteLine("Book borrowed.");
        }

        static void ReturnBook(BookstoreService service)
        {
            Console.Write("User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userIdReturn)) throw new Exception("Invalid user ID");
            Console.Write("Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out int bookIdReturn)) throw new Exception("Invalid book ID");
            service.ReturnBook(userIdReturn, bookIdReturn);
            Console.WriteLine("Book returned.");
        }

        static void ViewActiveLoans(BookstoreService service)
        {
            foreach (var loan in service.GetActiveLoans())
                Console.WriteLine($"Loan: User {loan.UserId} -> Book {loan.BookId}");
        }

        static void ViewEventLog(InMemoryDataRepository repo)
        {
            foreach (var evt in repo.GetEvents())
                Console.WriteLine($"{evt.Timestamp}: {evt.Description}");
        }

        static void ViewUsers(InMemoryDataRepository repo)
        {
            foreach (var user in repo.GetUsers())
                Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}");
        }

        static int GenerateNewId(IEnumerable<int> existingIds)
        {
            if (!existingIds.Any())
                return 1;
            return existingIds.Max() + 1;
        }
    }
}
