using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveData;

namespace TextRPG.Scenes
{
    internal class SkillScene
    {
        static void DisplaySkillUI()
        {
            Console.Clear();
            Console.WriteLine("스킬");
            Console.WriteLine("보유 중인 스킬을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            //for (int i = 0; i < inventory.Count; i++) // 임시 for
            //{
            //    int targetItem = inventory[i];

            //    string displayEquipped = equipList.Contains(targetItem) ? "[E]" : "";
            //    Console.WriteLine($"- {displayEquipped} {itemNames[targetItem]}  |  {(itemType[targetItem] == 0 ? "공격력" : "방어력")} +{itemValue[targetItem]}  |  {itemDesc[targetItem]}");
            //}
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            string input = Console.ReadLine();
        }

        static void DisplayEquipSkillUI()
        {
            Console.Clear();
            Console.WriteLine("스킬 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            //for (int i = 0; i < inventory.Count; i++) // 임시 for
            //{
            //    int targetItem = inventory[i];

            //    string displayEquipped = equipList.Contains(targetItem) ? "[E]" : "";
            //    Console.WriteLine($"- {i + 1} {displayEquipped} {itemNames[targetItem]}  |  {(itemType[targetItem] == 0 ? "공격력" : "방어력")} +{itemValue[targetItem]}  |  {itemDesc[targetItem]}");
            //}
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            string input = Console.ReadLine();

            //switch (result) // 임시 switch
            //{
            //    case 0:
            //        DisplayInventoryUI();
            //        break;

            //    default:

            //        int targetItem = result - 1;
            //        bool isEquipped = equipList.Contains(targetItem);

            //        if (isEquipped)
            //        {
            //            equipList.Remove(targetItem);
            //            if (itemType[targetItem] == 0)
            //                extraAtk -= itemValue[targetItem];
            //            else
            //                extraDef -= itemValue[targetItem];
            //        }
            //        else
            //        {
            //            equipList.Add(targetItem);
            //            if (itemType[targetItem] == 0)
            //                extraAtk += itemValue[targetItem];
            //            else
            //                extraDef += itemValue[targetItem];
            //        }

            //        DisplayEquipUI();
            //        break;
            //}
        }
    }
}
