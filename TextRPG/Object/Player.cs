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
            this.attack = 10;
            this.defense = 5;
            this.gold = 1500;
            this.exp = 0;
        }

        public List<Skill> Skills { get; set; } = new List<Skill>();
        public void AddSkill(Skill skill)
        {
            if (skill != null && !Skills.Contains(skill))
            {
                Skills.Add(skill);
            }
            if (Skills.Count > 2)
            {
                Skills.RemoveAt(0); // 오래된 스킬 제거
            }
        }
        public void RemoveSkill(Skill skill)
        {
            if (skill != null && Skills.Contains(skill))
            {
                Skills.Remove(skill);
            }
        }
    }
}
