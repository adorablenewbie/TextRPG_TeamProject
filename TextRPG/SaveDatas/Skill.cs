using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        //public bool IsEquipped { get; set; } // 스킬이 장착되어 있는지 여부
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
        public bool CanUse(int playerLevel, float playerMana)
        {
            if (playerLevel < RequiredLevel || playerMana < RequiredMana)
                return false;
            else
                return true;
        }
        public override string ToString()
        {
            return $"{Name} / 필요마나: {RequiredMana} / {(AttackValue == 0 ? "" : $"공격력: {AttackValue * player.BaseAttack}")} / " +
                   $"{(DefenseValue == 0 ? "" : $"방어력: {DefenseValue}")} / {(HealValue == 0 ? "" : $"치유력: {HealValue}")} / 상태이상 효과: {(Effect == Effects.None ? "없음" : Effect)} / " +
                   $"필요 레벨: {RequiredLevel} / {Description}";
        }
        public void UseSkill(Skill skill, Status target)
        {
            if (skill.CanUse(player.Level, player.Mana))
            {

                player.Mana -= skill.RequiredMana;
                float skillAttack = skill.AttackValue * player.BaseAttack; // 공격력 곱연산
                target.Hp -= skillAttack; // 대상에게 피해
                target.BaseDefense += skill.DefenseValue; // 방어력 합연산
                target.Hp += skill.HealValue; // 치유
                if (target.Hp >= target.MaxHP)
                {
                    target.Hp = target.MaxHP;
                }
                // 상태이상 효과 적용 로직 추가 가능
                Console.WriteLine($"{player.Name}이(가) {skill.Name}을(를) 시전!");
            }
            else
            {
                Console.WriteLine($"{player.Name}은(는) {skill.Name}을(를) 사용할 수 없습니다.");
            }
        }
        public static Skill Slam =>
           new Skill(
               id: 0,
               name: "강타",
               description: "적에게 강력한 일격을 날립니다. (공격력*2)",
               requiredMana: 10,
               attackValue: 2.0f, // 공격력 곱연산
               defenseValue: 0.0f, // 방어력 합연산
               healValue: 0.0f,
               effect: Effects.None,
               requiredLevel: 1,
               isEquipped: false
           );

        public static Skill Barrier =>
            new Skill(
                id: 1,
                name: "방어막",
                description: "자신에게 방어막을 감싸 피해를 흡수합니다. (방어력+3)",
                requiredMana: 10,
                attackValue: 0,
                defenseValue: 3.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.None,
                requiredLevel: 1,
                isEquipped: false
            );
        public static Skill Heal =>
            new Skill(
                id: 2,
                name: "치유",
                description: "자신의 체력을 회복합니다. (체력+20)",
                requiredMana: 10,
                attackValue: 0,
                defenseValue: 0,
                healValue: 20f,
                effect: Effects.None,
                requiredLevel: 1,
                isEquipped: false
            );
        public static Skill Bomb =>
            new Skill(
                id: 3,
                name: "폭탄 투척",
                description: "적에게 폭탄을 던집니다. 폭탄에 맞은 적은 화상 상태가 됩니다. (공격력*3)\n※화상: 적에게 2 고정 피해를 줍니다.",
                requiredMana: 15,
                attackValue: 3.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.Burn,
                requiredLevel: 2,
                isEquipped: false
            );
        public static Skill Stinger =>
            new Skill(
                id: 4,
                name: "독침",
                description: "적에게 독침을 발사해 피해를 줍니다. 독침에 맞은 적은 중독 상태가 됩니다.\n※중독: 적에게 5 고정 피해를 줍니다.",
                requiredMana: 15,
                attackValue: 0.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.Poison,
                requiredLevel: 2,
                isEquipped: false
            );
        public static Skill Lightning =>
            new Skill(
                id: 5,
                name: "번개",
                description: "적에게 벼락을 떨어뜨려 피해를 줍니다. 벼락에 맞은 적은 기절 상태가 됩니다. (공격력*4)\n※기절: 적이 1턴 동안 어떤 행동도 할 수 없습니다.",
                requiredMana: 20,
                attackValue: 4.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.Stun,
                requiredLevel: 3,
                isEquipped: false
            );
        public static Skill Hypnosis =>
            new Skill(
                id: 6,
                name: "최면술",
                description: "적에게 졸음을 유도하는 마법을 겁니다. 마법에 걸린 적은 잠듦 상태가 됩니다.\n※잠듦: 적이 3턴 동안 어떤 행동도 할 수 없습니다.",
                requiredMana: 30,
                attackValue: 0.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.Sleep,
                requiredLevel: 4,
                isEquipped: false
            );
        public static Skill HealingWind =>
            new Skill(
                id: 7,
                name: "치유의 바람",
                description: "바람의 힘으로 자신의 체력을 회복하고 방어력을 올립니다. (체력+200, 방어력+5)",
                requiredMana: 40,
                attackValue: 0.0f, // 공격력 곱연산
                defenseValue: 5.0f, // 방어력 합연산
                healValue: 200f,
                effect: Effects.None,
                requiredLevel: 5,
                isEquipped: false
            );
        public static Skill DeathBlow =>
            new Skill(
                id: 8,
                name: "치명적인 일격",
                description: "적에게 치명적인 피해를 줍니다. (공격력*10)",
                requiredMana: 50,
                attackValue: 10.0f, // 공격력 곱연산
                defenseValue: 0.0f, // 방어력 합연산
                healValue: 0.0f,
                effect: Effects.None,
                requiredLevel: 6,
                isEquipped: false
            );
    }
}