using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG.Object;

namespace TextRPG.SaveDatas
{
    public class SaveData
    {
        private const string SaveFileName = "player.json";
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
        public static Player LoadOrCreatePlayer()
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
                    return null;
                }
            }
        }
    }
}
