namespace TheMysteryMan.Logic
{
    public class Level
    {
        private int level;
        private int moves;
        private int rounds;
        private int time;
        private bool canReachBorder;

        public Level(int level)
        {
            level = this.level;
            initalize();
        }

        public int getLevel()
        {
            return level;
        }

        public int getMoves()
        {
            return moves;
        }
        public int getRounds()
        {
            return this.rounds;
        }
        public int getTimeInSeconds()
        {
            return time;
        }

        public bool getCanReachBorder()
        {
            return this.canReachBorder;
        }
            private void initalize()
        {
            switch (level)
            {
                case 1:
                    moves = 3;
                    rounds = 3;
                    time = 9999;
                    canReachBorder = false;
                    break;
                case 2:
                    moves = 6;
                    rounds = 3;
                    time = 9999;
                    canReachBorder = false;
                    break;
                case 3:
                    moves = 12;
                    rounds = 5;
                    time = 20;
                    canReachBorder = false;
                    break;
                case 4:
                    moves = 15;
                    rounds = 5;
                    time = 20;
                    canReachBorder = true;
                    break;


            }

        }
    }
}