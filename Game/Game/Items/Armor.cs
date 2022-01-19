using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using static Game.Program;

namespace Game
{
    class Armor : Item, IUsable
    {
        int defence;
        SoundPlayer sound = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\броня.wav");

        public Armor(string name, int defence, char sym, string description) : base(name, sym, description)
        {
            this.name = name;
            this.defence = defence;
            this.sym = sym;
            this.description = description;
        }

        public new void Use()
        {
            if (Player.armored == false)
            {
                sound.Play();
                sound.Dispose();
                Player.def += defence;
                Player.armored = true;
                Player.equippedArmor = this;
                if (Player.items.Count > 0)
                {
                    //if (pl.items.First() == this) Inventory.choosedItem = 1;
                    //if (pl.items.Last() == this) Inventory.choosedItem = pl.items.Count - 1;
                    Inventory.choosedItem = 0;
                }
                Player.items.Remove(this);
            } 
            else
            {
                sound.Play();
                sound.Dispose();
                Player.items.Add(Player.equippedArmor);
                Player.def = Player.startdef;
                Player.def += defence;
                Player.equippedArmor = this;
                if (Player.items.Count > 0)
                {
                    //if (pl.items.First() == this) Inventory.choosedItem = 1;
                    //else if (pl.items.Last() == this) Inventory.choosedItem = pl.items.Count;
                    Inventory.choosedItem = 0;
                }
                Player.items.Remove(this);
            }
        }
    }
}
