using System;
using System.Collections;
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
        public float NextExp = 10;
        public Player()
        {
            this.MaxHP = 100f;
            this.Hp = MaxHP;
            this.MaxMana = 50f;
            this.Mana = MaxMana;
            this.Level = 1;
            this.BaseAttack = 10;
            this.BaseDefense = 5;
            this.Gold = 1500;
            this.Exp = 0;
            this.Skills = new List<Skill>();
            this.EquippedSkills = new List<Skill>();
            this.Inventory = new List<Item>();
        }


        public void ArmedSkill(Skill selectedSkill)
        {
            if (selectedSkill.IsEquipped)
            {
                //해제
                EquippedSkills.Remove(selectedSkill);
                Console.WriteLine("스킬이 해제되었습니다.");
                selectedSkill.IsEquipped = false;
            }
            else
            {
                //장착
                EquippedSkills.Add(selectedSkill);
                Console.WriteLine($"{selectedSkill.Name} 스킬을 장착하였습니다.");
                selectedSkill.IsEquipped = true;

                if(EquippedSkills.Count > 2)
                {
                    Console.WriteLine("장착된 스킬이 2개를 초과했습니다. 가장 오래된 스킬을 해제합니다.");
                    EquippedSkills[0].IsEquipped = false;
                    EquippedSkills.RemoveAt(0); // 가장 오래된 스킬 해제\
                }
            }
        }
        public void UpdateStatus()
        {
            AddAttack = 0;
            AddDefence = 0;
            foreach (var item in Inventory)
            {
                if (item.IsEquipped)
                {
                    AddAttack += item.Attack;
                    AddDefence += item.Defense;
                }
            }
        }

        public void RestoreHealth()
        {
            float restoreHealth = MaxHP - Hp;
            Hp += restoreHealth;
        }

        public void LevelUpCheck()
        {
            if (Exp < NextExp) return;

            Exp -= NextExp;
            Level++;
            NextExp *= 1.5f; 
            float growth = (float)Level * 0.5f;

            MaxHP += 10f + growth;       // 기본 HP 증가 + 성장 비례
            BaseAttack += growth;           // 공격력 증가
            BaseDefense += growth;          // 방어력 증가
            Hp = MaxHP;                     // 체력 전부 회복

            // 출력
            Console.WriteLine($"== 레벨 업! 현재 레벨: {Level} ==");
            Console.WriteLine($"→ HP: {MaxHP}, 공격력: {TotalAttack}, 방어력: {TotalDefence}");
        }
        public void Initialize()
        {
            if (Inventory == null)
                Inventory = new List<Item>();
            if (EquippedSkills == null)
                EquippedSkills = new List<Skill>();
            if (Skills == null)
                Skills = new List<Skill>();
            if (EquippedWeapon == null)
                EquippedWeapon = Inventory.Find(item => item.Name == EquippedWeapon.Name);
            if (EquippedArmor == null)
                    EquippedArmor = Inventory.Find(item => item.Name == EquippedArmor.Name);
        }
    }
}