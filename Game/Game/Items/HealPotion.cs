using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class HealPotion : Item, IUsable
    {
        int healamount;
        SoundPlayer sound = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\зелье.wav");

        public HealPotion(string name, int healamount, char sym, string description) : base(name, sym, description)
        {
            this.name = name;
            this.healamount = healamount;
            this.sym = sym;
            this.description = description;
        }

        public new void Use()
        {
            sound.Play();
            Player.health += healamount;
            if (Player.health > Player.maxhealth) Player.health = Player.maxhealth;
            if (Player.items.Count > 0)
            {
                //if (pl.items.First() == this) Inventory.choosedItem = 1;
                //else if (pl.items.Last() == this) Inventory.choosedItem = pl.items.Count - 1;
                Inventory.choosedItem = 0;
            }
            Player.items.Remove(this);
            sound.Dispose();
        }
    }
}
