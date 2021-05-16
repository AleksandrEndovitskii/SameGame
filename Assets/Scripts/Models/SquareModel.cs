using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using Utils;

namespace Models
{
    public partial class SquareModel
    {
        public Action<PieceModel> PieceModelChanged = delegate {  };

        public Position Position => _position;
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

        private List<SquareModel> ConnectedSquareModels
        {
            get
            {
                return _directionSquareModels.Values.ToList();
            }
        }

        private Dictionary<Direction, SquareModel> _directionSquareModels = new Dictionary<Direction, SquareModel>();

        private Position _position;

        public SquareModel(Position position)
        {
            _directionSquareModels.Add(Direction.Top,null);
            _directionSquareModels.Add(Direction.Bot,null);
            _directionSquareModels.Add(Direction.Left,null);
            _directionSquareModels.Add(Direction.Right,null);

            _position = position;

            GameManager.Instance.PiecesManager.PieceModelRemoved += PieceModelRemoved;
            GameManager.Instance.SelectionManager.SelectableAdded += OnSelectableAdded;
        }

        public void SetConnectedSquareModel(Direction direction, SquareModel squareModel)
        {
            _directionSquareModels[direction] = squareModel;
        }
        public SquareModel GetConnectedSquareModel(Direction direction)
        {
            return _directionSquareModels[direction];
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
