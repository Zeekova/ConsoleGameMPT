using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Items;

namespace Game
{
    class Program
    {
        public static string map, saveslot, DefaultPath;
        public static bool start;
        public static Random randAssault = new Random();
        static void Main(string[] args)
        {
            int centerX, centerY;
            DefaultPath = "C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game";

            SoundPlayer step = new SoundPlayer(DefaultPath + "\\Sounds\\шаг.wav");
            //SoundPlayer phonk = new SoundPlayer(DefaultPath + "\\Sounds\\phonk.wav");
            SoundPlayer punch = new SoundPlayer(DefaultPath + "\\Sounds\\удар.wav");
            SoundPlayer takepunch = new SoundPlayer(DefaultPath + "\\Sounds\\получение_удара.wav");
            SoundPlayer inventory = new SoundPlayer(DefaultPath + "\\Sounds\\инвентарь_переключение.wav");

            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.Title = "Дурка";

            if (!File.Exists(DefaultPath + "\\test.txt")) File.Create(DefaultPath + "\\test.txt").Close();

            StreamReader sr = new StreamReader(DefaultPath + "\\test.txt", Encoding.UTF8);
            map = sr.ReadToEnd();
            sr.Close();

            int x = 0, y = 0;
            foreach (char c in map)
            {
                switch (c)
                {
                    default:
                        {
                            x++;
                            break;
                        }

                    case '\r':
                        {
                            y++;
                            break;
                        }

                    case '\n':
                        {
                            x = 0;
                            break;
                        }

                    case '♣':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Green);
                            x++;
                            break;
                        }

                    case '♠':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.DarkGray);
                            x++;
                            break;
                        }

