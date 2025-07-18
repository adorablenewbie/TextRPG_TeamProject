using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Items
{
    public class Usable : Item
    {
        public float Value;

        public Usable(string name, string descript, float price, float value, ItemType type)
        {
            this.Name = name;
            this.Description = descript;
            this.Attack = 0;
            this.Defense = 0;
            this.Price = price;
            this.Value = value;
            this.Type = type;
        }

        public static List<Item> usableItemData = new List<Item>()
        {         //    이름,   설명,            가격, 효과, 타입 
            new Usable("빵", "체력이 찰 것 같다", 100, 10, ItemType.Potion),
            new Usable("물약", "체력이 많이 회복된다", 300, 30, ItemType.Potion),
            new Usable("만능포션", "상태이상을 해제한다", 200, 0, ItemType.Antidote),
            new Usable("마나포션", "마나가 회복된다", 250, 20, ItemType.ManaPotion),
            new Usable("폭열병", "적에게 피해를 준다(대)", 200, 20, ItemType.Throwing),
            new Usable("돌멩이", "적에게 피해를 준다(소)", 50,  5, ItemType.Throwing),
        };

        public Usable GetItem(Usable item) //독립저장용 복사본
        {
            return new Usable(item.Name, item.Description, item.Price, item.Value, item.Type);
        }

        public override void UseItem()
        {
            Console.WriteLine($"{this.Name} 을 사용하였습니다.");
            Player.Instance.Inventory.Remove(this);

            if (this.Type == ItemType.Potion)
            {
                Player.Instance.Hp += this.Value;
                if (Player.Instance.Hp > Player.Instance.MaxHP)
                    Player.Instance.Hp = Player.Instance.MaxHP;
                Console.WriteLine($"체력이 {this.Value}만큼 회복되었습니다!");
            }
            else if (this.Type == ItemType.Antidote)
            {
                Player.Instance.RemoveAllEffect();
                Console.WriteLine("모든 상태이상이 해제되었습니다!");
            }
            else if (this.Type == ItemType.ManaPotion)
            {
                Player.Instance.Mana += this.Value;
                if (Player.Instance.Mana > Player.Instance.MaxMana)
                    Player.Instance.Mana = Player.Instance.MaxMana;
                Console.WriteLine($"마나가 {this.Value}만큼 회복되었습니다!");
            }
        }

        public void UseItem(Monster targetMonster)
        {
            Console.WriteLine($"{this.Name} 을 사용하였습니다.");
            Player.Instance.Inventory.Remove(this);

            if (this.Type == ItemType.Throwing)
            {
                targetMonster.Hp -= this.Value;
                Console.WriteLine($"{targetMonster.Name}에게 {this.Value}의 피해를 입혔습니다!");
            }
        }

        public override void AddItem()
        {
            Player.Instance.Inventory.Add(GetItem(this));
        }
    }
}