//File scoped namespace, namespace to group the different files
namespace ProductManager;

// create class for Product modell to declare each property
class Product
{
    // auto-implemented property , property = two metods get and set
    public int ProductId { get; set; }

    public required string ProductName { get; set; } // can be null and is public

    public required string ProductSku { get; set; }

    public required string ProductDescription { get; set; }

    public required string ProductPicture { get; set; }

    public required string ProductPrice { get; set; }
}