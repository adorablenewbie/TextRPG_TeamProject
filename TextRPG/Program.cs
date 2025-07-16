using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Object;
using TextRPG.Scenes;
using TextRPG.SaveDatas;
using TextRPG.Items;


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
        private static Dictionary<SceneType, Scene> scenes;
        private static Scene currentScene;
        public static List<Skill> skillList;
        Player player = Player.Instance;

        public static void init()
        {
            scenes = new Dictionary<SceneType, Scene>();

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
    }
}