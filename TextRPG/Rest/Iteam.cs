using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum ItemType
    {
        Armor,
        Weapon
    }
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Defense { get; set; }
        public float Attack { get; set; }
        public float Price { get; set; }
        public ItemType Type { get; set; }

        public Item(string name, string description, float defense, float attack, float price, ItemType itemType)
        {
            Name = name;
            Description = description;
            Defense = defense;
            Attack = attack;
            Price = price;
            Type = itemType;
        }

        // 대표 아이템: 무쇠갑옷
        public static Item NoviceArmor =>
            new Item(
                "수련자의 갑옷",
                "수련에 도움을 주는 갑옷입니다.",
                defense: 5,
                attack: 0,
                price: 100,
                itemType: ItemType.Armor
            );
        public static Item IronArmor =>
            new Item(
                "무쇠갑옷",
                "튼튼한 무쇠로 만들어진 갑옷. 방어력이 증가합니다.",
                defense: 9,
                attack: 0,
                price: 100,
                itemType: ItemType.Armor
            );
        public static Item SpartanArmor =>
            new Item(
                "스파르타의 갑옷",
                "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.",
                defense: 15,
                attack: 0,
                price: 100,
                itemType: ItemType.Armor
            );
        public static Item LegendArmor =>
        new Item(
            "뎐덜의 갑옷",
            "뎐덜의 레던드 답옷.",
            defense: 20,
            attack: 3,
            price: 100,
            itemType: ItemType.Armor
        );
        public static Item OldSword =>
            new Item(
                "낡은 검",
                "쉽게 볼 수 있는 낡은 검 입니다.",
                defense: 0,
                attack: 2,
                price: 100,
                itemType: ItemType.Weapon
            );
        public static Item BronzeAxe =>
            new Item(
                "청동 도끼",
                "어디선가 사용됐던거 같은 도끼입니다.",
                defense: 0,
                attack: 5,
                price: 100,
                itemType: ItemType.Weapon
            );
        public static Item SpartanSpear =>
            new Item(
                "스파르타의 창",
                "스파르타의 전사들이 사용했다는 전설의 창입니다.",
                defense: 0,
                attack: 7,
                price: 100,
                itemType: ItemType.Weapon
            );

        public static Item LegendSpear =>
            new Item(
                "뎐덜의 창",
                "뎐덜의 레던드 탕.",
                defense: 2,
                attack: 9,
                price: 100,
                itemType: ItemType.Weapon
            );

        public override string ToString()
        {
            return $"{Name} (타입: {Type}, 방어력 +{Defense}, 공격력 +{Attack} 가격: {Price}G) - {Description}";
        }

    }
}