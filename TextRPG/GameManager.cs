using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Channels;
namespace TextRPG;

public class GameManager
{
    private const string SaveFileName = "player.json";
    public static Player player;

    public static void Main(string[] args)
    {
        GameManager game = new GameManager();
        game.Start();
    }

    public GameManager()
    {
        player = LoadOrCreatePlayer();
    }

    public void Start()
    {
        while (true)
        {
            ShowVillageMenu();
        }
    }

    private void ShowVillageMenu()
    {
        Console.Clear();
        SavePlayer();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
        Console.WriteLine("1. 상태보기\r\n2. 인벤토리\r\n3. 상점\r\n4. 휴식하기\r\n5. 던전 입장\r\n6. 게임 종료");
        Console.Write("원하시는 행동을 입력해주세요: ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                player.ShowStatus();
                break;
            case "2":
                player.ShowInventory();
                break;
            case "3":
                ShowShop();
                break;
            case "4":
                ShowRest();
                break;
            case "5":
                Dungeon.ShowDungeonMenu();
                break;
            case "6":
                
                while (true) 
                {
                    Console.WriteLine("\n게임을 종료하시겠습니까? (y/n)\n");
                    string exitGameCheck = Console.ReadLine();

                    if (exitGameCheck == "y")
                    {
                        ExitGame();
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

    private void ShowShop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("전설의 전설의 전설의 상점임 ㄷㄷ");
            Console.WriteLine("1. 아이템 구매\r\n2. 아이템 판매\r\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();
            if (input == "0")
                break;
            else if (input == "1")
            {
                ShowBuy();
                break;
            }
            else if (input == "2")
            {
                ShowSell();
                break;
            }
            else
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            continue;
        }
    }
    


    private void ExitGame()
    {
        Console.Clear();
        SavePlayer(); // 게임 종료 전에 저장
        Console.WriteLine("게임을 종료합니다. 감사합니다!");
        Environment.Exit(0);
    }

    private Player LoadOrCreatePlayer()
    {
        if (File.Exists(SaveFileName))
        {
            Console.WriteLine("저장된 캐릭터가 있습니다. 불러오시겠습니까? (y: 불러오기, n: 새로 만들기)");
            string input = Console.ReadLine();
            if (input == "y")
            {
                try
                {
                    Console.Clear();
                    string json = File.ReadAllText(SaveFileName);
                    Player loaded = JsonSerializer.Deserialize<Player>(json);

                    if (loaded != null)
                    {
                        Console.WriteLine($"[{loaded.Name}] 캐릭터를 불러왔습니다.");
                        loaded.InitializeInventory();
                        loaded.RestoreEquippedItems();
                        return loaded;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("저장된 캐릭터를 불러오는 데 실패했습니다. 새로 만듭니다.");
                }
            }
        }

        // 새 캐릭터 생성
        Console.Clear();
        Console.WriteLine("새 캐릭터를 생성합니다.");
        Console.Write("캐릭터 이름을 입력하세요: ");
        string name = Console.ReadLine();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("직업을 입력하세요: ");
            Console.Write("\n1. 전사\r\n2. 궁수\r\n3. 도적\n");
            string selectClass = Console.ReadLine();
            if (selectClass == "1")
            {
               Console.WriteLine($"[{name}] 전사 캐릭터를 생성합니다.\n");
               var newPlayer = new Player(name, "전사", 1, 0, 150, 10, 6, 1000);
               return newPlayer;
            }
            else if (selectClass == "2")
            {
                Console.WriteLine($"[{name}] 궁수 캐릭터를 생성합니다.\n");
                var newPlayer = new Player(name, "궁수", 1, 0, 110, 12, 5, 1000);
                return newPlayer;
            }
            else if (selectClass == "3")
            {
                Console.WriteLine($"[{name}] 도적 캐릭터를 생성합니다.\n");
                var newPlayer = new Player(name, "도적", 1, 0, 80, 15, 4, 1000);
                return newPlayer;
             }
            else
            {
                Console.WriteLine("잘못된 직업입니다. 전사, 마법사, 궁수 중 하나를 입력해주세요.");
            }
        }
    }

    private void SavePlayer()
    {
        try
        {
            string json = JsonSerializer.Serialize(player);
            File.WriteAllText(SaveFileName, json);
            Console.WriteLine("캐릭터가 저장되었습니다.");
        }
        catch
        {
            Console.WriteLine("캐릭터 저장에 실패했습니다.");
        }
    }

    private void ShowRest()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("[휴게소]");
            Console.WriteLine($"휴게소에서 100골드로 잠시 쉬어가시겠습니까?.   (보유골드: {player.Gold})\n");
            Console.WriteLine("1. 예\r\n0. 아니요\n");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                if (100 <= player.Gold)
                {
                    PlayerRest();
                    break;
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다. 휴식을 취할 수 없습니다.");
                }
            }
            else if (input == "0")
            {
                break;

            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
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
            System.Threading.Thread.Sleep(2000); // 2초 대기
            player.RestoreHealth();
            Console.WriteLine("체력이 회복되었습니다.");
            player.Gold -= 100;
            Console.WriteLine($"남은 골드: {player.Gold}\n");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요: ");
            string input = Console.ReadLine();
            if (input == "0")
            {
                break;
            }
                
            else
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
        }
    }

    private void ShowBuy()
    {
        // 상점에서 판매하는 아이템 목록
        List<Item> shopItems = new List<Item>
        {
            Item.NoviceArmor,
            Item.IronArmor,
            Item.SpartanArmor,
            Item.LegendArmor,
            Item.OldSword,
            Item.BronzeAxe,
            Item.SpartanSpear,
            Item.LegendSpear
        };

        // 플레이어 인벤토리에 없는 아이템만 필터링
        var itemsToShow = shopItems.FindAll(shopItem => !player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));

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
            Console.WriteLine($"\n보유 골드: {player.Gold}");
            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
            string inputNum = Console.ReadLine();

            if (inputNum == "0")
                break;

            if (int.TryParse(inputNum, out int selected) && selected >= 1 && selected <= itemsToShow.Count)
            {
                Item selectedItem = itemsToShow[selected - 1];
                if (player.Gold >= selectedItem.Price)
                {
                    player.Gold -= selectedItem.Price;
                    player.AddItem(selectedItem);
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

    private void ShowSell ()
    {
        // 상점에서 판매하는 아이템 목록
        List<Item> shopItems = new List<Item>
        {
            Item.NoviceArmor,
            Item.IronArmor,
            Item.SpartanArmor,
            Item.LegendArmor,
            Item.OldSword,
            Item.BronzeAxe,
            Item.SpartanSpear,
            Item.LegendSpear
        };

        // 플레이어 인벤토리에 있는 아이템만 필터링
        var itemsToShow = shopItems.FindAll(shopItem => player.Inventory.Exists(invItem => invItem.Name == shopItem.Name));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            if (itemsToShow.Count == 0)
            {
                Console.WriteLine("판매할 수 있는 아이템이 없습니다.\n");
                Console.WriteLine($"\n보유 골드: {player.Gold}\n");
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
            Console.WriteLine($"\n보유 골드: {player.Gold}");
            Console.WriteLine("\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요(아이템 숫자 입력시 구매): ");
            string inputNum = Console.ReadLine();

            if (inputNum == "0")
                break;

            if (int.TryParse(inputNum, out int selected) && selected >= 1 && selected <= itemsToShow.Count)
            {
                Item selectedItem = itemsToShow[selected - 1];
                player.Gold += selectedItem.Price * 0.85f;
                player.RemoveItem(selectedItem);
                itemsToShow.RemoveAt(selected - 1);
                Console.WriteLine($"\n{selectedItem.Name}을(를) 판매했습니다! 보유 골드: {player.Gold}");
               
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            }
        }
    }
}