using System;
using System.Collections.Generic;

class Order
{
    private List<Product> products;
    private Customer customer;
    private decimal shippingCost;

    public Order(Customer customer)
    {
        products = new List<Product>();
        this.customer = customer;
        shippingCost = customer.Address.IsInUSA() ? 5m : 35m;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (Product product in products)
        {
            totalPrice += product.CalculatePrice();
        }
        totalPrice += shippingCost;
        return totalPrice;
    }

    public string GetPackingLabel()
    {
        string packingLabel = "";
        foreach (Product product in products)
        {
            packingLabel += "Product Name: " + product.Name + ", Product ID: " + product.ProductId + "\n";
        }
        return packingLabel;
    }

    public string GetShippingLabel()
    {
        string shippingLabel = "Customer Name: " + customer.Name + "\n";
        shippingLabel += "Address:\n" + customer.Address.GetFullAddress();
        return shippingLabel;
    }
}

class Product
{
    public string Name { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public decimal CalculatePrice()
    {
        return Price * Quantity;
    }
}

class Customer
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public bool IsInUSA()
    {
        return Country == "USA";
    }

    public string GetFullAddress()
    {
        return Street + "\n" + City + ", " + State + "\n" + Country;
    }
}

class EncapsulationwithOnlineOrdering
{
    static void Main(string[] args)
    {
        Address address1 = new Address
        {
            Street = "123 Main St",
            City = "New York",
            State = "NY",
            Country = "USA"
        };

        Address address2 = new Address
        {
            Street = "456 Elm St",
            City = "London",
            State = "N/A",
            Country = "United Kingdom"
        };

        Customer customer1 = new Customer
        {
            Name = "John Smith",
            Address = address1
        };

        Customer customer2 = new Customer
        {
            Name = "Jane Doe",
            Address = address2
        };

        Product product1 = new Product
        {
            Name = "Product 1",
            ProductId = 1,
            Price = 10.99m,
            Quantity = 2
        };

        Product product2 = new Product
        {
            Name = "Product 2",
            ProductId = 2,
            Price = 5.99m,
            Quantity = 3
        };

        Console.WriteLine();
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product1);
        order2.AddProduct(product2);

        Console.WriteLine("Order 1:");
        Console.WriteLine("Packing Label:\n" + order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:\n" + order1.GetShippingLabel());
        Console.WriteLine("Total Price: $" + order1.CalculateTotalPrice());
        Console.WriteLine();

        Console.WriteLine("Order 2:");
        Console.WriteLine("Packing Label:\n" + order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:\n" + order2.GetShippingLabel());
        Console.WriteLine("Total Price: $" + order2.CalculateTotalPrice());
        Console.WriteLine();
    }
}

// This program is an online ordering system for a fitness center. It allows customers to create orders 
// by selecting products, calculates the total price including shipping costs based on the customer's location, 
// and generates packing labels and shipping labels for each order. 