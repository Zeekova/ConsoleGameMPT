using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Helmet : Item, IUsable
    {
        int health;
        SoundPlayer sound = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\броня.wav");

        public Helmet(string name, int health, char sym, string description) : base(name, sym, description)
        {
            this.name = name;
            this.health = health;
            this.sym = sym;
            this.description = description;
        }

        public new void Use()
        {
            sound.Play();
            sound.Dispose();
            Player.maxhealth += health;
            Player.helmeted = true;
            Player.equippedHelmet = this;
            if (Player.items.Count > 0)
            {
                //if (pl.items.First() == this) Inventory.choosedItem = 1;
                //if (pl.items.Last() == this) Inventory.choosedItem = pl.items.Count - 1;
                Inventory.choosedItem = 0;
            }
            Player.items.Remove(this);
        }
    }
}
