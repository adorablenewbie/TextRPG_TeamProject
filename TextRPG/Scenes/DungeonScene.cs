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

        // 던전 입장 여부
        public static void EnterDungeon(DungeonType dungeonType)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"[{GetDungeonName(dungeonType)}]에 입장합니다.");
                Console.Write("정말 입장하시겠습니까? (y/n): ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "y")
                {
                    //굳이 들어가자 마자 싸울 이유가 없음
                    Fight();
                    //SpawnMonsters(dungeonType, Player.Instance);
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
            List<Monster> spawnedMonster = new();
            int turn = 1;
            Random rand = new Random();
            //몬스터 생성
            CreateMonster(spawnedMonster, rand);

            while (true)
            {
                while (spawnedMonster.Count > 0) {
                    Console.Clear();
                    Console.WriteLine("┌────────────[ 전투 시작 ]────────────┐");
                    SpawnMonster(spawnedMonster);
                    Console.WriteLine("└────────────────────────────────────┘");
                    Console.WriteLine($"▶ 당신: HP: {Player.Instance.Hp} / ATK: {Player.Instance.Attack} / DEF: {Player.Instance.Defense} / GOLD: {Player.Instance.Gold}");
                    Console.WriteLine("\n[1] 공격  [2]스킬  [3] 도망치기");
                    Console.Write("행동 선택: ");
                    //플레이어의 턴
                    string input = Console.ReadLine();
                    int num = 0;

                    if (int.TryParse(input, out num))
                    {
                        if(num == 1)
                        {
                            int targetNumber = 0;
                            Console.Write("공격할 몬스터 번호선택");
                            if(int.TryParse(Console.ReadLine(), out targetNumber))
                            {
                                PlayerTurn(spawnedMonster, targetNumber);
                                Thread.Sleep(1000);
                            }
                        }
                        else if(num == 2)
                        {
                            int targetNumber = 0;
                            Skill selectedSkill = UseSkill();
                            Console.WriteLine("스킬을 사용할 대상 선택");
                            Console.WriteLine($"[5] {Player.Instance.Name} (자신에게 사용)");
                            if (int.TryParse(Console.ReadLine(), out targetNumber))
                            {
                                PlayerSkillTurn(spawnedMonster, targetNumber, selectedSkill);
                                Thread.Sleep(1000);
                            }
                            Thread.Sleep(1000);
                        }
                        else if (num == 3)
                        {
                            Console.WriteLine("도망갔습니다.");
                            break;
                        }
                        else
                        {
                            Console.Write("잘못된 입력입니다.");
                        }
                    }
                    //몬스터의 턴
                    MonsterTurn(spawnedMonster, Player.Instance);
                    //결과
                    turn++;
                }
                //다음 층 또는 다음 이벤트 이동, 임시적 break 삽입
                Console.WriteLine("모든 적을 처치하였습니다.");
                break;
            }
        }

        public static void CreateMonster(List<Monster> mList, Random randomNum)
        {
            int count = randomNum.Next(1, 5);

            for (int i = 1; i < count; i++) {
                Monster cloneMonster = Monster.monstersData[randomNum.Next(0, Monster.monstersData.Count)];
                mList.Add(cloneMonster.Clone());
            }
        }

        public static void SpawnMonster(List<Monster> mList)
        {
            for (int i = 0; i < mList.Count; i++) {
                Console.WriteLine($"|{i + 1} {mList[i].Name} 몬스터 출현");
            }
        }

        public static void PlayerTurn(List<Monster> mList, int targetNumber)
        {
            if (mList.Count <= 0) return;
            int idx = targetNumber - 1;


                mList[idx].Hp -= Player.Instance.Attack;

                //이곳에 몬스터 체력 몇 달았는지 적기
                Console.WriteLine($"{mList[idx].Name}의 남은 HP: {mList[idx].Hp}");
                Thread.Sleep(500);
                if (mList[idx].Hp <= 0)
                {
                    mList.RemoveAt(idx);
                }
            
        }

        public static void PlayerSkillTurn(List<Monster> mList, int targetNumber, Skill selectedSkill)
        {
            if (mList.Count <= 0) return;
            int idx = targetNumber - 1;

            if (targetNumber == 5)
            {
                selectedSkill.UseSkill(selectedSkill, Player.Instance);
                Console.WriteLine($"{Player.Instance.Name}의 남은 HP: {Player.Instance.Hp}");
            }
            else
            {
                    selectedSkill.UseSkill(selectedSkill, mList[idx]);
                //이곳에 몬스터 체력 몇 달았는지 적기
                Console.WriteLine($"{mList[idx].Name}의 남은 HP: {mList[idx].Hp}");
                if (mList[idx].Hp <= 0)
                {
                    mList.RemoveAt(idx);
                }
            }
            Thread.Sleep(500);
            
        }

        public static void MonsterTurn(List<Monster> mList, Player p)
        {
            if(mList.Count <= 0) return;

            foreach (Monster m in mList) {
                p.Hp -= m.Attack;
                Console.WriteLine($"몬스터 {m.Name} 의 공격! 데미지 {m.Attack}");
                Thread.Sleep(1000);
            }
        }
        public static Skill UseSkill()
        {
            Console.WriteLine("어떤 스킬을 사용하겠습니까?\n");
            for (int i = 0; i < Player.Instance.EquippedSkills.Count; i++)
            {
                string skillState = Player.Instance.EquippedSkills[i].ToString();
                Console.WriteLine($"[{i + 1}] {skillState}");
            }
            Console.Write("스킬 번호를 입력하세요: ");
            int input;
            int.TryParse(Console.ReadLine(), out input);
            Skill selectedSkill = Player.Instance.EquippedSkills[input-1];
            return selectedSkill;
        }

        // 몬스터 소환 및 전투 루프
        //public static void SpawnMonsters(DungeonType dungeonType, Player player)
        //{
        //    List<Monster> spawned = new();
        //    Random rand = new();
        //    int killCount = 0;

        //    while (true)
        //    {
        //        // 전투 시작: 몬스터 랜덤 생성
        //        spawned.Clear();
        //        var monsterList = DungeonMonsters[dungeonType];
        //        int enemyCount = rand.Next(1, 4);

        //        for (int i = 0; i < enemyCount; i++)
        //        {
        //            var (name, type) = monsterList[rand.Next(monsterList.Count)];
        //            spawned.Add(MonsterFactory.Create(name, dungeonType, type));
        //        }

        //        // 전투 UI 출력
        //        while (spawned.Count > 0)
        //        {
        //            Console.Clear();
        //            Console.WriteLine("┌────────────[ 전투 시작 ]────────────┐");
        //            for (int i = 0; i < spawned.Count; i++)
        //                Console.WriteLine($"| [{i + 1}] {spawned[i].Name} (타입: {spawned[i].Type}) HP:{spawned[i].Hp} ATK:{spawned[i].Attack} DEF:{spawned[i].Defense} |");
        //            Console.WriteLine("└────────────────────────────────────┘");
        //            Console.WriteLine($"▶ 당신: HP: {player.hp} / ATK: {player.attack} / DEF: {player.defense} / GOLD: {player.gold}");
        //            Console.WriteLine("\n[1] 공격    [2] 도망치기");
        //            Console.Write("행동 선택: ");
        //            string input = Console.ReadLine();

        //            if (input == "1")
        //            {
        //                Console.Write("공격할 몬스터 번호: ");
        //                if (int.TryParse(Console.ReadLine(), out int idx) && idx > 0 && idx <= spawned.Count)
        //                {
        //                    var target = spawned[idx - 1];
        //                    AttackEnemy(target, player, dungeonType);
        //                    if (target.Hp <= 0)
        //                    {
        //                        killCount++;
        //                        spawned.RemoveAt(idx - 1);
        //                    }

        //                    foreach (var m in spawned)
        //                    {
        //                        float dmg = Math.Max(1, m.Attack - player.Defense);
        //                        player.Hp -= dmg;
        //                        Console.WriteLine($"{m.Name}이(가) 공격! 피해: {dmg} ▶ 남은 HP: {player.Hp}");
        //                        if (player.Hp <= 0)
        //                        {
        //                            ShowDungeonResult(player, dungeonType, killCount, false);
        //                            Environment.Exit(0);
        //                        }
        //                        Thread.Sleep(1000);
        //                    }
        //                }
        //            }
        //            else if (input == "2")
        //            {
        //                Console.WriteLine("도망쳤습니다!");
        //                Thread.Sleep(1000);
        //                return;
        //            }
        //        }
        //    }
        //}

        // 전투 처리
        //public static void AttackEnemy(Monster monster, Player player, DungeonType dungeonType)
        //{
        //    float dmg = Math.Max(1, player.SumAttack() - monster.Defense);
        //    monster.hp -= dmg;

        //    if (monster.Hp <= 0)
        //    {
        //        Reward(dungeonType, monster, player);
        //        player.LevelUp();
        //    }
        //    else
        //    {
        //        Console.WriteLine($"{monster.name}의 남은 HP: {monster.hp}");
        //        Thread.Sleep(1000);
        //    }
        //}

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

        // 다음 스테이지 확률
        //public static bool NextStageChance(DungeonType dungeonType)
        //{
        //    Random rand = new();
        //    int chance = dungeonType switch
        //    {
        //        DungeonType.Forest => 90,
        //        DungeonType.Cave => 70,
        //        DungeonType.Castle => 50,
        //        DungeonType.DragonLair => 30,
        //        _ => 50
        //    };
        //    return rand.Next(100) < chance;
        //}

        // 결과창 출력
        public static void ShowDungeonResult(Player player, DungeonType dungeonType, int killCount, bool survived)
        {
            Console.Clear();
            Console.WriteLine(survived ? "🎉 던전 클리어! 🎉" : "☠️ 당신은 쓰러졌습니다! ☠️");
            Console.WriteLine("───────────────");
            Console.WriteLine($"▶ 던전: {GetDungeonName(dungeonType)}");
            Console.WriteLine($"▶ 처치 수: {killCount}");
            Console.WriteLine($"▶ 골드: {player.Gold}");
            Console.WriteLine($"▶ 경험치: {player.Exp}");
            Console.WriteLine($"▶ HP: {player.Hp}");
            Console.WriteLine($"▶ 레벨: {player}");
            Console.WriteLine("\n[엔터]를 눌러 돌아갑니다.");
            Console.ReadLine();
        }

        public static string GetDungeonName(DungeonType type) => type switch
        {
            DungeonType.Forest => "🌲 숲 던전",
            DungeonType.Cave => "🕳 동굴 던전",
            DungeonType.Castle => "🏰 성 던전",
            DungeonType.DragonLair => "🐉 드래곤 둥지",
            _ => "알 수 없음"
        };

        // 몬스터 정의
        //public class Monster
        //{
        //    public string Name { get; }
        //    public float Hp { get; set; }
        //    public float Attack { get; }
        //    public float Defense { get; }
        //    public float Gold { get; }
        //    public float Exp { get; }
        //    public MonsterType Type { get; }

        //    public Monster(string name, float hp, float atk, float def, float gold, float exp, MonsterType type)
        //    {
        //        Name = name;
        //        Hp = hp;
        //        Attack = atk;
        //        Defense = def;
        //        Gold = gold;
        //        Exp = exp;
        //        Type = type;
        //    }
        //}

        // 몬스터 생성 팩토리
        //public static class MonsterFactory
        //{
        //    private static readonly Dictionary<DungeonType, (float hp, float atk, float def, float gold, float exp)> BaseStats = new()
        //    {
        //        { DungeonType.Forest, (10, 2, 1, 10, 10) },
        //        { DungeonType.Cave, (20, 4, 2, 20, 30) },
        //        { DungeonType.Castle, (30, 6, 3, 30, 70) },
        //        { DungeonType.DragonLair, (50, 10, 5, 50, 120) }
        //    };

        //    private static readonly Dictionary<MonsterType, (float hp, float atk, float def, float gold, float exp)> Multiplier = new()
        //    {
        //        { MonsterType.Weak, (1f, 1f, 1f, 1f, 1f) },
        //        { MonsterType.Normal, (1.5f, 1.3f, 1.2f, 1.3f, 1.3f) },
        //        { MonsterType.Strong, (2.2f, 1.7f, 1.5f, 1.8f, 1.8f) }
        //    };

        //    public static Monster Create(string name, DungeonType dt, MonsterType mt)
        //    {
        //        var baseStat = BaseStats[dt];
        //        var multi = Multiplier[mt];
        //        return new Monster(
        //            name,
        //            baseStat.hp * multi.hp,
        //            baseStat.atk * multi.atk,
        //            baseStat.def * multi.def,
        //            baseStat.gold * multi.gold,
        //            baseStat.exp * multi.exp,
        //            mt
        //        );
        //    }
        //}
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
                Console.WriteLine($" 💧 회복의 샘 발견! HP가 { heal} 회복되었습니다!");
                 player.Hp += heal;
                Thread.Sleep(1000);
            }
        }

        

        // 예시: 스테이지 클리어 후 이벤트 삽입 위치
        // if (NextStageChance(dungeonType))
        // {
        //     TryTriggerTrapOnStageTransition(player);
        //     TryTriggerHealingFountain(player);
        //     Console.WriteLine("👉 다음 스테이지로 이동합니다...");
        //     Thread.Sleep(1500);
        // }
    }
    

 }