                    case '▒':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Blue);
                            x++;
                            break;
                        }

                    case '║':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }

                    case '═':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }

                    case '╔':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }

                    case '╗':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }

                    case '╚':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }

                    case '╝':
                        {
                            Object no = new Object(c, x, y, ConsoleColor.Gray);
                            x++;
                            break;
                        }
                }
            }

            if (!Directory.Exists(DefaultPath + "\\Saves")) Directory.CreateDirectory(DefaultPath + "\\Saves");
            for (int i = 0; i < 3; i++)
            {
                if (!File.Exists(DefaultPath + "\\Saves\\SaveSlot" + i + ".txt")) File.Create(DefaultPath + "\\Saves\\SaveSlot" + i + ".txt").Close();
            }

            Console.CursorVisible = false;
            NPC villager = new NPC("Старейшина", '☺', 59, 18, ConsoleColor.DarkYellow);

            do
            {
                Menu();
                while (start)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        //перемещение
                        case ConsoleKey.W:
                            {
                                Player.posY--;
                                if (Player.IsInObject()) Player.posY++;
                                if (Player.posY < 0) Player.posY = 0;
                                Player.Move();
                                step.Play();
                                step.Dispose();
                                if (!Battle.started) if (randAssault.Next(1, 100) == 1) Battle.Start(new Enemy("Негр-людоед", 25, 3));
                                if (Player.IsNearEnemy()) Battle.Start(Player.NearestEnemy());
                                break;
                            }

                        case ConsoleKey.A:
                            {
                                Player.posX--;
                                if (Player.IsInObject()) Player.posX++;
                                if (Player.posX < 0) Player.posX = 0;
                                Player.Move();
                                step.Play();
                                step.Dispose();
                                if (!Battle.started) if (randAssault.Next(1, 100) == 1) Battle.Start(new Enemy("Негр-людоед", 25, 3));
                                if (Player.IsNearEnemy()) Battle.Start(Player.NearestEnemy());
                                break;
                            }

                        case ConsoleKey.S:
                            {
                                Player.posY++;
                                if (Player.IsInObject()) Player.posY--;
                                if (Player.posY > Console.WindowHeight - 4) Player.posY = Console.WindowHeight - 4;
                                Player.Move();
                                step.Play();
                                step.Dispose();
                                if (!Battle.started) if (randAssault.Next(1, 100) == 1) Battle.Start(new Enemy("Негр-людоед", 25, 3));
                                if (Player.IsNearEnemy()) Battle.Start(Player.NearestEnemy());
                                break;
                            }

                        case ConsoleKey.D:
                            {
                                Player.posX++;
                                if (Player.IsInObject()) Player.posX--;
                                if (Player.posX > Console.WindowWidth - 1) Player.posX = Console.WindowWidth - 1;
                                Player.Move();
                                step.Play();
                                step.Dispose();
                                if(!Battle.started) if (randAssault.Next(1, 100) == 1) Battle.Start(new Enemy("Негр-людоед", 25, 3));
                                if (Player.IsNearEnemy()) Battle.Start(Player.NearestEnemy());
                                break;
                            }

                        //взаимодействие
                        case ConsoleKey.E:
                            {
                                if (Player.IsNearNPC()) NPC.StartDialog(Player.NearestNPC());
                                break;
                            }

                        //инвентарь
                        case ConsoleKey.I:
                            {
                                Inventory.Choose();
                                break;
                            }

                        case ConsoleKey.Escape:
                            {
                                PauseMenu();
                                break;
                            }
                    }
                    if (Player.alive == false) start = false;
                }
                 if (Player.alive == false)
                {
                    centerX = (Console.WindowWidth / 2) - ("Конец игры".Length / 2);
                    centerY = Console.WindowHeight / 2;
                    Console.SetCursorPosition(centerX + 1, centerY);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Конец игры");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
                else
                {
                    Console.Clear();
                    Console.ReadKey(true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    SlowWrite($"{Player.name}: Вот и всё.");
                    Thread.Sleep(200);
                    Console.WriteLine();

                    Console.ReadKey(true);
                    Console.Clear();

                    centerX = (Console.WindowWidth / 2) - ("ТЕПЕРЬ Я НОВЫЙ ПРЕПОД.".Length / 2);
                    centerY = Console.WindowHeight / 2;

                    Console.SetCursorPosition(centerX + 1, centerY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    SlowWrite("ТЕПЕРЬ ");
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.SetCursorPosition(centerX + 8, centerY);
                    SlowWrite("Я ");
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.SetCursorPosition(centerX + 10, centerY);
                    SlowWrite("НОВЫЙ ");
                    Thread.Sleep(500);
                    Console.WriteLine();
                    Console.SetCursorPosition(centerX + 16, centerY);
                    SlowWrite("ПРЕПОД.");

                    Console.ReadKey(true);
                    Console.Clear();

                    centerX = (Console.WindowWidth / 2) - ("Конец игры".Length / 2);
                    centerY = Console.WindowHeight / 2;
                    Console.SetCursorPosition(centerX + 1, centerY);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Конец игры");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }
            while (true);
        }

        public interface IAttackable
        {
            void DealDamage(int Damage);
            void Die(SoundPlayer sound);
        }

        public interface IUsable
        {
            void Use();
        }

        public static void SlowWrite(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10);
            }
        }

        public static void Menu()
        {
            int op, sop, centerX, centerY;
            bool end = true;

            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            string MenuText = "Дурка", StartGame = "Начать игру", Load = "Загрузить", Exit = "Выйти";

            centerY = Console.WindowHeight / 3 - 1;

            while (end)
            {
                op = 1; 
                sop = 1;

                Console.Clear();

                centerX = (Console.WindowWidth / 2) - (MenuText.Length / 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(MenuText);
                Console.WriteLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                centerX = (Console.WindowWidth / 2) - (StartGame.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(StartGame);
                Console.ResetColor();

                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 4);
                Console.WriteLine(Load);

                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 6);
                Console.WriteLine(Exit);

                ConsoleKey k = 0;

                do
                {
                    k = Console.ReadKey(true).Key;
                    switch (k)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (op > 1) op--;
                                else op = 3;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.DownArrow:
                            {
                                if (op < 3) op++;
                                else op = 1;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.W:
                            {
                                if (op > 1) op--;
                                else op = 3;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.S:
                            {
                                if (op < 3) op++;
                                else op = 1;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }
                    }

                    //курсор
                    switch (op)
                    {
                        case 1:
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (StartGame.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(StartGame);
                                Console.ResetColor();

                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Load);

                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                break;
                            }

                        case 2:
                            {
                                centerX = (Console.WindowWidth / 2) - (StartGame.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(StartGame);

                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Load);
                                Console.ResetColor();

                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                break;
                            }

                        case 3:
                            {
                                centerX = (Console.WindowWidth / 2) - (StartGame.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(StartGame);

                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Load);

                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                Console.ResetColor();
                                break;
                            }
                    }
                }
                while (k != ConsoleKey.Enter && k != ConsoleKey.E);

                decaccept.Play();
                decaccept.Dispose();

                switch (op)
                {
                    //Начало новой игры
                    case 1:
                        {
                            start = true;
                            //выбор слота сохранения для записи
                            SavesMenuStart(ref sop);
                            if (sop == 0)
                            {
                                sop++;
                                break;
                            }

                            string name;
                            do
                            {
                                Console.SetCursorPosition(0, Console.WindowHeight - 3);
                                Console.Write("________________________________________________________________________________________________________________________");

                                centerX = (Console.WindowWidth / 2) - ("Назови своё имя".Length / 2);
                                centerY = Console.WindowHeight / 3 - 1;
                                Console.SetCursorPosition(centerX, centerY);
                                Console.ForegroundColor = ConsoleColor.Green;
                                SlowWrite("Назови своё имя");
                                Console.ResetColor();
                                Console.SetCursorPosition(centerX - 5, centerY + 2);
                                Console.Write("-------------------------");
                                Console.SetCursorPosition(centerX - 5, centerY + 1);
                                name = Console.ReadLine();

                                Console.Clear();
                            }
                            while (name.Trim() == "" || name.Length > 25);

                            Player.name = name;
                            Player.maxhealth = Player.health;
                            Player.maxstamina = Player.stamina;
                            Player.startdmg = Player.dmg;
                            Player.startdef = Player.def;

                            Boss Volkov = new Boss("Нилов", 150, 5, 5, 31);
                            Boss BuBushin = new Boss("Бу-Бушин", 150, 5, 61, 1);
                            Boss Musev = new Boss("МЫСев", 150, 5, 110, 31);

                            ShowMap();

                            end = false;
                            break;
                        }

                    //Загрузка
                    case 2:
                        {
                            SavesMenu(ref sop);
                            if (sop == 0)
                            {
                                sop++;
                                break;
                            }
                            else //здесь будет считывание информации с выбранного сохранения
                            {
                                switch (sop)
                                {
                                    case 1:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot0.txt";
                                            break;
                                        }

                                    case 2:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot1.txt";
                                            break;
                                        }

                                    case 3:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot2.txt";
                                            break;
                                        }
                                }
                                LoadGame();
                                ShowMap();
                            }
                            end = false;
                            start = true;
                            break;
                        }

                    //Выход
                    case 3:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
             }
        }

        public static int SavesMenu(ref int sop)
        {
            Console.Clear();

            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            int centerX = (Console.WindowWidth / 2) - ("Сохранения".Length / 2);
            int centerY = Console.WindowHeight / 3 - 1;

            Console.SetCursorPosition(centerX, centerY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Сохранения");
            Console.ResetColor();

            string saves = "";
            List<string> fNames = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                FileInfo fi = new FileInfo("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Saves\\SaveSlot" + i);
                fNames.Add(fi.Name);
                saves += fi.Name + "    ";
            }
            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
            centerY = Console.WindowHeight / 3 + 1;
            Console.SetCursorPosition(centerX, centerY);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(fNames[0] + "     ");
            Console.ResetColor();
            Console.Write(fNames[1] + "     ");
            Console.Write(fNames[2] + "     ");

            ConsoleKey k = 0;

            do
            {
                k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (sop > 1) sop--;
                            else sop = 3;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            if (sop < 3) sop++;
                            else sop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.A:
                        {
                            if (sop > 1) sop--;
                            else sop = 3;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.D:
                        {
                            if (sop < 3) sop++;
                            else sop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.Backspace:
                        {
                            sop = 0;
                            decaccept.Play();
                            decaccept.Dispose();
                            break;
                        }

                    case ConsoleKey.Escape:
                        {
                            sop = 0;
                            decaccept.Play();
                            decaccept.Dispose();
                            break;
                        }
                }

                switch (sop)
                {
                    case 1:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[0] + "     ");
                            Console.ResetColor();
                            Console.Write(fNames[1] + "     ");
                            Console.Write(fNames[2] + "     ");
                            break;
                        }

                    case 2:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.Write(fNames[0] + "     ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[1] + "     ");
                            Console.ResetColor();
                            Console.Write(fNames[2] + "     ");
                            break;
                        }

                    case 3:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.Write(fNames[0] + "     ");
                            Console.Write(fNames[1] + "     ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[2] + "     ");
                            Console.ResetColor();
                            break;
                        }
                }
                if (sop == 0) break;
            }
            while (k != ConsoleKey.Enter && k != ConsoleKey.E);

            decaccept.Play();
            decaccept.Dispose();

            return sop;
        }

        public static int SavesMenuStart(ref int sop)
        {
            Console.Clear();

            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            int centerX = (Console.WindowWidth / 2) - ("Выберите слот для сохранения".Length / 2);
            int centerY = Console.WindowHeight / 3 - 1;
            Console.SetCursorPosition(centerX, centerY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Выберите слот для сохранения");
            Console.ResetColor();

            string saves = "";
            List<string> fNames = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                FileInfo fi = new FileInfo("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Saves\\SaveSlot" + i);
                fNames.Add(fi.Name);
                saves += fi.Name + "    ";
            }
            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
            centerY = Console.WindowHeight / 3 + 1;
            Console.SetCursorPosition(centerX, centerY);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(fNames[0] + "     ");
            Console.ResetColor();
            Console.Write(fNames[1] + "     ");
            Console.Write(fNames[2] + "     ");

            ConsoleKey k = 0;

            do
            {
                k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (sop > 1) sop--;
                            else sop = 3;
                            decision.Play();
                            decision.Dispose();

                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            if (sop < 3) sop++;
                            else sop = 1;
                            decision.Play();
                            decision.Dispose();

                            break;
                        }

                    case ConsoleKey.A:
                        {
                            if (sop > 1) sop--;
                            else sop = 3;
                            decision.Play();
                            decision.Dispose();

                            break;
                        }

                    case ConsoleKey.D:
                        {
                            if (sop < 3) sop++;
                            else sop = 1;
                            decision.Play();
                            decision.Dispose();

                            break;
                        }

                    case ConsoleKey.Backspace:
                        {
                            sop = 0;
                            decaccept.Play();
                            decaccept.Dispose();
                            break;
                        }

                    case ConsoleKey.Escape:
                        {
                            sop = 0;
                            decaccept.Play();
                            decaccept.Dispose();
                            break;
                        }
                }

                switch (sop)
                {
                    case 1:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[0] + "     ");
                            Console.ResetColor();
                            Console.Write(fNames[1] + "     ");
                            Console.Write(fNames[2] + "     ");
                            break;
                        }

                    case 2:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.Write(fNames[0] + "     ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[1] + "     ");
                            Console.ResetColor();
                            Console.Write(fNames[2] + "     ");
                            break;
                        }

                    case 3:
                        {
                            centerX = (Console.WindowWidth / 2) - (saves.Length / 2);
                            centerY = Console.WindowHeight / 3 + 1;
                            Console.SetCursorPosition(centerX, centerY);
                            Console.Write(fNames[0] + "     ");
                            Console.Write(fNames[1] + "     ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(fNames[2] + "     ");
                            Console.ResetColor();
                            break;
                        }
                }
                if (sop == 0) break;
            }
            while (k != ConsoleKey.Enter && k != ConsoleKey.E);

            decaccept.Play();
            decaccept.Dispose();

            switch (sop)
            {
                case 1:
                    {
                        saveslot = DefaultPath + "\\Saves\\SaveSlot0.txt";
                        break;
                    }

                case 2:
                    {
                        saveslot = DefaultPath + "\\Saves\\SaveSlot1.txt";
                        break;
                    }

                case 3:
                    {
                        saveslot = DefaultPath + "\\Saves\\SaveSlot2.txt";
                        break;
                    }
            }
            Console.Clear();

            return sop;
        }

        public static void PauseMenu()
        {
            Console.Clear();

            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");
            string Continue = "Вернуться", Load = "Загрузиться", Save = "Сохраниться", Exit = "Выйти";
            int centerX, centerY;
            bool stop = true;
            centerY = Console.WindowHeight / 3;
            do
            {
                centerX = (Console.WindowWidth / 2) - (Continue.Length / 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine(Continue);
                Console.WriteLine();
                Console.ResetColor();

                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 2);
                Console.WriteLine(Load);

                centerX = (Console.WindowWidth / 2) - (Save.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 4);
                Console.WriteLine(Save);

                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                Console.SetCursorPosition(centerX, centerY + 6);
                Console.WriteLine(Exit);

                int op = 1;

                ConsoleKey k = 0;

                do
                {
                    k = Console.ReadKey(true).Key;
                    switch (k)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                if (op > 1) op--;
                                else op = 4;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.DownArrow:
                            {
                                if (op < 4) op++;
                                else op = 1;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.W:
                            {
                                if (op > 1) op--;
                                else op = 4;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }

                        case ConsoleKey.S:
                            {
                                if (op < 4) op++;
                                else op = 1;
                                decision.Play();
                                decision.Dispose();
                                break;
                            }
                    }

                    //курсор
                    switch (op)
                    {
                        case 1:
                            {
                                centerX = (Console.WindowWidth / 2) - (Continue.Length / 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(centerX, centerY);
                                Console.WriteLine(Continue);
                                Console.WriteLine();
                                Console.ResetColor();

                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(Load);

                                centerX = (Console.WindowWidth / 2) - (Save.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Save);

                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                break;
                            }

                        case 2:
                            {
                                centerX = (Console.WindowWidth / 2) - (Continue.Length / 2);
                                Console.SetCursorPosition(centerX, centerY);
                                Console.WriteLine(Continue);
                                Console.WriteLine();

                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(Load);
                                Console.ResetColor();

                                centerX = (Console.WindowWidth / 2) - (Save.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Save);

                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                break;
                            }

                        case 3:
                            {
                                centerX = (Console.WindowWidth / 2) - (Continue.Length / 2);
                                Console.SetCursorPosition(centerX, centerY);
                                Console.WriteLine(Continue);
                                Console.WriteLine();

                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(Load);

                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (Save.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Save);
                                Console.ResetColor();

                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                break;
                            }

                        case 4:
                            {
                                centerX = (Console.WindowWidth / 2) - (Continue.Length / 2);
                                Console.SetCursorPosition(centerX, centerY);
                                Console.WriteLine(Continue);
                                Console.WriteLine();

                                centerX = (Console.WindowWidth / 2) - (Load.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 2);
                                Console.WriteLine(Load);

                                centerX = (Console.WindowWidth / 2) - (Save.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 4);
                                Console.WriteLine(Save);

                                Console.ForegroundColor = ConsoleColor.Green;
                                centerX = (Console.WindowWidth / 2) - (Exit.Length / 2);
                                Console.SetCursorPosition(centerX, centerY + 6);
                                Console.WriteLine(Exit);
                                Console.ResetColor();
                                break;
                            }
                    }
                }
                while (k != ConsoleKey.Enter && k != ConsoleKey.E);

                decaccept.Play();
                decaccept.Dispose();

                switch (op)
                {
                    //Возвращение на карту
                    case 1:
                        {
                            ShowMap();
                            stop = false;
                            break;
                        }

                    //Загрузка
                    case 2:
                        {
                            int sop = 1;
                            SavesMenu(ref sop);
                            if (sop == 0) Console.Clear();
                            else
                            {
                                switch (sop)
                                {
                                    case 1:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot0.txt";
                                            break;
                                        }

                                    case 2:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot1.txt";
                                            break;
                                        }

                                    case 3:
                                        {
                                            saveslot = DefaultPath + "\\Saves\\SaveSlot2.txt";
                                            break;
                                        }
                                }
                                LoadGame();
                                ShowMap();
                                stop = false;
                            }
                            break;
                        }

                    //Сохранение
                    case 3:
                        {
                            SaveGame();
                            //ShowMap();
                            break;
                        }

                    //Выход
                    case 4:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
            while (stop);
        }

        public static void ShowMap()
        {
            Console.Clear();
            int x = 0, y = 0, objnum = 0;

            foreach(char c in map)
            {
                switch (c)
                {
                    case '+':
                        {
                            Console.SetCursorPosition(x,y);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(c);
                            Console.ResetColor();
                            x++;
                            break;
                        }

                    case '\r':
                        {
                            y++;
                            break;
                        }

                    case '\n':
                        {
                            x = 0;
                            break;
                        }

                    default:
                        {
                            Object.Draw(Object.objects[objnum]);
                            objnum++;
                            x++;
                            break;
                        }
                }
            }

            foreach(Boss b in Boss.BossList)
            {
                if (b.alive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(b.posX, b.posY);
                    Console.Write("¤");
                    Console.ResetColor();
                }
            }

            foreach (NPC npc in NPC.npcs)
            {
                npc.draw();
            }

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("________________________________________________________________________________________________________________________");

            Console.SetCursorPosition(Player.posX, Player.posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("☻");
            Console.ResetColor();
        }

        public static void ClearDialogfield()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            //Console.Write("                                                                                                                                                                                                                ");
            Console.Write("                                                                                                          ");
            Console.Write("                                                                                                          ");
        }

        public static void SaveGame()
        {
            StreamWriter sw = new StreamWriter(saveslot, false, Encoding.UTF8);
            sw.WriteLine(Player.name);
            foreach(Item it in Player.items)
            {
                sw.Write(it.sym);
            }
            sw.WriteLine();
            sw.WriteLine($"{Player.dmg} {Player.def} {Player.health} {Player.stamina}");
            sw.WriteLine($"{Player.posX} {Player.posY} {Player.authority} {Player.killedEnemies}");
            sw.WriteLine($"{Player.Volkov} {Player.Bubushin} {Player.Musev} {Player.inventoryfull}");
            sw.WriteLine($"{Player.armored} {Player.weaponed} {Player.helmeted}");
            //12
            //int dmg, def, health, stamina, maxhealth, maxstamina, startdmg, startdef;
            //int posX, posY, authority, killedEnemies;
            //3
            //bool armored, weaponed, inventoryfull;
            if (Player.armored) sw.WriteLine(Player.equippedArmor.name);
            if (Player.weaponed) sw.WriteLine(Player.equippedWeapon.name);
            if (Player.helmeted) sw.WriteLine(Player.equippedHelmet.name);

            sw.WriteLine($"{Player.firstDialog} {Player.firstReward}");
            sw.WriteLine(Player.money);

            sw.Close();
            sw.Dispose();
        }

        public static void LoadGame()
        {
            FileInfo fi = new FileInfo(saveslot);
            if (fi.Length > 0)
            {
                StreamReader sr = new StreamReader(saveslot, Encoding.UTF8);
                Player.name = sr.ReadLine();
                string inv = sr.ReadLine();
                if (inv != "")
                {
                    Player.items.Clear();
                    foreach (char c in inv)
                    {
                        switch (c)
                        {
                            case '♥':
                                {
                                    Item.Add(new HealPotion("Малое зелье лечения", 20, '♥', @"Приятное на вкус!
Восстанавливает 20 ед. здоровья"));
                                    break;
                                }

                            case '░':
                                {
                                    Item.Add(new Armor("Лёгкая броня", 10, '░', @"Простой лёгкий доспех, не сковывает движений.
Увеличивает защиту на 10 ед."));
                                    break;
                                }

                            case '▓':
                                {
                                    Item.Add(new Armor("Тяжёлая броня", 20, '▓', @"Надёжная стальная броня, никогда не подведёт.
Увеличивает защиту на 20 ед."));
                                    break;
                                }

                            case '↓':
                                {
                                    Item.Add(new Weapon("Короткий меч", 5, '↓', @"Короткий железный меч, таких везде полно.
Увеличивает урон на 5 ед."));
                                    break;
                                }

                            case '⌂':
                                {
                                    Item.Add(new Armor("Корпус ПК", 30, '⌂', @"Это точно броня?
Хотя какая разница.
Одновременно лёгкий и прочный корпус. 
Защищает от большинства атак. 
Защита +30"));
                                    break;
                                }

                            case '†':
                                {
                                    Item.Add(new Weapon("Журнал смерти", 20, '†', @"Когда-то в него записывались имена студентов,
Но Бу-бушин искривил его сущность. 
Отныне он служит лишь для убийств.
Этот журнал дарует невероятную мощь.
Но за каждую силу есть своя цена...
Атаки журналом наносят больший урон и не тратят выносливость, 
Однако вы теряете здоровье, используя его. 
Урон +20"));
                                    break;
                                }

                            case '#':
                                {
                                    Item.Add(new Helmet("Сервер", 50, '#', @"Странный старый шлем.
Удивительно, но, когда вы его надеваете, ваши мысли проясняются.
Максимальное здоровье +50"));
                                    break;
                                }

                            case '♦':
                                {
                                    Player.items.Add(new StaminaPotion("Зелье выносливости", 20, '♦', @"Небольшое зелье восстановления выносливости.
Восстанавливает 20 ед выносливости."));
                                    break;
                                }
                        }
                    }
                }
                else Player.items.Clear();
                
                string[] stroke = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Player.dmg = Convert.ToInt32(stroke[0]);
                Player.def = Convert.ToInt32(stroke[1]);
                Player.health = Convert.ToInt32(stroke[2]);
                Player.stamina = Convert.ToInt32(stroke[3]);

                stroke = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Player.posX = Convert.ToInt32(stroke[0]);
                Player.posY = Convert.ToInt32(stroke[1]);
                Player.authority = Convert.ToInt32(stroke[2]);
                Player.killedEnemies = Convert.ToInt32(stroke[3]);

                stroke = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Player.Volkov = Convert.ToBoolean(stroke[0]);
                Player.Bubushin = Convert.ToBoolean(stroke[1]);
                Player.Musev = Convert.ToBoolean(stroke[2]);
                Player.inventoryfull = Convert.ToBoolean(stroke[3]);
                if (Player.Volkov == true)
                {
                    Boss Volkov = new Boss("Нилов", 150, 5, 5, 31);
                }
                if (Player.Bubushin == true)
                {
                    Boss BuBushin = new Boss("Бу-Бушин", 150, 5, 61, 1);
                }
                if (Player.Musev == true)
                {
                    Boss Musev = new Boss("МЫСев", 150, 5, 110, 31);
                }
                if (Player.Volkov == false && Player.Bubushin == false && Player.Musev == false) Player.final = true;

                stroke = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Player.armored = Convert.ToBoolean(stroke[0]);
                Player.weaponed = Convert.ToBoolean(stroke[1]);
                Player.helmeted = Convert.ToBoolean(stroke[2]);
                if (Player.armored)
                {
                    switch (sr.ReadLine())
                    {
                        case "Тяжёлая броня":
                            {
                                Player.equippedArmor = new Armor("Тяжёлая броня", 20, '▓', @"Надёжная стальная броня, никогда не подведёт.
Увеличивает защиту на 20 ед.");
                                break;
                            }

                        case "Лёгкая броня":
                            {
                                Player.equippedArmor = new Armor("Лёгкая броня", 10, '░', @"Простой лёгкий доспех, не сковывает движений.
Увеличивает защиту на 10 ед.");
                                break;
                            }

                        case "Корпус ПК":
                            {
                                Player.equippedArmor = new Armor("Корпус ПК", 30, '⌂', @"Это точно броня?
Хотя какая разница.
Одновременно лёгкий и прочный корпус. 
Защищает от большинства атак. 
Защита +30");
                                break;
                            }
                    }
                }
                if (Player.weaponed)
                {
                    switch (sr.ReadLine())
                    {
                        case "Короткий меч":
                            {
                                Player.equippedWeapon = new Weapon("Короткий меч", 5, '↓', @"Короткий железный меч, таких везде полно.
Увеличивает урона на 5 ед.");
                                break;
                            }

                        case "Журнал смерти":
                            {
                                Player.Attacks.Clear();
                                Player.Attacks.Add("Записать имя в журнал");
                                Player.Attacks.Add("Ща те нб поставлю");
                                Player.Attacks.Add("Пробить стол");
                                Player.equippedWeapon = new Weapon("Журнал смерти", 20, '†', @"Когда-то в него записывались имена студентов,
Но Бу-бушин искривил его сущность. 
Отныне он служит лишь для убийств.
Но за каждую силу есть своя цена...
Атаки журналом наносят больший урон и не тратят выносливость, 
Однако вы теряете здоровье, используя его. 
Урон +20");
                                break;
                            }
                    }
                }
                if (Player.helmeted)
                {
                    switch (sr.ReadLine())
                    {
                        case "Сервер":
                            {
                                Player.equippedHelmet = new Helmet("Сервер", 50, '#', @"Странный старый шлем.
Удивительно, но, когда вы его надеваете, ваши мысли проясняются
максимальное здоровье +50");
                                Player.maxhealth += 50;
                                break;
                            }
                    }
                }

                stroke = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Player.firstDialog = Convert.ToBoolean(stroke[0]);
                Player.firstReward = Convert.ToBoolean(stroke[1]);
                Player.money = Convert.ToInt32(sr.ReadLine());

                sr.Close();
                sr.Dispose();
            }
            else
            {
                Console.Clear();

                string name;
                do
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 3);
                    Console.Write("________________________________________________________________________________________________________________________");

                    int centerX = (Console.WindowWidth / 2) - ("Назови своё имя".Length / 2);
                    int centerY = Console.WindowHeight / 3 - 1;
                    Console.SetCursorPosition(centerX, centerY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    SlowWrite("Назови своё имя");
                    Console.ResetColor();
                    Console.SetCursorPosition(centerX - 5, centerY + 2);
                    Console.Write("-------------------------");
                    Console.SetCursorPosition(centerX - 5, centerY + 1);
                    name = Console.ReadLine();

                    Console.Clear();
                }
                while (name.Trim() == "" || name.Length > 25);
                Player.name = name;
            }
        }
    }
}
