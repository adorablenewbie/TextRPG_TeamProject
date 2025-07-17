using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.SaveDatas
{
    public class Quest
    {
        private string questDescript;

        public static List<Quest> questList = new()
        {
            new Quest("마을을 위협하는 미니언 처치"),
            new Quest("장비를 장착해보기"),
            new Quest("더욱 더 강해지기"),
        };
        public Quest(string descript)
        {
            this.questDescript = descript;
        }

        public void ChooseQuest()
        {

        }

        public void ShowQuestList()
        {
            Console.WriteLine(this.questDescript);
        }
    }
}
