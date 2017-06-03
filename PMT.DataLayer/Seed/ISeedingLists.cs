﻿using System.Collections.Generic;
using PMT.Entities;

namespace PMT.DataLayer.Seed
{
    public interface ISeedingLists
    {
        List<Icon> GetIcons();
        List<Category> GetMainCategories();
        List<Repeat> GetRepeats();
        List<SubCategory> GetSubCategries();
        void PrepareIcons();
    }
}