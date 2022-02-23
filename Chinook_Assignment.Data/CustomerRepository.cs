using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Data
{
    public class CustomerRepository : IAssignmentRepository
    {
        private static ChinookContext _context = new ChinookContext();

        public void MakeColumnHead()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("{0,6} {1,-15} {2,-15} {3,-15} {4,-15} {5, -20} {6, -18}", "Id", "FirstName", "LastName", "Country", "PostalCode", "PhoneNumber", "Email"));
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public CustomerRepository()
        {
            _context.Database.EnsureCreated();
        }
        /// <summary>
        /// Outputs the database customers to the console. If none found it will write a "error msg" to the console.
        /// TagWith used for stack tracing in SQL profiler.
        /// </summary>
        public void DisplayCustomersToConsole()
        {
            var customers = _context.Customers.TagWith("consoleApp.Program.GetCustomers").ToList();
            if (customers.Count == 0 || customers[0] is null)
            {
                Console.WriteLine("No customers were found");
            }
            MakeColumnHead();
            foreach (var customer in customers)
            {
                Console.WriteLine("{0,6} {1,-15} {2,-15} {3,-15} {4, -15} {5, -20} {6, -18}",
                    customer.CustomerId, customer.FirstName, customer.LastName,
                    customer.Country, customer.PostalCode, customer.Phone, customer.Email);
            }
        }
        /// <summary>
        /// Takes user id and displays some of the customer information to the console. Performs null check.
        /// </summary>
        public void DisplayCustomerToConsoleById(int customerId)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer is null)
            {
                Console.WriteLine("No customer with that name was found");
            }
            else
            {
                MakeColumnHead();
                Console.WriteLine("{0,6} {1,-15} {2,-15} {3,-15} {4, -15} {5, -20} {6, -18}",
                    customer.CustomerId, customer.FirstName, customer.LastName, customer.Country,
                    customer.PostalCode, customer.Phone, customer.Email);
            }
        }
        /// <summary>
        /// Searches for customers where the first name begins with the userInputted string "customerName"
        /// for this assignment i assume that name means firstname
        /// </summary>
        public void DisplayCustomersToConsoleByFirstName(string customerName)
        {
            try
            {
                var customers = _context.Customers.TagWith("consoleApp.Program.GetCustomerByFirstNameBeginsWith")
                    .Where(customer => EF.Functions.Like(customer.FirstName, $"{customerName}%")).ToList();
                MakeColumnHead();
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0,6} {1,-15} {2,-15} {3,-15} {4, -15} {5, -20} {6, -18}",
                        customer.CustomerId, customer.FirstName, customer.LastName,
                        customer.Country, customer.PostalCode, customer.Phone, customer.Email);
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        ///  Searches for a specific customer and displays the first partial match, matches against firstname
        /// for this assignment i assume that name means firstname.
        /// </summary>
        public void DisplayCustomerToConsoleByFirstName(string customerName)
        {
            var customer = _context.Customers.TagWith("consoleApp.Program.GetCustomerBySpecificName")
                .Where(customer => EF.Functions.Like(customer.FirstName, $"%{customerName}%")).ToList();

            if (customer[0] is null || customer.Count == 0)
            {
                Console.WriteLine("No customer with that name was found");
            }
            MakeColumnHead();
            Console.WriteLine("{0,6} {1,-15} {2,-15} {3,-15} {4, -15} {5, -20} {6, -18}",
                customer[0].CustomerId, customer[0].FirstName, customer[0].LastName,
                customer[0].Country, customer[0].PostalCode, customer[0].Phone, customer[0].Email);
        }

        /// <summary>
        /// Returns a "page", offset is where in the table you start retrieving rows, limit is how many rows you want
        /// </summary>
        public List<Customer> ReturnPage(int offset, int limit)
        {
            try
            {
                var customers = _context.Customers.Skip(offset).Take(limit).ToList();
                MakeColumnHead();
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0,6} {1,-15} {2,-15} {3,-15} {4, -15} {5, -20} {6, -18}",
                        customer.CustomerId, customer.FirstName, customer.LastName,
                        customer.Country, customer.PostalCode, customer.Phone, customer.Email);
                }
                return customers;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Customer>();
        }
        /// <summary>
        /// Adds a customer according to assignment specs, normally i would have added the option for adding all the fields,
        /// in this case i leave it be to avoid cluttering.
        /// </summary>
        public void AddCustomer(string firstName, string lastName, string postalCode, string phone
                           , string email, string country)
        {
            try
            {
                var customer = new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PostalCode = postalCode,
                    Country = country,
                    Phone = phone,
                    Email = email
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
                Console.WriteLine($"{firstName} was added successfully to the database!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Updates a customer from the database
        /// </summary>
        public void UpdateCustomerById(int id, string firstName, string lastName, string postalCode,
                                                       string phone, string email, string country)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer is null)
                {
                    Console.WriteLine("Sorry no customer with that id was found!");
                }
                else
                {
                    customer.FirstName = firstName;
                    customer.LastName = lastName;
                    customer.PostalCode = postalCode;
                    customer.Phone = phone;
                    customer.Email = email;
                    customer.Country = country;
                    _context.SaveChanges();
                    Console.WriteLine($"The update of {firstName} with id {id} was successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// Shows the number of customers in each country ordered descending
        /// </summary>
        public void ShowCustomersPerCountry()
        {
            var customers = _context.Customers.GroupBy(c => c.Country).Select(c =>
                new { Country = c.Key, Total = c.Count() }).OrderByDescending(c => c.Total).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0,-24} {1,-15}", "Country", "Total");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in customers)
            {
                Console.WriteLine("{0, -24} {1, -15}", item.Country, item.Total);
            }
        }

        /// <summary>
        /// Shows all of the customers ordered by the amount they have spent on music
        /// </summary>
        public void ShowHighestSpenders()
        {
            var highestSpenders = _context.Invoices
                .Select(i => new
                {
                    FirstName = i.Customer.FirstName,
                    Total = i.Total,
                    customerName = $"{i.Customer.FirstName} {i.Customer.LastName}"
                }).OrderByDescending(i => i.Total).ThenBy(i => i.FirstName);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0,-24} {1,-15}", "Name", "TotalSpent");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var customer in highestSpenders)
            {
                Console.WriteLine("{0, -24} {1, -15}", customer.customerName, customer.Total);
            }
        }

        /// <summary>
        ///  Shows the customers most popular genre, in case of ties it only shows the first two genre's on equal footing.
        /// </summary>
        public List<Genre> ShowCustomerMostPopularGenre(int id)
        {
            var sql = _context.Genres.FromSqlInterpolated(
                $"SELECT TOP 1 WITH TIES genre.Name, genre.GenreId ,COUNT(*) AS NumInEachGenre\r\nFROM Genre\r\nJOIN Track ON genre.GenreId = Track.GenreId\r\nJOIN InvoiceLine ON Track.TrackId = InvoiceLine.InvoiceLineId\r\nJOIN Invoice ON InvoiceLine.InvoiceId = Invoice.InvoiceId\r\nJOIN Customer ON Invoice.CustomerId = Customer.CustomerId\r\nWHERE Customer.CustomerId = {id}\r\nGROUP BY genre.GenreId, Genre.Name\r\nORDER BY NumInEachGenre DESC").ToList();
            if (sql.Count == 0)
            {
                Console.WriteLine("\nSorry, the customer haven't bought anything!");
                return sql;
            }
            if (sql.Count() == 2)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nCustomers favorite genre is both: {0} and {1}", sql[0].Name, sql[1].Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nCustomers most popular genre is: {0}", sql[0].Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            return sql;
        }
    }
}
