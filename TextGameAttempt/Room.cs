using System.Collections.Generic;

namespace TextGameAttempt
{
    public class Room
    {
        public int x, y; // 2D Space
        public string name;
        public bool accessed = false;
        public List<Enemy> enemies = new List<Enemy>();
        public List<Enemy> enemiesDefeated = new List<Enemy>();

        public Room(int x, int y, string name = "an insignificant room", List<Enemy> roomEnemies = default)
        {
            this.name = name;
            this.x = x;
            this.y = y;

            if (roomEnemies == null)
            {
                enemies = new List<Enemy>();
            }
            else
            {
                enemies = new List<Enemy>(roomEnemies);
            }
        }
    }
}
