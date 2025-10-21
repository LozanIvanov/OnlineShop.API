namespace OnlineShop.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
