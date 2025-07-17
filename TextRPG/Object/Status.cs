using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.SaveDatas;

namespace TextRPG.Object
{
    public abstract class Status
    {
        private Dictionary<Effects, Effect> currentEffect = new();

        public string Name { get; set; }
        public int Level { get; set; }
        public float Mana { get; set; }
        public float MaxMana { get; set; }
        public string PlayerClass { get; set; }
        public float Hp { get; set; }
        public float MaxHP { get; set; }
        public float BaseAttack { get; set; }
        public float BaseDefense { get; set; }

        public float TotalAttack
        {
            get
            {
                return BaseAttack + AddAttack;
            }
        }
        public float TotalDefence
        {
            get
            {
                return BaseDefense + AddDefence;
            }
        }

        public float AddAttack {get; set;}
        public float AddDefence {get; set;}

        public float Gold { get; set; }
        public float Exp { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Skill> EquippedSkills { get; set; } = new List<Skill>();
        public List<Item> Inventory { get; set; }

        public void AddEffect(Effects effect, int duration)
        {
            if (currentEffect.ContainsKey(effect))
            {
                Effect plusEffect = currentEffect[effect];
                if (plusEffect.Duration < duration)
                {
                    plusEffect.Duration = duration; //상태 갱신
                }
            }
            else
            {
                Effect newEffect = new Effect(effect, duration);
                currentEffect.Add(effect, newEffect); //상태 추가
            }
        }

        public void RemoveEffect(Effects effect)
        {
            if (currentEffect.Remove(effect))
            {
                Console.WriteLine($"{effect} 상태이상 제거"); //디버그용
            }
        }
        public void RemoveAllEffect()
        {
            currentEffect.Clear();
        }
        public bool HasEffect(Effects effect)
        {
            return currentEffect.ContainsKey(effect);
        }

        public void ApplyEffect()
        {
            if (Hp <= 0) return;

            List<Effects> effectKeys = currentEffect.Keys.ToList();

            foreach (Effects key in effectKeys)
            {
                if (currentEffect.TryGetValue(key, out Effect effect))
                {
                    switch (effect.Type)
                    {
                        case Effects.Poison:
                            Hp -= 5;
                            break;
                        case Effects.Sleep:
                            break;
                        case Effects.Stun:
                            break;
                    }
                }
                effect.Duration--;

                if (effect.Duration <= 0)
                {
                    //제거됨
                    currentEffect.Remove(key);
                }
            }

            if (Hp <= 0)
            {
                //쓰러짐
                Hp = 0;
            }
        }
    }
}