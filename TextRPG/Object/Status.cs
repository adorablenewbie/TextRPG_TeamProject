using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Player
{
    public abstract class Status
    {
        public string name {  get; set; }
        public string playerClass { get; set; }
        public float hp { get; set; }
        public float attack { get; set; }
        public float defense { get; set; }
        public float gold { get; set; }
        public float exp { get; set; }
    }
}
