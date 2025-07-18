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

namespace TextRPG.SaveDatas
{
    public class Quest
    {
        private string questTitle;
        private string questDescript;
        private float questRewardExp;
        private float questRewardGold;
        private Item questSpecialReward;
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
            new Quest("장비 장착", "갑옷이든 무기든 착용해보자!", 0, 200, 10),
            new Quest("더욱 더 강해지기", "레벨 5이상으로 달성하자", 2, 500, 25, Equipable.equipedItemData[0]),
            new Quest("마을을 위협하는 오크", "동굴에 거주하는 오크 3마리 처치", 7, 1000, 30),
            new Quest("구원", "세에에에에에에에계ㅔㅔㅔㅔㅔㅔㅔㅔ 평화를 위해 흑염룡 퇴치", 10, 5000, 60, Equipable.equipedItemData[0]),
        };
        public Quest(string title, string descript, int requiredLevel, float questRewardGold, float questRewardExp, Item questSpecialReward = null)
        {
            this.questTitle = title;
            this.questDescript = descript;
            this.requiredLevel = requiredLevel;
            this.questRewardGold = questRewardGold;
            this.questRewardExp = questRewardExp;
            this.questSpecialReward = questSpecialReward;
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
                Player.Instance.Inventory.Add(questSpecialReward);
                Console.WriteLine($"특별 보상으로 {questSpecialReward.Name}을 획득했습니다.\n");
            }
            Console.WriteLine($"퀘스트 '{questTitle}' 완료! 보상을 받았습니다.");
            System.Threading.Thread.Sleep(2000);
        }
        public void CheckCompletQuest()
        {
            if (!isCompleted)
            {
                if (Player.Instance.EquippedArmor != null || Player.Instance.EquippedWeapon != null)
                {
                    questList[0].isCompleted = true;
                    questList[0].CompleteQuest();
                }
                if (Player.Instance.Level >= 5)
                {
                    questList[1].isCompleted = true;
                    questList[1].CompleteQuest();
                }
                if (orcKillCount >= 3)
                {
                    questList[2].isCompleted = true;
                    questList[2].CompleteQuest();
                }
                if (dragonKillCount >= 1)
                {
                    questList[3].isCompleted = true;
                    questList[3].CompleteQuest();
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
