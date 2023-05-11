using System;
using System.Collections.Generic;

namespace TestConsole
{
    public class Player
    {
        Weapons w = new Weapons();
        Room r = new Room();
        private static Room _currentRoom;
        private List<string> _inventory;
        private static int _roomIndex;
        private static int _playerHealth;
        public Player(int startRoom)
        {
            _playerHealth = 100;
            _roomIndex = startRoom;
            _inventory = new List<string>();
            _currentRoom = Environment.Scene[_roomIndex];
        }
        public static Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }
        public Room WhereAmI { get { return _currentRoom; } set { _currentRoom = value; } } 
        public List<string> Inventory { get { return _inventory; } }
        public int Health
        {
            get { return _playerHealth; }
            set { _playerHealth = value; }
        }
        public static int RoomIndex { get { return _roomIndex; } set { _roomIndex = value; } }
    }

}
