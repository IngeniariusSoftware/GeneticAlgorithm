using System;

namespace Data
{
    [Serializable]
    class Bug : Creature
    {
        private static int _lengthGenom = 64;

        public static int PublicLengthGenom
        {
            get => _lengthGenom;
        }

        private static int _typeCell = 4;

        public static int PublicTypeCell
        {
            get => _typeCell;
        }

        private static int _startLife = 50;

        public Bug()
        {
            Life = 0;
            X = 0;
            Y = 0;
            Genom = null;
            Direction = 0;
            LifeTime = 0;
            IsDead = false;
        }

        public Bug(int x, int y, int[] genom, int direction) : base(x, y, genom, direction)
        {
            Life = _startLife;
        }

        public static Bug operator --(Bug bug)
        {
            if (bug.Life > 0)
            {
                bug.Life--;
            }
            return bug;
        }

        public static int Generation;

        public override void Move(int cell, int cellY, int cellX)
        {
            switch (cell)
            {
                case 0:
                {
                    X = cellX;
                    Y = cellY;
                    break;
                }
                case 1:
                {
                    X = cellX;
                    Y = cellY;
                    if (Life < 90)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 99;
                    }
                    break;
                }
                case 2:
                {
                    Life = 0;
                    break;
                }
            }
        }

        public override void Check(int cell)
        {
            switch (cell)
            {
                case 1:
                {
                    if (Life < 90)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 99;
                    }
                    break;
                }
            }
        }
    }
}
