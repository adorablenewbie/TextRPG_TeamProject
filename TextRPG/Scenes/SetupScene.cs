using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    public class SetupScene : Scene 
    {
        public static Object.Player player = Object.Player.Instance;
        public override void ShowScene()
        {
            player = SaveData.LoadOrCreatePlayer();

            if (player == null)
            {
                Console.Clear();
                Console.WriteLine("새 캐릭터를 생성합니다.");
                Console.WriteLine("캐릭터 이름을 입력해주세요.");
                string playerName = Console.ReadLine();

                string jobName = "";
                while (true)
                {
                    Console.WriteLine("캐릭터 직업을 선택해주세요.");
                    Console.WriteLine("1. 전사");
                    Console.WriteLine("2. 마법사");
                    Console.WriteLine("3. 궁수");
                    Console.WriteLine("4. 도적");
                    string jobChoice = Console.ReadLine();

                    switch (jobChoice)
                    {
                        case "1":
                            jobName = "전사";
                            break;
                        case "2":
                            jobName = "마법사";
                            break;
                        case "3":
                            jobName = "궁수";
                            break;
                        case "4":
                            jobName = "도적";
                            break;
                        default:
                            Console.WriteLine("올바른 번호를 입력해주세요.");
                            continue;
                    }
                    break;
                }

                // 캐릭터 정보 Player.Instance에 저장
                Player.Instance.Name = playerName;
                Player.Instance.JobName = jobName;
            }
            // 메인신으로 이동
            Program.ChangeScene(SceneType.MainScene);

        }
    }
}