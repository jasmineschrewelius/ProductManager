﻿using Microsoft.Data.SqlClient;
using static System.Console;
//File scoped namespace, namespace to group the different files
namespace ProductManager;

class Program
{
    //global variable for connection with database
    static string connectionString = "Server=.;Database=ProductManager;Integrated Security=true;Encrypt=False";

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
        // create sql command
        var sql = @"
            SELECT ProductName,
                 ProductSku,
                 ProductDescription,
                 ProductPicture,
                 ProductPrice
            FROM Product
            WHERE ProductSku = @ProductSku    
         ";
        // create connection to databas
        using var connection = new SqlConnection(connectionString);

        // create comman
        using var command = new SqlCommand(sql, connection);

        // ass value to the sql command
        command.Parameters.AddWithValue("@ProductSku", productSku);

        // open connection
        connection.Open();

        // we want data back and to read it
        var reader = command.ExecuteReader();

        // read = true or false, if product true, found get this information
        var product = reader.Read()
            ? new Product
            {
                ProductName = reader["ProductName"].ToString(),
                ProductSku = reader["ProductSku"].ToString(),
                ProductDescription = reader["ProductDescription"].ToString(),
                ProductPicture = reader["ProductPicture"].ToString(),
                ProductPrice = reader["ProductPrice"].ToString()

            }
            : default; // if false it returns null

        // close connection
        connection.Close();


        return product; 
   }

    private static void SaveProduct(Product product)
    {
        // create sql command
        var sql = $@"
            INSERT INTO Product( 
                 ProductName,
                 ProductSku,
                 ProductDescription,
                 ProductPicture,
                 ProductPrice
            ) VALUES (
                 @ProductName,
                 @ProductSku,
                 @ProductDescription,
                 @ProductPicture,
                 @ProductPrice
            )";

        // create the connection to database
        using var connection = new SqlConnection(connectionString);

        // create command 
        using var command = new SqlCommand(sql, connection);

        // add value to the sql
        command.Parameters.AddWithValue("@ProductName", product.ProductName);
        command.Parameters.AddWithValue("@ProductSku", product.ProductSku);
        command.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
        command.Parameters.AddWithValue("@ProductPicture", product.ProductPicture);
        command.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);

        connection.Open();

        command.ExecuteNonQuery();

        connection.Close();
    }
}