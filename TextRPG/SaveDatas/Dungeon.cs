using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TextRPG.Items;
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

        public static Usable UseBattleItem()
        {
            Console.WriteLine("어떤 아이템을 사용하겠습니까?\n");
            List<Usable> usables = Player.Instance.Inventory.OfType<Usable>().ToList();
            if (usables.Count <= 0)
            {
                Console.WriteLine("사용가능한 아이템이 없습니다");
            }
            else
            {
                for (int i = 0; i < usables.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {usables[i].Name} {usables[i].Description}");
                }
            }

            Console.WriteLine("[0] 이전으로");
            Console.Write("행동 번호를 입력하세요: ");

            int input;
            int.TryParse(Console.ReadLine(), out input);
            if (input < 0 || input > usables.Count)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                return UseBattleItem(); // 재귀 호출로 다시 입력 받기
            }
            else if (input == 0)
            {
                Console.WriteLine("이전으로 돌아갑니다.");
                return null; // 이전으로 돌아가기
            }
            Usable selectedItem = usables[input - 1];
            return selectedItem;
        }

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
                if (!m.IsDead)
                {
                    if (EffectCheck(m))
                    {
                        p.Hp -= m.BaseAttack;
                        Console.WriteLine($"몬스터 {m.Name} 의 공격! 데미지 {m.BaseAttack}");
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public static void PlayerTurn(List<Monster> mList, int targetNumber)
        {
            if (mList.Count <= 0) return;
            int idx = targetNumber - 1;

            if (mList[idx].IsDead)
            {
                Console.WriteLine("시체에 무기를 박고 말았다.");
                return;
            }

            int missChance = new Random().Next(0, 101);
            if(missChance < 10)
            {
                Console.WriteLine($"{mList[idx].Name}을 공격했지만 아무 일도 일어나지 않았습니다.");
            }
            else
            {
                mList[idx].Hp -= PlayerCritChance(PlayerDamageRange());

                //이곳에 몬스터 체력 몇 달았는지 적기
                Console.WriteLine($"{mList[idx].Name}의 남은 HP: {mList[idx].Hp:F0}");
                Thread.Sleep(500);
                if (mList[idx].Hp <= 0)
                {
                    Quest.CountKill(mList[idx]);
                    DungeonScene.Reward(mList[idx], Player.Instance);
                    mList[idx].IsDead = true;
                    //mList.RemoveAt(idx);
                }
            }
        }

        public static float PlayerDamageRange()
        {
            float interval = Player.Instance.TotalAttack * 0.1f;
            int damage = new Random().Next((int)Player.Instance.TotalAttack - (int)interval, (int)Player.Instance.TotalAttack + (int)interval);
            return (float)damage;
        }

        public static float PlayerCritChance(float damage)
        {
            int Random = new Random().Next(0, 101);
            if(Random < 16)
            {
                return damage * 1.6f;
            }
            return damage;
        }

        public static void PlayerSkillTurn(List<Monster> mList, int targetNumber, Skill selectedSkill)
        {
            if (mList.Count <= 0) return;
            int idx = targetNumber - 1;

            if (targetNumber == 5)
            {
                selectedSkill.UseSkill(selectedSkill, Player.Instance);
                Console.WriteLine($"{Player.Instance.Name}의 남은 HP: {Player.Instance.Hp}");
                return;
            }

            if (mList[idx].IsDead)
            {
                Console.WriteLine("시체에 스킬을 박고 말았다.");
                return;
            }else
            {
                selectedSkill.UseSkill(selectedSkill, mList[idx]);
                //이곳에 몬스터 체력 몇 달았는지 적기
                Console.WriteLine($"{mList[idx].Name}의 남은 HP: {mList[idx].Hp}");
                if (mList[idx].Hp <= 0)
                {
                    Quest.CountKill(mList[idx]);
                    DungeonScene.Reward(mList[idx], Player.Instance);
                    mList[idx].IsDead = true;
                    //mList.RemoveAt(idx);
                }
            }
            Thread.Sleep(500);
        }

        public static List<Monster> CreateMonster(DungeonType dungeonType)
        {
            List<Monster> mList = new();
            Random randomNum = new Random();
            int count;

            if (dungeonType == DungeonType.DragonLair)
            {
                List<Monster> dragon = Monster.monstersData.Where(m => m.DungeonType == DungeonType.DragonLair).ToList();
                return dragon;
            }
            else
            {
                var matchingMonsters = Monster.monstersData
                    .Where(m => m.DungeonType == dungeonType)
                    .ToList();

                count = randomNum.Next(1, 5);

                for (int i = 0; i < count; i++)
                {
                    if (matchingMonsters.Count == 0)
                        break;

                    int index = randomNum.Next(matchingMonsters.Count);
                    Monster selectedMonster = matchingMonsters[index];
                    mList.Add(selectedMonster.Clone());
                }
                return mList;
            }
        }
        

        public static void SpawnMonster(List<Monster> mList)
        {
            
            for (int i = 0; i < mList.Count; i++)
            {
                string log = $"|{i + 1} Lv.{mList[i].Level} {mList[i].Name} {(mList[i].IsDead ? "Dead" : mList[i].Hp)}";
                if (mList[i].IsDead)
                {
                    Program.ConsoleColorHelper(log, ConsoleColor.DarkGray, ConsoleColor.Black, false);
                }
                else
                {
                    Console.WriteLine(log);
                }
            }
        }

        public static bool MonsterClearCheck(List<Monster> mList)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                if (mList[i].IsDead)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static void AttackMonster(List<Monster> spawnedMonster)
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
                Console.WriteLine("당신은 허공에다가 무기를 휘둘렀다..");
            }
        }

        public static bool AttackMonsterSkill(List<Monster> spawnedMonster)
        {
            int targetNumber = 0;
            Skill selectedSkill = Dungeon.UseSkill();
            if (selectedSkill == null)
            {
                Console.WriteLine("이전으로 돌아갑니다.");
                return false; // 이전으로 돌아가기
            }

            Console.WriteLine("스킬을 사용할 대상 선택");
            Console.WriteLine($"[1~{spawnedMonster.Count}]번까지의 몬스터를 선택하세요.");
            Console.WriteLine($"[5] {Player.Instance.Name} (자신에게 사용)");
            int.TryParse(Console.ReadLine(), out targetNumber);
            if (targetNumber >= 1 && targetNumber <= spawnedMonster.Count || targetNumber == 5)
            {
                Dungeon.PlayerSkillTurn(spawnedMonster, targetNumber, selectedSkill);
                Thread.Sleep(1000);
                return true;
            }
            else
            {
                Console.WriteLine("당신은 허공에다가 스킬을 낭비했다..");
                Thread.Sleep(1000);
                return true;
            }
        }

        public static bool AttackMonsterItem(List<Monster> spawnedMonster)
        {
            int targetNumber = 0;
            Usable selectedItem = Dungeon.UseBattleItem();
            if (selectedItem == null) {
                Console.WriteLine("이전으로 돌아갑니다.");
                return false;
            }
            Console.WriteLine("아이템을 사용할 대상 선택");
            Console.WriteLine($"[1~{spawnedMonster.Count}]번까지의 몬스터를 선택하세요.");
            Console.WriteLine($"[5] {Player.Instance.Name} (자신에게 사용)");
            int.TryParse(Console.ReadLine(), out targetNumber);
            if (targetNumber >= 1 && targetNumber <= spawnedMonster.Count || targetNumber == 5)
            {
                Dungeon.PlayerItemTurn(spawnedMonster, targetNumber, selectedItem);
                Thread.Sleep(1000);
                return true;
            }
            else
            {
                Console.WriteLine("당신은 머뭇거리다 시간을 낭비했다..");
                Thread.Sleep(1000);
                return true;
            }
        }

        private static void PlayerItemTurn(List<Monster> mList, int targetNumber, Usable selectedItem)
        {
            if(mList.Count <= 0) return;
            int idx = targetNumber - 1;

            if (targetNumber == 5)
            {
                selectedItem.UseItem();
                Console.WriteLine($"{Player.Instance.Name}의 남은 HP: {Player.Instance.Hp}");
                return;
            }

            if (mList[idx].IsDead)
            {
                Console.WriteLine("시체에 아이템을 쓰고 말았다.");
                return;
            }
            else
            {
                selectedItem.UseItem(mList[idx]);
                //이곳에 몬스터 체력 몇 달았는지 적기
                Console.WriteLine($"{mList[idx].Name}의 남은 HP: {mList[idx].Hp}");
                if (mList[idx].Hp <= 0)
                {
                    Quest.CountKill(mList[idx]);
                    DungeonScene.Reward(mList[idx], Player.Instance);
                    mList[idx].IsDead = true;
                    //mList.RemoveAt(idx);
                }
            }
            Thread.Sleep(500);
        }

        public static bool RunbyMonster(DungeonType dungeonType)
        {
            Random random = new Random();
            int escapeChance = random.Next(1, 101);
            if (escapeChance <= 70) // 70% 확률로 도망 성공
            {
                Console.WriteLine("도망에 성공했습니다!");
                Thread.Sleep(1000);
                return true;
                //Console.WriteLine("엔터를 눌러서 다음 스테이지 진행");
                //Console.ReadLine();
                //DungeonScene.RandomStage(dungeonType);
            }
            else
            {
                Console.WriteLine("도망에 실패했습니다!");
                Thread.Sleep(1000);
                return false;
            }
        }

        public static void EffectAll(List<Monster> mList)
        {
            if (Player.Instance.HasEffect())
            {
                Console.WriteLine($"{Player.Instance.Name}이(가)");
                Player.Instance.ApplyEffect();
            }
            
            for (int i = 0; i < mList.Count; i++) {
                if (mList[i].HasEffect()) {
                    mList[i].ApplyEffect();
                    if (mList[i].Hp <= 0)
                    {
                        mList[i].IsDead = true;
                    }
                }
            }
        }

        public static bool EffectCheck(Status target)
        {
            if (target.HasEffect(Effects.Stun))
            {
                Console.Write($"{target}은 몸이 저릿해서 움직일 수 없다.");
                return false;
            }
            else if (target.HasEffect(Effects.Stun))
            {
                Console.Write($"{target}은 자고 있다.");
                return false;
            }
            return true;
        }

        public static bool ChooseAction(List<Monster> spawnedMonster, DungeonType dungeonType)
        {
            int num = 0;

            //플레이어의 턴
            string input = Console.ReadLine();
            if (int.TryParse(input, out num) && EffectCheck(Player.Instance))
            {
                if (num == 1)
                {
                    AttackMonster(spawnedMonster);
                }
                else if (num == 2)
                {
                    if (!AttackMonsterSkill(spawnedMonster))
                    {
                        return false;
                    }
                }else if(num == 3)
                {
                    if (!AttackMonsterItem(spawnedMonster))
                    {
                        return false;
                    }
                }
                else if (num == 4)
                {
                    if (RunbyMonster(dungeonType))
                    {
                        return true;
                    }
                }
                else
                {
                    Console.Write("당신은 머뭇거리다가 상대에게 틈을 내주었다..\n");
                    Thread.Sleep(1000);
                }
            }

            Player.Instance.LevelUpCheck();
            Thread.Sleep(500);

            //몬스터의 턴
            Dungeon.MonsterTurn(spawnedMonster, Player.Instance);
            if (Player.Instance.Hp <= 0)
            {
                Console.WriteLine("당신은 쓰러졌습니다. 게임 오버!");
                Thread.Sleep(2000);
                Environment.Exit(0); // 게임 종료
            }
            //결과
            Dungeon.EffectAll(spawnedMonster);
            return false;
            //turn++;
        }
    }
}
