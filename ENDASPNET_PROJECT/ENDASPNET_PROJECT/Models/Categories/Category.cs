using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ENDASPNET_PROJECT.Models.Categories
{
    public class Category
    {
        [Display(Name = "Category id")]
        public int categoryId { get; set; }

        [Display(Name = "Category name")]
        public string categoryName { get; set; }
    }
}