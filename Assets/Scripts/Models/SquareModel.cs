using System;
using UnityEngine;
using Views;

namespace Models
{
    public class SquareModel
    {
        public Action<PieceModel> PieceModelChanged = delegate {  };

        public Vector2 Index => _index;
        public PieceModel PieceModel
        {
            get
            {
                return _pieceModel;
            }
            set
            {
                if (_pieceModel == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(PieceModelChanged)}");

                _pieceModel = value;

                PieceModelChanged.Invoke(_pieceModel);
            }
        }

        private PieceModel _pieceModel;

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
