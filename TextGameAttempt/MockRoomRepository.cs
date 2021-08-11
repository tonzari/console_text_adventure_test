using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGameAttempt
{
    class MockRoomRepository : IRoomRepository
    {
        private readonly IEnemyRepository _enemyRepository = new MockEnemyRepository();

        public IEnumerable<Room> AllRooms =>
            new List<Room>
            {
                new Room(0,3),
                new Room(0,0, "the foyer"),
                new Room(0,1, "a long dark hallway. You see a door to the right."),
                new Room(1,1,"the bathroom", new List<Enemy> { _enemyRepository.AllEnemies.ToList()[0]}),
                new Room(0,2),
                new Room(1,3),
                new Room(2,3),
                new Room(3, 3, "a stinky bedroom", new List<Enemy> { _enemyRepository.AllEnemies.ToList()[1],  _enemyRepository.AllEnemies.ToList()[2] }),
                new Room(3, 2, "a dark and empty closet. There seems to be a large hole in the south wall..."),
                new Room(3, 1, "the exit")
            };

        public Room GetRoomByCoord(int x, int y)
        {
            foreach (Room room in AllRooms)
            {
                if (x == room.x && y == room.y)
                {
                    return room;
                }
            }

            return null;
        }
    }
}
