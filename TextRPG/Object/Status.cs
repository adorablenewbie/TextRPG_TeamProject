using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    public abstract class Status
    {
        private Effects currentEffect = Effects.None;

        public string Name { get; set; }
        public int Level { get; set; }
        public float Mana { get; set; }
        public string PlayerClass { get; set; }
        public float Hp { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Gold { get; set; }
        public float Exp { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Skill> EquippedSkills { get; set; } = new List<Skill>();
        public List<Item> Inventory { get; set; }

        public void AddEffect(Effects effect)
        {
            currentEffect |= effect;
            //상태이상 부여
            Console.WriteLine($"{effect} 상태이상 적용"); //디버그용
        }
        public void RemoveEffect(Effects effect)
        {
            currentEffect &= ~effect;
            Console.WriteLine($"{effect} 상태이상 제거"); //디버그용
        }
        public void RemoveAllEffect()
        {
            currentEffect = Effects.None;
            Console.WriteLine("모든 상태이상 제거");
        }
        public bool HasEffect(Effects effect)
        {
            return currentEffect.HasFlag(effect);
        }

        public void ApplyEffect()
        {
            if (Hp <= 0) return;

            if (HasEffect(Effects.Poison))
            {
                Console.WriteLine("독 상태이상");
                Hp -= 2;
                return;
            }
            if (HasEffect(Effects.Sleep))
            {
                Console.WriteLine("수면 상태이상");
            }
            if (HasEffect(Effects.Stun))
            {
                Console.WriteLine("기절 상태이상");
            }
        }
    }
}