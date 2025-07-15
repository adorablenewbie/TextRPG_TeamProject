//using System;
//using System.Collections.Generic;
//using System.Numerics;

//namespace TextRPG.Old
//{
//    public enum DungeonType
//    {
//        Forest,
//        Cave,
//        Castle,
//        DragonLair
//    }

//    public enum MonsterType
//    {
//        Weak,
//        Normal,
//        Strong
//    }

//    public class Dungeon
//    {
//        private static readonly Dictionary<DungeonType, List<(string name, MonsterType type)>> DungeonMonsters = new()
//        {
//            { DungeonType.Forest, new List<(string, MonsterType)> { ("늑대", MonsterType.Weak), ("곰", MonsterType.Normal), ("도적", MonsterType.Strong) } },
//            { DungeonType.Cave, new List<(string, MonsterType)> { ("이상한 박쥐", MonsterType.Weak), ("트롤", MonsterType.Normal), ("오우거", MonsterType.Strong) } },
//            { DungeonType.Castle, new List<(string, MonsterType)> { ("해골병사", MonsterType.Weak), ("리빙아머", MonsterType.Normal), ("암흑기사", MonsterType.Strong) } },
//            { DungeonType.DragonLair, new List<(string, MonsterType)> { ("헤츨링", MonsterType.Weak), ("작은 드래곤", MonsterType.Normal), ("성난 드래곤", MonsterType.Strong) } }
//        };

//        public static void ShowDungeonMenu()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("[던전 탐험]\n");
//                Console.WriteLine("1. 숲 던전");
//                Console.WriteLine("2. 동굴 던전");
//                Console.WriteLine("3. 성 던전");
//                Console.WriteLine("4. 드래곤 둥지 던전");
//                Console.WriteLine("0. 나가기\n");
//                Console.Write("원하시는 던전을 선택해주세요: ");
//                string input = Console.ReadLine();
//                switch (input)
//                {
//                    case "1":
//                        EnterDungeon(DungeonType.Forest);
//                        break;
//                    case "2":
//                        EnterDungeon(DungeonType.Cave);
//                        break;
//                    case "3":
//                        EnterDungeon(DungeonType.Castle);
//                        break;
//                    case "4":
//                        EnterDungeon(DungeonType.DragonLair);
//                        break;
//                    case "0":
//                        return;
//                    default:
//                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//                        Thread.Sleep(1000);
//                        break;
//                }
//            }
//        }
//        public static void EnterDungeon(DungeonType dungeonType)
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine($"[{dungeonType} 던전 입장]");
//                Console.WriteLine("던전을 탐험하시겠습니까? (y/n)");
//                string input = Console.ReadLine();
//                if (input.ToLower() == "y")
//                {
//                    SpawnMonsters(dungeonType, GameManager.player);
//                }
//                else if (input.ToLower() == "n")
//                {
//                    Console.WriteLine("던전 탐험을 취소합니다.");
//                    Thread.Sleep(1000);
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다. 'y' 또는 'n'을 입력해주세요.");
//                    Thread.Sleep(1000);
//                }
//            }
//        }

//        public static void SpawnMonsters(DungeonType dungeonType, Player player)
//        {
//            var monsterList = DungeonMonsters[dungeonType];
//            Random rand = new();
//            int enemyCount = rand.Next(1, 4);
//            List<Monster> spawnedMonsters = new List<Monster>();
//            for (int i = 0; i < enemyCount; i++)
//            {
//                var (name, type) = monsterList[rand.Next(monsterList.Count)];
//                Monster monster = MonsterFactory.Create(name, dungeonType, type);
//                spawnedMonsters.Add(monster);

//            }

