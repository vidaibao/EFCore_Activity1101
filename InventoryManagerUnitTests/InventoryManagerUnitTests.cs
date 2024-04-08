using AutoMapper;
using InventoryBusinessLayer;
using InventoryDatabaseLayer;
using InventoryModels;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace InventoryManagerUnitTests
{
    [TestClass]
    public class InventoryManagerUnitTests
    {
        private const string TITLE_NEWHOPE = "Star Wars IV: A New Hope";
        private const string TITLE_EMPIRE = "Star Wars V: The Empire Strikes Back";
        private const string TITLE_RETURN = "Star Wars VI: The Return of the Jedi";
        private const string DESC_NEWHOPE = "Luke's Friends";
        private const string DESC_EMPIRE = "Luke's Dad";
        private const string DESC_RETURN = "Luke's Sister";

        private IItemsService _itemsService;
        private Mock<IItemsRepo> _itemsRepo;

        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private static IServiceProvider _serviceProvider;
        public TestContext TestContext { get; set; }


        [ClassInitialize]
        public static void InitializeTestEnvironment(TestContext testContext)
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(InventoryMapper));
            _serviceProvider = services.BuildServiceProvider();
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InventoryMapper>();
            });
            _mapperConfig.AssertConfigurationIsValid();
            _mapper = _mapperConfig.CreateMapper();
        }

        [TestInitialize]
        public void InitializeTests()
        {
            InstantiateItemsRepoMock();
            _itemsService = new ItemsService(_itemsRepo.Object, _mapper);
        }

        private void InstantiateItemsRepoMock()
        {
            _itemsRepo = new Mock<IItemsRepo>();

            var items = GetItemsTestData();

            //_itemsRepo.Setup(m => m.GetItems()).Returns(items);
            _itemsRepo.Setup(m => m.GetItems()).Returns(Task.FromResult(items));
        }

        private List<Item> GetItemsTestData()
        {
            return new List<Item>() 
            {
                new() { Id = 1, Name=TITLE_NEWHOPE, Description = DESC_NEWHOPE, CategoryId = 2 },
                new() { Id = 2, Name=TITLE_EMPIRE, Description = DESC_EMPIRE, CategoryId = 2 },
                new Item() { Id = 3, Name=TITLE_RETURN, Description = DESC_RETURN, CategoryId = 2}
            };
        }

        [TestMethod]
        public async Task TestGetItems()
        {
            var result = await _itemsService.GetItems();
            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.Any()); //Assert.IsTrue(result.Count > 0);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(3);
            var expected = GetItemsTestData();
            var item1 = result[0];
            item1.Name.ShouldBe(TITLE_NEWHOPE);
            item1.Description.ShouldBe(DESC_NEWHOPE);
            var item2 = result[1];
            item2.Name.ShouldBe(expected[1].Name);
            item2.Description.ShouldBe(expected[1].Description);
        }
    }
}