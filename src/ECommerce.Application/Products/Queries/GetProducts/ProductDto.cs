namespace ECommerce.Application.Products.Queries.GetProducts;
public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}