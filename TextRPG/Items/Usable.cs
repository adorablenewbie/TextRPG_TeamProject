using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Item
{
    internal class Usable : Item
    {
        public bool IsUse;
        public float Value;

        public Usable(string name, string descript, float atk, float def, float price) {
            this.Name = name;
            this.Description = descript;
            this.Attack = atk;
            this.Defense = def;
            this.Price = price;
        }
    }
}
