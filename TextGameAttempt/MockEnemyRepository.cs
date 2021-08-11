using System.Collections.Generic;
using TextGameAttempt;

public class MockEnemyRepository : IEnemyRepository
{
    public IEnumerable<Enemy> AllEnemies =>
        new List<Enemy>
        {
            new Enemy { name = "giant spider", Health = 3},
            new Enemy { name = "slimy goblin", Health = 10 },
            new Enemy { name = "six headed walrus", Health = 15}
        };
}

