using System;
using System.Collections.Generic;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic {
  public class GameEngine : IGameEngine {
    private readonly GameEngineOptions _options;

    public GameEngine() {
      _options = new GameEngineOptions {
        Randomizer = new Randomizer(),
        Configuration = new GameConfiguration(1)
      };
      _moveState = new List<MoveDirection>();
    }

    internal GameEngine(GameEngineOptions options):this() {
      _options = options;
    }

    private readonly List<MoveDirection> _moveState;
    private int _movesLeft;
    private int _levelCounter;
    private int _roundsCounter;

    internal Map Map { get; set; }
    public MysticMan Man { get; internal set; }

    /// <inheritdoc />
    public bool NextRoundAvailable => true;

    /// <inheritdoc />
    public int Level => _levelCounter;

    /// <inheritdoc />
    public int Round => _roundsCounter;

    /// <inheritdoc />
    public string CurrentPosition => Man.Position;

    /// <inheritdoc />
    public void Start() {
      _moveState.Clear();
      _movesLeft = Configuration.Moves;
      _levelCounter = Configuration.Level;
      _roundsCounter = 0;


      UpdateState();
    }

    /// <summary>
    /// Updates the ENgines state based on the current state
    /// </summary>
    private void UpdateState() {
      switch (State) {
        case GameEngineState.Initialized:
        case GameEngineState.WaitingForNextRound:
        case GameEngineState.WaitingForNextLevel:
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.WaitingForMove:
          _movesLeft -= 1;
          if (_movesLeft <= 0) {
            State = GameEngineState.WaitingForResolving;
          }
          break;
        case GameEngineState.WaitingForResolving:
          State = GameEngineState.WaitingForNextRound;
          break;
        case GameEngineState.GameLost:
          _moveState.Clear();
          _movesLeft = Configuration.Moves;
          State = GameEngineState.WaitingForMove;
          break;
        case GameEngineState.GameWon:

          if (MoreRoundsAvailable()) {
            State = GameEngineState.WaitingForNextRound;
          }
          else if (MoreLevelsAvailable()) {
            State = GameEngineState.WaitingForNextLevel;
          }
          else {
            //TODO: Specify what should happen if the player succeeds all levels ;-)
            State = GameEngineState.Initialized;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool MoreLevelsAvailable() {
      return _levelCounter < Configuration.LevelsTotalCount;
    }

    private bool MoreRoundsAvailable() {
      return _roundsCounter < Configuration.Rounds;
    }

    /// <inheritdoc />
    public ISolutionResult Resolve(string solution) {
      UpdateState();

      return null;
    }

    /// <inheritdoc />
    public event EventHandler WallReachedEvent;

    /// <inheritdoc />
    public void StartNextRound() {

      UpdateState();
    }

    /// <inheritdoc />
    public void PrepareNextRound() {
      // TODO: calulate new random start position
      Man = new MysticMan();
      // TODO: inkrement LevelCounter ? 
      // TODO: prepare map for next level
      UpdateState();
    }

    public IGameConfiguration Configuration { get; internal set; }

    /// <inheritdoc />
    public void Initialize() {
      Configuration = _options.Configuration;
      Size size = Configuration.MapSize.Size;
      Map = new Map(size);
      Man = new MysticMan();
      Position position = _options.Randomizer.GetRandomPosition(size);
      Man.Position = Map.GetPosition(position.Left, position.Top);
    }

    public void MoveUp() {
      Move(MoveDirection.Up);
    }

    private void Move(MoveDirection direction) {
      string current = Man.Position;

      Cell cell = Map.GetPosition(current);

      int x = cell.X;
      int y = cell.Y;

      switch (direction) {
        case MoveDirection.Up:
          --y;
          break;
        case MoveDirection.Down:
          ++y;
          break;
        case MoveDirection.Left:
          --x;
          break;
        case MoveDirection.Right:
          ++x;
          break;
      }

      string newPosition = Map.GetPosition(x, y);

      if (newPosition != null) {
        Man.Position = newPosition;
      }
      else {
        RaiseWallReachedEvent();
      }

      UpdateState();
    }

    private void RaiseWallReachedEvent() {
      WallReachedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void MoveDown() {
      Move(MoveDirection.Down);
    }

    public void MoveRight() {
      Move(MoveDirection.Right);
    }

    /// <inheritdoc />
    public GameEngineState State { get; private set; }

    /// <inheritdoc />
    public int MovesLeft => _movesLeft;

    public void MoveLeft() {
      Move(MoveDirection.Left);
    }
  }
}
