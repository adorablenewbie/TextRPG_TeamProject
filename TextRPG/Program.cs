using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Old;
using TextRPG.Scene;

namespace TextRPG
{
    internal class Program
    {
        private Dictionary<string, TextRPG.Scene.Scene> scenes;
        private TextRPG.Scene.Scene currentScene;

        public void initScenes()
        {
            scenes.Add("StatusScene", new StatusScene());

        }

        public static void Main(string[] args)
        {
            
        }
    }
}