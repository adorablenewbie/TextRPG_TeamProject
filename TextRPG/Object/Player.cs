using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Player()
        {
            this.hp = 100f;
            this.mana = 50f;
            this.level = 1;
            this.attack = 10;
            this.defense = 5;
            this.gold = 1500;
            this.exp = 0;
            this.skills = new List<Skill>();
            this.equippedSkills = new List<Skill>();
        }

        public void EquipSkill(Skill selectedSkill)
        {
            equippedSkills.Add(selectedSkill);
            Console.WriteLine($"{selectedSkill.Name} 스킬이 장착되었습니다.");
            System.Threading.Thread.Sleep(1000); // 1초 대기

            if (equippedSkills.Count > 2)
            {
                Console.WriteLine("장착된 스킬이 2개를 초과했습니다. 가장 오래된 스킬을 해제합니다.");
                equippedSkills.RemoveAt(0); // 가장 오래된 스킬 해제
            }
        }
        public void UnequipSkill(Skill selectedSkill)
        {
            if (equippedSkills.Contains(selectedSkill))
            {
                equippedSkills.Remove(selectedSkill);
                Console.WriteLine($"{selectedSkill.Name} 스킬이 해제되었습니다.");
            }
            else
            {
                Console.WriteLine("해제할 수 없는 스킬입니다.");
            }
            System.Threading.Thread.Sleep(1000); // 1초 대기
        }
    }
}