using System.Collections.Generic;
using System.Linq;
using Helpers;
using Models;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameStateManager : MonoBehaviour, IInitilizable
    {
        public ReactiveProperty<GameState> GameState = new ReactiveProperty<GameState>();

        private GameState _gameState;

        public void Initialize()
        {
            GameState.Value = Utils.GameState.InProgress;

            GameState.Subscribe(PieceModelOnChanged);
            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            if (GameManager.Instance.PiecesManager.PieceModels.Count == 0)
            {
                GameState.Value = Utils.GameState.Win;

                return;
            }

            var pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor =
                GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                    x.SquareModel.ConnectedSquareModelsOfTheSameColor.Count >= 1).ToList();
            if (pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor.Count == 0)
            {
                GameState.Value = Utils.GameState.Loss;

                return;
            }
        }
        private void PieceModelOnChanged(GameState gameState)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
