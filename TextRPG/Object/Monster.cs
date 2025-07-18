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
        public DungeonType DungeonType { get; set; } // 몬스터가 등장하는 던전 종류

        public static List<Monster> monstersData = new List<Monster>()
        {
            new Monster(1 ,"슬라임", 10, 2, 0, 10, 10, DungeonType.Forest),
            new Monster(1 ,"고블린", 20, 3, 1, 15, 15, DungeonType.Forest),
            new Monster(3 ,"산적", 25, 3, 2, 25, 25, DungeonType.Forest),
            new Monster(4 ,"늑대인간", 30, 4, 2, 30, 30, DungeonType.Forest),
            new Monster(2 ,"박쥐", 35, 5, 2, 40, 40, DungeonType.Cave),
            new Monster(1 ,"좀비", 50, 5, 3, 50, 50, DungeonType.Cave),
            new Monster(2 ,"스켈레톤", 60, 6, 3, 60, 60, DungeonType.Cave),
            new Monster(4 ,"오크", 75, 8, 4, 75, 75, DungeonType.Cave),
            new Monster(3 ,"미믹", 80, 10, 5, 80, 80, DungeonType.Castle),
            new Monster(5 ,"뱀파이어", 100, 10, 5, 100, 100, DungeonType.Castle),
            new Monster(7 ,"흑마법사", 120, 12, 5, 120, 120, DungeonType.Castle),
            new Monster(8 ,"흑기사", 150, 15, 7, 150, 150, DungeonType.Castle),
            new Monster(20 ,"흑염룡", 500, 25, 10, 300, 300, DungeonType.DragonLair)
        };

        public Monster Clone()
        {
            return new Monster(this.Level, this.Name, this.Hp, this.BaseAttack, this.BaseDefense, this.Gold, this.Exp, this.DungeonType);
        }

        public Monster(int level, string name, float hp, float attack, float defense, float gold, float exp, DungeonType dungeonType)
        {
            this.Level = level;
            this.Name = name;
            this.Hp = hp;
            this.BaseAttack = attack;
            this.BaseDefense = defense;
            this.Gold = gold;
            this.Exp = exp;
            this.DungeonType = dungeonType;
            this.Inventory = new List<Item>();
        }
    }
}
