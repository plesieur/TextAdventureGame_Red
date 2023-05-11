using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;
using System.Threading;
using TextAdventureGameInputParser;

namespace TestConsole
{
    internal class Program
    {
        const bool DEBUG = true;
        const bool CHANGE = false;
        const int WEAPONINV = 1;
        private static string name = "";
        private static int knifeDur = 0;
        private static int clip;
        private static int ammo;
        private static void Main()
        {
            int MAX_INVENTORY = 5;
            var parser = CreateParser();
            Environment scene = new Environment();
            Sentence results;
            Player player1 = new Player(0);
            Enemies e = new Enemies();
            Weapons weapon = new Weapons();
            knifeDur = weapon.getDurability(1);
            clip = weapon.getClip(0);
            ammo = weapon.getAmmo(0);
            Console.WriteLine("Hello, welcome to our game.\nWhat is your character's name?");
            name = Console.ReadLine();
            if(name == "Mr. L")
            {
                player1.Health = 2147483647;
            }
            Console.WriteLine("Type 'help' or '?' for a list of commands\n");
            scene.CurrentRoom().lookCmd();
            do
            {
                Console.Write("\nParse what?> ");
                var input = Console.ReadLine() ?? "";
                Console.WriteLine();
                if (string.IsNullOrWhiteSpace(input)) 
                    return;
                results = parser.Parse(input);
                executeCommand(results, parser, scene, player1, weapon,MAX_INVENTORY);
            } while (true);
        }
        private static Parser CreateParser()
        {
            var parser = new Parser();

            parser.AddVerbs("GO", "OPEN", "CLOSE", "GIVE", "SHOW", "LOOK", "INVENTORY", "GET", "TAKE", "DROP", "USE", "EXAMINE", "HELP", "QUIT","DRINK","EAT");
            parser.AddVerbs("TAKE", "SEARCH", "ASK","FIGHT");
            parser.AddImportantFillers("TO", "ON", "IN");
            parser.AddUnimportantFillers("THE", "A", "AN", "AT");
            parser.AddNouns(
                "NORTH",
                "EAST",
                "WEST",
                "SOUTH",
                "UP",
                "DOWN",
                "CHEST",
                "FOOD",
                "WATER",
                "WEAPON",
                "Hunting Rifle",
                "Hunting Knife",
                "Fist",
                "Kick",
                "Metal Pipe",
                "Crowbar",
                "Rusty Scalpel",
                "Spongebob Knife",
                "Rusty Quarter",
                "Large Stick",
                "Large Rock",
                "Ammo",
                "Suitcase",
                "Flashlight"
            );
            parser.Aliases.Add("GO NORTH", "N", "NORTH");
            parser.Aliases.Add("GO EAST", "E", "EAST");
            parser.Aliases.Add("GO SOUTH", "S", "SOUTH");
            parser.Aliases.Add("GO WEST", "W", "WEST");
            parser.Aliases.Add("GO UP", "U", "UP");
            parser.Aliases.Add("GO DOWN", "D", "DOWN");
            parser.Aliases.Add("INVENTORY", "I", "INV");
            parser.Aliases.Add("HELP", "H", "?");
            parser.Aliases.Add("QUIT", "Q", "EXIT");
            parser.Aliases.Add("UNLOCK", "U", "OPEN");


            return parser;
        }



