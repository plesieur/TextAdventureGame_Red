using System;
using System.Linq;
using System.Reflection.Metadata;

namespace TestConsole
{
    public class Enemies
    {
        const int numEnemies = 8;
        Enemies[] enemies = new Enemies[numEnemies];
        private string _dialogue1;
        private string _dialogue2;
        private string _dialogue3;
        private string _name;
        private int _health;
        private int _damage;
        public Enemies()
        {
            _dialogue1 = "";
            _dialogue2 = "";
            _dialogue3 = "";
            _name = "";
            _health = 0;
            _damage = 0;
        }
        public string Dialogue1
        {
            get { return _dialogue1; }
            set { _dialogue1 = value; }
        }
        public string Dialogue2
        {
            get { return _dialogue2; }
            set { _dialogue2 = value; }
        }
        public string Dialogue3
        {
            get { return _dialogue3; }
            set { _dialogue3 = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }
        public void create(int item)
        {
            for (int i = 0; i < numEnemies; i++)
            {
                enemies[i] = new Enemies();

            }
            enemies[0].Name = "Antler Men";
            enemies[0].Damage = 30;
            enemies[0].Health = 50;
            enemies[0].Dialogue1 = "DIE!";
            enemies[0].Dialogue2 = "Unga Bunga";
            enemies[0].Dialogue3 = "Hambrger, Cheeseburger, big mac, whopper";
            enemies[1].Name = "Jade";
            enemies[1].Damage = 50;
            enemies[1].Health = 100;
            enemies[1].Dialogue1 = "My pronouns are PLASTIC and BAD";
            enemies[1].Dialogue2 = "I identify as an ATTACK HELIMACHOPPER";
            enemies[1].Dialogue3 = "SATAN IS MY BOY TOY!!";
            enemies[2].Name = "REQUIS";
            enemies[2].Damage = 50;
            enemies[2].Health = 75;
            enemies[2].Dialogue1 = "puff puff SMASH!!";
            enemies[2].Dialogue2 = "Finger lickin' good";
            enemies[2].Dialogue3 = "FaZe Jev who??";
            enemies[3].Name = "Jared Fogle";
            enemies[3].Damage = 12;
            enemies[3].Health = 50;
            enemies[3].Dialogue1 = "Ya like sandwiches?";
            enemies[3].Dialogue2 = "I can pay you a little finders fee.";
            enemies[3].Dialogue3 = "I like a 6-inch turkey sub.";
            enemies[4].Name = "Juan Contrez";
            enemies[4].Damage = 25;
            enemies[4].Health = 100;
            enemies[4].Dialogue1 = "Brandon... stop";
            enemies[4].Dialogue2 = "Give me tacos";
            enemies[4].Dialogue3 = "Hop on guilty gear";
            enemies[5].Name = "EDP445";
            enemies[5].Damage = 5;
            enemies[5].Health = 5;
            enemies[5].Dialogue1 = "I just want a cupcake";
            enemies[5].Dialogue2 = "Ok this dude is mentally sick in the head.";
            enemies[5].Dialogue3 = "I have stage 5 kidney failure!";
            enemies[6].Name = "Mr. Hamilton";
            enemies[6].Damage = 20000;
            enemies[6].Health = 10000;
            enemies[6].Dialogue1 = "Elio get off of discord";
            enemies[6].Dialogue2 = "You're as dumb as a box of rocks";
            enemies[6].Dialogue3 = "You silly goose!";
            enemies[7].Name = "A Drunk old man named Esteban";
            enemies[7].Damage = 40;
            enemies[7].Health = 20;
            enemies[7].Dialogue1 = "Beer";
            enemies[7].Dialogue2 = "More beer";
            enemies[7].Dialogue3 = "You want some beer?";

        }
        public int getDamage(int item)
        {
            create(item);
            return enemies[item].Damage;
        }
        public int getHealth(int item)
        {
            create(item);
            return enemies[item].Health;
        }
        public string getName(int item)
        {
            create(item);
            return enemies[item].Name;
        }
        public string[] getDialogues(int item)
        {
            create(item);
            string dia1 = enemies[item].Dialogue1;
            string dia2 = enemies[item].Dialogue2;
            string dia3 = enemies[item].Dialogue3;
            string[] dialogues = { dia1, dia2, dia3 };
            return dialogues;
        }
    }

}


