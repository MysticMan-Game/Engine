namespace MysticMan.Logic.Internals{
  internal class GameEngineOptions {
    public IRandomizer Randomizer { get; set; }
    public IGameConfiguration Configuration { get; set; }
  }
}
