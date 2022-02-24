using Chinook_Assignment.Data;

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

            // repo.DisplayCustomerToConsoleByFirstName("He");      //3A 

            // repo.DisplayCustomersToConsoleByFirstName("a");      //3B

            // repo.ReturnPageWithOffsetAndLimit(3, 4);             //4 

            // repo.AddCustomer("Gabriel", "Mr.Anderson", "6666", "0000", "at@Awesome.com", "Sweden");   //5

            // repo.UpdateCustomerById(59, "melvin", "melvinsson", "9999", "023456789", "glhf@awesome.com", "the awesome Country"); //6      

            // repo.ShowCustomersPerCountry();                      //7

            // repo.ShowHighestSpenders();                          //8

            // repo.ShowCustomerMostPopularGenreByCustomerId(1);    //9A

            // repo.ShowCustomerMostPopularGenreByCustomerId(2);    //9B
        }
    }
}
