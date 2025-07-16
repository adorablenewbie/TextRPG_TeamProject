using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.Items
{
    internal class Equipable : Item
    {
        public bool isEquipped;

        public static List<Item> equipedItemData = new List<Item>()
        {
            new Equipable("갑옷", "대체 이 갑옷은 뭐냐!", 0, 10, 1000, ItemType.Armor),
        };

        public Equipable(string name, string descript, float atk, float def, float price, ItemType type)
        {
            this.Name = name;
            this.Description = descript;
            this.Attack = atk;
            this.Defense = def;
            this.Price = price;
            this.Type = type;
        }

        public override void UseItem(Item item)
        {
            if (item.Type == ItemType.Weapon)
            {
                this.isEquipped = !this.isEquipped;
                if (this.isEquipped) {
                    Player.Instance.Attack += item.Attack;
                }
                //Player.Instance.UpdateStatus();
            }
        }
    }
}
