﻿using System;
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

        [SerializeField]
        private List<Color> _colors = new List<Color>();
        [SerializeField]
        private int _piecePerColorCount;

        private List<PieceModel> _pieceModels = new List<PieceModel>();

        public void Initialize()
        {
            CreatePieceModels();

            PlacePieceModelsOnFreeSquares(_pieceModels);
        }

        public void Add(PieceModel pieceModel)
        {
            _pieceModels.Add(pieceModel);

            Debug.Log($"{this.GetType().Name}.{nameof(PieceModelAdded)}");

            PieceModelAdded.Invoke(pieceModel);
        }
        public void Remove(PieceModel pieceModel)
        {
            _pieceModels.Remove(pieceModel);

            Debug.Log($"{this.GetType().Name}.{nameof(PieceModelRemoved)}");

            PieceModelRemoved.Invoke(pieceModel);
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
    }
}
