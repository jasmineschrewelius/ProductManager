using ProductManager.Data;
using static System.Console;

//File scoped namespace, namespace to group the different files
namespace ProductManager;

class Program
{
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

                    // call function to search for a product
                    SearchProductView();

                    break;

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:

                    Environment.Exit(0);

                    return;
            }

            Clear();
        }
    }

    // show the different product info needed
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
        // ask if information is correct
        WriteLine("Är detta korrekt? [J]A  [N]EJ");

        var keyPressed = ReadKey(intercept: true);
        // clear screen
        Clear();

        switch (keyPressed.Key)
            {   
                case ConsoleKey.J: // if J key is pressed by user, call save product function
                    // call function to save the product
                    SaveProduct(product);

                    WriteLine("Produkt Sparad");

                    Thread.Sleep(2000);

                    break;
            
                case ConsoleKey.N: // if N key is pressed, clear screen and start the information needed again
                    RegisterProduct();

                    break;    
            }        
   
    }

    private static void SaveProduct(Product product)
    {
        // create instance of DbContext
        using var context = new ApplicationDbContext();
        
            //use context to add product
            context.Product.Add(product);

            // save change
            context.SaveChanges();
    }

    private static void SearchProductView()
    {
        // ask user for SKU
        Write("SKU:");

        string productSku = ReadLine();

        Clear();

        // call search funtion for the SKU
        var product = SearchProductBySku(productSku);

        // if we find a product with that SKU, do this
        if( product is not null)
        {
            // write out the information about the product we found
            WriteLine($"Namn: {product.ProductName}");
            WriteLine($"SKU: {product.ProductSku}");
            WriteLine($"Beskrivning: {product.ProductDescription}");
            WriteLine($"Bild (URL): {product.ProductPicture}");
            WriteLine($"Pris: {product.ProductPrice}");

            // while loop, the product information will show until key pressed is not equal to Escape key
            while (ReadKey(true).Key != ConsoleKey.Escape);
        }
        else
        {
            WriteLine("Hittade ingen produkt");

            Thread.Sleep(2000);
        }

    }

    

   
   private static Product? SearchProductBySku(string productSku) // will return a product or null(no value)
    {
        // create instance of DbContext
        using var context = new ApplicationDbContext();

        // use context to find product by sku, will contain a referens to the product or a default (null)
        var product = context
            .Product
            .FirstOrDefault(product => product.ProductSku == productSku);

        // then return the product
        return product;   
    }

}