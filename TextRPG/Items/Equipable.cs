using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Items
{
    public class Equipable : Item
    {
        public override bool IsEquipped { get; set; }

        public static List<Item> equipedItemData = new List<Item>()
        {
            new Equipable("갑옷", "대체 이 갑옷은 뭐냐!", 0, 10, 1000, ItemType.Armor),
        };

        public Equipable() { }
        public Equipable(string name, string descript, float atk, float def, float price, ItemType type)
        {
            this.Name = name;
            this.Description = descript;
            this.Attack = atk;
            this.Defense = def;
            this.Price = price;
            this.Type = type;
        }

        public override void UseItem()
        {
            if (!Player.Instance.Inventory.Contains(this)) return;

            if (this.Type == ItemType.Weapon)
            {
                if (Player.Instance.EquippedWeapon != null)
                {
                    this.IsEquipped = false;
                }

                if (Player.Instance.EquippedWeapon == this)
                {
                    Player.Instance.EquippedWeapon = null;
                    this.IsEquipped = false;
                }
                else
                {
                    Player.Instance.EquippedWeapon = this;
                    this.IsEquipped = true;
                }
            }
            else if (this.Type == ItemType.Armor)
            {
                if (Player.Instance.EquippedArmor != null)
                {
                    this.IsEquipped = false;
                }

                if (Player.Instance.EquippedArmor == this)
                {
                    Player.Instance.EquippedArmor = null;
                    this.IsEquipped = false;
                }
                else
                {
                    Player.Instance.EquippedArmor = this;
                    this.IsEquipped = true;
                }
            }
            Player.Instance.UpdateStatus();
        }

        public override void AddItem()
        {
            Player.Instance.Inventory.Add(this);
            Shop.shopItems.Remove(this);
        }

        public override void RemoveItem() 
        {
            Shop.shopItems.Add(this);
            base.RemoveItem();
        }
    }
}
