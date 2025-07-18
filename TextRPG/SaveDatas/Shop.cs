using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.Object;

namespace TextRPG.SaveDatas
{
    public class Shop
    {
        // 상점에서 판매하는 아이템 목록
        public static List<Item> shopItems = new();
        public static void InitItems()
        {
            for (int i = 0; i < Usable.usableItemData.Count; i++) { 
                shopItems.Add(Usable.usableItemData[i]);
            }
            for (int i = 0; i < Equipable.equipedItemData.Count; i++) {
                shopItems.Add(Equipable.equipedItemData[i]);
            }
        }

        public static void CheckBuyUI()
        {
            var itemsToShow = shopItems;

            if (itemsToShow.Count == 0)
            {
                Console.WriteLine("구매할 수 있는 아이템이 없습니다.\n");
                Console.WriteLine($"\n보유 골드: {Player.Instance.Gold}\n");
            }
            else
            {
                DisplayBuyItems(itemsToShow, Player.Instance.Gold);
            }
        }

        public static void CheckSellUI()
        {
            var itemsToShow = Player.Instance.Inventory;

            if (itemsToShow.Count == 0)
            {
                Console.WriteLine("판매할 수 있는 아이템이 없습니다.\n");
                Console.WriteLine($"\n보유 골드: {Player.Instance.Gold}\n");
            }
            else
            {
                DisplaySellItems(itemsToShow, Player.Instance.Gold);
            }
        }

        private static void DisplayBuyItems(List<Item> items, float playerGold)
        {
            Console.WriteLine("구매 가능한 아이템 목록:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name}");
            }
            Console.WriteLine($"\n보유 골드: {playerGold}");
        }

        private static void DisplaySellItems(List<Item> items, float playerGold)
        {
            Console.WriteLine("판매 가능한 아이템 목록:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Name}");
            }
            Console.WriteLine($"\n보유 골드: {playerGold}");
        }

        public static int ParseSelection(string input, bool buy)
        {
            List<Item> itemsToShow;

            if (buy)
            {itemsToShow = shopItems; }
            else 
            {itemsToShow = Player.Instance.Inventory; }

            int maxIndex = itemsToShow.Count;
            if (int.TryParse(input, out int index))
            {
                if (index >= 1 && index <= maxIndex)
                    return index - 1; // 실제 인덱스 반환
            }
            return -1; // 실패 시 -1 반환
        }

        public static void CheckBuyResult(int result)
        {
            var itemsToShow = shopItems;
            if (result >= 0 && result < itemsToShow.Count)
            {
                Item selectedItem = itemsToShow[result];
                if (Player.Instance.Gold >= selectedItem.Price)
                {
                    Player.Instance.Gold -= selectedItem.Price;
                    selectedItem.AddItem();
                    Console.WriteLine($"\n{selectedItem.Name}을(를) 구매했습니다! 남은 골드: {Player.Instance.Gold}");
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

        public static void CheckSellResult(int result)
        {
            var itemsToShow = Player.Instance.Inventory;
            if (result >= 0 && result < itemsToShow.Count)
            {
                Item selectedItem = itemsToShow[result];
                Player.Instance.Gold += selectedItem.Price;
                selectedItem.RemoveItem();
                Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다! 현재 골드: {Player.Instance.Gold}");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            }
        }
    }
}
