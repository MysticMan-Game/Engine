using System;

namespace TheMysteryMan.Logic
{
    public class GameEngine
    {
        Func<int, int, int> _randomGenerator;

public GameEngine(Func<int, int, int> randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public EventHandler WallReachedEvent;

        public Map Map { get; internal set; }
        public MysticMan Man { get; internal set; }

        public GameConfiguration Level { get; internal set; }

        public void Run()
        {
            Map = new Map(12, 10);
            Man = new MysticMan();
            Level = new GameConfiguration(1);
            int xPosition = _randomGenerator(0,9);
            int yPosition = _randomGenerator(0, 11);
            Man.Position = Map.GetPosition(xPosition, yPosition);

        }

        public void MoveUp()
        {

            Move(MoveDirection.Up);

            
        }

        private void Move(MoveDirection direction)
        {
      string current = Man.Position;

            Cell cell = Map.GetPosition(current);

            int x = cell.X;
            int y = cell.Y;

            switch (direction)
            {
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

            if (newPosition != null)
            {
                Man.Position = newPosition;
            }
            else
            {

                RaiseWallReachedEvent();
                //throw new InvalidOperationException("Unable to move the mystic man");
            }
        }

        private void RaiseWallReachedEvent()
        {
            WallReachedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void MoveDown()
        {
            Move(MoveDirection.Down);
        }

        public void MoveRight()
        {
            Move(MoveDirection.Right);
        }

        public void MoveLeft()
        {
            Move(MoveDirection.Left);
        }
    }
}