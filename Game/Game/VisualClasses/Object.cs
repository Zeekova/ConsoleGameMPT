using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Object
    {
        public static List<Object> objects = new List<Object>();
        public int posX, posY;
        char sym;
        ConsoleColor cl;

        public Object(char sym, int posX, int posY, ConsoleColor cl)
        {
            this.sym = sym;
            this.posX = posX;
            this.posY = posY;
            this.cl = cl;

            objects.Add(this);
        }

        public static void Draw(Object obj)
        {
            Console.SetCursorPosition(obj.posX, obj.posY);
            Console.ForegroundColor = obj.cl;
            Console.Write(obj.sym);
            Console.ResetColor();
        }

        public void draw()
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = cl;
            Console.Write(sym);
            Console.ResetColor();
        }
    }
}
