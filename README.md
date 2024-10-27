# What is Entity Framework?

Entity Framework is an open-source [ORM framework](https://en.wikipedia.org/wiki/Object-relational_mapping "Object-relational Mapping") for .NET applications supported by Microsoft. It enables developers to work with data using objects of domain specific classes without focusing on the underlying database tables and columns where this data is stored. With the Entity Framework, developers can work at a higher level of abstraction when they deal with data, and can create and maintain data-oriented applications with less code compared with traditional applications.


##  Entity Framework Features

-   **Cross-platform:**  EF Core is a cross-platform framework which can run on Windows, Linux and Mac.
-   **Modelling:**  EF (Entity Framework) creates an EDM (Entity Data Model) based on POCO (Plain Old CLR Object) entities with get/set properties of different data types. It uses this model when querying or saving entity data to the underlying database.
-   **Querying:**  EF allows us to use LINQ queries (C#/VB.NET) to retrieve data from the underlying database. The database provider will translate this LINQ queries to the database-specific query language (e.g. SQL for a relational database). EF also allows us to execute raw SQL queries directly to the database.
-   **Change Tracking:**  EF keeps track of changes occurred to instances of your entities (Property values) which need to be submitted to the database.
-   **Saving:**  EF executes INSERT, UPDATE, and DELETE commands to the database based on the changes occurred to your entities when you call the  `SaveChanges()`  method. EF also provides the asynchronous  `SaveChangesAsync()`  method.
-   **Concurrency:**  EF uses Optimistic Concurrency by default to protect overwriting changes made by another user since data was fetched from the database.
-   **Transactions:**  EF performs automatic transaction management while querying or saving data. It also provides options to customize transaction management.
-   **Caching:**  EF includes first level of caching out of the box. So, repeated querying will return data from the cache instead of hitting the database.
-   **Built-in Conventions:**  EF follows conventions over the configuration programming pattern, and includes a set of default rules which automatically configure the EF model.
-   **Configurations:**  EF allows us to configure the EF model by using data annotation attributes or Fluent API to override default conventions.
-   **Migrations:**  EF provides a set of migration commands that can be executed on the NuGet Package Manager Console or the Command Line Interface to create or manage underlying database Schema.

##  What is an Entity in Entity Framework?

An entity in Entity Framework is a class that maps to a database table. This class must be included as a `DbSet<TEntity>` type property in the `DbContext` class. EF API maps each entity to a table and each property of an entity to a column in the database.
For example, the following `ProductGroup`, and `Product` are domain classes in the EFSamples application.
``` 
 public class BaseEntity
  {
      public long Id { get; set; }
      public bool IsRemoved { get; set; }
  }
  public class ProductGroup:BaseEntity
  {
     public string Title { get; set; } = null!;
     public string Description { get; set; } = null!;
     public ICollection<Product> Products { get; set; }
  }
  public class Product:BaseEntity
  {
     public long ProductGroupId { get; set; }
     public string Title { get; set; } = null!;
     public string ShortDescription { get; set; } = null!;
     public string LongDescription { get; set; } = null!;
     public DateTime CreateDate { get; set; }
     public string ImageName { get; set; } = null!;
     public int Quantity { get; set; }
     public decimal Price { get; set; }
     public ProductGroup ProductGroup { get; set; }
     public ICollection<OrderDetail> OrderDetails { get; set; }
   }
   public class ECommerceDB: DbContext
   {
      public ECommerceDB()
      {
      }
      public DbSet<Product> Products{ get; set; }
      public DbSet<ProductGroup> ProductGroups { get; set; }
   }
```
## Fluent API in Entity Framework Core
Entity Framework Fluent API is used to configure domain classes to override conventions. EF Fluent API is based on a Fluent API design pattern (a.k.a [Fluent Interface](https://en.wikipedia.org/wiki/Fluent_interface)) where the result is formulated by [method chaining](https://en.wikipedia.org/wiki/Method_chaining).

Entity Framework Core Fluent API configures the following aspects of a model:
1.  Model Configuration: Configures an EF model to database mappings. Configures the default Schema, DB functions, additional data annotation attributes and entities to be excluded from mapping.
2.  Entity Configuration: Configures entity to table and relationships mapping e.g. PrimaryKey, AlternateKey, Index, table name, one-to-one, one-to-many, many-to-many relationships etc.
3.  Property Configuration: Configures property to column mapping e.g. column name, default value, nullability, Foreignkey, data type, concurrency column etc.

```
public class ECommerceDB: DbContext 
{
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Write Fluent API configurations here

        //Property Configurations
        modelBuilder.Entity<Products >()
                .Property(s => s.Id)
                .HasColumnName("Id")
                .HasDefaultValue(0)
                .IsRequired();
    }
}
 ```
 ```
  //Or we can use like This Method:
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
  }
  
 public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.CreateDate)
                .HasDefaultValue(DateTime.Now);

            builder.HasQueryFilter(o=>!o.IsRemoved);

            //Order-Datetime.now-Id
            builder.Property(o => o.Code)
                .HasComputedColumnSql("'Order-'+Cast(getdate() As varchar)+ '-' + Cast(Id as varchar) ");


            builder.Property<DateTime>("UpdateDate")
                .HasDefaultValueSql("getdate()");


        }
    }
 ```
## Working with DbContext in EF Core
Let's use the `EnsureCreated()` method to create a database and use the context class to save Product and ProductGroups data in the database.
```
using (var context = new DatabaseContext())
{
    //creates db if not exists 
    context.Database.EnsureCreated();
    
    //create entity objects
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

	 //add entitiy to the context
	   context.ProductGroups.AddRange(productGroupsList);

	 //save data to the database tables
	   context.SaveChanges();
  
     //retrieve all the ProductGroups from the database
	   foreach (var item in context.ProductGroups.ToList())
		{
		    Console.WriteLine($"Category: {item.Title}");
		}

    }
}
```
## Migrations in Entity Framework Core
EF Core provides migrations commands to create, update, or remove tables and other DB objects based on the entities and configurations. At this point, there is no `ECommerceDB` database. So, we need to create the database from the model (entities and configurations) by adding a migration.
In Visual Studio, open NuGet Package Manager Console from Tools -> NuGet Package Manager -> Package Manager Console and enter the following command:
```
1-enable-migrations
2-add-migration Initial-ECommerceDB
3-update-database 
4-remove-migration
```
## Project Example
In this Project we use from :
1-  Shadow Property in Entity Framework Core
2- Conversions in Entity Framework Core
3- Query Filter in Entity Framework Core
4- Value Generator in Entity Framework Core
5- Owns
6- Property Configuration

---
i hope so enjoy That.
