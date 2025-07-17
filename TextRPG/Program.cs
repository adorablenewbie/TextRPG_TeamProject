using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG.Items;
using TextRPG.Object;
using TextRPG.SaveDatas;
using TextRPG.Scenes;


namespace TextRPG
{
    public enum SceneType
    {
        SetupScene,
        MainScene,
        ShopScene,
        RestScene,
        StatusScene,
        SkillScene,
        InventoryScene,
        DungeonScene,
    }
    internal class Program
    {
        private const string SaveFileName = "player.json";
        private static Dictionary<SceneType, Scene> scenes;
        private static Scene currentScene;
        public static List<Skill> skillList;
        Player player = Player.Instance;

        public static void init()
        {
            scenes = new Dictionary<SceneType, Scene>();
            Shop.InitItems();
            scenes.Add(SceneType.SetupScene, new SetupScene());
            scenes.Add(SceneType.MainScene, new MainMenu());
            scenes.Add(SceneType.StatusScene, new StatusScene());
            scenes.Add(SceneType.RestScene, new RestScene());
            scenes.Add(SceneType.ShopScene, new ShopScene());
            scenes.Add(SceneType.SkillScene, new SkillScene());
            scenes.Add(SceneType.InventoryScene, new InventoryScene());
            scenes.Add(SceneType.DungeonScene, new DungeonScene());
        }

        public static void Main(string[] args)
        {
            init();
            currentScene = scenes[SceneType.MainScene];

            while (currentScene != null)
            {
                currentScene.ShowScene();
            }
        }

        public static void ChangeScene(SceneType sceneKey)
        {
            if (scenes.ContainsKey(sceneKey))
            {
                currentScene = scenes[sceneKey];
            }
            else
            {
                //ㅋㅋ 아무거나 써놔야지
            }
        }

        public void ConsoleColorHelper(string text, ConsoleColor color, bool line)
        {
            Console.ForegroundColor = color;
            if (line)
            {
                Console.Write(text);
            }
            else
            {
                Console.WriteLine(text);
            }
            Console.ResetColor();
        }
        public static void SavePlayer()
        {
            try
            {
                string json = JsonSerializer.Serialize(Player.Instance);
                File.WriteAllText(SaveFileName, json);
                Console.WriteLine("캐릭터가 저장되었습니다.");
                System.Threading.Thread.Sleep(1000);
            }
            catch
            {
                Console.WriteLine("캐릭터 저장에 실패했습니다.");
                System.Threading.Thread.Sleep(1000);
            }
        }
        public Player LoadOrCreatePlayer()
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
                                Console.WriteLine($"[{loaded.Name}] 캐릭터를 불러왔습니다.");
                                loaded.Initialize();
                                return loaded;
                            }
                        }
                        catch
                        {
                            Console.Clear();
                            Console.WriteLine("저장된 캐릭터를 불러오는 데 실패했습니다. 새로 만듭니다.");
                        }
                    }
                    else if (input == "n")
                    {
                        Console.Clear();
                        Console.WriteLine("새로운 캐릭터를 생성합니다.");
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
                    Console.Clear();
                    Console.WriteLine("새로운 캐릭터를 생성합니다.");
                    return null;
                }
            }
        }
     }
}