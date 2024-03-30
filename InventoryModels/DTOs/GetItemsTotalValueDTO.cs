﻿namespace InventoryModels.DTOs
{
    public class GetItemsTotalValueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Quantity { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? TotalValue { get; set; }
    }

}
