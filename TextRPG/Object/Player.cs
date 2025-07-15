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
    }
}
