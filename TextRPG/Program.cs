using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Old;
using TextRPG.Scenes;

namespace TextRPG
{
    internal class Program
    {
        private static Dictionary<string, Scene> scenes;
        private static Scene currentScene;

        public static void initScenes()
        {
            scenes = new Dictionary<string, Scene>();

            scenes.Add("shopScene", new ShopScene());
            scenes.Add("StatusScene", new StatusScene());
            scenes.Add("MainScene", new MainMenu());
            scenes.Add("RestScene", new RestScene());
        }

        public static void Main(string[] args)
        {
            initScenes();
            currentScene = scenes["MainScene"];

        }
    }
}