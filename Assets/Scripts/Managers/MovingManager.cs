using System.Linq;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MovingManager : MonoBehaviour, IInitilizable
    {
        public void Initialize()
        {
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved()
        {
            TryMovePieceModelsDownRecursively();
        }

        private void TryMovePieceModelsDownRecursively()
        {
            var botPieceModels = GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                x.SquareModel.Bot != null &&
                x.SquareModel.Bot.PieceModel == null).ToList();
            Debug.Log($"{nameof(botPieceModels)}.{nameof(botPieceModels.Count)} = {botPieceModels.Count}");
            if (botPieceModels.Count == 0)
            {
                return;
            }

            foreach (var botPieceModel in botPieceModels)
            {
                botPieceModel.SquareModel = botPieceModel.SquareModel.Bot;
            }

            TryMovePieceModelsDownRecursively();
        }
    }
}
