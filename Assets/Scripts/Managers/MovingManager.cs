using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MovingManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private Direction direction = Direction.Left;

        public void Initialize()
        {
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            TryMovePieceModelsRecursively(Direction.Bot);
            TryMovePieceModelsRecursively(direction);
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
