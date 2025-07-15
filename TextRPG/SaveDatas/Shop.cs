using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.SaveDatas
{
    public class Shop
    {
        public static void ShowBuy()
        {
            // 상점에서 판매하는 아이템 목록
            List<Old.Item> shopItems = new List<Old.Item>
        {
            Old.Item.NoviceArmor,
            Old.Item.IronArmor,
            Old.Item.SpartanArmor,
            Old.Item.LegendArmor,
            Old.Item.OldSword,
            Old.Item.BronzeAxe,
            Old.Item.SpartanSpear,
            Old.Item.LegendSpear
        };

            // 플레이어 인벤토리에 없는 아이템만 필터링
            var itemsToShow = shopItems.FindAll(shopItem => !Old.GameManager.player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상점]");
                if (itemsToShow.Count == 0)
                {
                    Console.WriteLine("구매할 수 있는 아이템이 없습니다.\n");
                    Console.WriteLine("0. 나가기\n");
                    Console.Write("원하시는 행동을 입력해주세요: ");
                    string input = Console.ReadLine();
                    if (input == "0")
                        break;
                    else
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    continue;
                }

                Console.WriteLine("구매 가능한 아이템 목록:");
                for (int i = 0; i < itemsToShow.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {itemsToShow[i]}");
                }
                Console.WriteLine($"\n보유 골드: {Old.GameManager.player.Gold}");
                Console.WriteLine("\n0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
                string inputNum = Console.ReadLine();

                if (inputNum == "0")
                    break;

                if (int.TryParse(inputNum, out int selected) && selected >= 1 && selected <= itemsToShow.Count)
                {
                    Old.Item selectedItem = itemsToShow[selected - 1];
                    if (Old.GameManager.player.Gold >= selectedItem.Price)
                    {
                        Old.GameManager.player.Gold -= selectedItem.Price;
                        Old.GameManager.player.AddItem(selectedItem);
                        itemsToShow.RemoveAt(selected - 1);
                        Console.WriteLine($"\n{selectedItem.Name}을(를) 구매했습니다! 남은 골드: {player.Gold}");
                    }
                    else
                    {
                        Console.WriteLine("\n골드가 부족합니다. 구매할 수 없습니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }

        public static void ShowSell()
        {
            // 상점에서 판매하는 아이템 목록
            List<Old.Item> shopItems = new List<Old.Item>
        {
            Old.Item.NoviceArmor,
            Old.Item.IronArmor,
            Old.Item.SpartanArmor,
            Old.Item.LegendArmor,
            Old.Item.OldSword,
            Old.Item.BronzeAxe,
            Old.Item.SpartanSpear,
            Old.Item.LegendSpear
        };

            // 플레이어 인벤토리에 있는 아이템만 필터링
            var itemsToShow = shopItems.FindAll(shopItem => Old.GameManager.player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상점]");
                if (itemsToShow.Count == 0)
                {
                    Console.WriteLine("판매할 수 있는 아이템이 없습니다.\n");
                    Console.WriteLine($"\n보유 골드: {Old.GameManager.player.Gold}\n");
                    Console.WriteLine("0. 나가기\n");
                    Console.Write("원하시는 행동을 입력해주세요: ");
                    string input = Console.ReadLine();
                    if (input == "0")
                        break;
                    else
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    continue;
                }

                Console.WriteLine("판매 가능한 아이템 목록:");
                for (int i = 0; i < itemsToShow.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {itemsToShow[i]}");
                }
                Console.WriteLine($"\n보유 골드: {Old.GameManager.player.Gold}");
                Console.WriteLine("\n0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
                string inputNum = Console.ReadLine();

                if (inputNum == "0")
                    break;

                if (int.TryParse(inputNum, out int selected) && selected >= 1 && selected <= itemsToShow.Count)
                {
                    Old.Item selectedItem = itemsToShow[selected - 1];
                    Old.GameManager.player.Gold += selectedItem.Price * 0.85f;
                    Old.GameManager.player.RemoveItem(selectedItem);
                    itemsToShow.RemoveAt(selected - 1);
                    Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다! 보유 골드: {Old.GameManager.player.Gold}");

                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }
    }
}
