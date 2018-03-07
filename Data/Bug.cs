using System;

namespace Data
{
    [Serializable]
    class Bug : Creature
    {
        private static int _lengthGenom = 80;

        public static int PublicLengthGenom
        {
            get => _lengthGenom;
        }

        private static int _typeCell = 12;

        public static int PublicTypeCell
        {
            get => _typeCell;
        }

        private static int _startLife = 50;

        private static int _attack = 10;

        public static int PublicAttack
        {
            get => _attack;
        }

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

        //private bool _dead;

        //public bool PublicDead
        //{
        //    set { _dead = value; }
        //    get => _dead;
        //}

        //private int _lifeTime;

        //public int PublicLifeTime
        //{
        //    set { _lifeTime = value; }
        //    get => _lifeTime;
        //}

        //private int _life;

        //public int PublicLife
        //{
        //    set { _life = value; }
        //    get => _life;
        //}

        //private int _x;

        //public int PublicX
        //{
        //    set { _x = value; }
        //    get => _x;
        //}

        //private int _y;

        //public int PublicY
        //{
        //    set { _y = value; }
        //    get => _y;
        //}

        //public static Bug operator --(Bug creature)
        //{
        //    if (creature._life > 0)
        //    {
        //        creature._life--;
        //    }
        //    return creature;
        //}

        //public int[] Genom;

        //private int _genomSelected;

        //public int PublicGenomSelected
        //{
        //    set { _genomSelected = value; }
        //    get => _genomSelected;
        //}

        //private int _direction;

        //public int Direction
        //{
        //    set { _direction = value; }
        //    get => _direction;
        //}
        //public Bug()
        //{
        //    _life = 0;
        //    _x = 0;
        //    _y = 0;
        //    Genom = null;
        //    _direction = 0;
        //    _lifeTime = 0;
        //    _dead = false;
        //}

        //public Bug(int life, int x, int y, int[] genom, int direction)
        //{
        //    _life = life;
        //    _x = x;
        //    _y = y;
        //    Genom = genom;
        //    _direction = direction;
        //    _lifeTime = 0;
        //    _dead = false;
        //}

        //public void See(int cell)
        //{
        //    switch (cell)
        //    {
        //        case 0:
        //        {
        //            _genomSelected += 5;
        //            break;
        //        }
        //        case 1:
        //        {
        //            _genomSelected += 4;
        //                break;
        //        }
        //        case 2:
        //        {
        //            _genomSelected += 1;
        //                break;
        //        }
        //        case 3:
        //        {
        //            _genomSelected += 3;
        //                break;
        //        }
        //        case 4:
        //        {
        //            _genomSelected += 2;
        //                break;
        //        }
        //    }
        //}
    }
}
