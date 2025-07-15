using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    internal class InventoryScene
    {
        static void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                int targetItem = inventory[i];

                string displayEquipped = equipList.Contains(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {itemNames[targetItem]}  |  {(itemType[targetItem] == 0 ? "공격력" : "방어력")} +{itemValue[targetItem]}  |  {itemDesc[targetItem]}");
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
                    ShowMain();
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

            for (int i = 0; i < inventory.Count; i++)
            {
                int targetItem = inventory[i];

                string displayEquipped = equipList.Contains(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {i + 1} {displayEquipped} {itemNames[targetItem]}  |  {(itemType[targetItem] == 0 ? "공격력" : "방어력")} +{itemValue[targetItem]}  |  {itemDesc[targetItem]}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, inventory.Count);

            switch (result) // 
            {
                case 0:
                    ShowInventory();
                    break;

                default:

                    int targetItem = result - 1;
                    bool isEquipped = equipList.Contains(targetItem);

                    if (isEquipped)
                    {
                        equipList.Remove(targetItem);
                        if (itemType[targetItem] == 0)
                            extraAtk -= itemValue[targetItem];
                        else
                            extraDef -= itemValue[targetItem];
                    }
                    else
                    {
                        equipList.Add(targetItem);
                        if (itemType[targetItem] == 0)
                            extraAtk += itemValue[targetItem];
                        else
                            extraDef += itemValue[targetItem];
                    }

                    ShowEquipItem();
                    break;
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
