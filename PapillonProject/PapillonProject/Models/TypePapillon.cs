using System;
using System.Collections.Generic;
using System.Text;

namespace PapillonProject.Models
{
    public class TypePapillon
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Texte { get; set; }
        public Uri Image { get; set; }
    }
}
