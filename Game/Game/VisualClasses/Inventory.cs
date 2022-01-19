using System;
using System.Collections.Generic;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Inventory
    {
        public static int choosedItem;
        public static void Show()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Player.name);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Здоровье: {Player.health}/{Player.maxhealth}");//0, 0
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Выносливость: {Player.stamina}/{Player.maxstamina}");//0, 1
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Урон: {Player.dmg}");//0, 2
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Защита: {Player.def}");//0, 3
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Стипуха: {Player.money}");//0, 4
            Console.ResetColor();

            Console.SetCursorPosition(25, 1);
            Console.Write("                    ");
            Console.SetCursorPosition(25, 2);
            Console.Write("--------------------"); //при переключении между предметами надо сделать вывод имени над этим полем
        }

        public static void Choose()
        {
            SoundPlayer inswitch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\инвентарь_переключение.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            int y = 6, x = 0;
            choosedItem = 0;
            ConsoleKey k = 0;

            do
            {
                if (Player.items.Count == 0)
                {
                    Show();
                    k = Console.ReadKey(true).Key;
                }
                else
                {
choose:
                    Show();
                    ChooseCell(x, y, Player.items[choosedItem].sym);
                    ClearName(Player.items[choosedItem].name, Player.items[choosedItem].description);

                    k = Console.ReadKey(true).Key;

                    switch (k)
                    {
                        case ConsoleKey.LeftArrow:
                            {
                                if (Player.items.Count > 0)
                                {
                                    x -= 5;
                                    choosedItem--;
                                    if (choosedItem < 0) choosedItem = Player.items.Count - 1;
                                    if (x < 0) x = (Player.items.Count - 1) * 5;
                                    ChooseCell(x, y, Player.items[choosedItem].sym);
                                    ClearName(Player.items[choosedItem].name, Player.items[choosedItem].description);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.RightArrow:
                            {
                                if (Player.items.Count > 0)
                                {
                                    x += 5;
                                    choosedItem++;
                                    if (choosedItem > Player.items.Count - 1) choosedItem = 0;
                                    if (x > (Player.items.Count - 1) * 5) x = 0;
                                    ChooseCell(x, y, Player.items[choosedItem].sym);
                                    ClearName(Player.items[choosedItem].name, Player.items[choosedItem].description);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.A:
                            {
                                if (Player.items.Count > 0)
                                {
                                    x -= 5;
                                    choosedItem--;
                                    if (choosedItem < 0) choosedItem = Player.items.Count - 1;
                                    if (x < 0) x = (Player.items.Count - 1) * 5;
                                    ChooseCell(x, y, Player.items[choosedItem].sym);
                                    ClearName(Player.items[choosedItem].name, Player.items[choosedItem].description);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.D:
                            {
                                if (Player.items.Count > 0)
                                {
                                    x += 5;
                                    choosedItem++;
                                    if (choosedItem > Player.items.Count - 1) choosedItem = 0;
                                    if (x > (Player.items.Count - 1) * 5) x = 0;
                                    ChooseCell(x, y, Player.items[choosedItem].sym);
                                    ClearName(Player.items[choosedItem].name, Player.items[choosedItem].description);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.E:
                            {
                                Player.UseItem(Player.items[choosedItem]);
                                //choosedItem--;
                                if (Player.items.Count > 0)
                                {
                                    //if (x > 0) x -= 5;
                                    //else x = 0;
                                    x = 0;
                                    goto choose;
                                }
                                else Show();
                                break;
                            }

                        case ConsoleKey.Enter:
                            {
                                Player.UseItem(Player.items[choosedItem]);
                                //choosedItem--;
                                if (Player.items.Count > 0)
                                {
                                    //if (x > 0) x -= 5;
                                    //else x = 0;
                                    x = 0;
                                    goto choose;
                                }
                                else Show();
                                break;
                            }

                        case ConsoleKey.Q:
                            {
                                Player.items.RemoveAt(choosedItem);
                                decaccept.Play();
                                decaccept.Dispose();
                                if (Player.items.Count > 0)
                                {
                                    //if (x > 0) x -= 5;
                                    //else x = 0;
                                    choosedItem = 0;
                                    x = 0;
                                    goto choose;
                                }
                                else Show();
                                break;
                            }
                    }
                }
            }
            while (k != ConsoleKey.Escape && k != ConsoleKey.Backspace && k != ConsoleKey.I);
            decaccept.Play();
            decaccept.Dispose();

            if (Battle.started != true) Program.ShowMap();
            else Console.Clear();
        }

        public static void ChooseCell(int posX, int posY, char sym)
        {
            DrawInventory();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(posX, posY);
            Console.Write("╔═══╗");
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write("║ ");
            Console.Write(sym);
            Console.Write(" ║");
            Console.SetCursorPosition(posX, posY + 2);
            Console.Write("╚═══╝");
            Console.ResetColor();
        }

        public static void DrawInventory()
        {
            int x = 0, y = 6;

            foreach (Item it in Player.items)
            {
                DrawCell(x, y, it.sym);
                x += 5;
            }
        }

        public static void DrawCell(int posX, int posY, char c)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write("╔═══╗");
            Console.SetCursorPosition(posX, posY + 1);
            Console.Write($"║ {c} ║");
            Console.SetCursorPosition(posX, posY + 2);
            Console.Write("╚═══╝");
        }

        public static void ClearName(string name, string description)
        {
            Console.SetCursorPosition(25, 1);
            Console.Write("                    ");
            Console.SetCursorPosition(25, 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(name);
            Console.SetCursorPosition(0, 10);
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.Write("                                                                       ");
            Console.SetCursorPosition(0, 10);
            Console.Write(description);
            Console.ResetColor();
        }
    }
}
