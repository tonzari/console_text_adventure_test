using System;

namespace TextGameAttempt
{
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
