using System;
using System.Linq;
using Library.EStore.Models;
using Library.EStore.Services;
using Newtonsoft.Json;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program 
    {
        //private static global::System.Object Console;

        static void Main(string[] args)
        {

            //set up a list of inventory items

            var inventory = ProductService.Current;

            var cart = CartService.Current;


            bool b = true;

            while (b)
            {
                //CRUD = create, read, update, and delete
                Console.WriteLine("Welcome to the E-commerece App! ");
                Console.WriteLine("Are you an employee(1) or customer(2) ");
                var choice = int.Parse(Console.ReadLine() ?? "0");
                if (choice == 1)
                {
                    Console.WriteLine("Welcome Employee ");



                    var employeeChoice = employeeMenu();

                    if (employeeChoice == Action.BOGO)
                    {
                        Console.WriteLine("You are now in BOGO mode");

                        Console.WriteLine("Select a product to make BOGO");

                        for (int i = 0; i < inventory.Products.Count(); i++)
                        {
                            var items = inventory.Products[i];
                            Console.WriteLine($"{items}");
                        }

                        var bogoFlag = false;
                        var bogoChoice = int.Parse(Console.ReadLine() ?? "0");

                        Product? product = new ProductByQuantity();

                        var find = cart.Cart.Find(product => product.Id == bogoChoice);

                        Console.WriteLine($"item found: {find}");


                        if (find is ProductByQuantity)
                        {
                            var p = find as ProductByQuantity;

                            if (p != null)
                            {

                                if (p.Quantity >= 2)
                                {
                                    var tp = p.totalPrice / 2;

                                    Console.WriteLine("Item is now bogo");

                                    Console.WriteLine($"Total Price is: ${tp}");

                                    //need to update the actual list



                                }



                            }
                        }



                    }


                }

                else if (choice == 2)
                {
                    bool cont = true;
                    while (cont)
                    {
                        var action = PrintMenu();
                        if (action == ActionType.Create)
                        {
                            Console.WriteLine("You chose to add product\n");
                            Console.WriteLine("1. Add by weight\n");
                            Console.WriteLine("2. Add by quantity\n");

                            var typeChoice = int.Parse(Console.ReadLine() ?? "0");

                            Product? newProduct = null;

                            if (typeChoice == 1)
                            {
                                newProduct = new ProductByWeight();
                            }
                            else
                            {
                                newProduct = new ProductByQuantity();
                            }

                            //var newProduct = new Product();

                            FillInventory(newProduct);

                            inventory.Create(newProduct);

                            Console.WriteLine("Product has been added\n");

                        }


                        else if (action == ActionType.Read)
                        {
                            Console.WriteLine("You chose to see current inventory.\n");

                            //for (int i = 0; i < inventory.Products.Take(5).Count(); i++)
                            //{
                            //    var items = inventory.Products[i];
                            //    Console.WriteLine($"{items}");
                            //}




                            while (true)
                            {

                                Console.Write("Please enter a page number 1,2, or 3." +
                                    " 4 to exit");
                                int pageNum = 0;
                                if (int.TryParse(Console.ReadLine(), out pageNum))
                                {
                                    if (pageNum >= 1 && pageNum <= 3)
                                    {
                                        int pageSize = 5;

                                        var items = inventory.Products.Skip((pageNum - 1)
                                            * pageSize).Take(pageSize);

                                        Console.WriteLine();

                                        Console.WriteLine("Displaying page" + pageSize);

                                        foreach (Product p in items)
                                        {
                                            Console.WriteLine(p.name + " "
                                                + " " + p.description + " " + "$" + p.price);
                                        }
                                    }
                                    else if (pageNum == 4)
                                    {
                                        Console.WriteLine("Back to menu");
                                        break;
                                    }
                                }



                                else
                                {
                                    Console.WriteLine("Page number must be integer between 1 and 3");
                                }

                                //////////////////////////////////////////////////////////
                                Console.WriteLine("\n\n");
                                Console.WriteLine("1. Sort by name\n");
                                Console.WriteLine("2. Sort by total price\n");
                                Console.WriteLine("3. Sort by unit price\n");
                                Console.WriteLine("4. Return to menu\n");


                                var typeChoice = int.Parse(Console.ReadLine() ?? "0");

                                //Product? newProduct = null;

                                if (typeChoice == 1)
                                {

                                    foreach (var n in inventory.Products.OrderBy(x => x.name))
                                    {
                                        Console.WriteLine(n.name);

                                    }

                                }

                                else if (typeChoice == 2)
                                {

                                    Product? p = null;
                                    var tpQuant = p as ProductByQuantity;
                                    // var tpWeight = p as ProductByWeight;


                                    //not done
                                    for (int i = 0; i < cart.Cart.Count(); i++)
                                    {
                                        tpQuant = (ProductByQuantity?)cart.Cart[i];

                                        if (tpQuant != null)
                                        {
                                            Console.WriteLine($"${tpQuant.totalPrice}");
                                        }




                                    }

                                }
                                else if (typeChoice == 3)
                                {
                                    foreach (var unit in inventory.Products.OrderBy(x => x.price))
                                    {
                                        Console.WriteLine($"${unit.price}");

                                    }
                                }

                                else if (typeChoice == 4)
                                {
                                    continue;
                                }

                            }

                        }
                        else if (action == ActionType.Update)
                        {
                            Console.WriteLine("You chose to update an item \n");

                            Console.WriteLine("Which item would you like to update?\n");

                            var choiceOf = int.Parse(Console.ReadLine() ?? "0");

                            var itemOfInterest =
                                inventory.Products.FirstOrDefault(t => t.Id == choiceOf);
                            FillInventory(itemOfInterest);
                            inventory.Update(itemOfInterest);


                            Console.WriteLine("Product has been updated\n");


                        }
                        else if (action == ActionType.Add)
                        {
                            Console.WriteLine("You chose to add an item to the cart\n");

                            Console.WriteLine("Which item would you like to add to cart?\n");

                            var id = int.Parse(Console.ReadLine() ?? "0");

                            var itemOfInterest = inventory.Products[id];

                            cart.Create(itemOfInterest);

                            Console.WriteLine("Item added\n");

                            //DELETE NOT WOKRINGGGGGGGGG

                            inventory.Delete(id);


                        }
                        else if (action == ActionType.ReadCart)
                        {
                            Console.WriteLine("You chose to see the cart.\n");

                            for (int i = 0; i < cart.Cart.Count; i++)
                            {
                                var items = cart.Cart[i];

                                Console.WriteLine($"{i} {items}");
                            }

                        }

                        else if (action == ActionType.Search)
                        {
                            Console.WriteLine("You chose to search for an item \n");
                            Console.WriteLine("You can search using name or description\n");

                            Console.WriteLine("Search: \n");
                            var search = Console.ReadLine() ?? String.Empty;

                            var product = new Product();
                            var find = inventory.Products.Find(product => product.name == search
                            || product.description == search);

                            Console.WriteLine("Item found \n");

                            Console.WriteLine($"{find}");

                        }


                        //not done missing total price
                        else if (action == ActionType.Checkout)
                        {
                            Console.WriteLine("Ready to Checkout? Your current total is:$ ");

                            Product? t = null;

                            //var totWeight = t as ProductByWeight;
                            //var totQuant = t as ProductByQuantity;

                            //var total = totWeight.totalPrice + totQuant.totalPrice;

                            //Console.WriteLine($"{total}");



                            Console.WriteLine("Next step\n");
                            Console.WriteLine("Continue to Payment press 1\n");
                            var typeChoice = int.Parse(Console.ReadLine() ?? "0");

                            Product? newPayment = null;

                            if (typeChoice == 1)
                            {
                                newPayment = new Payment();

                            }

                            FillPayment(newPayment);
                            cart.Create(newPayment);
                            Console.WriteLine("Checkout successful\n");


                        }


                        else if (action == ActionType.Exit)
                        {

                            cart.Save("SaveData.json");
                            Console.WriteLine("You chose to exit. Your data is saved." +
                                "Goodbye!\n");
                            cont = false;
                        }

                        else if (action == ActionType.Load)
                        {
                            cart.Load("SaveData.json");
                            Console.WriteLine("Welcome Back! Here is your cart\n");
                        }

                        else if (action == ActionType.Return)
                        {

                            Console.WriteLine("Back to selcect user type!\n");

                            break;

                        }


                    }

                    Console.WriteLine("Thank you for shopping with us!");

                }




            }



        }


        //FUNCTIONS

        public static Action employeeMenu()
        {
            Console.WriteLine("1. Enter bogo mode\n");
            Console.WriteLine("2. Enter sale mode\n");

            var input = int.Parse(Console.ReadLine() ?? "0");

            while (true)
            {
                switch (input)
                {
                    case 1:
                        return Action.BOGO;
                    default:
                        continue;
                }
            }


        }


        public static ActionType PrintMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("1. Add Inventory\n");
            Console.WriteLine("2. Show Current Inventory\n");
            Console.WriteLine("3. Update an Item\n");
            Console.WriteLine("4. Add Item to Cart\n");
            Console.WriteLine("5. View Cart\n");
            Console.WriteLine("6. Search for an item in inventory\n");
            Console.WriteLine("7. Checkout\n");
            Console.WriteLine("8. Save cart and exit\n");
            Console.WriteLine("9. Load Saved data\n");
            Console.WriteLine("10. Return to Select User");

            var input = int.Parse(Console.ReadLine() ?? "0");


            while (true)
            {
                switch (input)
                {
                    case 1:
                        return ActionType.Create;
                    case 2:
                        return ActionType.Read;
                    case 3:
                        return ActionType.Update;
                    case 4:
                        return ActionType.Add;
                    case 5:
                        return ActionType.ReadCart;
                    case 6:
                        return ActionType.Search;
                    case 7:
                        return ActionType.Checkout;
                    case 8:
                        return ActionType.Exit;
                    case 9:
                        return ActionType.Load;
                    case 10:
                        return ActionType.Return;

                    default:
                        continue;

                }

            }

        }

        public static void FillInventory(Product? item)
        {
            if (item == null)
            {
                return;
            }
            Console.WriteLine("What is the name of the product?\n");
            item.name = Console.ReadLine() ?? String.Empty;

            Console.WriteLine("What is the description of the " +
                "product?\n");
            item.description = Console.ReadLine() ?? String.Empty;


            if (item is ProductByWeight)
            {
                var weight = item as ProductByWeight;


                if (weight != null)
                {
                    Console.WriteLine("What is the weight of the product");

                    weight.Weight = double.Parse(Console.ReadLine() ?? "0");
                }
            }

            else if (item is ProductByQuantity)
            {
                var quantity = item as ProductByQuantity;

                if (quantity != null)
                {
                    Console.WriteLine("What is the quantity of the product");

                    quantity.Quantity = int.Parse(Console.ReadLine() ?? "0");

                }
            }

            Console.WriteLine("What is the price of the product?\n");
            item.price = double.Parse(Console.ReadLine() ?? "0");

        }


        public static void FillPayment(Product? info)
        {
            if (info == null)
            {
                return;
            }


            if (info is Payment)
            {
                var n = info as Payment;

                if (n != null)
                {
                    Console.WriteLine("First and last name: ");
                    n.pName = Console.ReadLine() ?? String.Empty;

                    Console.WriteLine("Email Address: ");
                    n.Email = Console.ReadLine() ?? String.Empty;

                    Console.WriteLine("Card Number: ");
                    n.CardNum = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Expiration Date MM/YY: ");
                    n.Expire = Console.ReadLine() ?? String.Empty;

                    Console.WriteLine("CVC: ");
                    n.CVC = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Address: ");
                    n.Address = Console.ReadLine() ?? String.Empty;

                }
            }


        }
    }

    public enum ActionType
    {
        Create, Read, Update, Delete, Exit, Add, ReadCart, Search,
        Checkout, Load, Return
    }

    public enum ProductType
    {
        Weight, Quantity

    }

    public enum Sorting
    {
        sortName, sortTotalPrice, sortUnitPrice
    }

    public enum Pay
    {
        pName, Email, CardNum, Expire, CVC, Address
    }

    public enum Action
    {
        BOGO
    }
}




