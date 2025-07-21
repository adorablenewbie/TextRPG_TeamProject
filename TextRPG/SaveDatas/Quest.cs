using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.SaveDatas
{
    public class Quest
    {
        public class QuestData
        {
            public string QuestTitle { get; set; }
            public bool IsProgress { get; set; }
            public bool IsCompleted { get; set; }
        }
        public class KillCountData
        {
            public int Slime { get; set; }
            public int Goblin { get; set; }
            public int Bandit { get; set; }
            public int Werewolf { get; set; }
            public int Bat { get; set; }
            public int Zombie { get; set; }
            public int Skeleton { get; set; }
            public int Mimic { get; set; }
            public int Vampire { get; set; }
            public int BlackMage { get; set; }
            public int BlackKnight { get; set; }
            public int Orc { get; set; }
            public int Dragon { get; set; }
        }
        public string QuestTitle => questTitle;
        public bool IsProgress { get => isProgress; set => isProgress = value; }
        public bool IsCompleted { get => isCompleted; set => isCompleted = value; }

        private string questTitle;
        private string questDescript;
        private float questRewardExp;
        private float questRewardGold;
        private Skill questSpecialReward;
        private int requiredLevel;
        private bool isProgress = false;
        private bool isCompleted = false;
        public static int slimeKillCount = 0; // 슬라임 처치 횟수
        public static int goblinKillCount = 0; // 고블린 처치 횟수
        public static int banditKillCount = 0; // 산적 처치 횟수
        public static int werewolfKillCount = 0; // 늑대인간 처치 횟수
        public static int batKillCount = 0; // 박쥐 처치 횟수
        public static int zombieKillCount = 0; // 좀비 처치 횟수
        public static int skeletonKillCount = 0; // 스켈레톤 처치 횟수
        public static int orcKillCount = 0; // 오크 처치 횟수
        public static int mimicKillCount = 0; // 미믹 처치 횟수
        public static int vampireKillCount = 0; // 뱀파이어 처치 횟수
        public static int blackMageKillCount = 0; // 흑마법사 처치 횟수
        public static int blackKnightKillCount = 0; // 흑기사 처치 횟수
        public static int dragonKillCount = 0; // 흑염룡 처치 횟수


        public static List<Quest> questList = new()
        {
            new Quest("장비 장착", "인벤토리에서 아이템을 장착해 보세요.", 0, 200, 10, Skill.Slam),
            new Quest("단련", "레벨 5 이상으로 달성해 보세요.", 2, 500, 25, Skill.Barrier),
            new Quest("토양 오염의 근원 퇴치", "숲 던전에서 슬라임을 5마리 잡으세요.", 1, 250, 20, Skill.Heal),
            new Quest("고블린의 습격 저지", "숲 던전에서 고블린을 5마리 잡으세요.", 3, 300, 25, Skill.Bomb),
            new Quest("산적 대장 체포 작전", "숲 던전에서 산적 5명을 잡으세요.", 4, 400, 30, Skill.Stinger),
            new Quest("밤의 공포", "숲 던전에서 늑대인간 5명을 잡으세요.", 5, 500, 40, null),
            new Quest("동굴 탐험의 시작", "동굴 던전에서 박쥐를 7마리 잡으세요.", 6, 700, 50, Skill.Lightning),
            new Quest("동굴 속 생존자의 흔적 탐색", "동굴 던전에서 좀비를 7마리 잡으세요.", 7, 800, 60, Skill.Hypnosis),
            new Quest("유물 수집", "동굴 던전에서 스켈레톤을 7마리 잡으세요.", 8, 900, 75, null),
            new Quest("동굴의 지배자 추적", "동굴 던전에서 오크를 10마리 잡으세요.", 8, 1000, 100, null),
            new Quest("보물 상자 찾기", "성 던전에서 미믹을 10마리 잡으세요.", 10, 1200, 125, Skill.HealingWind),
            new Quest("초대받지 않은 손님", "성 던전에서 뱀파이어 10명을 잡으세요.", 12, 1500, 150, null),
            new Quest("흑막의 정체", "성 던전에서 흑마법사 12명을 잡으세요.", 13, 1750, 200, Skill.DeathBlow),
            new Quest("마법진 해제 작업", "성 던전에서 흑기사 15명을 잡으세요.", 15, 2000, 300, null),
            new Quest("구원", "드래곤 둥지에서 흑마법사가 최후의 발악으로 소환한 흑염룡을 처치하세요.", 20, 5000, 500, null)
        };
        public Quest(string title, string descript, int requiredLevel, float Gold, float Exp, Skill SpecialReward)
        {
            this.questTitle = title;
            this.questDescript = descript;
            this.requiredLevel = requiredLevel;
            this.questRewardGold = Gold;
            this.questRewardExp = Exp;
            this.questSpecialReward =SpecialReward;
            this.isProgress = false;
            this.isCompleted = false;
        }
        public void ChooseQuest()
        {
            if (Player.Instance.Level < requiredLevel)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                System.Threading.Thread.Sleep(1000);
                return;
            }
            if (isCompleted)
            {
                Console.WriteLine("이미 완료한 퀘스트입니다.");
                System.Threading.Thread.Sleep(1000);
                return;
            }
            if (isProgress)
            {
                CheckCompletQuest();
                System.Threading.Thread.Sleep(1000);
                return;
            }
            Console.WriteLine($"퀘스트를 수락하였습니다: {questTitle}");
            isProgress = true;
            System.Threading.Thread.Sleep(1000);
        }

        public void ShowQuestTitle()
        {
            Console.WriteLine($"{questTitle}\n" +
                $"   | 레벨: {requiredLevel} 이상 \n" +
                $"   | 설명: {questDescript} \n" +
                $"   | 보상: {questRewardGold} 골드, {questRewardExp}경험치\n" +
                $"   | 특별보상: {(questSpecialReward != null ? questSpecialReward.Name : "거지라서 없음 ㅋ")}\n" +
                $"   | 진행 상황: {(isProgress ? (isCompleted ? "완료" : "진행 중") : "수락 안 함")}\n");

        }

        public static void ShowQuestList()
        {
            for (int i = 0; i < questList.Count; i++)
            {
                int questNumber = i + 1;
                if (Player.Instance.Level >= questList[i].requiredLevel)
                {
                    Console.Write($"[{questNumber}]");
                    questList[i].ShowQuestTitle();
                }
            }

        }
        public void CompleteQuest()
        {
            isCompleted = true;
            Player.Instance.Gold += questRewardGold;
            Player.Instance.Exp += questRewardExp;
            Player.Instance.LevelUpCheck();
            Console.WriteLine($"퀘스트 '{questTitle}' 완료! {questRewardGold} 골드와 {questRewardExp} 경험치를 획득했습니다.\n");
            if (questSpecialReward != null)
            {
                Player.Instance.Skills.Add(questSpecialReward);
                Console.WriteLine($"특별 보상으로 {questSpecialReward.Name}을 획득했습니다.\n");
            }
            Console.WriteLine($"퀘스트 '{questTitle}' 완료! 보상을 받았습니다.");
            System.Threading.Thread.Sleep(2000);
        }
        public void CheckCompletQuest()
        {
            if (!isCompleted)
            {
                switch(this.questTitle)
                    {
                        case "장비 장착":
                            if (Player.Instance.EquippedArmor != null || Player.Instance.EquippedWeapon != null)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            
                            }
                            break;
                        case "단련":
                            if (Player.Instance.Level >= 5)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "토양 오염의 근원 퇴치":
                            if (slimeKillCount >= 5)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "고블린의 습격 저지":
                            if (goblinKillCount >= 5)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "산적 대장 체포 작전":
                            if (banditKillCount >= 5)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "밤의 공포":
                            if (werewolfKillCount >= 5)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "동굴 탐험의 시작":
                            if (batKillCount >= 7)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "동굴 속 생존자의 흔적 탐색":
                            if (zombieKillCount >= 7)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "유물 수집":
                            if (skeletonKillCount >= 7)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "동굴의 지배자 추적":
                            if (orcKillCount >= 10)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "보물 상자 찾기":
                            if (mimicKillCount >= 10)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "초대받지 않은 손님":
                            if (vampireKillCount >= 10)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "흑막의 정체":
                            if (blackMageKillCount >= 12)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "마법진 해제 작업":
                            if (blackKnightKillCount >= 15)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                        case "구원":
                            if (dragonKillCount >= 1)
                            {
                                isCompleted = true;
                                CompleteQuest();
                            }
                            break;
                     }
            }
            else
            {
                Console.WriteLine("퀘스트가 진행 중이거나 이미 완료되었습니다.");
            }
        }

        public static void CountKill(Monster monster)
        {
            if(monster.Name == "슬라임")
            {
                slimeKillCount++;
            }
            else if (monster.Name == "고블린")
            {
                goblinKillCount++;
            }
            else if (monster.Name == "산적")
            {
                banditKillCount++;
            }
            else if (monster.Name == "늑대인간")
            {
                werewolfKillCount++;
            }
            else if (monster.Name == "박쥐")
            {
                batKillCount++;
            }
            else if (monster.Name == "좀비")
            {
                zombieKillCount++;
            }
            else if (monster.Name == "스켈레톤")
            {
                skeletonKillCount++;
            }
            else if (monster.Name == "미믹")
            {
                mimicKillCount++;
            }
            else if (monster.Name == "뱀파이어")
            {
                vampireKillCount++;
            }
            else if (monster.Name == "흑마법사")
            {
                blackMageKillCount++;
            }
            else if (monster.Name == "흑기사")
            {
                blackKnightKillCount++;
            }
            else if (monster.Name == "오크")
            {
                orcKillCount++;
            }
            else if (monster.Name == "흑염룡")
            {
                dragonKillCount++;
            }
            else
            {
                Console.WriteLine("알 수 없는 몬스터입니다.");
            }
        }
    }
}
