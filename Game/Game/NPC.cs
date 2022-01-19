using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using static Game.Program;

namespace Game
{
    class NPC : Object
    {
        public static string name;
        public static List<NPC> npcs = new List<NPC>();
        public static List<Product> prods = new List<Product> 
        {
            new Product("Лёгкая броня", '░', @"Простой лёгкий доспех, не сковывает движений.
Увеличивает защиту на 10 ед.", 45),
            new Product("Тяжёлая броня",'▓', @"Надёжная стальная броня, никогда не подведёт.
Увеличивает защиту на 20 ед.", 85),
            new Product("Короткий меч", '↓', @"Короткий железный меч, таких везде полно.
Увеличивает урона на 5 ед.", 30),
            new Product("Малое зелье лечения", '♥', @"Приятное на вкус!
Восстанавливает 20 ед. здоровья", 15),
            new Product("Зелье выносливости", '♦', @"Небольшое зелье восстановления выносливости.
Восстанавливает 20 ед выносливости.", 20)
        };

        public NPC(string Name, char sym, int posX, int posY, ConsoleColor cl) : base (sym, posX, posY, cl)
        {
            name = Name;

            npcs.Add(this);
        }

        public static void StartDialog(NPC npc)
        {
            int ans = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;

            if (Player.Volkov == false && Player.Bubushin == false && Player.Musev == false) Player.final = true;

            if (Player.firstDialog)
            {
                SlowWrite($"{name}: О, наркоман-извращенец. Сколько ж вас ещё будет сюда приходить?");
                Thread.Sleep(200);
                Console.WriteLine();
                SlowWrite("Ладно, неважно. Добро пожаловать в наш посёлок городского типа МПТ.");
                Thread.Sleep(200);
                Console.WriteLine();
                SlowWrite("Ты здесь за тем, чтобы победить 'их', так ведь?");
                Thread.Sleep(200);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1 - Да");
                Console.WriteLine("2 - Нет");
                ConsoleKey k = 0;
                do
                {
                    k = Console.ReadKey(true).Key;
                }
                while (k != ConsoleKey.D1 && k != ConsoleKey.D2);
                if (k == ConsoleKey.D1) ans = 1;
                else if (k == ConsoleKey.D2) ans = 2;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Clear();
                switch (ans)
                {
                    case 1:
                        {
                            SlowWrite("Ну ещё бы. Все вы такие.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("В общем, тебе предстоит победить трёх местных преподов.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Пока никому не удавалось этого сделать. Это так, к слову.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Gray;
                            SlowWrite("Кхм-кхм. Так вот, если ты не ищешь неприятностей, то советую начать с Нилова.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Самый мирный из них всех. Говорят, ему в этом помогает какой-то странный шлем, быть может, ты сможешь его заполучить.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Чтоб добраться до него тебе предстоит пробраться через лес на юге.");
                            Console.ReadKey(true);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            SlowWrite("Следующий - МЫСев. Двигайся на восток к мысу и найдёшь его.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Вот только лучше к нему подготовься. Ни одно из наших оружий так и не смогло даже поцарапать его.");
                            Console.ReadKey(true);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            SlowWrite("И последний, самый опасный из всех - Бу-Бушин. Даже не думай, что сможешь победить его, не пройдя предыдущих.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("В отличие от Нилова и МЫСева, он утратил свою человечность и стал злым духом на кладбище.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Он гордился тем, что душнил студентов, так что на милость не рассчитывай.");
                            Console.ReadKey(true);
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            SlowWrite("И ещё. Тут ошиваются негры-людоеды, так что осторожней.");
                            Thread.Sleep(200);
                            Console.WriteLine();
                            SlowWrite("Что делать теперь - решай сам, это уже не моё дело.");
                            Console.ReadKey(true);
                            ShowMap();
                            break;
                        }

                    case 2:
                        {
                            SlowWrite("А, ну тогда пока.");
                            Console.ReadKey(true);
                            Environment.Exit(0);
                            break;
                        }
                }
                Console.ResetColor();
                Player.firstDialog = false;
            }
            else
            {
                if (Player.authority < 50 && !Player.final)
                {
                    SlowWrite($"{name}: Слышал тебя тут заставили побегать, ну ты чмошник, конечно.");
                    Thread.Sleep(200);
                    Console.WriteLine();
                    SlowWrite("Ты уверен, что справишься с Бу-Бушиным?))");
                    Console.WriteLine();
                    Console.ReadKey(true);
                }
                if (Player.killedEnemies >= 10 && Player.firstReward == true)
                {
                    SlowWrite($"{name}: Мне сказали, что ты помог разобраться с местными неграми. Спасибо");
                    Thread.Sleep(200);
                    Console.WriteLine();
                    SlowWrite("Вот твоя награда: 100 стипухи");
                    Player.money += 100;
                    Thread.Sleep(200);
                    Console.ReadKey(true);
                    Console.WriteLine();
                }
                if (!Player.final)
                {
                    SlowWrite($"{name}: Так что ты хотел?");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("1- Где я могу прикупить снаряжения?");
                    Console.WriteLine("2 - Напомни, где тут все боссы?");
                    ConsoleKey k = 0;
                    do
                    {
                        k = Console.ReadKey(true).Key;
                    }
                    while (k != ConsoleKey.D1 && k != ConsoleKey.D2);
                    if (k == ConsoleKey.D1) ans = 1;
                    else if (k == ConsoleKey.D2) ans = 2;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Clear();

                    switch (ans)
                    {
                        case 1:
                            {
                                SlowWrite("Вот это ты по адресу, выбирай, что приглянётся.");
                                Console.ReadKey(true);
                                Shop.Open();
                                ShowMap();
                                break;
                            }

                        case 2:
                            {
                                SlowWrite($"{name}: Эх, сколько ещё раз мне тебе повторять. Ты что дурачок?");
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Gray;
                                SlowWrite("Нилов живёт в уединении в лесу на юге.");
                                Thread.Sleep(200);
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                SlowWrite("МЫСев придаётся одиночеству на мысе к востоку от сюда.");
                                Thread.Sleep(200);
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Red;
                                SlowWrite("А Бу-бушин обитает на местном кладбище на севере.");
                                Thread.Sleep(200);
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Blue;
                                SlowWrite("В этот раз, пожалуйста, не забудь.");
                                Console.ReadKey(true);
                                ShowMap();
                                break;
                            }
                    }
                }
                else
                {
                    SlowWrite($"{name}: Что ж, похоже, ты действительно всех победил.");
                    Console.WriteLine();
                    Console.ReadKey(true);
                    name = "Абрамов";
                    Console.ForegroundColor = ConsoleColor.Red;
                    SlowWrite($"{name}: Почти всех.");
                    Console.ReadKey(true);
                    npcs.Remove(npc);
                    Boss Abramov = new Boss("Абрамов", 150, 5, 59, 18);
                    Console.Clear();
                    Battle.Start(Abramov);
                }
            }
            Console.ResetColor();
        }
    }
}
