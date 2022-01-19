using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Shop
    {
        public static int choosedItem;
        public static void Open()
        {
            SoundPlayer inswitch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\инвентарь_переключение.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            int y = 6, x = 0;
            choosedItem = 0;
            ConsoleKey k = 0;

            do
            {
                if (NPC.prods.Count == 0)
                {
                    Show();
                    k = Console.ReadKey(true).Key;
                }
                else
                {
choose:
                    Show();
                    ChooseCell(x, y, NPC.prods[choosedItem].sym);
                    ClearName(NPC.prods[choosedItem].name, NPC.prods[choosedItem].description, NPC.prods[choosedItem].cost);

                    k = Console.ReadKey(true).Key;

                    switch (k)
                    {
                        case ConsoleKey.LeftArrow:
                            {
                                if (NPC.prods.Count > 0)
                                {
                                    x -= 5;
                                    choosedItem--;
                                    if (choosedItem < 0) choosedItem = NPC.prods.Count - 1;
                                    if (x < 0) x = (NPC.prods.Count - 1) * 5;
                                    ChooseCell(x, y, NPC.prods[choosedItem].sym);
                                    ClearName(NPC.prods[choosedItem].name, NPC.prods[choosedItem].description, NPC.prods[choosedItem].cost);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.RightArrow:
                            {
                                if (NPC.prods.Count > 0)
                                {
                                    x += 5;
                                    choosedItem++;
                                    if (choosedItem > NPC.prods.Count - 1) choosedItem = 0;
                                    if (x > (NPC.prods.Count - 1) * 5) x = 0;
                                    ChooseCell(x, y, NPC.prods[choosedItem].sym);
                                    ClearName(NPC.prods[choosedItem].name, NPC.prods[choosedItem].description, NPC.prods[choosedItem].cost);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.A:
                            {
                                if (NPC.prods.Count > 0)
                                {
                                    x -= 5;
                                    choosedItem--;
                                    if (choosedItem < 0) choosedItem = NPC.prods.Count - 1;
                                    if (x < 0) x = (NPC.prods.Count - 1) * 5;
                                    ChooseCell(x, y, NPC.prods[choosedItem].sym);
                                    ClearName(NPC.prods[choosedItem].name, NPC.prods[choosedItem].description, NPC.prods[choosedItem].cost);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.D:
                            {
                                if (NPC.prods.Count > 0)
                                {
                                    x += 5;
                                    choosedItem++;
                                    if (choosedItem > NPC.prods.Count - 1) choosedItem = 0;
                                    if (x > (NPC.prods.Count - 1) * 5) x = 0;
                                    ChooseCell(x, y, NPC.prods[choosedItem].sym);
                                    ClearName(NPC.prods[choosedItem].name, NPC.prods[choosedItem].description, NPC.prods[choosedItem].cost);
                                    inswitch.Play();
                                    inswitch.Dispose();
                                }
                                break;
                            }

                        case ConsoleKey.E:
                            {
                                Player.UseItem(NPC.prods[choosedItem]);
                                decaccept.Play();
                                decaccept.Dispose();
                                //choosedItem--;
                                if (NPC.prods.Count > 0)
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
                                Player.UseItem(NPC.prods[choosedItem]);
                                decaccept.Play();
                                decaccept.Dispose();
                                //choosedItem--;
                                if (NPC.prods.Count > 0)
                                {
                                    //if (x > 0) x -= 5;
                                    //else x = 0;
                                    x = 0;
                                    goto choose;
                                }
                                else Show();
                                break;
                            }
                    }
                }
            }
            while (k != ConsoleKey.Escape && k != ConsoleKey.Backspace);
            decaccept.Play();
            decaccept.Dispose();
        }
        private static void Show()
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

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("________________________________________________________________________________________________________________________");
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

            foreach (Item prod in NPC.prods)
            {
                DrawCell(x, y, prod.sym);
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

        public static void ClearName(string name, string description, int cost)
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
            Console.WriteLine(description);
            Console.WriteLine();
            Console.Write($"Стоимость: {cost}");
            Console.ResetColor();
        }
    }
}
