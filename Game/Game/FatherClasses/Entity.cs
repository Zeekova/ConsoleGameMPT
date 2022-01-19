using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Entity
    {
        public int health, dmg, def, stamina;
        public string name;

        public Entity(string name, int health, int defence)
        {
            this.name = name;
            this.health = health;
            this.def = defence;
        }

        public Entity(string Name, int Health, int Dmg, int Defence, int Stamina)
        {
            this.name = Name;

            this.health = Health;
            this.dmg = Dmg;
            this.def = Defence;
            this.stamina = Stamina;
        }
    }
}
