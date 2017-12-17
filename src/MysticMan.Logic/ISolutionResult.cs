using System.Collections.Generic;

namespace MysticMan.Logic{
  public interface ISolutionResult {
    IEnumerable<string> Moves { get; }
    string MagicMan { get; }
    string AnsweredPosition { get; }
  }
}