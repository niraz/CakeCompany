using CakeCompany.Contract.Models;
using System.ComponentModel.DataAnnotations;

namespace CakeCompany.Models;

public class Product
{
    public Guid Id { get; set; }
    public ICake? Cake { get; set; }
    [Range(1, 1000)]
    public double Quantity { get; set; }
    public int OrderId { get; set; }
}
