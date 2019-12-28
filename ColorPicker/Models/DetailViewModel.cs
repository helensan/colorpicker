using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPicker.Models
{
    public class DetailViewModel
    {
        public Color Color { get; set; }
        public List<string> Tints { get; set; }
    }
}
