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
        private Dictionary<string, Scene> scenes;
        private Scene currentScene;

        public void initScenes()
        {
            scenes.Add("shopScene", new ShopScene());
            scenes.Add("StatusScene", new StatusScene());
            scenes.Add("MainScene", new MainMenu());
            scenes.Add("RestScene", new RestScene());
        }

        public static void Main(string[] args)
        {
            
        }
    }
}