using System;
using TextRPG.Object;
using TextRPG.SaveDatas;

namespace TextRPG.Scenes
{
    public class SetupScene : Scene
    {
        public static Player player = Player.Instance;

        public override void ShowScene()
        {
            // 저장된 데이터 로드 시도
            var loadedPlayer = SaveData.LoadOrCreatePlayer();

            if (loadedPlayer == null)
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

                // 새 캐릭터 기본값 설정
                player.Name = playerName;
                player.JobName = jobName;

                Console.WriteLine("캐릭터가 생성되었습니다.");
            }
            else
            {
                // 저장된 데이터 적용
                player.ApplyLoadedData(loadedPlayer);
                Console.WriteLine("저장된 캐릭터 데이터를 불러왔습니다.");
            }

            // 초기화 (장비 등)
            player.Initialize();

            // 다음 씬으로 이동
            Program.ChangeScene(SceneType.MainScene);
        }
    }
}