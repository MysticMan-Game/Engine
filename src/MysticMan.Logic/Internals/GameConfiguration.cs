using System.Linq;
using System.Collections.Generic;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic {
  internal class GameConfiguration : IGameConfiguration {

    private List<LevelConfiguration> levels = new List<LevelConfiguration>();


    public GameConfiguration() {
      Level = 1;

      Initalize();

    }

    public int Level { get; private set; }

    public int Moves => Current().Moves;

    public int Rounds => Current().Rounds;

    public Classification Classification => Current().Classification;

    public MapSize MapSize => new MapSize( Current().Size);

    public int TimeInSeconds => Current().TimeInSeconds;

    public bool CanReachBorder => Current().CanReachBorder;
    public int LevelsTotalCount => levels.Count;

    /// <inheritdoc />
    public void NextLevel() {
      Level += 1;
    }

    private void Initalize() {
      levels = new List<LevelConfiguration>() {
         new LevelConfiguration{Level = 1,  Moves = 3, Rounds = 3, TimeInSeconds = -1, CanReachBorder = true, Classification = Classification.Beginner,  Size =new Size(5,5) },
         new LevelConfiguration{Level = 2,  Moves = 5, Rounds = 3, TimeInSeconds = -1, CanReachBorder = true, Classification = Classification.Beginner,  Size =new Size(5,5) },
         new LevelConfiguration{Level = 3,  Moves = 7, Rounds = 3, TimeInSeconds = -1, CanReachBorder = true, Classification = Classification.Professional,  Size =new Size(8,8) },
         new LevelConfiguration{Level = 4,  Moves = 9, Rounds = 3, TimeInSeconds = -1, CanReachBorder = true, Classification = Classification.Professional,  Size =new Size(8,8) },
         new LevelConfiguration{Level = 5,  Moves = 9, Rounds = 3, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Professional,  Size =new Size(8,8) },
         new LevelConfiguration{Level = 6,  Moves = 12, Rounds = 3, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Professional,  Size =new Size(8,8) },
         new LevelConfiguration{Level = 7,  Moves = 3,  Rounds = 3, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Expert,  Size =new Size(10,10) },
         new LevelConfiguration{Level = 8,  Moves = 3,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Expert,  Size =new Size(10,10) },
         new LevelConfiguration{Level = 9,  Moves = 6,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Expert,  Size =new Size(10,10) },
         new LevelConfiguration{Level = 10, Moves = 12,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Expert,  Size =new Size(10,10) },
         new LevelConfiguration{Level = 11, Moves = 9,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Maniac,  Size =new Size(15,15) },
         new LevelConfiguration{Level = 12, Moves = 12,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Maniac,  Size =new Size(15,15) },
         new LevelConfiguration{Level = 13, Moves = 24,  Rounds = 5, TimeInSeconds = -1, CanReachBorder = false, Classification = Classification.Maniac,  Size =new Size(15,15) },
      };


      
    }

    private LevelConfiguration Current() {
      return levels.Single(level => level.Level == Level);
    }
  }

  internal class LevelConfiguration {

    public int Level { get; set; }

    public int Moves { get; set; }

    public int Rounds { get; set; }

    public int TimeInSeconds { get; set; }

    public bool CanReachBorder { get; set; }

    public Classification Classification { get; set; }

    public Size Size { get; set; }

  }
}
