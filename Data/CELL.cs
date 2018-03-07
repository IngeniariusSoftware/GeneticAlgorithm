using System;

namespace Data
{
    class Cell
    {
        private int _content;
        public int PublicContent
        {
            set
            {
                _content = value;
                IsChange = true;
            }
            get => _content;
        }

        public bool IsChange;

        public Cell()
        {
            _content = 0;
            IsChange = true;
        }
        public Cell(int content)
        {
            _content = content;
            IsChange = true;
        }
    }
}
