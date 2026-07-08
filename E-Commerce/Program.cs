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
        }
    }
}