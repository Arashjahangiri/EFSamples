namespace EFSamples.Domain.Entities
{
    public class ProductGroup:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Product> Products { get; set; }

    }
}
