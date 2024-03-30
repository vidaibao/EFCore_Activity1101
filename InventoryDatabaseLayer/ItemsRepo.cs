using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore_DBLibrary;
using InventoryModels.DTOs;

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

        public List<ItemDto> GetItems()
        {
            return _context.Items
                        .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                        .ToList();
        }

        public List<ItemDto> GetItemsByDateRange(DateTime minDateValue, DateTime maxDateValue)
        {
            throw new NotImplementedException();
        }

        public List<GetItemsForListingDto> GetItemsForListingFromProcedure()
        {
            throw new NotImplementedException();
        }

        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive)
        {
            throw new NotImplementedException();
        }

        public List<FullItemDetailDto> GetItemsWithGenresAndCategories()
        {
            throw new NotImplementedException();
        }
    }
}
