using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using TextRPG;
using TextRPG.Items;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    public class DungeonScene : Scene
    {
        public static int killCount; // 처치한 몬스터 수
        public static int getGold; // 획득한 골드
        public static int getExp; // 획득한 경험치
        // 던전 메뉴
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│         [ 던전 선택 ]       │");
            Console.WriteLine("├─────────────────────────────┤");
            Console.WriteLine("│ 1. 숲 던전 (난이도: 쉬움)     │");
            Console.WriteLine("│ 2. 동굴 던전 (난이도: 보통)    │");
            Console.WriteLine("│ 3. 성 던전   (난이도: 어려움)   │");
            Console.WriteLine("│ 4. 드래곤 둥지 (난이도: 매우 어려움)│");
            Console.WriteLine("│ 0. 나가기                   │");
            Console.WriteLine("└─────────────────────────────┘");
            Console.Write("원하시는 던전을 선택해주세요: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1": EnterDungeon(DungeonType.Forest); break;
                case "2": EnterDungeon(DungeonType.Cave); break;
                case "3": EnterDungeon(DungeonType.Castle); break;
                case "4": EnterDungeon(DungeonType.DragonLair); break;
                case "0": Program.ChangeScene(SceneType.MainScene); break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                    break;
            }
        }

        public static void EnterDungeon(DungeonType dungeonType)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{Dungeon.GetDungeonName(dungeonType)}]에 입장합니다.");
                Console.Write("정말 입장하시겠습니까? (y/n): ");
                string input = Console.ReadLine()?.ToLower();
                if (input == "y")
                {
                    killCount = 0;
                    getGold = 0;
                    getExp = 0;
                    Fight(dungeonType);
                    break;
                }
                else if (input == "n")
                {
                    Console.WriteLine("입장을 취소했습니다.");
                    Thread.Sleep(1000);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        
        //전투 진행
        public static void Fight(DungeonType dungeonType)  // 던전 타입을 인자로 받음~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        {   
            //몬스터 생성
            List<Monster> spawnedMonster = Dungeon.CreateMonster(dungeonType);
            bool end = false;
            while (true)
            {
                while (spawnedMonster.Count > 0) 
                {

                    Console.Clear();
                    Console.WriteLine("┌────────────[ 전투 시작 ]────────────┐");
                    Dungeon.SpawnMonster(spawnedMonster);
                    Console.WriteLine("└────────────────────────────────────┘");

                    if (Dungeon.MonsterClearCheck(spawnedMonster))
                    {
                        Thread.Sleep(1000);
                        break;
                    }
                    
                    Console.WriteLine($"▶ 당신: Lv:{Player.Instance.Level} HP: {Player.Instance.Hp} / MANA: {Player.Instance.Mana} / ATK: {Player.Instance.BaseAttack} / DEF: {Player.Instance.BaseDefense} / GOLD: {Player.Instance.Gold}");
                    Console.WriteLine("\n[1] 공격  [2]스킬  [3]아이템 사용  [4] 도망치기");
                    Console.Write("행동 선택: ");
                    //플레이어의 턴
                    if(Dungeon.ChooseAction(spawnedMonster, dungeonType))
                    {
                        end = true;
                        break;
                    }
                }
                if (end)
                {
                    Console.WriteLine("다음 장소로 이동합니다.");
                }
                else
                {
                    Console.WriteLine("모든 적을 처치하였습니다.");
                    Player.Instance.Mana += 10;
                    if (Player.Instance.MaxMana < Player.Instance.Mana)
                    {
                        Player.Instance.Mana = Player.Instance.MaxMana;
                    }
                    
                    System.Threading.Thread.Sleep(1000);
                }
                break;
            }
            Console.Clear();
            Console.WriteLine("더 나아가시겠습니까?\n");
            Console.WriteLine("엔터로 다음 스테이지로 이동\n");
            Console.ReadLine();
           
            Console.Clear();
            Console.WriteLine("다음 스테이지로 이동합니다...");
            Thread.Sleep(1000);
            RandomStage(dungeonType);
        }


        // 보상 지급
        public static void Reward(Monster monster, Player player)
        {

            int rewardGold = (int)(monster.Gold);
            int rewardExp = (int)(monster.Exp);
            killCount++;
            getGold += rewardGold;
            getExp += rewardExp;

            player.Gold += rewardGold;
            player.Exp += rewardExp;

            int random = new Random().Next(0, Shop.shopItems.Count);
            Item selected = Shop.shopItems[random];
            selected.AddItem();

            Console.WriteLine($"{monster.Name} 처치! 골드 +{rewardGold}, 경험치 +{rewardExp}");
            Console.WriteLine($"{selected.Name} 을 획득하였습니다.");

            Thread.Sleep(1000);
        }

        // [스테이지 이동 시 함정 발동 함수]
        public static void TrapStage(Player player, DungeonType dungeonType)
        {
            Random rand = new();
            while (true)
            {
                int trapRoad = rand.Next(0, 2);
                Console.Clear();
                Console.WriteLine("당신은 두 갈래길이 있는 방에 도착했습니다.");
                Console.WriteLine("두 갈래의 길 중 한 곳은 함정이 있습니다.");
                Console.WriteLine("당신은 어느 길로 가시겠습니까?\n");
                Console.WriteLine("[1] 왼쪽 길로 간다.\n[2] 오른쪽 길로 간다.");
                Console.Write("행동 선택: ");
                string inputStr = Console.ReadLine();
                int.TryParse(inputStr, out int input);
                if (input == 1| input == 2)
                {
                    if (trapRoad == input - 1)
                    {
                        int damage = 0;
                        if (dungeonType == DungeonType.Forest)
                        {
                            damage = rand.Next(5, 16);
                            Console.WriteLine("숲 던전의 함정에 걸렸습니다!");
                        }
                        else if (dungeonType == DungeonType.Cave)
                        {
                            damage = rand.Next(10, 26);
                            Console.WriteLine(" 동굴 던전의 함정에 걸렸습니다!");
                        }
                        else if (dungeonType == DungeonType.Castle)
                        {
                            damage = rand.Next(20, 36);
                            Console.WriteLine("성 던전의 함정에 걸렸습니다!");
                        }
                        else if (dungeonType == DungeonType.DragonLair)
                        {
                            damage = rand.Next(30, 46);
                            Console.WriteLine("드래곤 둥지의 함정에 걸렸습니다!");

                        }
                        Console.WriteLine($"HP {damage} 감소!");
                        player.Hp -= damage;
                        Console.WriteLine($"남은 HP: {player.Hp}");
                        if (player.Hp <= 0)
                        {
                            Console.WriteLine("당신은 함정에 의해 사망했습니다...");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                        }

                    }
                    else
                    {
                        Console.WriteLine("당신은 함정이 없는 길을 선택했습니다.");
                    }
                    Console.WriteLine("엔터로 다음 스테이지로 진행");
                    Console.ReadLine();
                    RandomStage(dungeonType);
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Thread.Sleep(1000);
                    continue;
                }
            }
        }

        // [스테이지 이동 시 회복의 샘 발견 함수]
        public static void HealingFountainStage(Player player, DungeonType dungeonType)
        {
            Random rand = new Random();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("당신은 회복의 샘이 있는 곳에 도착했습니다.");
                Console.WriteLine("샘에서 물을 마시면 HP가 회복될 것 같습니다.");
                Console.WriteLine("당신은 달고 단 샘물을 들이켰습니다.\n");
                Thread.Sleep(1000);
                int healingAmount = 0;
                if (DungeonType.Forest == dungeonType)
                {
                    healingAmount = rand.Next(0, 100);
                }
                else if (DungeonType.Cave == dungeonType)
                {
                    healingAmount = rand.Next(0, 200);
                }
                else if (DungeonType.Castle == dungeonType)
                {
                    healingAmount = rand.Next(0, 300);
                }
                else if (DungeonType.DragonLair == dungeonType)
                {
                    healingAmount = rand.Next(0, 400);
                }
                //int heal = Math.Min(healingAmount, player.MaxHp - player.Hp); // 최대 HP를 초과하지 않도록 조정
                //Console.WriteLine($" 회복의 샘 발견! HP가 {heal} 회복되었습니다!");
                //player.Hp += heal;
                Console.WriteLine($" 회복의 샘 발견! HP가 {healingAmount} 회복되었습니다!");
                
                player.Hp += healingAmount;
                if (player.Hp >= player.MaxHP)
                {
                    player.Hp = player.MaxHP;
                }

                Console.WriteLine($"남은 HP: {player.Hp}\n");
                Console.WriteLine("엔터로 다음 스테이지로 진행");
                Console.ReadLine();
                RandomStage(dungeonType); // 다음 스테이지로 이동
                break;
            }
            
        }

        public static void BoxStage(DungeonType dungeonType)
        {
            Random random = new();
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("당신은 상자가 덩그러니 있는 방에 도착했습니다.");
                Console.WriteLine("상자 안에는 무언가가 들어있을 것 같습니다.");
                Console.WriteLine("그것이 당신에게 어떤 결과를 줄지는 모릅니다.\n");
                Console.WriteLine("상자를 열어보시겠습니까?\n");
                Console.WriteLine("[1] 열어본다.\n[2] 열지 않고 다음 스테이지로 간다.");
                Console.Write("행동 선택: ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    int boxResult = random.Next(0, 10);
                    if (boxResult < 3)
                    {
                        Console.WriteLine("상자를 열었더니 함정에 걸렸습니다!");
                        int damage = 0;
                        if (dungeonType == DungeonType.Forest)
                        {
                            damage = random.Next(3, 10);
                        }
                        else if (dungeonType == DungeonType.Cave)
                        {
                            damage = random.Next(8, 20);
                        }
                        else if (dungeonType == DungeonType.Castle)
                        {
                            damage = random.Next(13, 30);
                        }
                        else if (dungeonType == DungeonType.DragonLair)
                        {
                            damage = random.Next(18, 40);
                        }
                        Console.WriteLine($"HP {damage} 감소!");
                        Player.Instance.Hp -= damage;
                        Console.WriteLine($"남은HP: {Player.Instance.Hp}");
                        if (Player.Instance.Hp <= 0)
                        {
                            Console.WriteLine("당신은 상자의 함정에 의해 사망했습니다...");
                            Thread.Sleep(1000);
                            Environment.Exit(0);
                        }
                    }
                    else if (boxResult < 6)
                    {
                        Console.WriteLine("상자 안에는 골드가 들어있습니다!");
                        int gold = 0;
                        if (dungeonType == DungeonType.Forest)
                        {
                            gold = random.Next(50, 201);
                        }
                        else if (dungeonType == DungeonType.Cave)
                        {
                            gold = random.Next(100, 401);
                        }
                        else if (dungeonType == DungeonType.Castle)
                        {
                            gold = random.Next(150, 601);
                        }
                        else if (dungeonType == DungeonType.DragonLair)
                        {
                            gold = random.Next(200, 801);
                        }

                        Player.Instance.Gold += gold;
                        Console.WriteLine($"골드 +{gold}");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine($"상자 안에는 {dungeonType}을 다녀간 모험가의 기록이 있습니다!");
                        int exp = 0;
                        if (dungeonType == DungeonType.Forest)
                        {
                            exp = random.Next(10, 21);
                        }
                        else if (dungeonType == DungeonType.Cave)
                        {
                            exp = random.Next(30, 61);
                        }
                        else if (dungeonType == DungeonType.Castle)
                        {
                            exp = random.Next(90, 111);
                        }
                        else if (dungeonType == DungeonType.DragonLair)
                        {
                            exp = random.Next(140, 181);
                        }
                        Player.Instance.Exp += exp;
                        Console.WriteLine($"경험치 +{exp}");
                        Thread.Sleep(1000);
                        
                    }
                    Console.WriteLine("엔터로 다음 스테이지 진행");
                    Console.ReadLine();
                    RandomStage(dungeonType); // 다음 스테이지로 이동
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("상자를 열지 않고 다음 스테이지로 이동합니다.");
                    Thread.Sleep(1000);
                    RandomStage(dungeonType); // 다음 스테이지로 이동
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }
        public static void ExitableStage(DungeonType dungeonType)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("당신은 탈출 가능한 방에 도착했습니다.");
                Console.WriteLine("던전을 탈출하시겠습니까?\n");
                Console.WriteLine("[1] 탈출한다.\n[2] 다음 스테이지로 간다.");
                Console.WriteLine("행동을 선택해주세요: ");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.WriteLine("당신은 지친 몸을 이끌며 던전을 탈출합니다. 던전에는 고요한 적막만이 남았습니다.");
                    Thread.Sleep(2000);
                    ShowDungeonResult(dungeonType, killCount,getGold,getExp); // 던전 결과 출력
                    Program.ChangeScene(SceneType.MainScene); // 메인 씬으로 돌아가기
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("다음 스테이지로 이동합니다.");
                    Thread.Sleep(1000);
                    RandomStage(dungeonType); // 다음 스테이지로 이동
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(1000);
                }
            }
        }
        // 결과창 출력
        public static void ShowDungeonResult(DungeonType dungeonType, int killCount, int getGold, int getExp)
        {
            Console.Clear();
            Console.WriteLine("🎉 던전 클리어! 🎉");
            Console.WriteLine("───────────────");
            Console.WriteLine($"▶ 던전: {Dungeon.GetDungeonName(dungeonType)}");
            Console.WriteLine($"▶ 처치 수: {killCount}");
            Console.WriteLine($"▶ 골드: +{getGold}");
            Console.WriteLine($"▶ 경험치: +{getExp}");
            Console.WriteLine("\n[엔터]를 눌러 돌아갑니다.");
            Console.ReadLine();
            
        }
        public static void RandomStage(DungeonType dungeonType)
        {
            Random rand = new Random();
            int result = rand.Next(0, 10);
            if (result < 1)
            {
                ExitableStage(dungeonType);
            }
            else if (result < 3)
            {
                BoxStage(dungeonType);
            }
            else if (result < 6)
            {
                Fight(dungeonType); 
            }
            else if (result < 8)
            {
                TrapStage(Player.Instance, dungeonType);
            }
            else
            {
                HealingFountainStage(Player.Instance, dungeonType);
            }


        }
    }
}


