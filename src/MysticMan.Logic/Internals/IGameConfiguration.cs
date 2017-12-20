namespace MysticMan.Logic.Internals{
  public interface IGameConfiguration{
    int Level{ get; }
    int Moves{ get; }
    int Rounds{ get; }
    Classification Classification{ get; }
    MapSize MapSize{ get; }
    int TimeInSeconds{ get; }
    bool CanReachBorder{ get;  }
    int LevelsTotalCount{ get; }
    void NextLevel();
  }
}
