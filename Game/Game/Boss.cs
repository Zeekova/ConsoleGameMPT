using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Boss : Enemy
    {
        public static List<Boss> BossList = new List<Boss>();
        public int posX, posY;

        public Boss(string name, int health, int defence, int posX, int posY) : base (name, health, defence)
        {
            this.alive = true;

            this.name = name;

            this.maxhealth = health;

            this.health = health;
            this.def = defence;
            this.posX = posX;
            this.posY = posY;

            BossList.Add(this);
        }

        public static void Die(SoundPlayer sound, string name)
        {
            Helmet helmet = new Helmet("Сервер", 50, '#', @"Странный старый шлем.
Удивительно, но, когда вы его надеваете, ваши мысли проясняются.
Максимальное здоровье +50");
            Weapon weapon = new Weapon("Журнал смерти", 20, '†', @"Когда-то в него записывались имена студентов,
Но Бу-бушин искривил его сущность. 
Отныне он служит лишь для убийств.
Этот журнал дарует невероятную мощь.
Но за каждую силу есть своя цена...
Атаки журналом наносят больший урон и не тратят выносливость, 
Однако вы теряете здоровье, используя его. 
Урон +20");
            Armor armor = new Armor("Корпус ПК", 30, '⌂', @"Это точно броня?
Хотя какая разница.
Одновременно лёгкий и прочный корпус. 
Защищает от большинства атак. 
Защита +30");

            sound.Play();
            sound.Dispose();
            Random dengi = new Random();

            //enemies.Remove(this);
            Player.money += dengi.Next(100, 248);
            if (Player.authority < 100) Player.authority += 2;
            Player.killedEnemies++;
            Battle.started = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - "ВЫ ПОБЕДИЛИ".Length / 2, Console.WindowHeight / 2 - 1);
            Console.WriteLine("ВЫ ПОБЕДИЛИ");
            Console.ResetColor();

            Console.ReadKey(true);

            Console.Clear();

            switch (name)
            {
                case "Нилов":
                    {
                        Player.Volkov = false;
                        Item.Add(helmet);

                        Console.ForegroundColor = ConsoleColor.Gray;
                        SlowWrite("Нилов: Ну и не очень-то хотелось.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        Console.ReadKey(true);
                        Console.ResetColor();

                        ShowMap();
                        break;
                    }

                case "Бу-Бушин":
                    {
                        Player.Bubushin = false;
                        Item.Add(weapon);

                        Console.ForegroundColor = ConsoleColor.Red;
                        SlowWrite("Бу-Бушин: Даже на том свете я дождусь твоих практических.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        Console.ReadKey(true);
                        Console.ResetColor();

                        ShowMap();
                        break;
                    }

                case "МЫСев":
                    {
                        Player.Musev = false;
                        Item.Add(armor);

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        SlowWrite("МЫСев: Ты тоже не задерживайся. Не люблю, когда опаздывают.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        Console.ReadKey(true);
                        Console.ResetColor();

                        ShowMap();
                        break;
                    }

                case "Абрамов":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        SlowWrite("Абрамов: Все равно бы не дожил до смены власти.");
                        Thread.Sleep(200);
                        Console.WriteLine();
                        SlowWrite("Удачи на дорогах.");
                        Console.ReadKey(true);
                        Console.ResetColor();
                        start = false;
                        break;
                    }
            }
        }
    }
}
