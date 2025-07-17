using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scenes
{
    public class ShopScene : Scene
    {
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("전설의 전설의 전설의 상점임 ㄷㄷ");
            Console.WriteLine("1. 아이템 구매\r\n2. 아이템 판매\r\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();
            if (input == "0")
                Program.ChangeScene(SceneType.MainScene);
            else if (input == "1")
            {
                ShowBuy();
            }
            else if (input == "2")
            {
                ShowSell();
            }
            else
            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
        }

        public void ShowBuy()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상점]");
                Console.WriteLine("[아이템 구매]");
                SaveDatas.Shop.CheckBuyUI();
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
                string input = Console.ReadLine();
                if (input == "0")
                    break;
                else 
                { 
                    int result = SaveDatas.Shop.ParseSelection(input, true); 
                    SaveDatas.Shop.CheckBuyResult(result);
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }
            }
        }
        public void ShowSell()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상점]");

                Console.WriteLine("[아이템 판매]");
                SaveDatas.Shop.CheckSellUI();
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
                string input = Console.ReadLine();
                if (input == "0")
                    break;
                else
                {
                    int result = SaveDatas.Shop.ParseSelection(input, false);
                    SaveDatas.Shop.CheckSellResult(result);
                    System.Threading.Thread.Sleep(2000);
                    continue;
                }
            }
        }
    }
}

