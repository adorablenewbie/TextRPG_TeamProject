using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.SaveDatas
{
    [Flags]
    public enum Effects
    {
        None = 0,
        Poison = 1 << 0, //0000 0001
        Sleep = 1 << 1, //0000 0010
        Freeze = 1 << 2, //0000 0100
        Stun = 1 << 3, //0000 1000
        Burn = 1 << 4, //0001 0000
    }
    internal class Effect
    {
        
    }
}