        private static void executeCommand(Sentence results, Parser parser, Environment scene, Player player1, Weapons weapon, int maxInv)
        {
            Battles battles = new Battles();
            if (DEBUG)
            {
                Console.WriteLine(results);   //print debug info about parsed sentence
            }
            if (!results.ParseSuccess)
            {
                Console.WriteLine("Excuse Me?");   //Did not recognize command
            }
            else if (results.Ambiguous)
            {
                Console.WriteLine("Be more specfic with {0}", results.Word4.Value.ToLower());
            }
            else
            {
                switch (results.Word1.Value)
                {
                    case "HELP":
                        Console.WriteLine("COMMANDS\n--------\n");
                        parser.PrintVerbs();
                        break;
                    case "QUIT":
                        Console.WriteLine("Ok, we won't miss {0}\n",name);
                        System.Environment.Exit(0);
                        break;
                    case "GO":
                        if (battles.enemyThere(player1) && !battles.done())
                        {
                            Console.WriteLine("{0} has to fight first",name);
                        }
                        else if (battles.gameOver(player1) && player1.Inventory.Contains("flashlight"))
                        {
                            Console.WriteLine("{0} shines their flashlight and cop sees them.  He drives over and takes them to the station.\nYOU WIN!!!", name);
                            Thread.Sleep(5000);
                            System.Environment.Exit(0);
                        }
                        else if (battles.gameOver(player1) && !player1.Inventory.Contains("Flashlight"))
                        {
                            Console.WriteLine("Man, if only I had a flashlight maybe someone could see me");
                            System.Environment.Exit(0);
                        }
                        else
                        {
                            Player.CurrentRoom.movement(results.Word4.Value, player1);
                            scene.CurrentRoom().lookCmd();
                            battles.changeDone();
                        }
                        break;
                    case "LOOK":
                        scene.CurrentRoom().lookCmd();
                        break;
                    case "INVENTORY":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("{0} is not carrying anything",name);
                        }
                        else
                        {
                            Console.WriteLine("{0} is carrying",name);
                            foreach (string item in player1.Inventory)
                            {
                                Console.WriteLine("  {0}", item);
                            }
                        }
                        break;
                    case "TAKE":
                        if (player1.Inventory.Contains("suitcase"))
                        {
                            maxInv = 10;
                            Console.WriteLine("The suitcase increases your inventory space to {0}\n",maxInv);
                        }
                        if (player1.Inventory.Count == maxInv)
                        {
                            Console.WriteLine("{0} cannot carry any more items\nDrop an item first",name);
                        }
                        else
                        {
                            string item = parseItem((results.Word4.Value).ToLower(),
                            results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            Player.CurrentRoom.take(item, player1);
                            Console.WriteLine("{0} can pick up {1} more items",name, (maxInv-player1.Inventory.Count));
                        }
                        break;
                    case "DROP":
                        if (player1.Inventory.Count == 0)
                        {
                            Console.WriteLine("{0} doesn't have any items to drop",name);
                        }
                        else
                        {
                            string item = parseItem((results.Word4.Value).ToLower(),
                                results.Word4.PrecedingAdjective == null ? null : (results.Word4.PrecedingAdjective.Value).ToLower());
                            Player.CurrentRoom.drop(item, player1);
                        }
                        break;
                    case "FIGHT":
                        Console.WriteLine("What would you like to use?");
                        weapon.printWeapons();
                        string weaponChoice = Console.ReadLine();
                        weaponChoice = weaponChoice.ToUpper();
                        switch (weaponChoice)
                        {

                            case "HUNTING RIFLE":
                                if (player1.Inventory.Contains("hunting rifle") && player1.Inventory.Contains("ammo"))
                                {
                                        if (clip <= 5 && clip > 0)
                                        {
                                            clip--;
                                            battles.who(0, player1, name);
                                        }

                                        if (clip == 0)
                                        {
                                            Console.WriteLine("{0} have an empty clip, please reload.", name);
                                            break;
                                        }
                                    }
                                else
                                {
                                    Console.WriteLine("{0} has to find the Hunting Rifle and Ammo", name);
                                }
                                break;
                            case "RELOAD":
                                if (player1.Inventory.Contains("ammo") && player1.Inventory.Contains("hunting rifle"))
                                {
                                    if (clip == 0)
                                    {
                                        while (clip <= 5)
                                        {
                                            clip++;
                                            ammo--;
                                        }
                                        Console.WriteLine("{0}'s clip is now {1} bullets.", name, clip);
                                        Console.WriteLine("{0}'s ammo is now {1} bullets.", name, ammo);
                                    }
                                }
                                break;
                            case "HUNTING KNIFE":
                                if (player1.Inventory.Contains("hunting knife"))
                                {
                                    if (knifeDur >= 0)
                                    {
                                        knifeDur--;
                                        battles.who(1, player1, name);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Weapon has been overused");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Hunting Knife", name);
                                }
                                break;
                            case "FIST":
                                battles.who(2, player1, name);
                                break;
                            case "KICK":
                                battles.who(3, player1, name);
                                break;
                            case "METAL PIPE":
                                if (player1.Inventory.Contains("metal pipe"))
                                {
                                    battles.who(4, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Metal Pipe", name);
                                }
                                break;
                            case "LARGE ROCK":
                                if (player1.Inventory.Contains("large rock"))
                                {
                                    battles.who(10, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Large Rock", name);
                                }

                                break;
                            case "LARGE STICK":
                                if (player1.Inventory.Contains("large stick"))
                                {
                                    battles.who(9, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Large Stick", name);
                                }
                                break;
                            case "RUSTY QUARTER":
                                if (player1.Inventory.Contains("rusty quarter"))
                                {
                                    battles.who(8, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Rusty Quarter", name);
                                }
                                break;
                            case "SPONGEBOB KNIFE":
                                if (player1.Inventory.Contains("spongebob knife"))
                                {
                                    battles.who(7, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Spongebob Knife", name);
                                }
                                break;
                            case "RUSTY SCALPEL":
                                if (player1.Inventory.Contains("rusty scalpel"))
                                {
                                    battles.who(6, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Rusty Scalpel", name);
                                }
                                break;
                            case "STALL":
                                Console.WriteLine("{0} is able to stall for a little bit",name);
                                break;
                            case "CROWBAR":
                                if (player1.Inventory.Contains("crowbar"))
                                {
                                    battles.who(5, player1, name);
                                }
                                else
                                {
                                    Console.WriteLine("{0} needs to find the Crowbar", name);
                                }
                                break;
                            default:
                                Console.WriteLine("Sorry, that doesn't exist");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("{0} doesn't understand",name);
                        break;

                }
            }

        }

        private static string parseItem(string noun, string adjective)
        {
            string item;

            if (adjective != null)
            {
                item = adjective + " " + noun;
            }
            else
            {
                item = noun;
            }
            return item;
        }



    }
}