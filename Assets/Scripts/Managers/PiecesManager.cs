using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class PiecesManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private List<Color> _colors = new List<Color>();
        [SerializeField]
        private int _piecePerColorCount;

        private List<PieceModel> _pieceModels = new List<PieceModel>();

        public void Initialize()
        {
            _pieceModels = CreatePieceModels();

            PlacePieceModelsOnFreeSquares(_pieceModels);
        }

        private void PlacePieceModelsOnFreeSquares(List<PieceModel> pieceModels)
        {
            foreach (var pieceModel in pieceModels)
            {
                var freeSquareModel = GameManager.Instance.BoardManager.GetFreeSquareModel();
                pieceModel.SquareModel = freeSquareModel;
            }
        }

        private List<PieceModel> CreatePieceModels()
        {
            var pieceModels = new List<PieceModel>();
            foreach (var color in _colors)
            {
                for (var i = 0; i < _piecePerColorCount; i++)
                {
                    var pieceModel = new PieceModel(color);
                    pieceModels.Add(pieceModel);
                }
            }

            return pieceModels;
        }
    }
}
