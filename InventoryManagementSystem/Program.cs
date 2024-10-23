using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}

public class Sale
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
}

public class InventoryManagementSystem
{
    private List<Product> products = new List<Product>();
    private List<Sale> sales = new List<Sale>();
    private int nextProductId = 1;

    public void AddProduct(string name, string description, double price, int stock)
    {
        Product product = new Product
        {
            Id = nextProductId++,
            Name = name,
            Description = description,
            Price = price,
            Stock = stock
        };
        products.Add(product);
        Console.WriteLine($"Added product: {product.Name} (ID: {product.Id})");
    }

    public void UpdateStock(int productId, int quantity)
    {
        var product = products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            product.Stock += quantity;
            Console.WriteLine($"Updated stock for {product.Name}: {product.Stock} units.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void RecordSale(int productId, int quantity)
    {
        var product = products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            if (product.Stock >= quantity)
            {
                product.Stock -= quantity;
                Sale sale = new Sale
                {
                    ProductId = productId,
                    Quantity = quantity,
                    SaleDate = DateTime.Now
                };
                sales.Add(sale);
                Console.WriteLine($"Recorded sale: {quantity} of {product.Name}");
            }
            else
            {
                Console.WriteLine("Insufficient stock for sale.");
            }
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    public void GenerateInventoryReport()
    {
        Console.WriteLine("Current Inventory Status:");
        foreach (var product in products)
        {
            Console.WriteLine($"- ID: {product.Id}, Name: {product.Name}, Stock: {product.Stock}, Price: {product.Price:C}");
        }
    }

    public void GenerateSalesReport()
    {
        Console.WriteLine("Sales Transactions:");
        foreach (var sale in sales)
        {
            var product = products.FirstOrDefault(p => p.Id == sale.ProductId);
            Console.WriteLine($"- Sold {sale.Quantity} of {product.Name} on {sale.SaleDate}");
        }
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        InventoryManagementSystem ims = new InventoryManagementSystem();

        ims.AddProduct("Laptop", "MacBook Pro", 1200.00, 10);
        ims.AddProduct("Smartphone", "Latest model smartphone", 800.00, 20);
        ims.AddProduct("Smartphone", "I-phone 16", 20000.00, 15);
        ims.AddProduct("Smartphone", "I-phone 16 Pro Max",15000.00, 18);
        ims.AddProduct("Tablet", "I-Pad", 30000.00, 15);
        ims.AddProduct("Laptop", "Lenovo", 15000.00, 25);




        ims.UpdateStock(1, 5); // Add 5 more laptops
        ims.RecordSale(1, 3);  // Sell 3 laptops
        ims.RecordSale(2, 2);  // Sell 2 smartphones

        ims.GenerateInventoryReport();
        ims.GenerateSalesReport();
    }
}