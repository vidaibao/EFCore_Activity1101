﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_DBLibrary;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        
        public List<Item> GetItems()
        {
            return _context.Items
                        .Include(x => x.Category)
                        .AsEnumerable()
                        .Where(x => !x.IsDeleted)
                        .OrderBy(x => x.Name)
                        .ToList();
        }

        public List<ItemDto> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue)
        {
            return _context.Items.Include(x => x.Category)
                        .Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
                        .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                        .ToList();
        }

        public List<GetItemsForListingDto> GetItemsForListingFromProcedure()
        {
            return _context.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing").ToList();
        }

        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive)
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            return _context.GetItemsTotalValues
                        .FromSqlRaw("SELECT * from [dbo].[GetItemsTotalValue] (@IsActive)", isActiveParm)
                        .ToList();
        }

        public List<FullItemDetailDto> GetItemsWithGenresAndCategories()
        {
            return _context.FullItemDetailDtos
                        .FromSqlRaw("SELECT * FROM [dbo].[vwFullItemDetails]")
                        .AsEnumerable()
                        .OrderBy(x => x.ItemName).ThenBy(x => x.GenreName)
                        .ThenBy(x => x.Category).ThenBy(x => x.PlayerName)
                        .ToList();
        }

        public int UpsertItem(Item item)
        {
            if (item.Id > 0)
            {
                return UpdateItem(item);
            }
            return CreateItem(item);
        }

        public void UpsertItems(List<Item> items)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in items)
                {
                    var success = UpsertItem(item) > 0;
                    if (!success) throw new Exception($"Error saving the item { item.Name }");
                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //log it:
                Debug.WriteLine(ex.ToString());
                transaction.Rollback();
                throw;
            }
        }
        public void DeleteItem(int id)
        {
            var item = _context.Items.FirstOrDefault(x => x.Id == id);
            if (item == null) return;
            item.IsDeleted = true;
            _context.SaveChanges();
        }
        public void DeleteItems(List<int> itemIds)
        {
            using var t = _context.Database.BeginTransaction();
            try
            {
                foreach (var itemId in itemIds)
                {
                    DeleteItem(itemId);
                }
                t.Commit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                t.Rollback();
                throw ex; // make sure it is known that the transaction failed
            }
        }

        private int CreateItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            var newItem = _context.Items.ToList()
                            .FirstOrDefault(x => x.Name.ToLower()
                            .Equals(item.Name.ToLower())) ?? throw new Exception("Could not Create the item as expected");
            return newItem.Id;
        }
        private int UpdateItem(Item item) 
        {
            var dbItem = _context.Items
                            .Include(x => x.Category)
                            .Include(x => x.ItemGenres)
                            //.Include(x => x.Players)    // error
                            .FirstOrDefault(x => x.Id == item.Id) ?? throw new Exception("Item not found");
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
