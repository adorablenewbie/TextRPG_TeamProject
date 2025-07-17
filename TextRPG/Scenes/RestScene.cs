using System;
using TextRPG.Object;

namespace TextRPG.Scenes
{
    public class RestScene : Scene
    {

        public override void ShowScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[휴게소]");
                Console.WriteLine($"휴게소에서 100골드로 잠시 쉬어가시겠습니까?.   (보유골드: {Player.Instance.Gold})\n");
                Console.WriteLine("1. 예\r\n0. 아니요\n");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    if (100 <= Player.Instance.Gold)
                    {
                        PlayerRest();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다. 휴식을 취할 수 없습니다.");
                        Console.WriteLine("계속하려면 아무 키나 누르세요...");
                        Console.ReadKey();
                    }
                }
                else if (input == "0")
                {
                    Program.ChangeScene(SceneType.MainScene);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadKey();
                }
            }
        }

        private void PlayerRest()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[휴식]");
                Console.WriteLine("휴식을 취합니다...");
                System.Threading.Thread.Sleep(1000);
                Player.Instance.RestoreHealth();
                Console.WriteLine("체력이 회복되었습니다.");
                Player.Instance.Gold -= 100;
                Console.WriteLine($"남은 골드: {Player.Instance.Gold}\n");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    break;
                }
                else
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                Console.ReadKey();
            }
        }
    }
}