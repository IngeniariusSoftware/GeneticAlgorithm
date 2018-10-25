using System;

namespace Data
{
    [Serializable]
    class Ant : Creature
    {
        private static int _lengthGenom = 80;

        public static int PublicLengthGenom
        {
            get => _lengthGenom;
        }

        private static int _typeCell = 13;

        public static int PublicTypeCell
        {
            get => _typeCell;
        }

        private static int _startLife = 60;

        private static int _attack = 20;

        public static int PublicAttack
        {
            get => _attack;
        }

        public Ant()
        {
            Life = 0;
            X = 0;
            Y = 0;
            Genom = null;
            Direction = 0;
            LifeTime = 0;
            IsDead = false;
        }

        public Ant(int x, int y, int[] genom, int direction) : base(x, y, genom, direction)
        {
            Life = _startLife;
        }

        public static Ant operator --(Ant ant)
        {
            if (ant.Life > 0)
            {
                ant.Life--;
            }
            return ant;
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
                case 1:
                {
                    X = cellX;
                    Y = cellY;
                    if (Life < 140)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 149;
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
                    if (Life < 140)
                    {
                        Life += 10;
                    }
                    else
                    {
                        Life = 149;
                    }
                    break;
                }
            }
        }
    }
}
