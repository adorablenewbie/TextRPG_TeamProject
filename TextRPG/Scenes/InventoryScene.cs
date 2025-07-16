using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;
using TextRPG.Items;

namespace TextRPG.Scenes
{
    public class InventoryScene : Scene
    {
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < Player.Instance.Inventory.Count; i++) // 변수 대입 예정
            {
                Item targetItem = Player.Instance.Inventory[i];
                string displayEquipped = targetItem.IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {targetItem.Name}  |  {(targetItem.Type == 0 ? "공격력" : "방어력")} + {(targetItem.Type == 0 ? targetItem.Attack : targetItem.Defense)}  |  {targetItem.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, 1);

            switch (result)
            {
                case 0:
                    Program.ChangeScene(SceneType.MainScene);
                    break;

                case 1:
                    ShowEquipItem();
                    break;
            }
        }

        static void ShowEquipItem()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < Player.Instance.Inventory.Count; i++) // 변수 대입 예정
            {
                
                Item targetItem = Player.Instance.Inventory[i];
                string displayEquipped = targetItem.IsEquipped ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {targetItem.Name}  |  {(targetItem.Type == 0 ? "공격력" : "방어력")} + {(targetItem.Type == 0 ? targetItem.Attack : targetItem.Defense)}  |  {targetItem.Description}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, Player.Instance.Inventory.Count); // 변수 대입 예정

            if (result == 0) Program.ChangeScene(SceneType.InventoryScene);
            else
            {
                int targetItem = result - 1;
                Player.Instance.Inventory[targetItem].UseItem();
                //플레이어 스테이터스 업데이트 함수 필요

                ShowEquipItem();
            }
        }

        static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;

                }
                Console.WriteLine("잘못된 입력입니다. 다시 시도해 주세요.");
            }
        }

    }
}
