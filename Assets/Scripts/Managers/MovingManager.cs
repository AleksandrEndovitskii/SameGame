using System.Linq;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MovingManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private MovementDirection _movementDirection = MovementDirection.Left;

        public void Initialize()
        {
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved()
        {
            TryMovePieceModelsRecursively(MovementDirection.Bot);
            TryMovePieceModelsRecursively(_movementDirection);
        }

        private void TryMovePieceModelsRecursively(MovementDirection movementDirection)
        {
            var botPieceModels = GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                x.SquareModel.GetConnectedSquareModelByDirection(movementDirection) != null &&
                x.SquareModel.GetConnectedSquareModelByDirection(movementDirection).PieceModel == null).ToList();
            Debug.Log($"{nameof(botPieceModels)}.{nameof(botPieceModels.Count)} = {botPieceModels.Count}");
            if (botPieceModels.Count == 0)
            {
                return;
            }

            foreach (var botPieceModel in botPieceModels)
            {
                botPieceModel.SquareModel = botPieceModel.SquareModel.GetConnectedSquareModelByDirection(movementDirection);
            }

            TryMovePieceModelsRecursively(movementDirection);
        }
    }
}
