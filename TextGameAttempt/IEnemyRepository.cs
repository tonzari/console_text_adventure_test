using System.Collections.Generic;
using TextGameAttempt;

public interface IEnemyRepository
{
    IEnumerable<Enemy> AllEnemies { get; }
}