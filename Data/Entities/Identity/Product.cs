﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Identity
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public decimal Price { get; set; }

        [StringLength(255)]
        public string Photo { get; set; }
        public virtual ICollection<CartEntity> CartEntities { get; set; }
    }
}
