using System;

namespace TheMysteryMan.Logic
{
    public class MysticMan
    {
      private string _position;
      private string _startPositon;

      public string Position {
        get { return _position; }
        internal set {
          _position = value;
          if (_startPositon == null) {
            _startPositon = value;
          }
        }
      }
      public string StartPosition => _startPositon;
    }
}
