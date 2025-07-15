using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveDatas;
using TextRPG.Object;

namespace TextRPG.Scenes
{
    internal class SkillScene
    {
        public static Object.Player player = Object.Player.Instance;
        static void ShowSkill()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스킬");
                Console.WriteLine("보유 중인 스킬을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[스킬 목록]");

                ShowSkillList();

                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int result = CheckInput(0, 1);

                switch (result)
                {
                    case 0: // 메인 메뉴로 돌아가기
                        break;

                    case 1: // 스킬 장착 관리
                        ShowEquipSkill();
                        break;
                }
            }
        }

        static void ShowEquipSkill()
        {
            Console.Clear();
            Console.WriteLine("스킬 - 장착관리");
            Console.WriteLine("보유 중인 스킬을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            ShowSkillList();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = CheckInput(0, player.skills.Count); 

            switch (result) 
            {
                case 0:
                    ShowSkill();
                    break;

                default:

                    int skillIdx = result - 1;
                    Skill targetSkill = player.skills[skillIdx]; //이거 맞는지 검증해야됨 ~~~~~~~~~~~~~~~~~~~~~~(스킬 선택하면 맞게 착용되는지)
                    player.EquipSkill(targetSkill);

                    ShowEquipSkill();
                    break;
            }
        }

        static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public static void ShowSkillList()
        {
            for (int i = 0; i < player.skills.Count; i++)
            {
                Skill targetSkill = player.skills[i];
                string displayEquipped = player.skills.Contains(targetSkill) ? "[E]" : "";
                Console.WriteLine($"{i + 1}. {displayEquipped} {targetSkill.ToString()}");
            }
        }
    }
}
