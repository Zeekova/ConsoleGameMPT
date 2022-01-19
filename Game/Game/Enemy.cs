using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Enemy : Entity, IAttackable
    {
        //public static List<Enemy> enemies = new List<Enemy>();
        public bool alive;
        public int maxhealth;

        public Enemy(string name, int health, int defence) : base (name, health, defence)
        {
            this.alive = true;

            this.name = name;

            this.maxhealth = health;

            this.health = health;
            this.def = defence;
            //this.posX = posX;
            //this.posY = posY;

            //enemies.Add(this);

            //Console.SetCursorPosition(posX, posY);
            //Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.Write("■");
            //Console.ResetColor();
        }
        public void Attack(int Damage)
        {
            SoundPlayer takepunch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\получение_удара.wav");
            takepunch.Play();
            Player.DealDamage(Damage);
            takepunch.Stop();
            takepunch.Dispose();
        }

        public void DealDamage(int Damage)
        {
            int TakenDamage = Damage - def;
            if (TakenDamage < 0) TakenDamage = 0;
            health -= TakenDamage;

            if (health < 0) health = 0;
            Battle.ShowEnMenu(this);

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.Green;
            if (TakenDamage <= 0) SlowWrite($"{name} заблокировал вашу атаку");
            else SlowWrite($"Враг {name} получил урон в размере {TakenDamage}");
            Console.ResetColor();
            Console.ReadKey(true);

            if (health <= 0)
            {
                alive = false;
                SoundPlayer enemydeath = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\смерть_врага.wav");
                if (this is Boss) Boss.Die(enemydeath, name);
                else Die(enemydeath);
            }
        }

        public void Die(SoundPlayer sound)
        {
            sound.Play();
            sound.Dispose();
            Random dengi = new Random();

            //enemies.Remove(this);
            Player.money += dengi.Next(20, 45);
            if (Player.authority < 100) Player.authority += 2;
            Player.killedEnemies++;
            Battle.started = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - "ВЫ ПОБЕДИЛИ".Length / 2, Console.WindowHeight / 2 - 1);
            Console.WriteLine("ВЫ ПОБЕДИЛИ");
            Console.ResetColor();

            Console.ReadKey(true);

            ShowMap();
        }
    }
}
