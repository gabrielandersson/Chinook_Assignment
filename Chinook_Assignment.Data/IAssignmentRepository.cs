using System.Collections.Generic;
using Chinook_Assignment.Domain.Models;

namespace Chinook_Assignment.Data
{
    public interface IAssignmentRepository
    {
        void DisplayCustomersToConsole();
        void DisplayCustomerToConsoleById(int customerId);
        void DisplayCustomersToConsoleByFirstName(string customerFirstName);
        void DisplayCustomerToConsoleByFirstName(string customerFirstName);
        List<Customer> ReturnPageWithOffsetAndLimit(int offset, int limit);
        void AddCustomer(string firstName, string lastName, string postalCode, string phone
            , string email, string country);
        void UpdateCustomerById(int id, string firstName, string lastName, string postalCode,
            string phone, string email, string country);
        void ShowCustomersPerCountry();
        void ShowHighestSpenders();
        List<Genre> ShowCustomerMostPopularGenreByCustomerId(int id);
    }
}
