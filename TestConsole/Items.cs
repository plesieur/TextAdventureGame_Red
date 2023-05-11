using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Items
    {
        List<(string _item, string _description)> items = new List<(string, string)>
            {
                ("Suitcase", "Tattered brawny case with ripped leather"),
                ("Water", "Metallic flask with a wide openeing"),
                ("Flashlight", "A small heldheld flashlisht with a button the bottom"),
                ("Key", "dirty gold key with a 'J' engraved on it "),
                ("Glass bottle", "A brown blurry bottle with a 'Pawtucket pat' logo on the front"),
                ("Tree branch", "A mossy thick stick with a parting brach at the end."),
                ("Chest", "A dark wood box with a silver lock")
            };

        public class Healing
        {
            private string _name; 
            private int _effect;

            public Healing(string _name, int _effect)
            {
                this._name = _name;
                this._effect = _effect;
            }

            public string GetName()
            {
                return _name;
            }

            public int GetEffect()
            {
                return _effect;
            }
            public void Display()
            {
                Console.WriteLine("This item " + _name + ", has " + _effect + "healing points", _name, _effect);
            }
        }
    }
}



