using ECommerce_System;
using ECommerceSystem;
using ECommerceSystem.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static ECommerceContext context = new ECommerceContext();

    // --------------------------------------------------
    // Case 1: Register New User
    // --------------------------------------------------
    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("----- Register New User -----");

        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        User user = new User
        {
            username = username,
            email = email,
            passwordHash = password,
            registrationDate = DateTime.Now,
            isActive = true
        };

        context.Users.Add(user);
        context.SaveChanges();

        Console.WriteLine("User registered successfully.");
        Console.WriteLine("New User ID: " + user.userId);
    }

    // --------------------------------------------------
    // Case 2: Add New Product to Category
    // --------------------------------------------------
    static void AddProduct()
    {
        Console.Clear();
        Console.WriteLine("----- Add New Product -----");

        var categories = context.Categories.ToList();

        if (!categories.Any())
        {
            Console.WriteLine("No categories found. Add categories first.");
            return;
        }

        Console.WriteLine("Available Categories:");
        foreach (Category category in categories)
        {
            Console.WriteLine("ID: " + category.categoryId +
                              " | Name: " + category.categoryName);
        }

        Console.Write("Enter category ID: ");
        int categoryId = int.Parse(Console.ReadLine());

        Category selectedCategory = context.Categories
            .FirstOrDefault(c => c.categoryId == categoryId);

        if (selectedCategory == null)
        {
            Console.WriteLine("Category not found.");
            return;
        }

        Console.Write("Enter product name: ");
        string productName = Console.ReadLine();

        Console.Write("Enter description: ");
        string description = Console.ReadLine();

        Console.Write("Enter price: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Enter stock quantity: ");
        int stockQuantity = int.Parse(Console.ReadLine());

        Product product = new Product
        {
            productName = productName,
            description = description,
            price = price,
            stockQuantity = stockQuantity,
            categoryId = categoryId,
            createdAt = DateTime.Now,
            isAvailable = true
        };

        context.Products.Add(product);
        context.SaveChanges();

        Console.WriteLine("Product added successfully.");
        Console.WriteLine("New Product ID: " + product.productId);
    }



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

            // --------------------------------------------------
            // Main Menu
            // --------------------------------------------------
            static void Main(string[] args)
            {
                int choice;

                do
                {
                    Console.WriteLine("\n----- E-Commerce EF Core System ----");
                    Console.WriteLine("1. Register New User");
                    Console.WriteLine("2. Add New Product");
                    Console.WriteLine("3. Place Order");
                    Console.WriteLine("4. Write Product Review");
                    Console.WriteLine("5. Update Product Price and Availability");
                    Console.WriteLine("6. Cancel Order");
                    Console.WriteLine("7. Delete Review");
                    Console.WriteLine("8. View All Products");
                    Console.WriteLine("9. Filter Products by Category and Price Range");
                    Console.WriteLine("10. Get Category With Products");
                    Console.WriteLine("11. View User Order History");
                    Console.WriteLine("12. Product Summary Report");
                    Console.WriteLine("0. Exit");

                    Console.Write("Enter choice: ");
                    int.TryParse(Console.ReadLine(), out choice);

                    switch (choice)
                    {
                        case 1:
                            RegisterUser();
                            break;

                        case 2:
                            AddProduct();
                            break;

                        case 3:
                            //PlaceOrder();
                            break;

                        case 4:
                            //WriteReview();
                            break;

                        case 5:
                            //UpdateProduct();
                            break;

                        case 6:
                            //CancelOrder();
                            break;

                        case 7:
                            //DeleteReview();
                            break;

                        case 8:
                            //ViewAllProducts();
                            break;

                        case 9:
                            //FilterProducts();
                            break;

                        case 10:
                            //GetCategoryWithProducts();
                            break;

                        case 11:
                            //ViewOrderHistory();
                            break;

                        case 12:
                            //ProductSummaryReport();
                            break;

                        case 0:
                            Console.WriteLine("Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                    if (choice != 0)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }

                } while (choice != 0);
            }
        }
    }
}