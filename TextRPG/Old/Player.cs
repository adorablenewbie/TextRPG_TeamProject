//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//namespace TextRPG.Old

//{
//    public class Player
//    {
//        public string Name { get; set; }
//        public string PlayerClass { get; set; }
//        public int Level { get; set; }
//        public float Exp { get; set; }
//        public float Hp { get; set; }
//        public float Attack { get; set; }
//        public float Defense { get; set; }
//        public float Gold { get; set; }
//        public List<Item> Inventory { get; set; }
//        public Item EquippedArmor { get; set; }
//        public Item EquippedWeapon { get; set; }

//        public Player(string name, string playerClass, int level, float exp, float hp, float attack, float defense, float gold)
//        {
//            Name = name;
//            PlayerClass = playerClass;
//            Level = level;
//            Exp = exp;
//            Hp = hp;
//            Attack = attack;
//            Defense = defense;
//            Gold = gold;
//            Inventory = new List<Item>();
//            EquippedArmor = null; // 장착된 갑옷 초기화
//            EquippedWeapon = null; // 장착된 무기 초기화
//        }

//        public void InitializeInventory()
//        {
//            if (Inventory == null)
//                Inventory = new List<Item>();
//        }

//        public void ShowStatus()
//        {

//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine($"[상태 보기]\n");
//                Console.WriteLine($"Lv. {Level}");
//                Console.WriteLine($"{Name} ({PlayerClass})");
//                Console.WriteLine($"공격력: {Attack + (EquippedWeapon != null ? EquippedWeapon.Attack : 0) + (EquippedArmor != null ? EquippedArmor.Attack : 0)} (+{(EquippedWeapon != null ? EquippedWeapon.Attack : 0) + (EquippedArmor != null ? EquippedArmor.Attack : 0)})");
//                Console.WriteLine($"방어력: {Defense + (EquippedWeapon != null ? EquippedWeapon.Defense : 0) + (EquippedArmor != null ? EquippedArmor.Defense : 0)}  (+{(EquippedWeapon != null ? EquippedWeapon.Defense : 0) + (EquippedArmor != null ? EquippedArmor.Defense : 0)})");
//                Console.WriteLine($"체력: {Hp}");
//                Console.WriteLine($"골드: {Gold}\n");

//                Console.WriteLine("0. 나가기\n");
//                Console.Write("원하시는 행동을 입력해주세요: ");
//                string input = Console.ReadLine();
//                if (input == "0")
//                {
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//                }
//            }
//        }

//        public void ShowInventory()
//        {


//            while (true)
//            {
//                Console.Clear();

//                Console.WriteLine("[인벤토리]");
//                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
//                Console.WriteLine("[아이템 목록]\n");

//                if (Inventory.Count == 0)
//                {
//                    Console.WriteLine("인벤토리가 비어 있습니다.");
//                }
//                else
//                {
//                    for (int i = 0; i < Inventory.Count; i++)
//                    {
//                        if (Inventory[i] == EquippedArmor || Inventory[i] == EquippedWeapon)
//                        {
//                            Console.WriteLine($"아이템 {i + 1}. [E]{Inventory[i].Name} - {Inventory[i]}");
//                        }
//                        else
//                        {
//                            Console.WriteLine($"아이템 {i + 1}. {Inventory[i].Name} - {Inventory[i]}");

//                        }
//                    }
//                }

//                Console.WriteLine("\n1. 장착관리");
//                Console.WriteLine("0. 나가기\n");
//                Console.Write("원하시는 행동을 입력해주세요: ");
//                string input = Console.ReadLine();
//                if (input == "0")
//                {
//                    break;
//                }
//                else if (input == "1")
//                {
//                    ShowItemEquip();
//                    break;

//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//                }
//            }
//        }
//        public void ShowItemEquip()
//        {
//            while (true)
//            {
//                Console.Clear();
//                Console.WriteLine("인벤토리 - 장착관리");
//                Console.WriteLine("보유 중인 아이템을 장착 혹은 탈착 할 수 있습니다.\n");
//                Console.WriteLine("[아이템 목록]\n");

