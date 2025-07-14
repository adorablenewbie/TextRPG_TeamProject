using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Item
{
    internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Attack {  get; set; }
        public float Defense { get; set; }
        public float Price { get; set; }
    }
}
