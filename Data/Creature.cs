using System;

namespace Data
{
    [Serializable]
    abstract class Creature
    {
        protected bool IsDead;

        public bool PublicIsDead
        {
            set { IsDead = value; }
            get => IsDead;
        }

        protected int X;

        public int PublicX
        {
            set { X = value; }
            get => X;
        }

        protected int Y;

        public int PublicY
        {
            set { Y = value; }
            get => Y;
        }

        protected int LifeTime;

        public int PublicLifeTime
        {
            set { LifeTime = value; }
            get => LifeTime;
        }

        protected int Life;

        public int PublicLife
        {
            set { Life = value; }
            get => Life;
        }

        public int[] Genom;

        protected int GenomSelected;

        public int PublicGenomSelected
        {
            set { GenomSelected = value; }
            get => GenomSelected;
        }

        protected int Direction;

        public int PublicDirection
        {
            set { Direction = value; }
            get => Direction;
        }
        
        public Creature()
        {
            X = 0;
            Y = 0;
            Genom = null;
            Direction = 0;
            LifeTime = 0;
            IsDead = false;
        }

        public Creature( int x, int y, int[] genom, int direction)
        {
            X = x;
            Y = y;
            Genom = genom;
            Direction = direction;
            LifeTime = 0;
            IsDead = false;
        }

        public abstract void Move(int cell, int cellY, int cellX);
        //{
            //switch (cell)
            //{
            //    case 0:
            //    {
            //        X = cellX;
            //        Y = cellY;
            //        break;
            //    }
            //    case 1:
            //    {
            //        X = cellX;
            //        Y = cellY;
            //        if (Life < 90)
            //        {
            //            Life += 10;
            //        }
            //        else
            //        {
            //            Life = 99;
            //        }
            //        break;
            //    }
            //    case 2:
            //    {
            //        Life = 0;
            //        break;
            //    }
            //}
        //}

        public abstract void Check(int cell);
       // {
            //switch (cell)
            //{
            //    case 1:
            //    {
            //        if (Life < 90)
            //        {
            //            Life += 10;
            //        }
            //        else
            //        {
            //            Life = 99;
            //        }
            //        break;
            //    }
            //}
      //  }

        public void See(int cell)
        {
            switch (cell)
            {
                case 0:
                {
                    GenomSelected += 1;
                    break;
                }
                case 1:
                {
                    GenomSelected += 2;
                    break;
                }
                case 2:
                {
                    GenomSelected += 3;
                    break;
                }
                case 3:
                {
                    GenomSelected += 4;
                    break;
                }
                case 4:
                {
                    GenomSelected += 5;
                    break;
                }
                case 12:
                {
                    GenomSelected += 12;
                    break;
                }
                case 13:
                {
                    GenomSelected += 13;
                    break;
                }
            }
        }
    }
}
