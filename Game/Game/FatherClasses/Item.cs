using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Item : IUsable
    {
        public string name, description;
        public char sym;

        public Item(string name, char sym, string description)
        {
            this.name = name;
            this.sym = sym;
            this.description = description;
        }

        public static void Add(Item item)
        {
            if (Player.items.Count < 10) Player.inventoryfull = false;
            if (!Player.inventoryfull) Player.items.Add(item);
            else
            {
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.ForegroundColor = ConsoleColor.Green;
                SlowWrite("Инвентарь полон");
                Console.ResetColor();
                Console.ReadKey(true);
                ClearDialogfield();
            }
            if (Player.items.Count == 10) Player.inventoryfull = true;
        }

        public virtual void Use()
        {
            
        }
    }
}
