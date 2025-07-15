//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TextRPG.SaveDatas
//{
//    public class Shop
//    {
//        // 상점에서 판매하는 아이템 목록

//        private static List<Old.Item> shopItems = new List<Old.Item>
//    {
//        Old.Item.NoviceArmor,
//        Old.Item.IronArmor,
//        Old.Item.SpartanArmor,
//        Old.Item.LegendArmor,
//        Old.Item.OldSword,
//        Old.Item.BronzeAxe,
//        Old.Item.SpartanSpear,
//        Old.Item.LegendSpear
//    };

//        public static void CheckBuyUI()
//        {
//            var player = Old.GameManager.player;
//            var itemsToShow = GetBuyableItems(player);

//            if (itemsToShow.Count == 0)
//            {
//                Console.WriteLine("구매할 수 있는 아이템이 없습니다.\n");
//                Console.WriteLine($"\n보유 골드: {Old.GameManager.player.Gold}\n");
//            }
//            else
//            {
//                DisplayBuyItems(itemsToShow, Old.GameManager.player.Gold);
//            }
//        }

//        public static void CheckSellUI()
//        {
//            var player = Old.GameManager.player;
//            var itemsToShow = GetSellableItems(player);

//            if (itemsToShow.Count == 0)
//            {
//                Console.WriteLine("판매할 수 있는 아이템이 없습니다.\n");
//                Console.WriteLine($"\n보유 골드: {Old.GameManager.player.Gold}\n");
//            }
//            else
//            {
//                DisplaySellItems(itemsToShow, Old.GameManager.player.Gold);
//            }
//        }

//        private static List<Old.Item> GetBuyableItems(Old.Player player)
//        {
//            return shopItems.FindAll(shopItem =>
//                !player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));
//        }
//        private static List<Old.Item> GetSellableItems(Old.Player player)
//        {
//            return shopItems.FindAll(shopItem =>
//                player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));
//        }

//        private static void DisplayBuyItems(List<Old.Item> items, float playerGold)
//        {
//            Console.WriteLine("구매 가능한 아이템 목록:");
//            for (int i = 0; i < items.Count; i++)
//            {
//                Console.WriteLine($"{i + 1}. {items[i]}");
//            }
//            Console.WriteLine($"\n보유 골드: {playerGold}");
//        }
//        private static void DisplaySellItems(List<Old.Item> items, float playerGold)
//        {
//            Console.WriteLine("판매 가능한 아이템 목록:");
//            for (int i = 0; i < items.Count; i++)
//            {
//                Console.WriteLine($"{i + 1}. {items[i]}");
//            }
//            Console.WriteLine($"\n보유 골드: {playerGold}");
//        }

//        public static int ParseSelection(string input, bool buy)
//        {
//            var player = Old.GameManager.player;
//            List<Old.Item> itemsToShow;

//            if (buy)
//            {itemsToShow = GetBuyableItems(player); }
//            else 
//            {itemsToShow = GetSellableItems(player); }

//            int maxIndex = itemsToShow.Count;
//            if (int.TryParse(input, out int index))
//            {
//                if (index >= 1 && index <= maxIndex)
//                    return index - 1; // 실제 인덱스 반환
//            }
//            return -1; // 실패 시 -1 반환
//        }

//        public static void CheckBuyResult(int result)
//        {
//            var player = Old.GameManager.player;
//            var itemsToShow = GetBuyableItems(player);
//            if (result >= 0 && result < itemsToShow.Count)
//            {
//                Old.Item selectedItem = itemsToShow[result];
//                if (player.Gold >= selectedItem.Price)
//                {
//                    player.Gold -= selectedItem.Price;
//                    player.AddItem(selectedItem);
//                    Console.WriteLine($"\n{selectedItem.Name}을(를) 구매했습니다! 남은 골드: {player.Gold}");
//                }
//                else
//                {
//                    Console.WriteLine("\n골드가 부족합니다. 구매할 수 없습니다.");
//                }
//            }
//            else
//            {
//                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//            }
//        }

//        public static void CheckSellResult(int result)
//        {
//            var player = Old.GameManager.player;
//            var itemsToShow = GetSellableItems(player);
//            if (result >= 0 && result < itemsToShow.Count)
//            {
//                Old.Item selectedItem = itemsToShow[result];
//                player.Gold += selectedItem.Price;
//                player.RemoveItem(selectedItem);
//                Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다! 현재 골드: {player.Gold}");
//            }
//            else
//            {
//                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//            }
//        }
//    }
//}
