using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeekShopping.ProductAPI.src.Models.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        public long Id { set; get; }
    }
}