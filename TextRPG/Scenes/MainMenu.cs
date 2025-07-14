using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scene
{
    public class MainMenu
    {
        private void ShowMainMenu()
        {
            Console.Clear();
            //SavePlayer(); // 플레이어 상태 저장 메소드 호출
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태보기\r\n2. 인벤토리\r\n3. 상점\r\n4. 휴식하기\r\n5. 던전 입장\r\n6. 게임 종료");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    player.ShowStatus();
                    break;
                case "2":
                    player.ShowInventory();
                    break;
                case "3":
                    ShowShop();
                    break;
                case "4":
                    ShowRest();
                    break;
                case "5":
                    Dungeon.ShowDungeonMenu();
                    break;
                case "6":

                    while (true)
                    {
                        Console.WriteLine("\n게임을 종료하시겠습니까? (y/n)\n");
                        string exitGameCheck = Console.ReadLine();

                        if (exitGameCheck == "y")
                        {
                            ExitGame();
                            break;
                        }
                        else if (exitGameCheck == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.\n");
                        }
                    }
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }
}
