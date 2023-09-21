using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager;

// create class for Product modell to declare each property
class Product
{
    // auto-implemented property , property = two metods get and set
    public int ProductId { get; set; }

    [MaxLength(25)] // set the maximum lenght for how our migrations will create this
    public required string ProductName { get; set; } // can be null and is public

    [Column(TypeName = "nchar(6)")] // sets it to 6 
    public required string ProductSku { get; set; }

    [MaxLength(100)]
    public required string ProductDescription { get; set; }

    [MaxLength(50)]
    public required string ProductPicture { get; set; }

    [MaxLength(25)]
    public required string ProductPrice { get; set; }
}