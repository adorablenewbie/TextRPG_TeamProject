using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Items
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion
    }

    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Attack {  get; set; }
        public float Defense { get; set; }
        public float Price { get; set; }
        public ItemType Type { get; set; }

        public virtual bool IsEquipped {get; set;} = false;
        public abstract void UseItem();
    }
}
