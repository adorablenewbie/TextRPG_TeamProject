using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TextRPG.Object;

namespace TextRPG.Scenes
{
    internal class StatusScene : Scene
    {
        
        public override void ShowScene()
        {
            while (true)
            {
                var player = Player.Instance;
                Console.Clear();
                Console.WriteLine($"[상태 보기]\n");
                Console.WriteLine($"닉네임:{player.Name}  직업:{player.JobName}");
                Console.WriteLine($"Lv. {player.Level} 경험치:{player.Exp}") ;
                Console.WriteLine($"공격력: {player.TotalAttack} {((player.AddAttack > 0) ? $"(+{player.AddAttack})" : "")}");
                Console.WriteLine($"방어력: {player.TotalDefence} {((player.AddDefence > 0) ? $"(+{player.AddDefence})" : "")}");
                Console.WriteLine($"체력: {player.Hp}");
                Console.WriteLine($"골드: {player.Gold}\n");

                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    Program.ChangeScene(SceneType.MainScene);
                    break;
                }
                else
                {
                    Program.ChangeScene(SceneType.MainScene);
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }
    }
}