//                if (Inventory.Count == 0)
//                {
//                    Console.WriteLine("인벤토리가 비어 있습니다.");
//                }
//                else
//                {
//                    for (int i = 0; i < Inventory.Count; i++)
//                    {
//                        if (Inventory[i] == EquippedArmor || Inventory[i] == EquippedWeapon)
//                        {
//                            Console.WriteLine($"아이템 {i + 1}. [E]{Inventory[i].Name} - {Inventory[i]}");
//                        }
//                        else
//                        {
//                            Console.WriteLine($"아이템 {i + 1}. {Inventory[i].Name} - {Inventory[i]}");

//                        }
//                    }
//                }

//                Console.WriteLine("\n0. 나가기\n");
//                Console.Write("원하시는 행동을 입력해주세요: ");
//                string input = Console.ReadLine();
//                if (int.TryParse(input, out int selected) && selected >= 1 && selected <= Inventory.Count)
//                {
//                    Item selectedItem = Inventory[selected - 1];
//                    if (selectedItem.Type == ItemType.Weapon)
//                    {
//                        if (selectedItem == EquippedWeapon)
//                        {
//                            Console.WriteLine("\n이미 장착된 무기입니다. 장착을 해제합니다.");
//                            EquippedWeapon = null; // 무기 장착 해제
//                        }
//                        else
//                        {
//                            Console.WriteLine($"\n{selectedItem.Name}을(를) 장착합니다.");
//                            EquippedWeapon = selectedItem; // 무기 장착
//                        }
//                    }
//                    else if (selectedItem.Type == ItemType.Armor)
//                    {
//                        if (selectedItem == EquippedArmor)
//                        {
//                            Console.WriteLine("\n이미 장착된 갑옷입니다. 장착을 해제합니다.");
//                            EquippedArmor = null; // 갑옷 장착 해제
//                        }
//                        else
//                        {
//                            Console.WriteLine($"\n{selectedItem.Name}을(를) 장착합니다.");
//                            EquippedArmor = selectedItem; // 갑옷 장착
//                        }
//                    }
//                    else
//                    {
//                        Console.WriteLine("\n장착을 해제합니다.");
//                    }
//                }
//                else if (input == "0")
//                {
//                    ShowInventory();
//                    break;
//                }
//                else
//                {
//                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
//                }
//            }
//        }
//        public void AddItem(Item item)
//        {
//            Inventory.Add(item);
//        }
//        public void RemoveItem(Item selectItem)
//        {
//            Inventory.Remove(Inventory.Find(item => item.Name == selectItem.Name));
//        }

//        public void RestoreEquippedItems()
//        {
//            if (EquippedArmor != null)
//                EquippedArmor = Inventory.Find(item => item.Name == EquippedArmor.Name);
//            if (EquippedWeapon != null)
//                EquippedWeapon = Inventory.Find(item => item.Name == EquippedWeapon.Name);
//        }

//        public void RestoreHealth()
//        {
//            Random random = new Random();
//            int hpRestore = random.Next(50, 100);
//            Hp += hpRestore;
//            Console.WriteLine($"\n체력이 {hpRestore} 회복되었습니다.\n");
//        }
//        public float SumAttack()
//        {
//            float totalAttack = Attack + (EquippedWeapon != null ? EquippedWeapon.Attack : 0) + (EquippedArmor != null ? EquippedArmor.Attack : 0);
//            return totalAttack;
//        }
//        public float SumDefense()
//        {
//            float totalDefense = Defense + (EquippedWeapon != null ? EquippedWeapon.Defense : 0) + (EquippedArmor != null ? EquippedArmor.Defense : 0);
//            return totalDefense;
//        }
//        public void LevelUp()
//        {
//            float requireExp = Level * 100 * (Level / 3 + 1);
//            if (requireExp <= Exp)
//            {
//                Level++;
//                Exp = Exp - requireExp;
//                Hp += 50; // 레벨업 시 체력 증가
//                Attack += 0.5f;
//                Defense += 1;
//                Console.WriteLine($"\n{Level}레벨로 상승했습니다! 축하합니다!\n");
//                Thread.Sleep(1000);
//            }

//        }
//    }

//}
