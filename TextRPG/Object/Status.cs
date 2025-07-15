using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    public abstract class Status
    {
        private Effects currentEffect = Effects.None;

        public string name { get; set; }
        public string playerClass { get; set; }
        public float hp { get; set; }
        public float attack { get; set; }
        public float defense { get; set; }
        public float gold { get; set; }
        public float exp { get; set; }

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
            if (hp <= 0) return;

            if (HasEffect(Effects.Poison))
            {
                Console.WriteLine("독 상태이상");
                hp -= 2;
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