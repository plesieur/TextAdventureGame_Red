using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace TestConsole
{
    public class Room
    {
        private string _name;
        private string _description;
        private List<string> _items = new List<string>();
        private int[] _direction;

        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public List<string> Items
        {
            get { return _items; }
            set
            {
                foreach (string item in value)
                {
                    _items.Add(item.ToLower());
                }
            }
        }

        public int[] Dir
        {
            get { return _direction; }
            set
            {
                _direction = value;
            }
        }

        public void movement(string direction, Player player1)
        {
            List<string> directions = new List<string>() { "NORTH", "EAST", "SOUTH", "WEST", "UP", "DOWN" };
            int location = Player.RoomIndex;
            int dir = directions.IndexOf(direction);
            if (this.Dir[dir] != -1)
            {
                location = this.Dir[dir];
                Player.RoomIndex = location;
                Player.CurrentRoom = Environment.Scene[location];
            }
            else
            {
                Console.WriteLine("I don't know how to go that way!\n");
            }
        }

        public void take(string item, Player player1)
        {

            if (this.Items.Contains(item))
            {
                player1.Inventory.Add(item);
                this.Items.Remove(item);
                Console.WriteLine("You have taken {0}", item);
            }
            else
            {
                Console.WriteLine("I don't see the {0}", item);
            }

        }

        public void drop(string item, Player player1)
        {

            if (player1.Inventory.Contains(item))
            {
                player1.Inventory.Remove(item);
                this.Items.Add(item);
                Console.WriteLine("You have dropped the {0}", item);
            }
            else
            {
                Console.WriteLine("You don't have the {0}", item);
            }

        }

        public void lookCmd()
        {
            Console.WriteLine("You are in the {0}", _name);
            Console.WriteLine(_description + "\n");
            if (_items.Count > 0)
            {
                Console.Write("You see");
                foreach (string item in _items) { Console.Write("\n{0}", item); }
            }
        }


    }

    public class Environment
    {
        private static List<Room> _scene = new List<Room>();
        public Environment()
        {
            _scene = CreateRooms();
        }

        public static List<Room> Scene { get { return _scene; } }

        public Room CurrentRoom()
        {
            return _scene[Player.RoomIndex];
        }
        private static List<Room> CreateRooms()
        {
            List<Room> rooms = new List<Room>();

            rooms.Add(new Room()
            {
                Name = "Train Wreck",
                Description = "Your eyes open to find yourself in an overturned train car, none of the passengers, and a water bottle smacks the top of your head. After realization hits you hear,\" I just want a cupcake\".  IT'S EDP445!",
                Items = new List<string> {"suitcase", "Hunting Knife" },
                Dir = new int[6] { 1, 4, 7, 9, -1, -1 },

            });

            rooms.Add(new Room()
            {
                Name = "Northern Woods",
                Description = "You walk North of the wreck into the woods. As you make your way through the woods, you find yourself before a weird deer-like monster. It stares at you menacingly as it brings out its claws, what will you do?",
                Items = new List<string> { "Large Stick" },
                Dir = new int[6] { 2, 4, 0, 9, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Shack",
                Description = "You look around to see a hunting rifle leaning against the wall of the shack, it's loaded. There's a key laying on a table.",
                Items = new List<string> {"SpongeBob Knife", "Hunting Rifle", "ammo" },
                Dir = new int[6] { -1, 4, 1, 8, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Eastern End",
                Description = "You walk east along the railroad tracks for about a mile, until you come to a railroad crossing. Perpendicular to the crossing is a long bending road, no cars, North to the crossing is what seems to be an abandoned house.  You also see someone else.  IT'S MR. HAMILTON!",
                Items = new List<string> { },
                Dir = new int[6] { 4, -1, 7, 4, -1, -1 },
            });

            rooms.Add(new Room()
            {
                Name = "HR1",
                Description = "You enter the house through an open window.  You see a short man playing guilty gear on a PC.  IT'S JUAN CONTREZ!",
                Items = new List<string> { "Rusty Scalpel" },
                Dir = new int[6] { 1, 11, 11, 0, 5, 11 }
            });

            rooms.Add(new Room()
            {
                Name = "HR2",
                Description = "You've entered the second room, on the floor there appears to be a Med Kit and a BLT.  Then you see him, JARED FOGLE!",
                Items = new List<string> { "Med Kit"},
                Dir = new int[6] { 1, 11, 4, 0, -1, 4 }
            });

            rooms.Add(new Room()
            {
                Name = "HR3",
                Description = "You enter the third room and see a young pale female dressed in all black. She sees you and looks confused out of her mind. You A: ask who she is and what she's doing there; Or B: ask where exactly you are.",
                Items = new List<string> { "Crowbar" },
                Dir = new int[6] { 1, 4, 11, 0, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Southern Woods",
                Description = "You walk South of the wreck into the woods. As you make your way through the woods, you find yourself before a big black man named REQUIS, what will you do?",
                Items = new List<string> { "Tree branch", "Metal Pipe" },
                Dir = new int[6] { 0, 11, 10, 9, -1, -1 }
            });

            rooms.Add(new Room()
            {
                Name = "Western End",
                Description = "You walk west along the railroad tracks for about two miles, until you come to A signal tower. It seems to be non-functional and somewhat of a museum piece, however you spot someone inside.",
                Items = new List<string> { "Large Rock" },
                Dir = new int[6] { 9, 9, -1, -1, -1, -1 },
            });


            rooms.Add(new Room()
            {
                Name = "Signal Tower",
                Description = "You stumble upon a signal tower.  You walk inside and you see a drunk old man named Esteban!",
                Items = new List<string> {"Rusty Quarter" },
                Dir = new int[6] { 1, 0, 7, 8, -1, -1 }
            });
            rooms.Add(new Room()
            {
                Name = "Hunting Tower",
                Description = "You run towards a large tower, it seems to be a hunting tower.",
                Items = new List<string> { "Flashlight" },
                Dir = new int[6] { 7, 11, -1, -1, -1, -1 }
            });
            rooms.Add(new Room()
            {
                Name = "Crossing",
                Description = "You stumble across a railroad crossing.  This is the first time you've seen a road since you've entered the woods.",
                Items = new List<string> { },
                Dir = new int[6] { 1, 3, 7, 0, -1, -1 }
            });


            return rooms;
        }
    }

}