using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeBills.Models
{
    public class Client
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string userID { get; set; }
        public string gender { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string race { get; set; }
        public string mstatus { get; set; }
        public long idno { get; set; }
        public int age { get; set; }

        public byte[] img { get; set; }
    }
}