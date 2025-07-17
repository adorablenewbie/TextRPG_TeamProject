using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    public class QuestScene : Scene
    {
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("Quest!!");
            Console.WriteLine();
            for (int i = 0; i < Quest.questList.Count; i++) {
                Quest.questList[i].ShowQuestList();
            }
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
            Console.WriteLine(">>");
            
            string Input = Console.ReadLine();
            bool isNumber = int.TryParse(Input, out int result);
            
            if(result == 0)
            {
                Program.ChangeScene(SceneType.MainScene);
            }
            else
            {
                int selectedNum = result - 1;
                //QuestList[selectedNum].ChooseQuest();
            }
        }
    }
}
