using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MovingManager : BaseMonoBehaviour
    {
        public Action PieceModelsMoved = delegate {  };

        [SerializeField]
        private Direction direction = Direction.Left;

        public override void Initialize()
        {
            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();
        }

        public override void Subscribe()
        {
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }
        public override void UnSubscribe()
        {
            if (GameManager.Instance.PiecesManager != null)
            {
                GameManager.Instance.PiecesManager.PieceModelsRemoved -= PiecesManagerOnPieceModelsRemoved;
            }
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            TryMovePieceModelsRecursively(Direction.Bot);
            TryMovePieceModelsRecursively(direction);

            PieceModelsMoved.Invoke();
        }

        private void TryMovePieceModelsRecursively(Direction direction)
        {
            var botPieceModels = GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                x.SquareModel.Value.GetConnectedSquareModel(direction) != null &&
                x.SquareModel.Value.GetConnectedSquareModel(direction).PieceModel.Value == null).ToList();
            Debug.Log($"{nameof(botPieceModels)}.{nameof(botPieceModels.Count)} = {botPieceModels.Count}");
            if (botPieceModels.Count == 0)
            {
                return;
            }

            foreach (var botPieceModel in botPieceModels)
            {
                botPieceModel.SquareModel.Value = botPieceModel.SquareModel.Value.GetConnectedSquareModel(direction);
            }

            TryMovePieceModelsRecursively(direction);
        }
    }
}
