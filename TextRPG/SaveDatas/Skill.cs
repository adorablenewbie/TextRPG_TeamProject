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
        public int AttackValue { get; set; }
        public int DefenseValue { get; set; }
        public int HealValue { get; set; }

        public int Level { get; set; } // 스킬 레벨
        public int RequiredLevel { get; set; } // 스킬 사용에 필요한 최소 레벨
        public bool IsEquipped { get; set; } // 스킬이 장착되어 있는지 여부
        public Skill()
        {
            RequiredItems = new List<int>();
        }
        public Skill(int id, string name, string description, int type, int value, int level, int requiredLevel, int cooldown)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Value = value;
            Level = level;
            RequiredLevel = requiredLevel;
            Cooldown = cooldown;
            IsEquipped = false; // 기본값은 장착되지 않음
            RequiredItems = new List<int>();
        }
        public void Equip()
        {
            IsEquipped = true;
        }
        public void Unequip()
        {
            IsEquipped = false;
        }
        public bool CanUse(int playerLevel, List<int> playerItems)
        {
            if (playerLevel < RequiredLevel)
                return false;
            foreach (var itemId in RequiredItems)
            {
                if (!playerItems.Contains(itemId))
                    return false;
            }
            return true;
        }
        public override string ToString()
        {
            return $"{Name} (Type: {(Type == 0 ? "Attack" : "Defense")}, Value: {Value}, Level: {Level}, Required Level: {RequiredLevel}, Cooldown: {Cooldown}s, Equipped: {IsEquipped})";
        }
        public static Skill CreateDefaultSkill()
        {
            return new Skill
            {
                Id = 0,
                Name = "기본 스킬",
                Description = "기본 공격 스킬입니다.",
                Type = 0, // 공격형
                Value = 10,
                Level = 1,
                RequiredLevel = 1,
                Cooldown = 5,
                IsEquipped = false,
                RequiredItems = new List<int>() // 기본 스킬은 아이템 필요 없음
            };
        }
        public static List<Skill> GetDefaultSkills()
        {
            return new List<Skill>
            {
                CreateDefaultSkill(),
                new Skill(1, "방어 스킬", "기본 방어 스킬입니다.", 1, 5, 1, 1, 10),
                new Skill(2, "강력한 공격", "강력한 공격을 가하는 스킬입니다.", 0, 20, 1, 5, 15)
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
