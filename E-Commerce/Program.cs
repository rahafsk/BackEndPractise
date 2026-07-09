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

        //This object represents one new row that will be inserted into the Users table.
        User user = new User // creates a new User object.
        {
            username = username,
            /*
             * The left username is the property inside the User class.
             * The right username is the variable that stores the input from the console.
             * So it means:Save the entered username into the user object's username property.
             */
            email = email,
            passwordHash = password, // saves the entered password into the passwordHash property.
            registrationDate = DateTime.Now,
            /*
             * This stores the current date and time automatically.
             * The user does not enter this value. The system generates it.
             */
            isActive = true
        };

        context.Users.Add(user);
        /*
         * This tells EF Core:“I want to add this new user to the Users table.”
         * Important: this line does not save the data permanently yet. It only marks the object as something that should be inserted.
         */
        context.SaveChanges();
        /*
         * saves the user into the database.
         * EF Core sends an SQL INSERT command to the database.
         * After this line runs, the database generates a new userId.
         */

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

        //This displays all available categories.
        Console.WriteLine("Available Categories:");
        foreach (Category category in categories)  // foreach goes through each category in the list one by one.
        {
            Console.WriteLine("ID: " + category.categoryId +
                              " | Name: " + category.categoryName);
        }

        Console.Write("Enter category ID: ");
        int categoryId = int.Parse(Console.ReadLine());

        // This searches the Categories table for a category with the same ID entered by the user.
        Category selectedCategory = context.Categories
            /* 
             * FirstOrDefault(c => c.categoryId == categoryId)
             * means:“Find the first category where categoryId equals the entered category ID.”
             * If it finds a category, it returns that category.
             * If it does not find one, it returns null.
             */
            .FirstOrDefault(c => c.categoryId == categoryId);

        // This checks if the selectedCategory is null, which means no category was found with that ID.
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

    // --------------------------------------------------
    // Case 3: Place Order
    // --------------------------------------------------
    static void PlaceOrder()
    {
        Console.Clear();
        Console.WriteLine("----- Place Order -----");

        var users = context.Users.ToList();

        if (!users.Any())
        {
            Console.WriteLine("No users found.");
            return;
        }

        Console.WriteLine("Available Users:");
        foreach (User user in users)
        {
            Console.WriteLine("ID: " + user.userId +
                              " | Username: " + user.username);
        }

        Console.Write("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());

        User selectedUser = context.Users
            .FirstOrDefault(u => u.userId == userId);

        if (selectedUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        Order order = new Order
        {
            userId = userId,
            orderDate = DateTime.Now,
            status = "Pending",
            totalAmount = 0
        };

        // Save order first to get orderId from database
        context.Orders.Add(order);
        context.SaveChanges();

        decimal totalAmount = 0;
        string addMore = "yes";

        while (addMore == "yes")
        {
            var products = context.Products
                .Where(p => p.isAvailable == true && p.stockQuantity > 0)
                .ToList();

            if (!products.Any())
            {
                Console.WriteLine("No available products.");
                break;
            }

            Console.WriteLine("\nAvailable Products:");
            foreach (Product product in products)
            {
                Console.WriteLine("ID: " + product.productId +
                                  " | Name: " + product.productName +
                                  " | Price: " + product.price +
                                  " | Stock: " + product.stockQuantity);
            }

            Console.Write("Enter product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Product selectedProduct = context.Products
                .FirstOrDefault(p => p.productId == productId);

            if (selectedProduct == null)
            {
                Console.WriteLine("Product not found.");
                continue;
            }

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be positive.");
                continue;
            }

            if (selectedProduct.stockQuantity < quantity)
            {
                Console.WriteLine("Not enough stock.");
                continue;
            }

            OrderItem orderProduct = new OrderItem
            {
                orderId = order.orderId,
                productId = productId,
                quantity = quantity,
                unitPrice = selectedProduct.price
            };

            context.OrderProducts.Add(orderProduct);

            selectedProduct.stockQuantity -= quantity;

            totalAmount += selectedProduct.price * quantity;

            Console.Write("Add another product? yes/no: ");
            addMore = Console.ReadLine().ToLower();
        }

        order.totalAmount = totalAmount;

        context.SaveChanges();

        Console.WriteLine("Order placed successfully.");
        Console.WriteLine("Order ID: " + order.orderId);
        Console.WriteLine("Total Amount: " + order.totalAmount);
    }

    // --------------------------------------------------
    // Case 4: Write Product Review
    // --------------------------------------------------
    static void WriteReview()
    {
        Console.Clear();
        Console.WriteLine("----- Write Product Review -----");

        var users = context.Users.ToList();

        if (!users.Any())
        {
            Console.WriteLine("No users found.");
            return;
        }

        Console.WriteLine("Available Users:");
        foreach (User user in users)
        {
            Console.WriteLine("ID: " + user.userId +
                              " | Username: " + user.username);
        }

        Console.Write("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());

        User selectedUser = context.Users
            .FirstOrDefault(u => u.userId == userId);

        if (selectedUser == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        var products = context.Products.ToList();

        if (!products.Any())
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("\nAvailable Products:");
        foreach (Product product in products)
        {
            Console.WriteLine("ID: " + product.productId +
                              " | Name: " + product.productName);
        }

        Console.Write("Enter product ID: ");
        int productId = int.Parse(Console.ReadLine());

        Product selectedProduct = context.Products
            .FirstOrDefault(p => p.productId == productId);

        if (selectedProduct == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }
        // Why is this important? Because userId is a foreign key in the Review table. A review cannot be linked to a user that does not exist.

        Console.Write("Enter rating from 1 to 5: ");
        int rating = int.Parse(Console.ReadLine());

        if (rating < 1 || rating > 5)
        {
            Console.WriteLine("Rating must be between 1 and 5.");
            return;
        }
        /*
         * This validates the rating.
         * The condition means: If rating is less than 1 OR greater than 5, it is invalid.
         */

        Console.Write("Enter comment: ");
        string comment = Console.ReadLine();

        Review review = new Review
        {
            userId = userId,
            productId = productId,
            rating = rating,
            comment = comment,
            reviewDate = DateTime.Now
        };

        context.Reviews.Add(review);
        context.SaveChanges();

        Console.WriteLine("Review added successfully.");
        Console.WriteLine("Review ID: " + review.reviewId);
    }

    // --------------------------------------------------
    // Case 5: Update Product Price and Availability
    // --------------------------------------------------
    static void UpdateProduct()
    {
        Console.Clear();
        Console.WriteLine("----- Update Product -----");

        var products = context.Products.ToList();

        if (!products.Any())
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("Products:");
        foreach (Product productItem in products)
        {
            Console.WriteLine("ID: " + productItem.productId +
                              " | Name: " + productItem.productName +
                              " | Price: " + productItem.price +
                              " | Available: " + productItem.isAvailable);
        }

        Console.Write("Enter product ID: ");
        int productId = int.Parse(Console.ReadLine());

        Product product = context.Products
            .FirstOrDefault(p => p.productId == productId);

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            return;
        }

        Console.Write("Enter new price: ");
        decimal newPrice = decimal.Parse(Console.ReadLine());

        Console.Write("Is product available? true/false: ");
        bool isAvailable = bool.Parse(Console.ReadLine());

        product.price = newPrice;
        product.isAvailable = isAvailable;

        context.SaveChanges();

        Console.WriteLine("Product updated successfully.");
    }

    // --------------------------------------------------
    // Case 6: Cancel Order
    // --------------------------------------------------
    static void CancelOrder()
    {
        Console.Clear();
        Console.WriteLine("----- Cancel Order -----");

        Console.Write("Enter order ID: ");
        int orderId = int.Parse(Console.ReadLine());

        Order order = context.Orders
            .FirstOrDefault(o => o.orderId == orderId);

        if (order == null)
        {
            Console.WriteLine("Order not found.");
            return;
        }

        if (order.status == "Cancelled")
        {
            Console.WriteLine("Order is already cancelled.");
            return;
        }

        var orderItems = context.OrderItems
            .Where(i => i.orderId == orderId)
            .ToList();

        foreach (OrderItem item in orderItems)
        {
            Product product = context.Products
                .FirstOrDefault(p => p.productId == item.productId);

            if (product != null)
            {
                product.stockQuantity += item.quantity;
            }
        }

        order.status = "Cancelled";

        context.SaveChanges();

        Console.WriteLine("Order cancelled successfully.");
        Console.WriteLine("Product stock restored.");
    }

    // --------------------------------------------------
    // Case 7: Delete Review
    // --------------------------------------------------
    static void DeleteReview()
    {
        Console.Clear();
        Console.WriteLine("----- Delete Review -----");

        var reviews = context.Reviews.ToList();

        if (!reviews.Any()) // !reviews.Any() means the list is empty.
        {
            Console.WriteLine("No reviews found.");
            return;
        }

        Console.WriteLine("Reviews:");
        foreach (Review reviewItem in reviews)
        {
            Console.WriteLine("ID: " + reviewItem.reviewId +
                              " | Rating: " + reviewItem.rating +
                              " | Comment: " + reviewItem.comment);

            /*
             * This loop goes through every review in the list.
             * Review reviewItem in reviews means: Take each review from the reviews list and temporarily call it reviewItem.
             */

            Console.Write("Enter review ID: ");
            int reviewId = int.Parse(Console.ReadLine());

            Review review = context.Reviews
                .FirstOrDefault(r => r.reviewId == reviewId);

            if (review == null)
            {
                Console.WriteLine("Review not found.");
                return;
            }

            context.Reviews.Remove(review);
            /*
             * This tells EF Core: Delete this review from the Reviews table.
             */
            context.SaveChanges(); // This actually applies the deletion in the database.

            Console.WriteLine("Review deleted successfully.");
        }

        // --------------------------------------------------
        // Case 8: View All Products
        // --------------------------------------------------
        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("----- View All Products -----");

            var products = context.Products.ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine("Product ID: " + product.productId);
                Console.WriteLine("Name: " + product.productName);
                Console.WriteLine("Price: " + product.price);
                Console.WriteLine("Stock Quantity: " + product.stockQuantity);
                Console.WriteLine("Available: " + product.isAvailable);
                Console.WriteLine("--------------------------------");
            }
        }

        // --------------------------------------------------
        // Case 9: Filter Products by Category and Price Range
        // --------------------------------------------------
        static void FilterProducts()
        {
            Console.Clear();
            Console.WriteLine("----- Filter Products -----");

            var categories = context.Categories.ToList();

            if (!categories.Any())
            {
                Console.WriteLine("No categories found.");
                return;
            }

            Console.WriteLine("Categories:");
            foreach (Category category in categories)
            {
                Console.WriteLine("ID: " + category.categoryId +
                                  " | Name: " + category.categoryName);
            }

            Console.Write("Enter category ID: ");
            int categoryId = int.Parse(Console.ReadLine());

            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());

            /*
             * This is the main part of the function.
             * It filters the Products table using three conditions.
             */
            var products = context.Products
                .Where(p => p.categoryId == categoryId && // Only show products whose category ID is equal to the category ID entered by the user.
                            p.price >= minPrice &&  // Only show products whose price is greater than or equal to the minimum price.
                            p.price <= maxPrice)
                /*
                 * .Where() is used to filter data.
                 * It only returns products that match the condition.
                 * Here, p means one product from the Products table.
                 */
                .OrderBy(p => p.price) // This sorts the filtered products by price from lowest to highest.
                .ToList();

            if (!products.Any())
            {
                Console.WriteLine("No products found in this filter.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine("ID: " + product.productId +
                                  " | Name: " + product.productName +
                                  " | Price: " + product.price +
                                  " | Stock: " + product.stockQuantity);
            }
        }


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
                                PlaceOrder();
                                break;

                            case 4:
                                WriteReview();
                                break;

                            case 5:
                                UpdateProduct();
                                break;

                            case 6:
                                //CancelOrder();
                                break;

                            case 7:
                                DeleteReview();
                                break;

                            case 8:
                                ViewAllProducts();
                                break;

                            case 9:
                                FilterProducts();
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
    
