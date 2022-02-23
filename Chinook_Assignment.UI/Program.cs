using System;
using Chinook.Data;

namespace Chinook_Assignment.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Just uncomment to try the methods out ^^ 

            IAssignmentRepository repo = new CustomerRepository();
            
            // repo.DisplayCustomersToConsole();                    //1

            // repo.DisplayCustomerToConsoleById(5);                //2

            // repo.DisplayCustomerToConsoleByFirstName("He");       //3 A 

            // repo.DisplayCustomersToConsoleByFirstName("a");      //3 B

            // repo.ReturnPage(3, 4);                               //4 

            // repo.AddCustomer("Gabriel", "Mr.Anderson", "6666", "0000", "at@Awesome.com", "Sweden");   //5

            // repo.UpdateCustomerById(59, "melvin", "melvinsson", "pppp", "at@Awesome.com", "glhf", "the awesome Country"); //6                          //6

            // repo.ShowCustomersPerCountry();                      //7

            // repo.ShowHighestSpenders();                          //8
            
            // repo.ShowCustomerMostPopularGenre(1);                //9A

            // repo.ShowCustomerMostPopularGenre(2);                //9B
        }
    }
}
