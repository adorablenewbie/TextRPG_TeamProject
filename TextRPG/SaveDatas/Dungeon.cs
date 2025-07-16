using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;

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
            for (int i = 0; i < Player.Instance.EquippedSkills.Count; i++)
            {
                string skillState = Player.Instance.EquippedSkills[i].ToString();
                Console.WriteLine($"[{i + 1}] {skillState}");
            }
            Console.Write("스킬 번호를 입력하세요: ");
            int input;
            int.TryParse(Console.ReadLine(), out input);
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

            for (int i = 1; i < count; i++)
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

        public static void ChooseAction(string input, List<Monster> spawnedMonster)
        {
            int num = 0;

            if (int.TryParse(input, out num))
            {
                if (num == 1)
                {
                    int targetNumber = 0;
                    Console.Write("공격할 몬스터 번호선택");
                    if (int.TryParse(Console.ReadLine(), out targetNumber))
                    {
                        Dungeon.PlayerTurn(spawnedMonster, targetNumber);
                        Thread.Sleep(1000);
                    }
                }
                else if (num == 2)
                {
                    int targetNumber = 0;
                    Skill selectedSkill = Dungeon.UseSkill();
                    Console.WriteLine("스킬을 사용할 대상 선택");
                    Console.WriteLine($"[5] {Player.Instance.Name} (자신에게 사용)");
                    if (int.TryParse(Console.ReadLine(), out targetNumber))
                    {
                        Dungeon.PlayerSkillTurn(spawnedMonster, targetNumber, selectedSkill);
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(1000);
                }
                else if (num == 3)
                {
                    Console.WriteLine("도망갔습니다.");
                }
                else
                {
                    Console.Write("잘못된 입력입니다.");
                }
            }
            //몬스터의 턴
            Dungeon.MonsterTurn(spawnedMonster, Player.Instance);
            //결과
            //turn++;
        }
        
    }
}
