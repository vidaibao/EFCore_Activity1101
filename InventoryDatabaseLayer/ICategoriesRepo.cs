﻿using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDatabaseLayer
{
    public interface ICategoriesRepo
    {
        Task<List<CategoryDto>> ListCategoriesAndDetails();
        //List<CategoryDto> ListCategoriesAndDetails();
    }
}
