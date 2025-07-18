using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.SaveDatas
{
    public class SaveData
    {
        private const string SaveFileName = "player.json";
        private const string QuestFileName = "quests.json";
        private const string KillCountFileName = "killcount.json";

        public static void SavePlayer()
        {
            try
            {
                string json = JsonSerializer.Serialize(Player.Instance);
                File.WriteAllText(SaveFileName, json);

                SaveQuests();
                SaveKillCount();

                Console.WriteLine("캐릭터가 저장되었습니다.");
                System.Threading.Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("캐릭터 저장에 실패했습니다.");
                System.Threading.Thread.Sleep(1000);
            }
        }

        public static Player LoadOrCreatePlayer()
        {
            while (true)
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
                                loaded.Initialize();

                                LoadQuestProgress();
                                LoadKillCount();

                                Console.WriteLine($"[{loaded.Name}] 캐릭터를 불러왔습니다.");
                                System.Threading.Thread.Sleep(1000);
                                return loaded;
                            }
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("저장된 캐릭터를 불러오는 데 실패했습니다. 새로 만듭니다.");
                            return null;
                        }
                    }
                    else if (input == "n")
                    {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        private static void SaveQuests()
        {
            var dataList = Quest.questList.Select(q => new Quest.QuestData
            {
                QuestTitle = q.QuestTitle,
                IsProgress = q.IsProgress,
                IsCompleted = q.IsCompleted
            }).ToList();

            string json = JsonSerializer.Serialize(dataList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(QuestFileName, json);
        }

        private static void LoadQuestProgress()
        {
            if (!File.Exists(QuestFileName)) return;

            try
            {
                string json = File.ReadAllText(QuestFileName);
                List<Quest.QuestData> dataList = JsonSerializer.Deserialize<List<Quest.QuestData>>(json);

                foreach (var data in dataList)
                {
                    var quest = Quest.questList.FirstOrDefault(q => q.QuestTitle == data.QuestTitle);
                    if (quest != null)
                    {
                        quest.IsProgress = data.IsProgress;
                        quest.IsCompleted = data.IsCompleted;
                    }
                }
            }
            catch
            {
                Console.WriteLine("퀘스트 데이터를 불러오는 데 실패했습니다.");
            }
        }

        private static void SaveKillCount()
        {
            var data = new Quest.KillCountData
            {
                Slime = Quest.slimeKillCount,
                Goblin = Quest.goblinKillCount,
                Bandit = Quest.banditKillCount,
                Werewolf = Quest.werewolfKillCount,
                Bat = Quest.batKillCount,
                Zombie = Quest.zombieKillCount,
                Skeleton = Quest.skeletonKillCount,
                Mimic = Quest.mimicKillCount,
                Vampire = Quest.vampireKillCount,
                BlackMage = Quest.blackMageKillCount,
                BlackKnight = Quest.blackKnightKillCount,
                Orc = Quest.orcKillCount,
                Dragon = Quest.dragonKillCount
            };

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(KillCountFileName, json);
        }

        private static void LoadKillCount()
        {
            if (!File.Exists(KillCountFileName)) return;

            try
            {
                string json = File.ReadAllText(KillCountFileName);
                Quest.KillCountData data = JsonSerializer.Deserialize<Quest.KillCountData>(json);

                Quest.slimeKillCount = data.Slime;
                Quest.goblinKillCount = data.Goblin;
                Quest.banditKillCount = data.Bandit;
                Quest.werewolfKillCount = data.Werewolf;
                Quest.batKillCount = data.Bat;
                Quest.zombieKillCount = data.Zombie;
                Quest.skeletonKillCount = data.Skeleton;
                Quest.mimicKillCount = data.Mimic;
                Quest.vampireKillCount = data.Vampire;
                Quest.blackMageKillCount = data.BlackMage;
                Quest.blackKnightKillCount = data.BlackKnight;
                Quest.orcKillCount = data.Orc;
                Quest.dragonKillCount = data.Dragon;
            }
            catch
            {
                Console.WriteLine("킬 카운트를 불러오는 데 실패했습니다.");
            }
        }
    }
}
