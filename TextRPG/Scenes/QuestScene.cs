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
            Console.WriteLine("----------[퀘스트]----------");
            Console.WriteLine();

            Quest.ShowQuestList();
            Console.WriteLine("\n[0]: 나가기\n");

            Console.Write("원하시는 행동, 퀘스트를 선택해주세요. >> ");
            string Input = Console.ReadLine();
            bool isNumber = int.TryParse(Input, out int result);

            if (isNumber && result == 0)
            {
                Program.ChangeScene(SceneType.MainScene);
            }
            else
            {
                if (!isNumber || result < 1 || result > Quest.questList.Count)
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    System.Threading.Thread.Sleep(1000);
                    return;
                }
                int selectedNum = result - 1;
                Quest.questList[selectedNum].ChooseQuest();
            }
        }
    }
}
