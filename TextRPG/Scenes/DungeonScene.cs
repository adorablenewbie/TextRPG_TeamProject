using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using TextRPG;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    public class DungeonScene : Scene
    {
        // 던전별 등장 몬스터 목록
        //private static readonly Dictionary<DungeonType, List<(string name, MonsterType type)>> DungeonMonsters = new()
        //{
        //    { DungeonType.Forest, new() { ("늑대", MonsterType.Weak), ("곰", MonsterType.Normal), ("도적", MonsterType.Strong) } },
        //    { DungeonType.Cave, new() { ("이상한 박쥐", MonsterType.Weak), ("트롤", MonsterType.Normal), ("오우거", MonsterType.Strong) } },
        //    { DungeonType.Castle, new() { ("해골병사", MonsterType.Weak), ("리빙아머", MonsterType.Normal), ("암흑기사", MonsterType.Strong) } },
        //    { DungeonType.DragonLair, new() { ("헤츨링", MonsterType.Weak), ("작은 드래곤", MonsterType.Normal), ("성난 드래곤", MonsterType.Strong) } }
        //};

        // 던전 메뉴
        public override void ShowScene()
        {
            Console.Clear();
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│         [ 던전 선택 ]       │");
            Console.WriteLine("├─────────────────────────────┤");
            Console.WriteLine("│ 1. 🌲 숲 던전 (난이도: 쉬움)     │");
            Console.WriteLine("│ 2. 🕳 동굴 던전 (난이도: 보통)    │");
            Console.WriteLine("│ 3. 🏰 성 던전   (난이도: 어려움)   │");
            Console.WriteLine("│ 4. 🐉 드래곤 둥지 (난이도: 매우 어려움)│");
            Console.WriteLine("│ 0. ❌ 나가기                   │");
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
                    Fight();
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
        public static void Fight()
        {   
            //몬스터 생성
            List<Monster> spawnedMonster = Dungeon.CreateMonster();
            //int turn = 1;
            string input;
            while (true)
            {
                while (spawnedMonster.Count > 0) 
                {
                    Console.Clear();
                    Console.WriteLine("┌────────────[ 전투 시작 ]────────────┐");
                    Dungeon.SpawnMonster(spawnedMonster);
                    Console.WriteLine("└────────────────────────────────────┘");
                    Console.WriteLine($"▶ 당신: HP: {Player.Instance.Hp} / ATK: {Player.Instance.Attack} / DEF: {Player.Instance.Defense} / GOLD: {Player.Instance.Gold}");
                    Console.WriteLine("\n[1] 공격  [2]스킬  [3] 도망치기");
                    Console.Write("행동 선택: ");
                    //플레이어의 턴
                    input = Console.ReadLine();
                    Dungeon.ChooseAction(input, spawnedMonster);
                }
                Console.WriteLine("모든 적을 처치하였습니다.");
                System.Threading.Thread.Sleep(1000);
                break;
            }
            Console.Clear();
            Console.WriteLine("더 나아가시겠습니까?\n");
            Console.WriteLine("[1] 다음 스테이지로 이동\n[2] 던전 나가기\n");
            Console.Write("행동 선택: ");
            input = Console.ReadLine();
            if (input == "1")
            {
                // 다음 스테이지로 이동
                // TryTriggerTrapOnStageTransition(Player.Instance);
                // TryTriggerHealingFountain(Player.Instance);
                Console.Clear();
                Console.WriteLine("다음 스테이지로 이동합니다...");
                Dungeon.RandomStage();
                Thread.Sleep(1500);
                Fight(); // 재귀 호출로 다음 스테이지 전투 시작
            }
            else if (input == "2")
            {
                Console.WriteLine("던전을 나갑니다.");
                Thread.Sleep(1000);
                Program.ChangeScene(SceneType.MainScene); // 메인 씬으로 돌아가기
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
            }

        }


        // 보상 지급
        public static void Reward(DungeonType dungeonType, Monster monster, Player player)
        {
            float multiplier = dungeonType switch
            {
                DungeonType.Forest => 1.0f,
                DungeonType.Cave => 1.2f,
                DungeonType.Castle => 1.5f,
                DungeonType.DragonLair => 2.0f,
                _ => 1.0f
            };

            int rewardGold = (int)(monster.Gold * multiplier);
            int rewardExp = (int)(monster.Exp * multiplier);

            player.Gold += rewardGold;
            player.Exp += rewardExp;

            Console.WriteLine($"{monster.Name} 처치! 골드 +{rewardGold}, 경험치 +{rewardExp}");
            Thread.Sleep(1000);
        }

        // 결과창 출력
        public static void ShowDungeonResult(Player player, DungeonType dungeonType, int killCount, bool survived)
        {
            Console.Clear();
            Console.WriteLine(survived ? "🎉 던전 클리어! 🎉" : "☠️ 당신은 쓰러졌습니다! ☠️");
            Console.WriteLine("───────────────");
            Console.WriteLine($"▶ 던전: {Dungeon.GetDungeonName(dungeonType)}");
            Console.WriteLine($"▶ 처치 수: {killCount}");
            Console.WriteLine($"▶ 골드: {player.Gold}");
            Console.WriteLine($"▶ 경험치: {player.Exp}");
            Console.WriteLine($"▶ HP: {player.Hp}");
            Console.WriteLine($"▶ 레벨: {player}");
            Console.WriteLine("\n[엔터]를 눌러 돌아갑니다.");
            Console.ReadLine();
        }

        // 🪤 [스테이지 이동 시 함정 발동 함수]
        public static void TryTriggerTrapOnStageTransition(Player player)
        {
            Random rand = new();
            int trapChance = rand.Next(0, 100);

            if (trapChance < 30)
            {
                int damage = rand.Next(5, 16);
                Console.WriteLine($"🪤 함정 발동! 숨겨진 함정에 걸렸습니다! HP { damage} 감소!");
                player.Hp -= damage;

                if (player.Hp <= 0)
                {
                    Console.WriteLine("☠️ 당신은 함정에 의해 사망했습니다...");
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

                Thread.Sleep(1000);
            }
        }

        // 💧 [스테이지 이동 시 회복의 샘 발견 함수]
        public static void TryTriggerHealingFountain(Player player)
        {
            Random rand = new();
            int chance = rand.Next(0, 100);

            if (chance < 20)
            {
                int heal = rand.Next(10, 21);
                Console.WriteLine($" 💧 회복의 샘 발견! HP가 {heal} 회복되었습니다!");
                player.Hp += heal;
                Thread.Sleep(1000);
            }
        }

        public static void BoxStage()
        {
            Random random = new();
            Console.WriteLine("당신은 상자가 덩그러니 있는 방에 도착했습니다.");
            Console.WriteLine("상자를 열어보시겠습니까?\n");
            Console.WriteLine("[1] 열어본다.\n[2] 열지 않고 다음 스테이지로 간다.\n[0] 던전을 탈출한다.");
        }
        public static void ExitableStage()
        {
            Console.WriteLine("당신은 탈출 가능한 방에 도착했습니다.");
            Console.WriteLine("던전을 탈출하시겠습니까?\n");
            Console.WriteLine("[1] 탈출한다.\n[2] 다음 스테이지로 간다.");
            Console.WriteLine("행동을 선택해주세요: ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("당신은 지친 몸을 이끌며 던전을 탈출합니다. 던전에는 고요한 적막만이 남았습니다.");
                Thread.Sleep(2000);
                ShowDungeonResult(Player.Instance, DungeonType.Forest, 0, true); // 던전 결과 출력
                Program.ChangeScene(SceneType.MainScene); // 메인 씬으로 돌아가기
            }
            else if (input == "2")
            {
                Console.WriteLine("다음 스테이지로 이동합니다.");
                Thread.Sleep(1000);
                Dungeon.RandomStage(); // 다음 스테이지로 이동
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(1000);
            }
        }
    }
}


