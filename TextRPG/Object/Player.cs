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
        
        }
        public void UnequipSkill(int skillIndex)
        {
            if (skillIndex < 0 || skillIndex >= equippedSkills.Count)
            {
                Console.WriteLine("잘못된 스킬 인덱스입니다.");
                return;
            }
            Skill skillToUnequip = equippedSkills[skillIndex];
            equippedSkills.Remove(skillToUnequip);
            Console.WriteLine($"{skillToUnequip.Name} 스킬이 해제되었습니다.");
            System.Threading.Thread.Sleep(1000); // 1초 대기
        }
    }
}