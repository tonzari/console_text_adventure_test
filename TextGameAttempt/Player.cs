using System;
using System.Collections.Generic;
using System.Linq;
using EffectsPlayground;

namespace TextGameAttempt
{
    public enum State
    {
        EXPLORING,
        FIGHTING,
        WON
    }

    public class Player
    {
        public Map currentMap;
        public Room currentRoom;
        public List<Room> neighboringRooms = new List<Room>();
        public State state;

        Random r = new Random();

        public void UpdateNeighborRoomList()
        {
            neighboringRooms.Clear();

            foreach (Room room in currentMap.rooms)
            {
                // look at current coords, for example 0 0

                // ask if room forward
                if (currentRoom.x == room.x && currentRoom.y == room.y - 1)
                {
                    neighboringRooms.Add(room);
                }

                // ask if room right 
                if (currentRoom.x == room.x - 1 && currentRoom.y == room.y)
                {
                    neighboringRooms.Add(room);
                }

                // ask if room back
                if (currentRoom.x == room.x  && currentRoom.y == room.y + 1)
                {
                    neighboringRooms.Add(room);
                }

                // ask if room left
                if (currentRoom.x == room.x + 1 && currentRoom.y == room.y)
                {
                    neighboringRooms.Add(room);
                }
            }
        }

        public void MoveToRoom(int targetX, int targetY)
        {
            // what are the surrounding rooms
            UpdateNeighborRoomList();

            // does the target destination match a surrounding room
            foreach (Room room in neighboringRooms)
            {
                if (targetX == room.x && targetY == room.y)
                {
                    Console.Clear();

                    currentRoom = room;

                    MapVisualizer.PrintMap(this);

                    if (room.accessed)
                    {
                        TextEffects.Typewrite("\n\n\n\t\t\tYou've been here before.");

                        if (currentRoom.enemiesDefeated.Count > 0 && currentRoom.enemies.Count == 0)
                        {
                            TextEffects.Typewrite("\t\t\tThere are some defeated enemies on the ground...");
                            
                            foreach (Enemy enemy in currentRoom.enemiesDefeated)
                            {
                                TextEffects.Typewrite($"\t\t\t... a {enemy.name}");
                            }
                        }
                    }

                    room.accessed = true;

                    TextEffects.Typewrite($"\t\t\tYou are currently in {currentRoom.name}.");
                    TextEffects.Typewrite($"\n\t\t\tMap coordinates: { currentRoom.x}, { currentRoom.y} \n");

                    // check room details, like enemies
                    if (currentRoom.enemies.Count > 0)
                    {
                        state = State.FIGHTING;

                        TextEffects.Typewrite("\t\t\tUH OH. There are enemies in this room... ");

                        foreach (Enemy enemy in currentRoom.enemies)
                        {
                            TextEffects.Typewrite($"\n\t\t\tA {enemy.name} blocks your path! Fight to escape!\n");
                        }

                        TextEffects.Typewrite("\t\t\tHit the spacebar to attack!");
                    }
                }
            }
        }

        public void AttackEnemies()
        {
            //TODO change this logic so you can leave the room as soon as the enemy dies

            if (state == State.FIGHTING)
            {
                if (currentRoom.enemies.Count > 0)
                {
                    int randomIndex = r.Next(0, currentRoom.enemies.Count());
                    Enemy targettedEnemy = currentRoom.enemies[randomIndex];

                    Console.WriteLine($"\n\n\n\t\t\tYou kicked {targettedEnemy.name}!");
                    targettedEnemy.Health -= 1;

                    if (targettedEnemy.Health <= 0)
                    {
                        TextEffects.Typewrite($"\nYou've defeated {targettedEnemy.name}!");
                        currentRoom.enemiesDefeated.Add(targettedEnemy);
                        currentRoom.enemies.RemoveAt(randomIndex);
                    }

                    if (currentRoom.enemies.Count == 0)
                    {
                        state = State.EXPLORING;
                        TextEffects.Typewrite("It's safe to leave the room now.\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("\t\t\tYou kicked the air. Why?");
            }
        }
    }
}
