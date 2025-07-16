using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.Items
{
    internal class Usable : Item
    {
        public float Value;

        public Usable(string name, string descript, float price , float value, ItemType type) {
            this.Name = name;
            this.Description = descript;
            this.Attack = 0;
            this.Defense = 0;
            this.Price = price;
            this.Value = value;
            this.Type = type;
        }

        public static List<Item> usableItemData = new List<Item>()
        {
            new Usable("제하하하하하", "하하하하", 100, 10, ItemType.Potion),
        };

        public Usable GetItem(Usable item)
        {
            return new Usable(item.Name, item.Description, item.Price, item.Value, item.Type);
        }

        public override void UseItem()
        {
            Console.WriteLine($"{this.Name} 을 사용하였습니다.");
            Player.Instance.Inventory.Remove(this);
            if (this.Name == "")
            {
                //일단 이렇게 둘듯?
            }
        }
    }
}
