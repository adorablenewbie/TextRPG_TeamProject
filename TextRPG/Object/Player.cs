using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    internal class Player : Status
    {
        public Player(string name, string playerClass, float hp, float attack, float defense, float gold, float exp)
        {
            this.name = name;
            this.playerClass = playerClass;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.gold = gold;
            this.exp = exp;
        }
    }
}