//            while (true)
//            {
//                Console.Clear();
//                for (int i = 0; i < spawnedMonsters.Count; i++)
//                {
//                    if (spawnedMonsters[i] != null) Console.WriteLine($"[{i + 1}] {spawnedMonsters[i].Name} (타입: {spawnedMonsters[i].Type}) - HP:{spawnedMonsters[i].Hp}, ATK:{spawnedMonsters[i].Attack}, DEF:{spawnedMonsters[i].Defense}, GOLD:{spawnedMonsters[i].Gold}");
//                }
//                Console.WriteLine($"\n{dungeonType}에서 {enemyCount}마리의 몬스터가 나타났습니다!\n");
//                Console.WriteLine("1. 전투");
//                Console.WriteLine("2. 도망치기\n");
//                Console.Write("원하시는 행동을 선택해주세요: ");
//                string input = Console.ReadLine();
//                switch (input)
//                {
//                    case "1":
//                        Console.WriteLine("\n어느 몬스터를 공격할까?\n");
//                        for (int i = 0; i < spawnedMonsters.Count; i++)
//                        {
//                            Console.WriteLine($"[{i + 1}] {spawnedMonsters[i].Name}");
//                        }
//                        Console.Write("\n공격할 몬스터의 번호를 입력해주세요: ");
//                        string monsterInput = Console.ReadLine();
//                        if (int.TryParse(monsterInput, out int monsterIndex) && monsterIndex > 0 && monsterIndex <= enemyCount)
//                        {
//                            Monster selectedMonster = spawnedMonsters[monsterIndex - 1];
//                            Console.WriteLine($"\n당신은 {selectedMonster.Name}을(를) 공격합니다!");
//                            AttackEnemy(selectedMonster, player);
//                            spawnedMonsters.RemoveAll(m => m.Hp <= 0);
//                            for (int i = 0; i < spawnedMonsters.Count; i++)
//                            {
//                                player.Hp -= spawnedMonsters[i].Attack - player.Defense;
//                                Console.WriteLine($"\n{spawnedMonsters[i].Name}이(가) 당신을 공격합니다! 당신의 남은 HP: {player.Hp}");
//                                if (player.Hp <= 0)
//                                {
//                                    Console.WriteLine("당신은 쓰러졌습니다! 게임 오버!");
//                                    Thread.Sleep(2000);
//                                    Environment.Exit(0);
//                                }
//                                Thread.Sleep(2000);
//                            }
//                            if (spawnedMonsters.Count == 0)
//                            {
//                                Console.WriteLine("모든 몬스터를 처치했습니다!");
//                                Thread.Sleep(2000);
//                                return;
//                            }
//                            break;
//                        }
//                        else
//                        {
//                            Console.WriteLine("\n잘못된 입력입니다. 다시 시도해주세요.");
//                        }
//                        goto case "1"; // 다시 전투 선택으로 돌아가기
//                    case "2":
//                        Console.WriteLine("\n도망쳤습니다!");
//                        Thread.Sleep(2000);
//                        return;
//                    default:
//                        Console.WriteLine("\n잘못된 입력입니다. 다시 시도해주세요.");
//                        Thread.Sleep(2000);
//                        break;
//                }
//            }
//        }

//        public static void AttackEnemy(Monster monster, Player player)
//        {
//            monster.Hp -= player.SumAttack() - monster.Defense;
//            if (monster.Hp <= 0)
//            {
//                player.Gold += monster.Gold;
//                player.Exp += monster.Exp;
//                Console.WriteLine($"\n{monster.Name}을 처치했습니다!\n {monster.Gold} 골드를 획득했습니다.  보유 골드: {player.Gold}");
//                Console.WriteLine($"{monster.Exp} 경험치를 획득했습니다.  보유 경험치: {player.Exp}");
//                Thread.Sleep(2000);
//                player.LevelUp();
//            }
//            else
//            {
//                Console.WriteLine($"\n{monster.Name}의 남은 HP: {monster.Hp}");
//                Thread.Sleep(2000);
//            }
//        }

//        public class Monster
//        {
//            public string Name { get; }
//            public float Hp { get; set; }
//            public float Attack { get; }
//            public float Defense { get; }
//            public float Gold { get; }
//            public float Exp { get; }
//            public MonsterType Type { get; }

//            public Monster(string name, float hp, float attack, float defense, float gold, float exp, MonsterType type)
//            {
//                Name = name;
//                Hp = hp;
//                Attack = attack;
//                Defense = defense;
//                Gold = gold;
//                Exp = exp;
//                Type = type;
//            }
//        }

//        public static class MonsterFactory
//        {
//            // 던전 타입별 기본 능력치
//            private static readonly Dictionary<DungeonType, (float hp, float atk, float def, float gold, float exp)> DungeonBaseStats = new()
//        {
//            { DungeonType.Forest, (10, 2, 1, 10, 10) },
//            { DungeonType.Cave, (20, 4, 2, 20, 30) },
//            { DungeonType.Castle, (30, 6, 3, 30, 70) },
//            { DungeonType.DragonLair, (50, 10, 5, 50, 120) }
//        };

//            // 몬스터 타입별 계수
//            private static readonly Dictionary<MonsterType, (float hp, float atk, float def, float gold, float exp)> MonsterTypeMultiplier = new()
//        {
//            { MonsterType.Weak, (1.0f, 1.0f, 1.0f, 1.0f, 1.0f) },
//            { MonsterType.Normal, (1.5f, 1.3f, 1.2f, 1.3f, 1.3f) },
//            { MonsterType.Strong, (2.2f, 1.7f, 1.5f, 1.8f, 1.8f) }
//        };

//            public static Monster Create(string name, DungeonType dungeonType, MonsterType monsterType)
//            {
//                var baseStat = DungeonBaseStats[dungeonType];
//                var multi = MonsterTypeMultiplier[monsterType];

//                return new Monster(
//                    name,
//                    hp: baseStat.hp * multi.hp,
//                    attack: baseStat.atk * multi.atk,
//                    defense: baseStat.def * multi.def,
//                    gold: baseStat.gold * multi.gold,
//                    exp: baseStat.exp * multi.exp,
//                    type: monsterType
//                );
//            }
//        }
//    }
//}
