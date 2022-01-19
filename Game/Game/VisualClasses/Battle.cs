using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Battle
    {
        public static bool started;
        private static int choosedOption;
        public static void ShowPlMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(Player.name);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Здоровье: {Player.health}/{Player.maxhealth}   ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Выносливость: {Player.stamina}/{Player.maxstamina}   ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"Защита: {Player.def}   ");
            Console.ResetColor();
        }

        public static void ShowEnMenu(Enemy en)
        {
            int rightX;
            string text;

            rightX = Console.WindowWidth - en.name.Length;
            Console.SetCursorPosition(rightX, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(en.name);
            text = $"   Здоровье: {en.health}/{en.maxhealth}";
            rightX = Console.WindowWidth - text.Length;
            Console.SetCursorPosition(rightX, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            text = $"   Защита: {en.def}";
            rightX = Console.WindowWidth - text.Length;
            Console.SetCursorPosition(rightX, 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ShowBossMenu(Boss en)
        {
            int rightX;
            string text;

            rightX = Console.WindowWidth - en.name.Length;
            Console.SetCursorPosition(rightX, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(en.name);
            text = $"   Здоровье: {en.health}/{en.maxhealth}";
            rightX = Console.WindowWidth - text.Length;
            Console.SetCursorPosition(rightX, 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            text = $"   Защита: {en.def}";
            rightX = Console.WindowWidth - text.Length;
            Console.SetCursorPosition(rightX, 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void Start(Enemy en)
        {
            SoundPlayer takepunch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\получение_удара.wav");
            SoundPlayer detection = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\обнаружение.wav");

            started = true;
            choosedOption = 1;

            detection.Play();
            detection.Dispose();
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.Green;
            SlowWrite($"На вас нападает {en.name}!");
            Console.ReadKey(true);
            Console.ResetColor();

            Console.Clear();

            switch (en.name)
            {
                case "Нилов":
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        SlowWrite("Нилов: И чем я всё это заслужил?");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        SlowWrite("Как хорошо тут было одному. Но тебе здесь не рады.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        Console.ReadKey(true);
                        Console.ResetColor();
                        break;
                    }

                case "Бу-Бушин":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        SlowWrite("БУ-Бушин: Оооооооо, новый студент)) Ты станешь отличным пополнением для моего журнала.");
                        Console.ReadKey(true);
                        Console.ResetColor();
                        break;
                    }

                case "МЫСев":
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        SlowWrite("МЫСев: Сидел себе, никого не трогал, и тут ты.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        SlowWrite("Давай просто побыстрей закончим.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        Console.ReadKey(true);
                        Console.ResetColor();
                        break;
                    }

                case "Абрамов":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        SlowWrite("Абрамов: НАША БИТВА БУДЕТ ЛЕГЕНДАРНОЙ!");
                        Console.ReadKey(true);
                        Console.ResetColor();
                        break;
                    }
            }

            Console.Clear();

            //отсюда и до вывода всех менюшек надо будет зациклить
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("________________________________________________________________________________________________________________________");
            
            do
            {
                ShowPlMenu();
                ShowEnMenu(en);
choose:
                ShowChooseMenu(ref choosedOption);

                switch (choosedOption)
                {
                    //Атаковать
                    case 1:
                        {
                            if (Player.stamina >= 10 || (Player.weaponed && Player.equippedWeapon.name == "Журнал смерти" && Player.health > 15))
                            {
                                ClearChooseMenu();
                                Player.ShowAttacksList(ref choosedOption);
                                switch (choosedOption)
                                {
                                    case 1:
                                        {
                                            if (Player.weaponed == true && Player.equippedWeapon.name == "Журнал смерти") Player.JournalAttack(en, 20, 15);
                                            else if (Player.stamina >= 10) Player.Attack(en, 10, 10);
                                            else
                                            {
                                                ClearChooseMenu();

                                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                SlowWrite("Вы пытаетесь атаковать, но вам не хватает сил");
                                                Console.ResetColor();
                                                Console.ReadKey(true);

                                                choosedOption = 1;
                                            }
                                            break;
                                        }

                                    case 2:
                                        {
                                            if (Player.weaponed == true && Player.equippedWeapon.name == "Журнал смерти") Player.JournalAttack(en, 25, 18);
                                            else if (Player.stamina >= 13) Player.Attack(en, 15, 13);
                                            else
                                            {
                                                ClearChooseMenu();

                                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                SlowWrite("Вы пытаетесь атаковать, но вам не хватает сил");
                                                Console.ResetColor();
                                                Console.ReadKey(true);

                                                choosedOption = 1;
                                            }
                                            break;
                                        }

                                    case 3:
                                        {
                                            if (Player.weaponed == true && Player.equippedWeapon.name == "Журнал смерти") Player.JournalAttack(en, 30, 20);
                                            else if (Player.stamina >= 15) Player.Attack(en, 20, 15);
                                            else
                                            {
                                                ClearChooseMenu();

                                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                SlowWrite("Вы пытаетесь атаковать, но вам не хватает сил");
                                                Console.ResetColor();
                                                Console.ReadKey(true);

                                                choosedOption = 1;
                                            }
                                            break;
                                        }
                                }
                                if (choosedOption == 0)
                                {
                                    SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

                                    decaccept.Play();
                                    decaccept.Dispose();
                                    choosedOption = 1;
                                    goto choose;
                                }
                                choosedOption = 1;
                            }
                            else
                            {
                                ClearChooseMenu();

                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                SlowWrite("Вы пытаетесь атаковать, но вам не хватает сил");
                                Console.ResetColor();
                                Console.ReadKey(true);
                                choosedOption = 1;
                            }
                            break;
                        }

                    //Защищаться
                    case 2:
                        {
                            ClearChooseMenu();

                            Console.SetCursorPosition(0, Console.WindowHeight - 2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            SlowWrite("Вы защищаетесь (защита +5)");
                            Console.ResetColor();

                            Player.def += 5;

                            ShowPlMenu();
                            Console.ReadKey(true);
                            break;
                        }

                    //Инвентарь
                    case 3:
                        {
                            Console.Clear();

                            Inventory.Choose();

                            ShowPlMenu();
                            ShowEnMenu(en);
                            
                            Console.SetCursorPosition(0, Console.WindowHeight - 3);
                            Console.Write("________________________________________________________________________________________________________________________");

                            choosedOption = 1;
                            goto choose;
                        }

                    //Бежать
                    case 4:
                        {
                            if (en is Boss)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                                SlowWrite("Бежать не получится");
                                Console.ResetColor();

                                Console.ReadKey(true);
                                ClearDialogfield();

                                choosedOption = 1;
                                goto choose;
                            }
                            else Leave(en);
                            break;
                        }
                }

                if (started == true)
                {
                    en.Attack(10);
                    if (choosedOption == 2) Player.def -= 5;
                    choosedOption = 1;
                    //ClearChooseMenu();
                }
            }
            while (started == true && en.alive == true && Player.alive == true);

            //Console.ReadKey(true);
        }

        public static int ShowChooseMenu(ref int cop)
        {
            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Выберите действие:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Атаковать                     ");
            Console.ResetColor();
            Console.WriteLine("Защищаться                 ");
            Console.WriteLine("Инвентарь                    ");
            Console.WriteLine("Бежать                                  ");

            ConsoleKey k = 0;

            do
            {
                k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (cop > 1) cop--;
                            else cop = 4;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cop < 4) cop++;
                            else cop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.W:
                        {
                            if (cop > 1) cop--;
                            else cop = 4;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (cop < 4) cop++;
                            else cop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }
                }

                switch (cop)
                {
                    case 1:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Атаковать");
                            Console.ResetColor();
                            Console.WriteLine("Защищаться");
                            Console.WriteLine("Инвентарь");
                            Console.Write("Бежать");
                            break;
                        }

                    case 2:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine("Атаковать");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Защищаться");
                            Console.ResetColor();
                            Console.WriteLine("Инвентарь");
                            Console.Write("Бежать");
                            break;
                        }

                    case 3:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine("Атаковать");
                            Console.WriteLine("Защищаться");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Инвентарь");
                            Console.ResetColor();
                            Console.Write("Бежать");
                            break;
                        }

                    case 4:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine("Атаковать");
                            Console.WriteLine("Защищаться");
                            Console.WriteLine("Инвентарь");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("Бежать");
                            Console.ResetColor();
                            Console.ResetColor();
                            break;
                        }
                }
            }
            while (k != ConsoleKey.Enter && k != ConsoleKey.E);

            decaccept.Play();
            decaccept.Dispose();

            return cop;
        }

        public static void ClearChooseMenu()
        {
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("                   ");
            Console.WriteLine("                       ");
            Console.WriteLine("                    ");
            Console.WriteLine("             ");
            Console.WriteLine("                                   ");
            Console.Write("                                          ");
        }

        public static void Leave(Enemy en)
        {
            Console.Clear();
            //Enemy.enemies.Remove(en);
            if (Player.authority > 0)Player.authority -= 5;
            started = false;
            choosedOption = 0;

            ShowMap();
        }
    }
}
