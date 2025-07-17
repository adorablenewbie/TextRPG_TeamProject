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
            this.Mana = 50f;
            this.Level = 1;
            this.BaseAttack = 10;
            this.BaseDefense = 5;
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

        private void LevelUpCheck()
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
    }
}