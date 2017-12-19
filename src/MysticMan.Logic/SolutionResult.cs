using System.Collections.Generic;

namespace MysticMan.Logic{
  public class SolutionResult : ISolutionResult {
    /// <inheritdoc />
    public IEnumerable<string> Moves { get; set; }

    /// <inheritdoc />
    public string MagicMan { get; set; }

    /// <inheritdoc />
    public string AnsweredPosition { get; set; }
  }
}
