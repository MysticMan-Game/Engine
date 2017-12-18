namespace MysticMan.Logic.Internals{
  public interface IGameConfiguration{
    int Level{ get; }
    int Moves{ get; }
    int Rounds{ get; }
    Classification Classification{ get; }
    MapSize MapSize{ get; }
    int TimeInSeconds{ get; set; }
    bool CanReachBorder{ get; set; }
    int LevelsTotalCount{ get; set; }
  }
}
