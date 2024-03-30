namespace InventoryModels.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public CategoryDetailDto CategoryDetail { get; set; }

    }
}
