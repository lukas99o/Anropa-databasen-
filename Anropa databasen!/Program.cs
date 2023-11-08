using Anropa_databasen_.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Anropa_databasen_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            using (var context = new NorthWindContext())
            {
                
                while (input != "q")
                {
                    input = "";
                    Console.WriteLine("Please enter one of these keys:");
                    Console.WriteLine($"[A] All customers\n[C] Create Customer\n[Q] Quit");

                    while (input != "q" && input != "a" && input != "c")
                    {
                        input = Console.ReadLine();
                        input = input.ToLower();

                        if (input != "q" && input != "a" && input != "c")
                        {
                            Console.WriteLine("Enter any of the given keys.\n[A]\n[C]\n[Q]");
                        }
                    }

                    switch (input)
                    {
                        case "a":
                            {
                                input = "";
                                while (input != "a" && input != "d" && input != "q")
                                {
                                    Console.WriteLine("Please enter either [A] for ascending order or [D] for descending order or [Q] to quit: ");
                                    input = Console.ReadLine();
                                    input = input.ToLower();
                                }

                                if (input == "q")
                                {
                                    break;
                                }

                                IQueryable<Customer> sortedQuery = null;
                                var query = context.Customers.Include(c => c.Orders);

                                if (input == "a")
                                {
                                    sortedQuery = query.OrderBy(c => c.CompanyName);
                                }
                                else if (input == "d")
                                {
                                    sortedQuery = query.OrderByDescending(c => c.CompanyName);

                                }

                                if (sortedQuery != null)
                                {
                                    foreach (var customer in sortedQuery)
                                    {
                                        Console.WriteLine($"Company name: {customer.CompanyName}\nCountry: {customer.Country}\nRegion: {customer.Region}\nPhone Number: {customer.Phone}\n{customer.Orders.Count}");
                                        Console.WriteLine();
                                    }
                                }
                                
                                while (input != "y" && input != "n" && input != "q")
                                {
                                    Console.WriteLine("Do you want to see the info of a specific company? Answer with [Y] for yes or [N] for no or [Q] for quit.");
                                    input = Console.ReadLine();
                                    input = input.ToLower();
                                }

                                if (input == "q")
                                {
                                    break;
                                }

                                if (input == "y")
                                {
                                    Console.WriteLine("Please enter the name of a company!");
                                    input = Console.ReadLine();
                                    Console.WriteLine();

                                    var query2 = context.Customers
                                        .Where(c => c.CompanyName == input)
                                        .Include(c => c.Orders)
                                        .Select(c => new { specificClient = c.CompanyName, c.Country, c.Region, c.Phone, c.Orders })
                                        .ToList();

                                    foreach (var customer in query2)
                                    {
                                        Console.WriteLine($"Company name: {customer.specificClient}\nCountry: {customer.Country}\nRegion: {customer.Region}\nPhone Number: {customer.Phone}\n{customer.Orders.Count}");
                                        Console.WriteLine();
                                    }
                                }

                                Console.WriteLine();
                                break;
                            }
                        case "c":
                            {
                                Customer createCustomer = new Customer();

                                Console.WriteLine("Enter contact name: ");
                                input = Console.ReadLine();
                                createCustomer.ContactName = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter company name: ");
                                input = Console.ReadLine();
                                createCustomer.CompanyName = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter country: ");
                                input = Console.ReadLine();
                                createCustomer.Country = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter city: ");
                                input = Console.ReadLine();
                                createCustomer.City = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter adress: ");
                                input = Console.ReadLine();
                                createCustomer.Address = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter region: ");
                                input = Console.ReadLine();
                                createCustomer.Region = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter postalcode: ");
                                input = Console.ReadLine();
                                createCustomer.PostalCode = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter phone: ");
                                input = Console.ReadLine();
                                createCustomer.Phone = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter contact title: ");
                                input = Console.ReadLine();
                                createCustomer.ContactTitle = string.IsNullOrWhiteSpace(input) ? null : input;

                                Console.WriteLine("Enter fax: ");
                                input = Console.ReadLine();
                                createCustomer.Fax = string.IsNullOrWhiteSpace(input) ? null : input;

                                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                                var stringChars = new char[5];
                                var random = new Random();

                                for (int i = 0; i < stringChars.Length; i++)
                                {
                                    stringChars[i] = chars[random.Next(chars.Length)];
                                }

                                createCustomer.CustomerId = new String(stringChars);

                                context.Customers.Add(createCustomer);
                                context.SaveChanges();

                                Console.WriteLine("Customer added successfully.");
                                Console.WriteLine();
                                break;
                            }
                    } 
                }
                Console.WriteLine();
                Console.Write("Once you press a button the program will exit... ");
                Console.ReadKey();
            }
        }
    }
}