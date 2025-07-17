using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.Scenes
{
    public class MainMenu : Scene
    {
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 던전 입장");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 스킬");
            Console.WriteLine("5. 상점");
            Console.WriteLine("6. 휴식하기");
            Console.WriteLine("7: 게임 저장하기");
            Console.WriteLine("8. 게임 종료");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Program.ChangeScene(SceneType.StatusScene);
                    break;
                case "2":
                    Program.ChangeScene(SceneType.DungeonScene);
                    break;
                case "3":
                    Program.ChangeScene(SceneType.InventoryScene);
                    break;
                case "4":
                    Program.ChangeScene(SceneType.SkillScene);
                    break;
                case "5":
                    Program.ChangeScene(SceneType.ShopScene);
                    break;
                case "6":
                    Program.ChangeScene(SceneType.RestScene);
                    break;
                case "7":
                    //Program.SavePlayer();
                    break;
                case "8":
                    while (true)
                    {
                        Console.WriteLine("\n게임을 종료하시겠습니까? (y/n)\n");
                        string exitGameCheck = Console.ReadLine();

                        if (exitGameCheck == "y")
                        {
                            //ExitGame();
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
