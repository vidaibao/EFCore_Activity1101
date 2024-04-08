using InventoryModels;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDatabaseLayer
{
    public interface IItemsRepo
    {
        Task<List<Item>> GetItems();
        Task<List<ItemDto>> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue);
        Task<List<GetItemsForListingDto>> GetItemsForListingFromProcedure();
        Task<List<GetItemsTotalValueDto>> GetItemsTotalValues(bool isActive);
        Task<List<FullItemDetailDto>> GetItemsWithGenresAndCategories();
        Task<int> UpsertItem(Item item);
        Task UpsertItems(List<Item> items);
        Task DeleteItem(int id);
        Task DeleteItems(List<int> itemIds);



        //int UpsertItem(Item item);
        //void UpsertItems(List<Item> items);
        //void DeleteItem(int id);
        //void DeleteItems(List<int> itemIds); 
        //List<Item> GetItems(); // ItemDto >> Item
        //List<ItemDto> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue);
        //List<GetItemsForListingDto> GetItemsForListingFromProcedure();
        //List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive);
        //List<FullItemDetailDto> GetItemsWithGenresAndCategories();
    }
}
