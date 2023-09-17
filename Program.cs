using static System.Console;
//File scoped namespace, namespace to group the different files
namespace ProductManager;

class Program
{
    // to save product need to create list 
    static List<Product> products = new List<Product>();

    static void Main()
    {
        CursorVisible = false;
        Title = "Product Manager";

        // while loop until user press exit
        while (true)
        {
            // write out menu options
            WriteLine("1. Ny Produkt");
            WriteLine("2. Sök Produkt");
            WriteLine("3. Avsluta");

            var keyPressed = ReadKey(intercept: true);

            // clear the screen
            Clear();

            // read what key the user presses and choose the right metod
            switch (keyPressed.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:

                    // call the function to register product
                    RegisterProduct();

                    break;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:

                    WriteLine("Du har valt 2");

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    private static void RegisterProduct()
    {
        Write("Namn:");

        string productName = ReadLine();

        Write("SKU:");

        string productSku = ReadLine();

        Write("Beskrivning:");

        string productDescription = ReadLine();

        Write("Bild (URL):");

        string productPicture = ReadLine();

        Write("Pris:");

        string productPrice = ReadLine();

        // create product
        var product = new Product
        {
            ProductName = productName,
            ProductSku = productSku,
            ProductDescription = productDescription,
            ProductPicture = productPicture,
            ProductPrice = productPrice
        };

            // call function to save the product
            SaveProduct(product);

            WriteLine("Produkt Sparad");

            Thread.Sleep(2000);
        
    }

    private static void SaveProduct(Product product)
    {
        // add the new product
        products.Add(product);
    }
}