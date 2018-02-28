using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class Bug
    {
        private int _life;

        public int PublicLife
        {
            set { _life = value; }
            get => _life;
        }

        private int _x;

        public int PublicX
        {
            set { _x = value; }
            get => _x;
        }

        private int _y;

        public int PublicY
        {
            set { _y = value; }
            get => _y;
        }

        public static Bug operator --(Bug creature)
        {
            if (creature._life > 0)
            {
                creature._life--;
            }
            return creature;
        }

        private int[] _genom;

        public int[] PublicGenom
        {
            set { _genom = value; }
            get => _genom;
        }

        private int _direction;

        public int Direction
        {
            set { _direction = value; }
            get => _direction;
        }

        public Bug()
        {
            _life = 0;
            _x = 0;
            _y = 0;
            _genom = null;
            _direction = 0;

        }

        public Bug(int life, int x, int y, int[] genom, int direction)
        {
            _life = life;
            _x = x;
            _y = y;
            _genom = genom;
            _direction = direction;
        }

        public void Move()
        {
            switch (_direction)
            {
                case 0:
                {
                    if (_y > 0)
                    {
                        _y--;
                    }
                    break;
                }
                case 1:
                {
                    _y--;
                    _x++;
                    break;
                }
                case 2:
                {
                    _x++;
                    break;
                }
                case 3:
                {
                    _x++;
                    _y++;
                    break;
                }
                case 4:
                {
                    _y++;
                    break;
                }
                case 5:
                {
                    _x--;
                    _y++;
                    break;
                }
                case 6:
                {
                    _x--;
                    break;
                }
                case 7:
                {
                    _x--;
                    _y--;
                    break;
                }
            }
        }

        public void Turn(int direction)
        {
            _direction = direction;
        }

        public bool Check(int cell)
        {
            if (cell == 2)
            {
                return true;
            }
            return false;
        }

        public bool Eat(int cell)
        {
            switch (cell)
            {
                case 1:
                {
                    _life += 10;
                    return true;
                }
                case 2:
                {
                    _life = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
