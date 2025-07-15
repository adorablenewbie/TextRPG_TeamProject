using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.SaveDatas
{
    public class Skill
    {
        public Player player = Player.Instance; // 플레이어 인스턴스

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
        public Skill(int id, string name, string description, float requiredMana, float attackValue, float defenseValue, float healValue, Effects effect, int requiredLevel, bool isEquipped)
        {
            ID = id;
            Name = name;
            Description = description;
            RequiredMana = requiredMana;
            AttackValue = attackValue;
            DefenseValue = defenseValue;
            HealValue = healValue;
            Effect = effect;
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
            return $"{Name} - 필요마나: {RequiredMana} - {(AttackValue == 0 ? "" : $"공격력: {AttackValue * player.attack}")} - " +
                   $"{(DefenseValue == 0 ? "" : $"방어력: {DefenseValue}")} - {(HealValue == 0 ? "" : $"치유력: {HealValue}")} - 상태이상 효과: {(Effect == Effects.None ? "없음" : Effect)} - " +
                   $"필요 레벨: {RequiredLevel} - {Description}";
        }
        public static Skill PowerAttack =>
            new Skill(
                id: 1,
                name: "강력한 공격",
                description: "적에게 강력한 공격을 가합니다.",
                requiredMana: 10,
                attackValue: 2.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.None,
                requiredLevel: 1,
                isEquipped: false
            );
    }
}
