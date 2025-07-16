using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    public class Monster : Status
    {
        public static List<Monster> monstersData = new List<Monster>()
        {
            new Monster("쥐", 10, 4),
        };

        public Monster Clone()
        {
            return new Monster(this.Name, this.Hp, this.Attack, this.Defense, this.Gold, this.Exp);
        }

        public Monster(string name, float hp, float attack, float defense = 0, float gold = 0, float exp = 0)
        {
            this.Name = name;
            this.Hp = hp;
            this.Attack = attack;
            this.Defense = defense;
            this.Gold = gold;
            this.Exp = exp;
            this.Inventory = new List<Item>();
        }
    }

    //public class MonsterManager
    //{
    //    public Monster[] Monsters = new Monster[5];

    //    public MonsterManager()
    //    {                                     //hp,공격,방어,골드,EXP
    //        Monsters[0] = new Monster("고블린",  30, 2, 1, 10, 10);
    //        Monsters[1] = new Monster("슬라임",  20, 4, 0, 20, 20);
    //        Monsters[2] = new Monster("스켈레톤", 40, 6, 0, 30, 30);
    //        Monsters[3] = new Monster("오크",    50, 8, 3, 50, 50);
    //        Monsters[4] = new Monster("드래곤",  200, 20, 5, 100, 500); 
    //    }
    //}
}
