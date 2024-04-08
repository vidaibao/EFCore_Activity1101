using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_DBLibrary;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Transactions;

namespace InventoryDatabaseLayer
{
    public class ItemsRepo : IItemsRepo
    {

        private readonly IMapper _mapper;
        private readonly InventoryDbContext _context;
        public ItemsRepo(InventoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<List<Item>> GetItems()
        {
            return await _context.Items
                        .Include(x => x.Category)
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x => x.Name)
                        .ToListAsync();
        }

        public async Task<List<ItemDto>> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue)
        {
            return await _context.Items.Include(x => x.Category)
                        .Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
                        .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async  Task<List<GetItemsForListingDto>> GetItemsForListingFromProcedure()
        {
            return await _context.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing").ToListAsync();
        }

        public async Task<List<GetItemsTotalValueDto>> GetItemsTotalValues(bool isActive)
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            return await _context.GetItemsTotalValues
                        .FromSqlRaw("SELECT * from [dbo].[GetItemsTotalValue] (@IsActive)", isActiveParm)
                        .ToListAsync();
        }

        public async Task<List<FullItemDetailDto>> GetItemsWithGenresAndCategories()
        {
            return await _context.FullItemDetailDtos
                        .FromSqlRaw("SELECT * FROM [dbo].[vwFullItemDetails]")
                        .OrderBy(x => x.ItemName).ThenBy(x => x.GenreName)
                        .ThenBy(x => x.Category).ThenBy(x => x.PlayerName)
                        .ToListAsync();
        }

        public async Task<int> UpsertItem(Item item)
        {
            if (item.Id > 0)
            {
                return await UpdateItem(item);
            }
            return await CreateItem(item);
        }

        public async Task UpsertItems(List<Item> items)
        {
            //using var transaction = _context.Database.BeginTransaction();
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });
            try
            {
                foreach (var item in items)
                {
                    var success = await UpsertItem(item) > 0;
                    if (!success) throw new Exception($"Error saving the item { item.Name }");
                }
                scope.Complete();
            }
            catch (Exception ex)
            {
                //log it:
                Debug.WriteLine(ex.ToString());
                //transaction.Rollback();
                throw;
            }
        }
        public async Task DeleteItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return;
            item.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItems(List<int> itemIds)
        {
            //using var t = _context.Database.BeginTransaction();
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });
            try
            {
                foreach (var itemId in itemIds)
                {
                    await DeleteItem(itemId);
                }
                //t.Commit();
                scope.Complete();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                //t.Rollback();
                throw ex; // make sure it is known that the transaction failed
            }
        }

        private async Task<int> CreateItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            var newItem = await _context.Items //.ToList()
                            .FirstOrDefaultAsync(x => x.Name.ToLower()
                            .Equals(item.Name.ToLower())) ?? throw new Exception("Could not Create the item as expected");
            return newItem.Id;
        }
        private async Task<int> UpdateItem(Item item) 
        {
            var dbItem = await _context.Items
                            .Include(x => x.Category)
                            .Include(x => x.ItemGenres)
                            //.Include(x => x.Players)    // error
                            .FirstOrDefaultAsync(x => x.Id == item.Id) ?? throw new Exception("Item not found");
            dbItem.CategoryId = item.CategoryId;
            dbItem.CurrentOrFinalPrice = item.CurrentOrFinalPrice;
            dbItem.Description = item.Description;
            dbItem.IsActive = item.IsActive;
            dbItem.IsDeleted = item.IsDeleted;
            dbItem.IsOnSale = item.IsOnSale;
            if (item.ItemGenres != null)
            {
                dbItem.ItemGenres = item.ItemGenres;
            }
            dbItem.Name = item.Name;
            dbItem.Notes = item.Notes;
            if (item.Players != null)
            {
                dbItem.Players = item.Players;
            }
            dbItem.PurchasedDate = item.PurchasedDate;
            dbItem.PurchasePrice = item.PurchasePrice;
            dbItem.Quantity = item.Quantity;
            dbItem.SoldDate = item.SoldDate;
            _context.SaveChanges();
            return item.Id;
        }

    }
}
