using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EffectsPlayground;

namespace TextGameAttempt
{
    class Program
    {
        static IRoomRepository _roomRepository;
        static IEnemyRepository _enemyRepository;

        static void Main(string[] args)
        {
            _roomRepository = new MockRoomRepository();
            _enemyRepository = new MockEnemyRepository();
            
            GameLoop();
        }

        private static void GameLoop()
        {
            do
            {
                Console.Clear();
                Player player = InitializeGame(_roomRepository.AllRooms.ToList());
                ConsoleKey key;


                // MAP TEST
                MapVisualizer mapVisualizer = new MapVisualizer();
                MapVisualizer.PrintMap(player);
                // END MAP TEST


                TextEffects.TypewriteHumanized($"\n\n\n\t\t\tYou wake up in an unfamiliar place. You're inside an old home. Feels a bit like a haunted house in here...");
                TextEffects.Typewrite($"\t\t\tThis room seems to be {player.currentRoom.name}.");
                TextEffects.Typewrite($"\n\n\n\t\t\tMap coordinates: {player.currentRoom.x}, {player.currentRoom.y}");
                TextEffects.Typewrite("\n\n\n\t\t\tuse the arrow keys to explore");
                TextEffects.Typewrite("\n\t\t\t[left][right][up][down]");


                while (player.state != State.WON)
                {         
                    if(player.state == State.FIGHTING)
                    {
                        key = Console.ReadKey(false).Key;

                        if (key == ConsoleKey.UpArrow || key == ConsoleKey.RightArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow)
                        {
                            TextEffects.Typewrite("\n\n\n\t\t\tYou can't leave the room!! You must fight your way out!");
                        }

                        switch (key)
                        {
                            case ConsoleKey.Spacebar:
                                player.AttackEnemies();
                                break;
                        }
                    }
                    else if (player.state == State.EXPLORING)
                    {
                        key = Console.ReadKey(false).Key;

                        switch (key)
                        {
                            case ConsoleKey.UpArrow:
                                player.MoveToRoom(player.currentRoom.x, player.currentRoom.y + 1);
                                break;
                            case ConsoleKey.RightArrow:
                                player.MoveToRoom(player.currentRoom.x + 1, player.currentRoom.y);
                                break;
                            case ConsoleKey.DownArrow:
                                player.MoveToRoom(player.currentRoom.x, player.currentRoom.y - 1);
                                break;
                            case ConsoleKey.LeftArrow:
                                player.MoveToRoom(player.currentRoom.x - 1, player.currentRoom.y);
                                break;
                            case ConsoleKey.Spacebar:
                                player.AttackEnemies();
                                break;
                        }
                    }

                    // Test condition to get out of loop, see if last room added to list. change this later
                    if (player.currentRoom == player.currentMap.rooms.Last())
                    {
                        TextEffects.Typewrite($"\n\n\n\t\t\tYou found {player.currentRoom.name}: {player.currentRoom.x}, {player.currentRoom.y} \n");
                        //game.wonGame = true;
                        player.state = State.WON;
                    }
                }


            } while (UserWantsToPlayAgain());
        }

        private static Player InitializeGame(List<Room> roomData)
        {
            Player game = new Player();
            game.currentMap = new Map();
            game.currentMap.rooms = roomData;
            game.currentRoom = game.currentMap.rooms[1];
            game.currentRoom.accessed = true;
            game.UpdateNeighborRoomList();
            
            return game;
        }

        /*private static List<Room> TestData()
        {
            List<Room> testRooms = new List<Room>();

            Room r0 = new Room(0,3);
            Room r1 = new Room(0,0, "the foyer");
            Room r2 = new Room(0,1, "a long dark hallway. You see a door to the right.");
            Room r3 = new Room(1,1, "the bathroom");
            r3.enemies.Add(new Enemy() { name = "giant spider" });
            Room r4 = new Room(0,2);
            Room r5 = new Room(1,3);
            Room r6 = new Room(2,3);
            Room r7 = new Room(3,3);
            r7.enemies.Add(new Enemy() { name = "slimy goblin" });
            Room r8 = new Room(3,2, "a dark room. There seems be a large hole in the south wall...");
            Room r9 = new Room(3,1, "the exit");

            testRooms.Add(r0);
            testRooms.Add(r1);
            testRooms.Add(r2);
            testRooms.Add(r3);
            testRooms.Add(r4);
            testRooms.Add(r5);
            testRooms.Add(r6);
            testRooms.Add(r7);
            testRooms.Add(r8);
            testRooms.Add(r9);

            return testRooms;
        }*/

        private static bool UserWantsToPlayAgain()
        {
            Console.WriteLine("===========END===========");
            Console.WriteLine("========PLAY AGAIN?======");

            string input = Console.ReadLine();

            List<string> acceptedResponses = new List<string> { "yes", "no", "y", "n", "okay", "sure", "yep", "k", "ok", "yeah", "nah", "nope", "absolutely", "absolutely not", "no way", "hell yeah", "heck yeah", "claro", "claro que si", "si", "yes please", "of course", "no thank you", "no thanks" };

            if (acceptedResponses.Exists(i => i == input.ToLower()))
            {
                Console.WriteLine("understood. \n\n\n");
                Thread.Sleep(1500);
                return true;
            }

            return false;
        }
    }

    // TO DO try out map visualization
    class MapVisualizer
    {
        public static void PrintMap(Player player)
        {
            Map map = player.currentMap;
            MockRoomRepository _roomRepository = new MockRoomRepository();


            int furthestX = 0;
            int furthestY = 0;

            for (int i = 0; i < map.rooms.Count; i++)
            {
                if (map.rooms[i].x > furthestX)
                {
                    furthestX = map.rooms[i].x;
                }

                if (map.rooms[i].y > furthestY)
                {
                    furthestY = map.rooms[i].y;
                }
            }

            for (int row = furthestY; row >= 0; row--)
            {
                for (int column = 0; column <= furthestX; column++)
                {
                    if (player.currentRoom.x == column && player.currentRoom.y == row)
                    {
                        Console.Write(" O ");
                    }
                    else
                    {
                        Room roomMatch = _roomRepository.GetRoomByCoord(column, row);

                        if (roomMatch != null) // && roomMatch.accessed)
                        {
                            Console.Write(" - ");
                        }
                        else
                        {
                            Console.Write(" ~ ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
