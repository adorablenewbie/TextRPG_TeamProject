using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;


namespace TextRPG.Items
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        Antidote,
        ManaPotion,
        Throwing
    }

    //public class ItemData
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public float Attack { get; set; }
    //    public float Defense { get; set; }
    //    public float Price { get; set; }
    //    public ItemType Type { get; set; }

    //    public virtual bool IsEquipped { get; set; } = false;
    //}
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Equipable), "equipable")]
    [JsonDerivedType(typeof(Usable), "usable")]
    public abstract class Item
    {
        //public ItemData itemData;
        public string Name { get; set; }
        public string Description { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Price { get; set; }
        public ItemType Type { get; set; }

        public virtual bool IsEquipped { get; set; } = false;
        public abstract void UseItem();
        public abstract void AddItem();
        public virtual void RemoveItem() 
        {
            Player.Instance.Inventory.Remove(this);
        }
    }
}
