using AutoMapper;
using EFCore_DBLibrary;
using InventoryDatabaseLayer;
using InventoryModels;
using InventoryModels.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Numerics;

namespace InventoryBusinessLayer
{
    public class ItemsService : IItemsService
    {
        private readonly IItemsRepo _dbRepo;
        private readonly IMapper _mapper;

        // the business/service layer is too tightly coupled to the database >>>
        public ItemsService(InventoryDbContext dbContext, IMapper mapper)
        {
            _dbRepo = new ItemsRepo(dbContext, mapper);
            _mapper = mapper;
        }
        // >>>
        public ItemsService(IItemsRepo dbRepo, IMapper mapper)
        {
            _dbRepo = dbRepo;
            _mapper = mapper;
        }


        public async Task<string> GetAllItemsPipeDelimitedString()
        {
            return string.Join("|", await GetItems());
        }

        public async Task<List<ItemDto>> GetItems()
        {
            return _mapper.Map<List<ItemDto>>(await _dbRepo.GetItems());
        }

        public async Task<List<ItemDto>> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue)
        {
            return await _dbRepo.GetItemsByDateRange(minDateValue, maxDateValue);
        }

        public async Task<List<GetItemsForListingDto>> GetItemsForListingFromProcedure()
        {
            return await _dbRepo.GetItemsForListingFromProcedure();
        }

        public async Task<List<GetItemsTotalValueDto>> GetItemsTotalValues(bool isActive)
        {
            return await _dbRepo.GetItemsTotalValues(isActive);
        }

        public async Task<List<FullItemDetailDto>> GetItemsWithGenresAndCategories()
        {
            return await _dbRepo.GetItemsWithGenresAndCategories();
        }

        public async Task<int> UpsertItem(CreateOrUpdateItemDto item)
        {
            if (item.CategoryId <= 0)
            {
                throw new ArgumentException("Please set the category id before insert or update");
            }
            return await _dbRepo.UpsertItem(_mapper.Map<Item>(item));
        }

        public async Task UpsertItems(List<CreateOrUpdateItemDto> items)
        {
            try
            {
                await _dbRepo.UpsertItems(_mapper.Map<List<Item>>(items));
            }
            catch (Exception ex)
            {
                //TODO: better logging/not squelching
                Console.WriteLine($"The transaction has failed: {ex.Message}");
            }
        }
        public async Task DeleteItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please set a valid item id before deleting");
            }
            await _dbRepo.DeleteItem(id);
        }

        public async Task DeleteItems(List<int> itemIds)
        {
            try
            {
                await _dbRepo.DeleteItems(itemIds);
            }
            catch (Exception ex)
            {
                //TODO: better logging/not squelching
                Console.WriteLine($"The transaction has failed: {ex.Message}");
            }
        }

    }
}
