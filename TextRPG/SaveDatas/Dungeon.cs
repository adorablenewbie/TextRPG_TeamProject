using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.Scenes;

namespace TextRPG.SaveDatas
{
    // 던전 종류를 정의하는 열거형
    public enum DungeonType
    {
        Forest,
        Cave,
        Castle,
        DragonLair
    }

    // 몬스터 강도
    public enum MonsterType
    {
        Weak,
        Normal,
        Strong
    }
    public class Dungeon
    {
        public static string GetDungeonName(DungeonType type) => type switch
        {
            DungeonType.Forest => "🌲 숲 던전",
            DungeonType.Cave => "🕳 동굴 던전",
            DungeonType.Castle => "🏰 성 던전",
            DungeonType.DragonLair => "🐉 드래곤 둥지",
            _ => "알 수 없음"
        };

        public static Skill UseSkill()
        {
            Console.WriteLine("어떤 스킬을 사용하겠습니까?\n");
            if (Player.Instance.EquippedSkills.Count == 0)
            {
                Console.WriteLine("장착된 스킬이 없습니다.\n");
            }
            else 
            {
                for (int i = 0; i < Player.Instance.EquippedSkills.Count; i++)
                {
                    string skillState = Player.Instance.EquippedSkills[i].ToString();
                    Console.WriteLine($"[{i + 1}] {skillState}");
                }
            }
            
            Console.WriteLine("[0] 이전으로");
            Console.Write("행동 번호를 입력하세요: ");
            
            int input;
            int.TryParse(Console.ReadLine(), out input);
            if (input < 0 || input > Player.Instance.EquippedSkills.Count)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                return UseSkill(); // 재귀 호출로 다시 입력 받기
            }
            else if (input == 0)
            {
                Console.WriteLine("이전으로 돌아갑니다.");
                return null; // 이전으로 돌아가기
            }
            Skill selectedSkill = Player.Instance.EquippedSkills[input - 1];
            return selectedSkill;
        }

        public static void MonsterTurn(List<Monster> mList, Player p)
        {
            if (mList.Count <= 0) return;

            foreach (Monster m in mList)
            {
                p.Hp -= m.BaseAttack;
                Console.WriteLine($"몬스터 {m.Name} 의 공격! 데미지 {m.BaseAttack}");
                Thread.Sleep(1000);
            }
        }

        public static void PlayerTurn(List<Monster> mList, int targetNumber)
        {
            if (mList.Count <= 0) return;
            int idx = targetNumber - 1;


            mList[idx].Hp -= Player.Instance.BaseAttack;

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
        public static List<Monster> CreateMonster()
        {
            List<Monster> mList = new();
            Random randomNum = new Random();
            int count = randomNum.Next(1, 5);

            for (int i = 1; i < count+1; i++)
            {
                Monster cloneMonster = Monster.monstersData[randomNum.Next(0, Monster.monstersData.Count)];
                mList.Add(cloneMonster.Clone());
            }
            return mList;
        }

        public static void SpawnMonster(List<Monster> mList)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                Console.WriteLine($"|{i + 1} {mList[i].Name} 몬스터 출현");
            }
        }

        public static void ChooseAction(List<Monster> spawnedMonster, DungeonType dungeonType)
        {
            ChooseReAction:
            int num = 0;
            string input = Console.ReadLine();
            if (int.TryParse(input, out num))
            {
                if (num == 1)
                {
                    int targetNumber = 0;
                    Console.WriteLine("공격할 몬스터 번호선택");
                    Console.WriteLine($"1~{spawnedMonster.Count}번까지의 몬스터를 선택하세요.");
                    int.TryParse(Console.ReadLine(), out targetNumber);
                    if (targetNumber >= 1 && targetNumber <= spawnedMonster.Count)
                    {
                        Dungeon.PlayerTurn(spawnedMonster, targetNumber);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                        goto ChooseReAction;
                    }
                }
                else if (num == 2)
                {
                    int targetNumber = 0;
                    Skill selectedSkill = Dungeon.UseSkill();
                    if (selectedSkill == null)
                    {
                        Console.WriteLine("이전으로 돌아갑니다.");
                        return; // 이전으로 돌아가기
                    }
                    Console.WriteLine("스킬을 사용할 대상 선택");
                    Console.WriteLine($"[1~{spawnedMonster.Count}]번까지의 몬스터를 선택하세요.");
                    Console.WriteLine($"[5] {Player.Instance.Name} (자신에게 사용)");
                    int.TryParse(Console.ReadLine(), out targetNumber);
                    if (targetNumber >= 1 && targetNumber <= spawnedMonster.Count || targetNumber == 5)
                    {
                        Dungeon.PlayerSkillTurn(spawnedMonster, targetNumber, selectedSkill);
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                        goto ChooseReAction;
                    }
                }
                else if (num == 3)
                {
                    Random random = new Random();
                    int escapeChance = random.Next(1, 101);
                    if (escapeChance <= 70) // 70% 확률로 도망 성공
                    {
                        Console.WriteLine("도망에 성공했습니다!");
                        Thread.Sleep(1000);
                        Console.WriteLine("엔터를 눌러서 다음 스테이지 진행");
                        Console.ReadLine();
                        DungeonScene.RandomStage(dungeonType);
                    }
                    else
                    {
                        Console.WriteLine("도망에 실패했습니다!");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.Write("당신은 머뭇거리다가 상대에게 틈을 내주었다..\n");
                    Thread.Sleep(1000);
                }
            }
            //몬스터의 턴
            Dungeon.MonsterTurn(spawnedMonster, Player.Instance);
            //결과
            //turn++;
        }
        
    }
}
