using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPicker.Models
{
    public class ListViewModel
    {
        public string ColorGroup { get; set; }
        public List<Color> Colors { get; set; }
    }
}
