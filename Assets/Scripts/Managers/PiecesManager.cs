using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Helpers;
using Models;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Managers
{
    public class PiecesManager : BaseMonoBehaviour
    {
        public Action<PieceModel> PieceModelAdded = delegate {  };
        public Action<PieceModel> PieceModelRemoved = delegate {  };
        public Action<List<PieceModel>> PieceModelsRemoved = delegate {  };
        public Action<List<PieceModel>> PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged = delegate {  };

        [SerializeField]
        private List<Color> _colors = new List<Color>();

        public List<PieceModel> PieceModels
        {
            get
            {
                return _pieceModels;
            }
        }
        public List<PieceModel> PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor
        {
            get
            {
                var pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor =
                    PieceModels.Where(x => x.SquareModel.Value.ConnectedSquareModelsOfTheSameColor.Count >= 1).ToList();

                return pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor;
            }
        }

        private List<PieceModel> _pieceModels = new List<PieceModel>();
        private Random _random = new Random();

        public override void Initialize()
        {
            CreatePieceModels();
            PlacePieceModelsOnFreeSquares(_pieceModels);

            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();

            RemovePieceModelsFromSquares(_pieceModels);
            DestroyPieceModels();
        }

        public override void Subscribe()
        {
            GameManager.Instance.SelectionManager.SelectedObjectsChanged += SelectionManagerOnSelectedObjectsChanged;
            GameManager.Instance.MovingManager.PieceModelsMoved += OnPieceModelsMoved;
            PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged += OnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged;
        }

        public override void UnSubscribe()
        {
            if (GameManager.Instance.SelectionManager != null)
            {
                GameManager.Instance.SelectionManager.SelectedObjectsChanged -= SelectionManagerOnSelectedObjectsChanged;
            }
            if (GameManager.Instance.MovingManager != null)
            {
                GameManager.Instance.MovingManager.PieceModelsMoved -= OnPieceModelsMoved;
            }
            PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged -= OnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged;
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

            if (pieceModel.SquareModel.Value != null)
            {
                pieceModel.SquareModel.Value.PieceModel.Value = null;
            }
            pieceModel.SquareModel = null;

            Debug.Log($"{this.GetType().Name}.{nameof(PieceModelRemoved)}");

            PieceModelRemoved.Invoke(pieceModel);
        }
        private void Remove(List<ISelectable> selectables)
        {
            var pieceModels = new List<PieceModel>();
            foreach (var selectable in selectables)
            {
                var pieceModel = selectable as PieceModel;
                if (pieceModel == null)
                {
                    continue;
                }

                pieceModels.Add(pieceModel);
            }

            foreach (var pieceModel in pieceModels)
            {
                Remove(pieceModel);
            }

            PieceModelsRemoved.Invoke(pieceModels);
        }

        private void CreatePieceModels()
        {
            // TODO: will just iterate for all squares and create equivalent amount of pieces
            // TODO: probably can be optimized
            foreach (var squareModels in GameManager.Instance.BoardManager.BoardModel.Value.SquareModels)
            {
                foreach (var squareModel in squareModels)
                {
                    var randomColorIndex = _random.Next(0, _colors.Count);
                    var randomColor = _colors[randomColorIndex];
                    var pieceModel = new PieceModel(randomColor);
                    Add(pieceModel);
                }
            }
        }
        private void DestroyPieceModels()
        {
            while (_pieceModels.Any())
            {
                Remove(_pieceModels.First());
            }
        }

        private void PlacePieceModelsOnFreeSquares(List<PieceModel> pieceModels)
        {
            foreach (var pieceModel in pieceModels)
            {
                var freeSquareModel = GameManager.Instance.BoardManager.GetFreeSquareModel();
                pieceModel.SquareModel.Value = freeSquareModel;
            }
        }
        private void RemovePieceModelsFromSquares(List<PieceModel> pieceModels)
        {
            foreach (var pieceModel in pieceModels)
            {
                pieceModel.SquareModel.Value = null;
            }
        }

        private void OnPieceModelsMoved()
        {
            PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged.Invoke(PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor);
        }
        private void OnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged(List<PieceModel> pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor.Count}");
        }
        private void SelectionManagerOnSelectedObjectsChanged(List<ISelectable> selectables)
        {
            if (selectables.Count < 2)
            {
                return;
            }

            Remove(selectables);

            GameManager.Instance.SelectionManager.ClearSelectedObjects();
        }
    }
}
