using AutoMapper;
using EFCore_DBLibrary;
using InventoryDatabaseLayer;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLayer
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepo _dpRepo;

        public CategoriesService(InventoryDbContext dbContext, IMapper mapper)
        {
            _dpRepo = new CategoriesRepo(dbContext, mapper);
        }

        public List<CategoryDto> ListCategoriesAndDetails()
        {
            return _dpRepo.ListCategoriesAndDetails();
        }
    }
}
