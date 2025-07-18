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
        QuestScene,
        DungeonScene,
    }
    internal class Program
    {
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
            scenes.Add(SceneType.QuestScene, new QuestScene());
            scenes.Add(SceneType.DungeonScene, new DungeonScene());
        }

        public static void Main(string[] args)
        {
            init();
            currentScene = scenes[SceneType.SetupScene]; // 첫 씬을 SetupScene으로 지정

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

        public static void ConsoleColorHelper(string text, ConsoleColor foreground, ConsoleColor background, bool line)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;

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


     }
}
