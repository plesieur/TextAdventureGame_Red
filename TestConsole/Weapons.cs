using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TestConsole
{
    public class Weapons
    {
        private string _name;
        const int numWeapons = 12;
        Weapons[] weapons = new Weapons[numWeapons];
        private int _dmg;
        private int _ammo;
        private int _clip;
        private bool _isShootable;
        private int _shoot;
        private int _reload; 
        private int _maxClip;
        private int _durability;

        public Weapons()
        { 
            _name = "";
            _dmg = 0;
            _ammo = 0;
            _isShootable = false;
            _shoot = 0;
            _reload = 0;
            _maxClip = 5;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int Dmg
        {
            get
            {
                return _dmg;
            }
            set
            {
                _dmg = value;
            }
        }

        public int Ammo
        {
            get
            {
                return _ammo;
            }
            set
            {
                _ammo = value;
            }
        }

        public int Clip
        {
            get
            {
                return _clip;
            }
            set
            {
                _clip = value;
            }
        }

        public bool IsShootable
        {
            get
            {
                return _isShootable;
            }
            set
            {
                _isShootable = value;
            }
        }

        public int Shoot
        {
            get
            {
                return _shoot;
            }
            set
            {
                _clip = _clip - 1;
            }
        }

        public int Reload
        {
            get
            {
                return _reload;
            }
            set
            {
                if (_clip < _maxClip)
                {
                    _ammo = _ammo - _clip;
                    _clip = _maxClip;
                }
            }
        }

        public int Durability
        {
            get
            {
                return _durability;
            }
            set
            {
                _durability = value;
            }
        }

        public void Display(int item)
        {

            for (int i = 0; i < numWeapons; i++)
            {
                weapons[i] = new Weapons();
            }

            weapons[0].Name = "Hunting Rifle";
            weapons[0].Dmg = 50;
            weapons[0].Ammo = 30;
            weapons[0].Clip = 5;
            weapons[0].IsShootable = true;

            weapons[1].Name = "Hunting Knife";
            weapons[1].Dmg = 30;
            weapons[1].Durability = 40;
            weapons[1].IsShootable = false;

            weapons[2].Name = "Fist";
            weapons[2].Dmg = 10;
            weapons[2].IsShootable = false;

            weapons[3].Name = "Kick";
            weapons[3].Dmg = 15;
            weapons[3].IsShootable = false;

            weapons[4].Name = "Metal Pipe";
            weapons[4].Dmg = 20;
            weapons[4].Durability = 30;
            weapons[4].IsShootable = false;

            weapons[5].Name = "Crowbar";
            weapons[5].Dmg = 20;
            weapons[5].Durability = 30;
            weapons[5].IsShootable = false;

            weapons[6].Name = "Rusty Scalpel";
            weapons[6].Dmg = 10;
            weapons[6].Durability = 15;
            weapons[6].IsShootable = false;

            weapons[7].Name = "Spongebob Knife";
            weapons[7].Dmg = 1;
            weapons[7].Durability = 1000000;
            weapons[7].IsShootable = false;
        
            weapons[8].Name = "Rusty Quarter";
            weapons[8].Dmg = 0;
            weapons[8].IsShootable = false;

            weapons[9].Name = "Large Stick";
            weapons[9].Dmg = 2;
            weapons[9].Durability = 1;
            weapons[9].IsShootable = false;

            weapons[10].Name = "Large Rock";
            weapons[10].Dmg = 10;
            weapons[10].Durability = 16;
            weapons[10].IsShootable = false;
            
            weapons[11].Name = "Stall";
            weapons[11].Dmg = 0;
            weapons[11].Durability = 0;
            weapons[11].IsShootable = false;





        }
        public int getDamage(int item){Display(item);return weapons[item].Dmg;}
        public int getDurability(int item){Display(item);return weapons[item].Durability;}
        public int getClip(int item){return weapons[item].Clip;}
        public bool getShootable(int item){return weapons[item].IsShootable;}
        public int getAmmo(int item){return weapons[item].Ammo;}
        public void printWeapons()
        {
            for(int i=0; i< weapons.Length; i++)
            {
                Console.WriteLine(weapons[i].Name);
            }
            Console.WriteLine();
        }
    }
}
