using System;
using System.Linq;
using System.Threading;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace TestConsole
{
    public class Battles
    {
        Player user;
        Enemies enemies = new Enemies();
        Weapons weapons = new Weapons();
        Room r = new Room();
        public static bool isDone = false;
        public static bool isEnemy;
        public void changeDone()
        {
            isDone = false;
        }
        public bool enemyThere(Player user1)
        {
            user = user1;
            Room curRoom = user.WhereAmI;
            switch (curRoom.Name)
            {
                case "Northern Woods":
                    isEnemy = true;
                    break;
                case "HR1":
                    isEnemy = true;
                    break;
                case "Eastern End":
                    isEnemy = true;
                    break;
                case "HR2":
                    isEnemy = true;
                    break;
                case "HR3":
                    isEnemy = true;
                    break;
                case "Southern Woods":
                    isEnemy = true;
                    break;
                case "Train Wreck":
                    isEnemy = true;
                    break;
                case "Signal Tower":
                    isEnemy = true;
                    break;
                default:
                    isEnemy = false;
                    break;
            }
            return isEnemy;
        }
        public bool gameOver(Player user1)
        {
            user = user1;
            Room curRoom = user.WhereAmI;
            if(curRoom.Name == "Crossing")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool done()
        {
            return isDone;
        }
        public void who(int item, Player user1,string who)
        {
            user = user1;
            int damage = weapons.getDamage(item);
            Room curRoom = user.WhereAmI;
            switch (curRoom.Name)
            {
                case "Northern Woods":
                    battle(0, damage,who);
                    break;
                case "Eastern End":
                    battle(6, damage,who);
                    break;
                case "HR1":
                    battle(4, damage,who);
                    break;
                case "HR2":
                    battle(3, damage,who);
                    break;
                case "HR3":
                    battle(1, damage,who);
                    break;
                case "Southern Woods":
                    battle(2, damage,who);
                    break;
                case "Train Wreck":
                    battle(5, damage,who);
                    break;
                case "Signal Tower":
                    battle(7, damage,who);
                    break;
                default:
                    Console.WriteLine("No enemies here");
                    break;
            }
        }
        public bool checkTurn(int turn)
        {
            bool playerTurn;
            if (turn % 2 == 0)
            {
                playerTurn = false;
            }
            else
            {
                playerTurn = true;
            }
            return playerTurn;

        } 
        public int attack(int damage, int health, int hit)
        {
            if (hit <= 20)
            {
                Console.WriteLine("Attack missed!");
            }
            else if (hit <= 50)
            {
                damage = damage / 2;
                health = health - damage;
                Console.WriteLine("Half Damage!");
            }
            else if (hit <= 90)
            {
                health = health - damage;
                Console.WriteLine("Full damage!");
            }
            else
            {
                health = (health - (damage * 2));
                Console.WriteLine("DOUBLE DAMAGE!!!");
            }
            return health;
        }
        public void battle(int theEnemy, int damage, string username)
        {
            isDone = false;
            string name = enemies.getName(theEnemy);
            int playerHealth = user.Health;
            int enemyDamage = enemies.getDamage(theEnemy);
            int turn = 0;
            int enemyHealth = enemies.getHealth(theEnemy);
            
            Random rnd = new Random();
            bool done = false;
            Console.WriteLine("Battle with {0}!",name);
            while (!done)
            {
                string[] dialogue = enemies.getDialogues(theEnemy);
                int which = rnd.Next(dialogue.Length);
                string saying = dialogue[which];
                Console.WriteLine("{0} yells {1}", name, saying);
                int hit = rnd.Next(0, 100);
                Thread.Sleep(2000);
                if (checkTurn(turn))
                {
                    Console.WriteLine("\n{0}'s turn",username);
                    enemyHealth = attack(damage, enemyHealth, hit);
                    Console.WriteLine("{0} health: {1}",name,enemyHealth);
                }
                else
                {
                    Console.WriteLine("\n{0}'s turn",name);
                    playerHealth = attack(enemyDamage, playerHealth, hit);
                    Console.WriteLine("{1} Health: {0}",playerHealth,username);
                }
                if (playerHealth < 0)
                {
                    Console.WriteLine("{1} was killed by {0}",name,username);
                    isDone = true;
                    System.Environment.Exit(0);
                }
                else if (enemyHealth < 0)
                {
                    Console.WriteLine("{1} killed {0}",name,username);
                    done = true;
                    isDone = true;
                }
                else
                {
                    done = false;
                    Console.WriteLine("The fight goes on");
                }
                turn++;
            }
        }
    }
}