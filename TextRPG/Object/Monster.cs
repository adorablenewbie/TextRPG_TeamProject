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
            new Monster("슬라임", 10, 2, 0, 10, 10, DungeonType.Forest),
            new Monster("고블린", 20, 3, 1, 15, 15, DungeonType.Forest),
            new Monster("늑대", 30, 4, 2, 25, 25, DungeonType.Forest),
            new Monster("스켈레톤", 40, 5, 2, 40, 40, DungeonType.Cave),
            new Monster("좀비", 50, 5, 3, 50, 50, DungeonType.Cave),
            new Monster("오크", 75, 8, 4, 70, 70, DungeonType.Cave),
            new Monster("미믹", 80, 10, 5, 90, 90, DungeonType.Castle),
            new Monster("흑마법사", 100, 12, 5, 100, 100, DungeonType.Castle),
            new Monster("흑염룡", 500, 25, 10, 300, 300, DungeonType.DragonLair),
        };

        public Monster Clone()
        {
            return new Monster(this.Name, this.Hp, this.BaseAttack, this.BaseDefense, this.Gold, this.Exp, this.DungeonType);
        }

        public Monster(string name, float hp, float attack, float defense, float gold, float exp, DungeonType dungeonType)
        {
            this.Name = name;
            this.Hp = hp;
            this.BaseAttack = attack;
            this.BaseDefense = defense;
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
    //        Monsters[0] = new Monster("고블린", 30, 2, 1, 10, 10);
    //        Monsters[1] = new Monster("슬라임", 20, 4, 0, 20, 20);
    //        Monsters[2] = new Monster("스켈레톤", 40, 6, 0, 30, 30);
    //        Monsters[3] = new Monster("오크", 50, 8, 3, 50, 50);
    //        Monsters[4] = new Monster("드래곤", 200, 20, 5, 100, 500);
    //    }
    //}
}
