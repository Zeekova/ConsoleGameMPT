using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Player
    {
        public static string name;
        public static int dmg = 10, def = 5, health = 50, stamina = 100, maxhealth = 50, maxstamina = 100, startdmg = 10, startdef = 5, money = 35;
        public static int posX = 61, posY = 19, authority = 50, killedEnemies = 0;
        public static bool alive = true, armored = false, weaponed = false, helmeted = false, inventoryfull= false, Volkov = true, Bubushin = true, Musev = true, firstDialog = true, Abramov = true, final = false, firstReward = true;
        public static Armor equippedArmor;
        public static Weapon equippedWeapon;
        public static Helmet equippedHelmet;
        public static List<Item> items = new List<Item>();
        public static List<string> Attacks = new List<string> {"Быстрая атака", "Обычная атака", "Сильная атака"};

        public static void UseItem(IUsable UsedItem)
        {
            UsedItem.Use();
        }

        public static void Attack(IAttackable AttackedEntity, int Damage, int Stamina)
        {
            SoundPlayer punch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\удар.wav");
            punch.Play();
            Damage += dmg;
            stamina -= Stamina;
            if (stamina < 0) stamina = 0;
            Battle.ShowPlMenu();
            AttackedEntity.DealDamage(Damage);
            punch.Stop();
            punch.Dispose();
        }

        public static void JournalAttack(IAttackable AttackedEntity, int Damage, int Health)
        {
            SoundPlayer punch = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\удар.wav");
            punch.Play();
            Damage += dmg;
            health -= Health;
            Battle.ShowPlMenu();
            AttackedEntity.DealDamage(Damage);
            punch.Stop();
            punch.Dispose();
            if (health <= 0)
            {
                alive = false;
                SoundPlayer playerdeath = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\смерть_игрока.wav");

                health = 0;
                Die(playerdeath);
            }
        }

        public static void DealDamage(int Damage)
        {
            int TakenDamage = Damage - def;
            if (TakenDamage < 0) TakenDamage = 0;
            health -= TakenDamage;

            if (health < 0) health = 0;
            Battle.ShowPlMenu();

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            if (TakenDamage <= 0) SlowWrite($"Игрок {name} заблокировал атаку противника");
            else SlowWrite($"Игроку {name} нанесли урон в рамере {TakenDamage}");
            Console.ResetColor();
            Console.ReadKey(true);
            ClearDialogfield();

            if (health <= 0)
            {
                alive = false;
                SoundPlayer playerdeath = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\смерть_игрока.wav");
                Die(playerdeath);
            }
        }

        public static void Die(SoundPlayer sound)
        {
            Console.Clear();

            sound.PlayLooping();

            int centerX = (Console.WindowWidth / 2) - ("ВЫ ПРОИГРАЛИ".Length / 2);
            int centerY = Console.WindowHeight / 2 - 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(centerX + 1, centerY);
            Console.WriteLine("ВЫ ПРОИГРАЛИ");
            Console.ResetColor();
            Console.ReadKey(true);
            sound.Stop();
            sound.Dispose();
            Console.Clear();
        }

        public static void Move()
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("☻");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (posX > 0)
            {
                Console.SetCursorPosition(posX - 1, posY);
                Console.Write("+");
            }
            if (posX < Console.WindowWidth - 1)
            {
                Console.SetCursorPosition(posX + 1, posY);
                Console.Write("+");
            }
            if (posY > 0)
            {
                Console.SetCursorPosition(posX, posY - 1);
                Console.Write("+");
            }
            if (posY < Console.WindowHeight - 4)
            {
                Console.SetCursorPosition(posX, posY + 1);
                Console.Write("+");
            }
            Console.ResetColor();

            foreach (Object obj in Object.objects)
            {
                //сверху, снизу
                if (obj.posX == posX)
                {
                    if (obj.posY == posY - 1) Object.Draw(obj);
                    else if (obj.posY == posY + 1) Object.Draw(obj);
                }
                //слева, справа
                else if (obj.posY == posY)
                {
                    if (obj.posX == posX - 1) Object.Draw(obj);
                    else if (obj.posX == posX + 1) Object.Draw(obj);
                }
            }

            foreach (Boss b in Boss.BossList)
            {
                if (b.alive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(b.posX, b.posY);
                    Console.Write("¤");
                    Console.ResetColor();
                }
            }
        }

        public static bool IsNearEnemy()
        {
            foreach (Boss en in Boss.BossList)
            {
                if (((posX == en.posX - 1 && (posY == en.posY || posY == posY - 1 || posY == en.posY + 1)) || (posX == en.posX && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX + 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1))) && en.alive) return true;
            }
            return false;
        }

        public static Enemy NearestEnemy()
        {
            foreach (Boss en in Boss.BossList)
            {
                if (((posX == en.posX - 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX + 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1))) && en.alive) return en;
            }
            return null;
        }

        public static bool IsNearNPC()
        {
            foreach (NPC en in NPC.npcs)
            {
                if ((posX == en.posX - 1 && (posY == en.posY || posY == posY - 1 || posY == en.posY + 1)) || (posX == en.posX && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX + 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1))) return true;
            }
            return false;
        }

        public static NPC NearestNPC()
        {
            foreach (NPC en in NPC.npcs)
            {
                if ((posX == en.posX - 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1)) || (posX == en.posX + 1 && (posY == en.posY || posY == en.posY - 1 || posY == en.posY + 1))) return en;
            }
            return null;
        }

        public static bool IsInObject()
        {
            foreach (Object ob in Object.objects)
            {
                if (posX == ob.posX && posY == ob.posY) return true;
            }
            return false;
        }

        public static int ShowAttacksList(ref int cop)
        {
            cop = 1;

            SoundPlayer decision = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\меню.wav");
            SoundPlayer decaccept = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\подтверждение.wav");

            Console.SetCursorPosition(0, 6);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Attacks[0]);
            Console.ResetColor();
            Console.WriteLine(Attacks[1]);
            Console.Write(Attacks[2]);

            ConsoleKey k = 0;

            do
            {
                k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (cop > 1) cop--;
                            else cop = 3;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cop < 3) cop++;
                            else cop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.W:
                        {
                            if (cop > 1) cop--;
                            else cop = 3;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.S:
                        {
                            if (cop < 3) cop++;
                            else cop = 1;
                            decision.Play();
                            decision.Dispose();
                            break;
                        }

                    case ConsoleKey.Escape:
                        {
                            cop = 0;
                            break;
                        }

                    case ConsoleKey.Backspace:
                        {
                            cop = 0;
                            break;
                        }
                }

                switch (cop)
                {
                    case 1:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(Attacks[0]);
                            Console.ResetColor();
                            Console.WriteLine(Attacks[1]);
                            Console.Write(Attacks[2]); 
                            break;
                        }

                    case 2:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine(Attacks[0]);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(Attacks[1]);
                            Console.ResetColor();
                            Console.Write(Attacks[2]);
                            break;
                        }

                    case 3:
                        {
                            Console.SetCursorPosition(0, 6);
                            Console.WriteLine(Attacks[0]);
                            Console.WriteLine(Attacks[1]);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(Attacks[2]);
                            Console.ResetColor();
                            break;
                        }
                }
                if (cop == 0) break;
            }
            while (k != ConsoleKey.Enter && k != ConsoleKey.E);

            return cop;
        }
    }
}
