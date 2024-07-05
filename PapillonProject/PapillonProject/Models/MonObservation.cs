using System;
using System.Collections.Generic;
using System.Text;

namespace PapillonProject.Models
{
    public class MonObservation
    {
        public string Releve { get; set; }
        public string ButterflyId { get; set; }
        public string Coordinates { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public TypePapillon Papillon { get; set; } 
    }
}
