using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scene
{
    public abstract class Scene
    {
        public Action action;
        public abstract void ShowScene();

        public void Show()
        {
            action?.Invoke();
        }
    }
}