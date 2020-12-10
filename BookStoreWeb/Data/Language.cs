using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace BookStoreWeb.Data
{
    public class Language
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public  ICollection<Books> Books { get; set; }

    }
}