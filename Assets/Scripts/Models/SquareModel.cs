using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utils;

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

        private List<SquareModel> ConnectedSquareModels
        {
            get
            {
                var result = new List<SquareModel>
                {
                    Top,
                    Right,
                    Left,
                    Bot,
                };
                return result;
            }
        }

        private Vector2 _index;

        public SquareModel(Vector2 index)
        {
            _index = index;

            GameManager.Instance.PiecesManager.PieceModelRemoved += PieceModelRemoved;
            GameManager.Instance.SelectionManager.SelectableAdded += OnSelectableAdded;
        }

        private void OnSelectableAdded(ISelectable selectable)
        {
            if (PieceModel == null)
            {
                return;
            }
            var pieceModel = selectable as PieceModel;
            if (pieceModel == null)
            {
                return;
            }
            if (pieceModel != PieceModel)
            {
                return;
            }

            foreach (var connectedSquareModel in ConnectedSquareModels)
            {
                if (connectedSquareModel == null)
                {
                    continue;
                }
                if (connectedSquareModel.PieceModel == null)
                {
                    continue;
                }
                if (connectedSquareModel.PieceModel.Color != PieceModel.Color)
                {
                    continue;
                }

                GameManager.Instance.SelectionManager.AddObjectToSelectedObjects(connectedSquareModel.PieceModel);
            }
        }

        private void PieceModelRemoved(PieceModel pieceModel)
        {
            if (pieceModel != PieceModel)
            {
                return;
            }

            PieceModel = null;
        }
    }
}
