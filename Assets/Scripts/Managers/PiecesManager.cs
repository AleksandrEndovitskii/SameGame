using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class PiecesManager : MonoBehaviour, IInitilizable
    {
        public Action<PieceModel> PieceModelAdded = delegate {  };
        public Action<PieceModel> PieceModelRemoved = delegate {  };
        public Action PieceModelsRemoved = delegate {  };

        [SerializeField]
        private List<Color> _colors = new List<Color>();
        [SerializeField]
        private int _piecePerColorCount;

        public List<PieceModel> PieceModels
        {
            get
            {
                return _pieceModels;
            }
        }

        private List<PieceModel> _pieceModels = new List<PieceModel>();

        public void Initialize()
        {
            CreatePieceModels();

            PlacePieceModelsOnFreeSquares(_pieceModels);

            GameManager.Instance.SelectionManager.SelectedObjectsChanged += SelectionManagerOnSelectedObjectsChanged;
        }

        public void Add(PieceModel pieceModel)
        {
            if (_pieceModels.Contains(pieceModel))
            {
                Debug.Log($"{this.GetType().Name}.{nameof(Add)} aborted");

                return;
            }

            _pieceModels.Add(pieceModel);

            Debug.Log($"{this.GetType().Name}.{nameof(PieceModelAdded)}");

            PieceModelAdded.Invoke(pieceModel);
        }
        public void Remove(PieceModel pieceModel)
        {
            if (!_pieceModels.Contains(pieceModel))
            {
                Debug.Log($"{this.GetType().Name}.{nameof(Remove)} aborted");

                return;
            }

            _pieceModels.Remove(pieceModel);

            pieceModel.SquareModel.PieceModel = null;
            pieceModel.SquareModel = null;

            Debug.Log($"{this.GetType().Name}.{nameof(PieceModelRemoved)}");

            PieceModelRemoved.Invoke(pieceModel);
        }
        private void Remove(List<ISelectable> selectables)
        {
            foreach (var selectable in selectables)
            {
                var pieceModel = selectable as PieceModel;
                if (pieceModel == null)
                {
                    continue;
                }

                Remove(pieceModel);
            }

            PieceModelsRemoved.Invoke();
        }

        private void PlacePieceModelsOnFreeSquares(List<PieceModel> pieceModels)
        {
            foreach (var pieceModel in pieceModels)
            {
                var freeSquareModel = GameManager.Instance.BoardManager.GetFreeSquareModel();
                pieceModel.SquareModel = freeSquareModel;
            }
        }

        private void CreatePieceModels()
        {
            foreach (var color in _colors)
            {
                for (var i = 0; i < _piecePerColorCount; i++)
                {
                    var pieceModel = new PieceModel(color);
                    Add(pieceModel);
                }
            }
        }

        private void SelectionManagerOnSelectedObjectsChanged(List<ISelectable> selectables)
        {
            Remove(selectables);

            GameManager.Instance.SelectionManager.ClearSelectedObjects();
        }
    }
}
