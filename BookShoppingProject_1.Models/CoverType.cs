using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject_1.Models.ViewModels
{
    public class CoverType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
