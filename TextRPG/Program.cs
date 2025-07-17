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
        public void SavePlayer()
        {
            try
            {
                string json = JsonSerializer.Serialize(player);
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
    }
}