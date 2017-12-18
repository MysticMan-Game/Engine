using System;
using System.Collections.Generic;
using System.Linq;
using MysticMan.Logic.Internals;

namespace MysticMan.Logic {
  internal class Map {
    private readonly Cell[,] _newMap;

    private Map(int x, int y) {
      _newMap = new Cell[x, y];

      for (int xAxis = 0; xAxis < x; xAxis++) {
        for (int yAxis = 0; yAxis < y; yAxis++)
          _newMap[xAxis, yAxis] = new Cell(xAxis, yAxis);
      }
    }

    internal Map(Size size) : this(size.Width, size.Height) {
    }

    public string GetPosition(int x, int y) {
      Cell position = Values().FirstOrDefault(cell => cell.X == x && cell.Y == y);
      return position?.Id;
    }

    public Cell GetPosition(string id) {
      foreach (Cell cell in Values()) {
        if (cell.Id == id) {
          return cell;
        }
      }
      return null;
    }

    internal int GetX(string current) {
      foreach (Cell cell in Values()) {
        if (cell.Id == current) {
          return cell.X;
        }
      }
      throw new InvalidOperationException($"Position {current} not found");
    }

    internal int GetY(string current) {
      foreach (Cell cell in Values()) {
        if (cell.Id == current) {
          return cell.Y;
        }
      }
      throw new InvalidOperationException($"Position {current} not found");
    }

    private IEnumerable<Cell> Values() {
      for (int x = 0; x < _newMap.GetLength(0); x++) {
        for (int y = 0; y < _newMap.GetLength(1); y++)
          yield return _newMap[x, y];
      }
    }
  }
}
