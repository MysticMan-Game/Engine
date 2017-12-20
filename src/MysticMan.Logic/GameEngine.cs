using System;
using System.Collections.Generic;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic {
  public class GameEngine : IGameEngine {
    private readonly GameEngineOptions _options;

    public GameEngine() {
      _options = new GameEngineOptions {
        Randomizer = new Randomizer(),
        Configuration = new GameConfiguration()
      };
      _moveState = new List<MoveDirection>();
    }

    internal GameEngine(GameEngineOptions options):this() {
      _options = options;
    }

    private readonly List<MoveDirection> _moveState;
    private int _movesLeft;
    private int _roundsCounter;

    internal Map Map { get; set; }
    public MysticMan Man { get; internal set; }

    /// <inheritdoc />
    public bool NextRoundAvailable => true;

    /// <inheritdoc />
    public int Level => Configuration.Level;

    /// <inheritdoc />
    public int Round => _roundsCounter;

    /// <inheritdoc />
    public string CurrentPosition => Man.Position;

    /// <inheritdoc />
    public void Start() {
      _moveState.Clear();
      _movesLeft = Configuration.Moves;
      _roundsCounter = 1;
      UpdateState();
    }

    /// <summary>
    /// Updates the Engines state based on the current state
    /// </summary>
    ///
    public  void Cheat() {

      State = GameEngineState.Cheat;
    }
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
        case GameEngineState.Cheat:

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
      return Level < Configuration.LevelsTotalCount;
    }

    private bool MoreRoundsAvailable() {
      return _roundsCounter < Configuration.Rounds;
    }

    /// <inheritdoc />
    public ISolutionResult Resolve(string solution) {
      string startPosition = Man.StartPosition;
      if (string.Equals(startPosition, solution, StringComparison.OrdinalIgnoreCase)) {
        State = GameEngineState.GameWon;
      }
      else {
        State = GameEngineState.GameLost;
      }

      List<string> moves = new List<string>();
      for (int i = 0; i < _moveState.Count; i++) {
        MoveDirection move = _moveState[i];
        switch (move) {
          case MoveDirection.Left:
            moves.Add("left");
            break;
          case MoveDirection.Right:
            moves.Add("right");
            break;
          case MoveDirection.Up:
            moves.Add("up");
            break;
          case MoveDirection.Down:
            moves.Add("down");
            break;
        }
      }

      SolutionResult result = new SolutionResult {
        AnsweredPosition = solution,
        MagicMan = startPosition,
        Moves = moves
      };

      return result;
    }

    /// <inheritdoc />
    public event EventHandler WallReachedEvent;

    /// <inheritdoc />
    public void StartNextRound() {
      _moveState.Clear();
      if (State == GameEngineState.WaitingForNextRound) {
        _roundsCounter += 1;
      }
      else if (State == GameEngineState.WaitingForNextLevel) {
        _roundsCounter = 1;
        Configuration.NextLevel();
        Map = new Map(Configuration.MapSize.Size);
        Man = new MysticMan();
        Position randomPosition = _options.Randomizer.GetRandomPosition(Configuration.MapSize.Size);
        Man.Position = Map.GetPosition(randomPosition.Left, randomPosition.Top);
      }
      _movesLeft = Configuration.Moves;

      UpdateState();
    }

    /// <inheritdoc />
    public void PrepareNextRound() {
      // This method is called either the last round was won or lost
      Man = new MysticMan();
      Position randomPosition = _options.Randomizer.GetRandomPosition(Configuration.MapSize.Size);
      Man.Position = Map.GetPosition(randomPosition.Left, randomPosition.Top);
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
        _moveState.Add(direction);
        Man.Position = newPosition;
      }
      else {
        if (!Configuration.CanReachBorder) {
          _moveState.Add(direction);
        }
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

    public MapSize MapSize => Configuration.MapSize;

    public void MoveLeft() {
      Move(MoveDirection.Left);
    }
  }
}
