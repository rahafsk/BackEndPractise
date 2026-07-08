using ECommerce_System;
using ECommerceSystem;
using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static ECommerceContext context = new ECommerceContext();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== E-Commerce EF Core System =====");
            Console.WriteLine("1. Register a New User");
            Console.WriteLine("2. Add a New Product to a Category");
            Console.WriteLine("3. Place an Order");
            Console.WriteLine("4. Write a Product Review");
            Console.WriteLine("5. Update Product Price and Availability");
            Console.WriteLine("6. Cancel an Order");
            Console.WriteLine("7. Delete a Review");
            Console.WriteLine("8. View All Products");
            Console.WriteLine("9. Filter Products by Category and Price Range");
            Console.WriteLine("10. Get Category with All Its Products");
            Console.WriteLine("11. View Order History with Full Details");
            Console.WriteLine("12. Product Summary Report + Lazy Loading Demo");
            Console.WriteLine("13. Add Category");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                //case "1": RegisterUser(); break;
                //case "2": AddProductToCategory(); break;
                //case "3": PlaceOrder(); break;
                //case "4": WriteProductReview(); break;
                //case "5": UpdateProductPriceAndAvailability(); break;
                //case "6": CancelOrder(); break;
                //case "7": DeleteReview(); break;
                //case "8": ViewAllProducts(); break;
                //case "9": FilterProductsByCategoryAndPriceRange(); break;
                //case "10": GetCategoryWithAllProducts(); break;
                //case "11": ViewOrderHistoryWithFullDetails(); break;
                //case "12": ProductSummaryReport(); break;
                //case "13": AddCategory(); break;
                case "0": return;
                default: Console.WriteLine("Invalid choice."); break;

            }

        }
    }

    static int ReadInt(string message)
    {
        Console.Write(message);
        while (!int.TryParse(Console.ReadLine(), out int value))
        {
            Console.Write("Invalid number. Try again: ");
        }
        return 0;
    }

    static decimal ReadDecimal(string message)
    {
        Console.Write(message);
        while (!decimal.TryParse(Console.ReadLine(), out decimal value))
        {
            Console.Write("Invalid decimal number. Try again: ");
        }
        return 0;
    }

}