﻿using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLayer
{
    public interface ICategoriesService
    {
        Task<List<CategoryDto>> ListCategoriesAndDetails();
    }
}
