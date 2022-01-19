using System;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game.Items
{
    class StaminaPotion : Item, IUsable
    {
        int staminaamount;
        SoundPlayer sound = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\зелье.wav");

        public StaminaPotion(string name, int staminaamount, char sym, string description) : base(name, sym, description)
        {
            this.name = name;
            this.staminaamount = staminaamount;
            this.sym = sym;
            this.description = description;
        }

        public new void Use()
        {
            sound.Play();
            Player.stamina += staminaamount;
            if (Player.stamina > Player.maxstamina) Player.stamina = Player.maxstamina;
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
