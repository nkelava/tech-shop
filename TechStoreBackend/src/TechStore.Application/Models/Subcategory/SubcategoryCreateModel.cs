﻿

namespace TechStore.Application.Models.Subcategory
{
    public class SubcategoryCreateModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public IList<SubcategoryPropertyModel> Properties { get; set; }
    }
}
