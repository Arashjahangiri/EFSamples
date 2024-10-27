

using EFSamples.DAL.DatabaseContext;
using EFSamples.Domain.Entities;

var context = new DatabaseContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();


#region ProductGroups

var productGroupsList = new List<ProductGroup>() {
new ProductGroup {
    Title = "Laptop",
    Description = "Laptop Category"
},
new ProductGroup {
    Title = "Mobile",
    Description = "Mobile Category"
},
new ProductGroup {
    Title = "Tvs",
    Description = "Tvs Category",
    IsRemoved=true
}
};

context.ProductGroups.AddRange(productGroupsList);
context.SaveChanges();

Console.WriteLine("Product Groups:-----------------");
foreach (var item in context.ProductGroups.ToList())
{

    Console.WriteLine($"{item.Title}");
}


#endregion


#region Product

var productList = new List<Product>()
{
    new Product
    {
        ProductGroupId=1,
        Title="Asus X",
        Price=1200,
        Quantity=10,
        ShortDescription="This is short text for Asus",
        LongDescription="This is long text for Asus",
        ImageName=Guid.NewGuid().ToString()
    }
};
context.Products.AddRange(productList);
context.SaveChanges();

Console.WriteLine("Products :-----------------");
foreach (var item in context.Products.ToList())
{

    Console.WriteLine($"{item.Title}-{item.Price}-{item.CreateDate}-{item.ShortDescription}");
}


#endregion


#region User

var User = new User()
{
    FirstName="Arash",
    LastName="Jahangiri",
    Username="a@a.com",
    Password="123",
    Home=new Address
    {
        City="Isfahan",
        Country="Iran",
        Street="X",
        MoreInfo="XXXXXX"
    }
};
context.Users.Add(User);
context.SaveChanges();

Console.WriteLine("Users :-----------------");
foreach (var item in context.Users.ToList())
{
    Console.WriteLine($"{item.FirstName}-{item.LastName}-{item.Username}");
}

#endregion


#region Order

var order = new Order()
{
    UserId = User.Id,
    IsFinaly = true,
    TotalPrice = 2400,
};

context.Orders.Add(order);
context.SaveChanges();

var orderDetail = new OrderDetail()
{
    OrderId = order.Id, 
    ProductId = 1,
    Count = 2,
    Price = 1200
};


context.OrderDetails.Add(orderDetail);
context.SaveChanges();


Console.WriteLine("Order :-----------------");
foreach (var item in context.Orders.ToList())
{
    Console.WriteLine($"{item.User.FirstName + " "+ item.User.LastName }-{item.TotalPrice}-{item.CreateDate}-{(item.IsFinaly ? "Finalized" : "Not Finalized")}");

    Console.WriteLine("Order Detail:-----------------");
    foreach (var itemorderDetail in context.OrderDetails.Where(od=>orderDetail.OrderId==item.Id))
    {
        Console.WriteLine($"{itemorderDetail.Product.Title}-{itemorderDetail.Count}-{itemorderDetail.Price}");
    }
}


#endregion



