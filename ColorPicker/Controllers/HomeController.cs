using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ColorPicker.Models;
using System.IO;
using Newtonsoft.Json;

namespace ColorPicker.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Color> Colors;
        public HomeController()
        {
            using (StreamReader r = new StreamReader("wwwroot/colors.json"))
            {
                string json = r.ReadToEnd();
                List<Color> colors = JsonConvert.DeserializeObject<List<Color>>(json).ToList();
                Colors = colors;
            }
        }
        public IActionResult Index(string colorGroup)
        {
            List<Color> colors = Colors;
            if (colorGroup != null)
            {
                colors = colors.Where(x => x.Name.Contains(colorGroup, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (colors.Count == 0)
                {
                    colors = Colors;
                }
            }
            var VM = new ListViewModel
            {
                ColorGroup = colorGroup,
                Colors = colors
            };
            return View(VM);
        }

        public IActionResult Detail(string hexCode)
        {
            Color color = Colors.Where(x => x.HexCode.Equals(hexCode, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (color == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<string> tints = new List<string> {
                ColorShader(hexCode, (float).2),
                ColorShader(hexCode, (float).1),
                ColorShader(hexCode, (float)0),
                ColorShader(hexCode, (float)-.1),
                ColorShader(hexCode, (float)-.2)
            };
            var VM = new DetailViewModel
            {
                Color = color,
                Tints = tints
            };
            return View(VM);
        }

        public IActionResult Random()
        {
            Random rnd = new Random();
            int idx = rnd.Next(Colors.Count);
            return RedirectToAction("Detail", "Home", new { hexCode = Colors[idx].HexCode });

        }

        private string ColorShader(string hex, float shade)
        {
            hex = hex.Replace("#", "");
            // convert to decimal
            string newHexCode = ""; int d; string c; 
            for (int i = 0; i < 3; i++)
            {
                d = Convert.ToInt32(hex.Substring(i * 2, 2), 16);
                c = Math.Min(Math.Max(0, Convert.ToInt32(Math.Round(d + ((float)d * shade)))), 255).ToString("X");
                if (c.Length == 1)
                {
                    c = "0" + c;
                } 
                newHexCode += c;
            }

            return "#" + newHexCode;
        }

        private int parseInt(string v1, int v2)
        {
            throw new NotImplementedException();
        }

        public List<Color> Search(string term)
        {
            List<Color> matches = Colors.Where(x => x.Name.Contains(term, StringComparison.InvariantCultureIgnoreCase) || x.HexCode.Contains(term, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return matches;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
