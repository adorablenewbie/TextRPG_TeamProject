using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.SaveData;

namespace TextRPG.Scene
{
    internal class SkillScene
    {
        static void ShowSkillUI()
        {
            Console.Clear();
            Console.WriteLine("스킬");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[스킬 목록]");

            for (int i = 0; i < skill.Count; i++) // 임시로 단어를 inventory에서 skill로 변경
            {
                int targetSkill = skill[i];

                string displayEquipped = equipList.Contains(targetSkill) ? "[E]" : "";
                Console.WriteLine($"- {displayEquipped} {skillNames[targetSkill]}  |  {(skillType[targetSkill] == 0 ? "공격력" : "방어력")} +{itemValue[targetItem]}  |  {itemDesc[targetItem]}");
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            string input = Console.ReadLine();

            switch (result)
            {
                case 0: // 메인 메뉴로 돌아가기
                    ShowMainMenu();
                    break;

                case 1: // 스킬 장착 관리 (미구현)
                    ShowEquipSkill();
                    break;
            }
        }
    }
}
