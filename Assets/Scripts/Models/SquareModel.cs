using System;
using Managers;
using UnityEngine;

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

            GameManager.Instance.PiecesManager.PieceModelRemoved += PieceModelRemoved;
        }

        private void PieceModelRemoved(PieceModel pieceModel)
        {
            if (pieceModel != PieceModel)
            {
                return;
            }

            Top?.TryRemovePieceModel(PieceModel);
            Right?.TryRemovePieceModel(PieceModel);
            Left?.TryRemovePieceModel(PieceModel);
            Bot?.TryRemovePieceModel(PieceModel);
            PieceModel = null;
        }

        private void TryRemovePieceModel(PieceModel pieceModel)
        {
            if (pieceModel == null ||
                PieceModel == null ||
                pieceModel.Color != PieceModel.Color)
            {
                Debug.Log($"{this.GetType().Name}.{nameof(TryRemovePieceModel)} aborted");

                return;
            }

            GameManager.Instance.PiecesManager.Remove(PieceModel);
        }
    }
}
