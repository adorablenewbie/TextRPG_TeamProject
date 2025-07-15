using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.SaveDatas
{
    public class Skill
    {
        public int ID { get; set; } // 스킬 ID
        public string Name { get; set; }
        public string Description { get; set; }
        public float RequiredMana { get; set; }
        public float AttackValue { get; set; } // 공격력 곱연산
        public float DefenseValue { get; set; } // 방어력 합연산
        public float HealValue { get; set; }
        public Effects Effect { get; set; } // 스킬 상태이상
        public int Level { get; set; } // 스킬 레벨
        public int RequiredLevel { get; set; } // 스킬 사용에 필요한 최소 레벨
        public bool IsEquipped { get; set; } // 스킬이 장착되어 있는지 여부
        public Skill()
        {
            //일단 보류
        }
        public Skill(int id, string name, string description, float requiredMana, float attackValue, float defenseValue, float healValue, Effects effect, int level, int requiredLevel, bool isEquipped)
        {
            ID = id;
            Name = name;
            Description = description;
            RequiredMana = requiredMana;
            AttackValue = attackValue;
            DefenseValue = defenseValue;
            HealValue = healValue;
            Effect = effect;
            Level = level;
            RequiredLevel = requiredLevel;
            IsEquipped = isEquipped;
        }
        public void Equip()
        {
            IsEquipped = true;
        }
        public void Unequip()
        {
            IsEquipped = false;
        }
        public bool CanUse(int playerLevel, float playerMana)
        {
            if (playerLevel < RequiredLevel || playerMana < RequiredMana)
                return false;
            else
                return true;
        }
        public override string ToString()
        {
            return $"{Name} - 필요마나: {RequiredMana} - {(AttackValue == 0 ? "" : $"공격력: {AttackValue * Old.GameManager.player.Attack}")}" +
                   $"{(DefenseValue == 0 ? "" : $"방어력: {DefenseValue}")} - {(HealValue == 0 ? "" : $"치유력: {HealValue}")}" +
                   $"레벨: {Level}, 필요 레벨: {RequiredLevel}, 장착 여부: {IsEquipped} {Description}상태이상 효과: {(Effect == Effects.None ? "없음" : Effect)}";
        }
        public static List<Skill> GetDefaultSkills()
        {
            return new List<Skill>
            {
                new Skill
                {
                ID = 0,
                Name = "강력한 공격",
                Description = "기본 공격 스킬입니다.",
                RequiredMana = 5,
                AttackValue = ,
                DefenseValue = 0,
                HealValue = 0,
                Effect = Effects.None,
                Level = 1,
                RequiredLevel = 1,
                IsEquipped = true
                },
                new Skill
                {
                    ID = 1,
                    Name = "방어 자세",
                    Description = "방어력을 증가시키는 스킬입니다.",
                    RequiredMana = 5,
                    AttackValue = 0,
                    DefenseValue = 15,
                    HealValue = 0,
                    Effect = Effects.None,
                    Level = 1,
                    RequiredLevel = 1,
                    IsEquipped = false
                },
                new Skill
                {
                    ID = 2,
                    Name = "치유",
                    Description = "아군을 치유하는 스킬입니다.",
                    RequiredMana = 10,
                    AttackValue = 0,
                    DefenseValue = 0,
                    HealValue = 20,
                    Effect = Effects.None,
                    Level = 1,
                    RequiredLevel = 1,
                    IsEquipped = false
                }
            };
        }
        public static List<Skill> GetSkillsByType(List<Skill> skills, int type)
        {
            return skills.Where(skill => skill.Type == type).ToList();
        }
        public static List<Skill> GetEquippedSkills(List<Skill> skills)
        {
            return skills.Where(skill => skill.IsEquipped).ToList();
        }
        public static List<Skill> GetUnequippedSkills(List<Skill> skills)
        {
            return skills.Where(skill => !skill.IsEquipped).ToList();
        }
    }
}
