using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
    public class AdressModel
    {
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int AdTypeId { get; set; }
        public int UserId { get; set; }
    }
}
