using MysticMan.Logic.Internals;

namespace MysticMan.Logic {
  internal class GameConfiguration : IGameConfiguration {


    public GameConfiguration(int level) {
      Level = level;

      Initalize();

    }

    public int Level { get; private set; }

    public int Moves { get; private set; }

    public int Rounds { get; private set; }

    public Classification Classification { get; private set; }

    public MapSize MapSize { get; private set; }

    public int TimeInSeconds { get; set; }

    public bool CanReachBorder { get; set; }
    public int LevelsTotalCount => 4;

    /// <inheritdoc />
    public void NextLevel() {
      Level += 1;
      Initalize();
    }

    private void Initalize() {
      //Check if time is 9999 and then set unlimited time.
      switch (Level) {
        case 1:
          Moves = 3;
          Rounds = 1;
          TimeInSeconds = 9999;
          CanReachBorder = true;
          Classification = Classification.Beginner;
          MapSize = new MapSize(new Size(5,5));
          break;
        case 2:
          Moves = 6;
          Rounds = 3;
          TimeInSeconds = 9999;
          CanReachBorder = true;
          Classification = Classification.Professional;
          MapSize = new MapSize(new Size(8, 8));
          break;
        case 3:
          Moves = 12;
          Rounds = 5;
          TimeInSeconds = 20;
          CanReachBorder = true;
          Classification = Classification.Expert;
          MapSize = new MapSize(new Size(10, 10));
          break;
        case 4:
          Moves = 15;
          Rounds = 5;
          TimeInSeconds = 20;
          CanReachBorder = false;
          Classification = Classification.Maniac;
          MapSize = new MapSize(new Size(15, 15));
          break;
      }
    }
  }
}
