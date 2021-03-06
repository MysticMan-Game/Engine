﻿using System;

namespace MysticMan.Logic{
  public interface IGameEngine {
    void Initialize();
    void MoveUp();
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    GameEngineState State { get;}
    int MovesLeft { get; }
    bool NextRoundAvailable { get; }
    int Level { get; }
    int Round { get; }
    string CurrentPosition { get; }
    MapSize MapSize { get; }

    void Start();
    ISolutionResult Resolve(string solution);
    event EventHandler WallReachedEvent;
    void StartNextRound();
    void PrepareNextRound();

    void Cheat();
  }
}
