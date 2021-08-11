using System.Collections.Generic;

namespace TextGameAttempt
{
    public interface IRoomRepository
    {
        IEnumerable<Room> AllRooms { get; }
        Room GetRoomByCoord(int x, int y);
    }
}
