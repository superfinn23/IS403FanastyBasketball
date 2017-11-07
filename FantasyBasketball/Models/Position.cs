using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FantasyBasketball.Models
{
    [Table("Position")]
    public class Position
    {
        [Key]
        public String positionCode { get; set; }
        public String positionDescription { get; set; }
    }
}