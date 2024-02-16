using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSpawnTest.Components
{
    public class Weapon
    {
        public Weapon(string className, int atkBoost, int speedBoost, int healthBoost, bool bought, int price)
        {
            Class = className;
            AtkBoost = atkBoost;
            SpeedBoost = speedBoost;
            HealthBoost = healthBoost;
            Price = price;
            Bought = bought;
        }

        public int Price { get; set; }
        public bool Bought { get; set; }

        public string Class { get; set; }
        public int AtkBoost { get; set; }
        public int SpeedBoost { get; set; }
        public int HealthBoost { get; set;}
    }
}
