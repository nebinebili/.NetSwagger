using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace UdemySwagger.Models
{
    public partial class Product
    {

        public int Id { get; set; }

        /// <summary>
        /// Mehsulun qiymeti
        /// </summary>
        [Required]
        public decimal? Price { get; set; }

        /// <summary>
        /// Mehsulun adi
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Mehsulun kategorisi
        /// </summary>
        public string Category { get; set; }


        public DateTime? Date { get; set; }
    }
}
