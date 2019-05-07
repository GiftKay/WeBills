using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeBills.Models
{
    public class Residence
    {
        [Key]
        public int homeid { get; set; }
        [Required]
        [Display(Name = "City/Town")]
        public string citytown { get; set; }
        [Required]
        [Display(Name = "Surbub")]
        public string surbub { get; set; }
        [Required]
        [Display(Name = "Street Name")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public int code { get; set; }
        public byte[] img { get; set; }
        public virtual ApplicationUser users { get; set; }
        public string Id { get; set; }
        public virtual bills bil { get; set; }


    }
    public class bills
    {
        [Key]
        public int billid { get; set; }
        [Required]
        [Display(Name = "Home ID")]
        public int homeid { get; set; }
        [Display(Name = "Water Cost")]
        public double waterCost { get; set; }
        [Display(Name = "Electricity Cost")]
        public double electricCost { get; set; }
        public IEnumerable<Residence> res { get; set; }

    }
}