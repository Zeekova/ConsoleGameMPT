using System;
using static Game.Program;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Items;

namespace Game
{
    class Product : Item, IUsable
    {
        public int cost;

        public Product(string name, char sym, string description, int cost) : base(name, sym, description)
        {
            this.cost = cost;
        }

        public new void Use()
        {
            if (!Player.inventoryfull)
            {
                if (Player.money >= cost)
                {
                    switch (sym)
                    {
                        case '♥':
                            {
                                Player.items.Add(new HealPotion("Малое зелье лечения", 20, '♥', @"Приятное на вкус!
Восстанавливает 20 ед. здоровья"));
                                break;
                            }

                        case '░':
                            {
                                Player.items.Add(new Armor("Лёгкая броня", 10, '░', @"Простой лёгкий доспех, не сковывает движений.
Увеличивает защиту на 10 ед."));
                                break;
                            }

                        case '▓':
                            {
                                Player.items.Add(new Armor("Тяжёлая броня", 20, '▓', @"Надёжная стальная броня, никогда не подведёт.
Увеличивает защиту на 20 ед."));
                                break;
                            }

                        case '↓':
                            {
                                Player.items.Add(new Weapon("Короткий меч", 5, '↓', @"Короткий железный меч, таких везде полно.
Увеличивает урона на 5 ед."));
                                break;
                            }

                        case '♦':
                            {
                                Player.items.Add(new StaminaPotion("Зелье выносливости", 20, '♦', @"Небольшое зелье восстановления выносливости.
Восстанавливает 20 ед выносливости."));
                                break;
                            }
                    }
                    Player.money -= cost;
                    Shop.choosedItem = 0;
                }
                else
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Сначала накопи деньжат, ссаная нищенка.");
                    Console.ReadKey(true);
                    ClearDialogfield();
                    Console.ResetColor();
                }
            }
            else
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Сначала освободите инвентарь.");
                Console.ReadKey(true);
                ClearDialogfield();
                Console.ResetColor();
            }
        }
    }
}
