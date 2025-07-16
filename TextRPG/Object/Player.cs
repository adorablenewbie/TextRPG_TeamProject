using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    public class Player : Status
    {
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }
        public Player()
        {
            this.Hp = 100f;
            this.Mana = 50f;
            this.Level = 1;
            this.Attack = 10;
            this.Defense = 5;
            this.Gold = 1500;
            this.Exp = 0;
            this.Skills = new List<Skill>();
            this.EquippedSkills = new List<Skill>();
            this.Inventory = new List<Item>();
        }

        public void EquipSkill(Skill selectedSkill)
        {
            EquippedSkills.Add(selectedSkill);
            selectedSkill.IsEquipped = true; 
            Console.WriteLine($"{selectedSkill.Name} 스킬이 장착되었습니다.");
            System.Threading.Thread.Sleep(1000); // 1초 대기

            if (EquippedSkills.Count > 2)
            {
                Console.WriteLine("장착된 스킬이 2개를 초과했습니다. 가장 오래된 스킬을 해제합니다.");
                EquippedSkills.RemoveAt(0); // 가장 오래된 스킬 해제
            }
        }
        public void UnequipSkill(Skill selectedSkill)
        {
            if (EquippedSkills.Contains(selectedSkill))
            {
                EquippedSkills.Remove(selectedSkill);
                Console.WriteLine($"{selectedSkill.Name} 스킬이 해제되었습니다.");
                selectedSkill.IsEquipped = false; // 스킬 장착 상태 해제
            }
            else
            {
                Console.WriteLine("해제할 수 없는 스킬입니다.");
            }
            System.Threading.Thread.Sleep(1000); // 1초 대기
        }

        public void AddItem(Item item)
        {
            if(item.Type == ItemType.Potion)
            {
                Usable useItem = (Usable)item;
                this.Inventory.Add(useItem.GetItem((Usable)item));
            }
            else
            {
                this.Inventory.Add(item);
                Shop.shopItems.Remove(item);
            }
        }

        public void RemoveItem(Item item)
        {
            this.Inventory.Remove(item);
        }
    }
}