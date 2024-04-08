using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLayer
{
    public interface IItemsService
    {
        Task<int> UpsertItem(CreateOrUpdateItemDto item);
        Task UpsertItems(List<CreateOrUpdateItemDto> items);
        Task DeleteItem(int id);
        Task DeleteItems(List<int> itemIds);
        Task<List<ItemDto>> GetItems();
        Task<List<ItemDto>> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue);
        Task<List<GetItemsForListingDto>> GetItemsForListingFromProcedure();
        Task<List<GetItemsTotalValueDto>> GetItemsTotalValues(bool isActive);
        Task<string> GetAllItemsPipeDelimitedString();
        Task<List<FullItemDetailDto>> GetItemsWithGenresAndCategories();
    }
}
