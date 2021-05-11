using UnityEngine;

namespace Models
{
    public class SquareModel
    {
        public Vector2 Index => _index;

        public SquareModel Top;
        public SquareModel Right;
        public SquareModel Left;
        public SquareModel Bot;

        private Vector2 _index;

        public SquareModel(Vector2 index)
        {
            _index = index;
        }
    }
}
