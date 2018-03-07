using System;

namespace Data
{
    [Serializable]
    class Spider : Creature
    {
        private static int _lengthGenom = 64;

        public static int PublicLengthGenom
        {
            get => _lengthGenom;
        }

        private static int _typeCell = 14;

        public static int PublicTypeCell
        {
            get => _typeCell;
        }

        private static int _startLife = 55;

        private static int _attack = 12;

        public static int PublicAttack
        {
            get => _attack;
        }

        public Spider()
        {
            Life = 0;
            X = 0;
            Y = 0;
            Genom = null;
            Direction = 0;
            LifeTime = 0;
            IsDead = false;
        }

        public Spider(int x, int y, int[] genom, int direction) : base(x, y, genom, direction)
        {
            Life = _startLife;
        }

        public static Spider operator --(Spider spider)
        {
            if (spider.Life > 0)
            {
                spider.Life--;
            }
            return spider;
        }

        public static int Generation = 1;

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
                case 2:
                {
                    X = cellX;
                    Y = cellY;
                    if (Life < 110)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 119;
                    }
                    break;
                }
                case 1:
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
                case 2:
                {
                    if (Life < 110)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 119;
                    }
                    break;
                }
            }
        }
    }
}
