namespace EFSamples.Domain.Entities
{
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
}
