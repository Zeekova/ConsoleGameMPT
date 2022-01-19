using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using static Game.Program;

namespace Game
{
    class Weapon : Item, IUsable
    {
        int damage;
        SoundPlayer sound = new SoundPlayer("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\Папка\\Занятия\\2 курс\\ОАиП\\Практические\\Game\\Sounds\\оружие.wav");

        public Weapon(string name, int damage, char sym, string description) : base(name, sym, description)
        {
            this.name = name;
            this.damage = damage;
            this.sym = sym;
            this.description = description;
        }

        public new void Use()
        {
            if (Player.weaponed == false)
            {
                sound.Play();
                sound.Dispose();
                Player.dmg += damage;
                Player.weaponed = true;
                Player.equippedWeapon = this;
                if (Player.items.Count > 0)
                {
                    //if (pl.items.First() == this) Inventory.choosedItem = 1;
                    //if (pl.items.Last() == this) Inventory.choosedItem = pl.items.Count - 1;
                    Inventory.choosedItem = 0;
                }
                if (name == "Журнал смерти")
                {
                    Player.Attacks.Clear();
                    Player.Attacks.Add("Записать имя в журнал");
                    Player.Attacks.Add("Ща те нб поставлю"); 
                    Player.Attacks.Add("Пробить стол");
                }
                Player.items.Remove(this);
            }
            else
            {
                sound.Play();
                sound.Dispose();
                Player.items.Add(Player.equippedWeapon);
                if (Player.equippedWeapon.name == "Журнал смерти")
                {
                    Player.Attacks.Clear();
                    Player.Attacks.Add("Быстрая атака");
                    Player.Attacks.Add("Обычная атака");
                    Player.Attacks.Add("Сильная атака");
                }
                if (name == "Журнал смерти")
                {
                    Player.Attacks.Clear();
                    Player.Attacks.Add("Записать имя в журнал");
                    Player.Attacks.Add("Ща те нб поставлю");
                    Player.Attacks.Add("Пробить стол");
                }
                Player.dmg = Player.startdmg;
                Player.dmg += damage;
                Player.equippedWeapon = this;
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
