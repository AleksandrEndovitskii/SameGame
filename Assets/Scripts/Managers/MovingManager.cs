using System.Linq;
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

        private void PiecesManagerOnPieceModelsRemoved()
        {
            TryMovePieceModelsRecursively(Direction.Bot);
            TryMovePieceModelsRecursively(direction);
        }

        private void TryMovePieceModelsRecursively(Direction direction)
        {
            var botPieceModels = GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                x.SquareModel.GetConnectedSquareModel(direction) != null &&
                x.SquareModel.GetConnectedSquareModel(direction).PieceModel == null).ToList();
            Debug.Log($"{nameof(botPieceModels)}.{nameof(botPieceModels.Count)} = {botPieceModels.Count}");
            if (botPieceModels.Count == 0)
            {
                return;
            }

            foreach (var botPieceModel in botPieceModels)
            {
                botPieceModel.SquareModel = botPieceModel.SquareModel.GetConnectedSquareModel(direction);
            }

            TryMovePieceModelsRecursively(direction);
        }
    }
}
