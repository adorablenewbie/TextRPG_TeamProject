using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG.Scene
{
    internal class StatusScene : Scene
    {
        public override void ShowScene()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[상태 보기]\n");
                Console.WriteLine($"Lv. Level");
                Console.WriteLine($"이름 ( 직업 )");
                Console.WriteLine($"공격력: ()");
                Console.WriteLine($"방어력: ()");
                Console.WriteLine($"체력: hp");
                Console.WriteLine($"골드: gold\n");

                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }
    }
}
