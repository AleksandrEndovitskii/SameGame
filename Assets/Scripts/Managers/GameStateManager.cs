using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameStateManager : MonoBehaviour, IInitilizable
    {
        public Action<GameState> GameStateChanged = delegate {  };

        public GameState GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                if (_gameState == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(GameStateChanged)}" +
                          $"\n {_gameState}->{value}");

                _gameState = value;

                GameStateChanged.Invoke(_gameState);
            }
        }

        private GameState _gameState;

        public void Initialize()
        {
            GameState = GameState.InProgress;

            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            if (GameManager.Instance.PiecesManager.PieceModels.Count == 0)
            {
                GameState = GameState.Win;

                return;
            }

            var pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor =
                GameManager.Instance.PiecesManager.PieceModels.Where(x =>
                    x.SquareModel.ConnectedSquareModelsOfTheSameColor.Count > 1).ToList();
            if (pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor.Count == 0)
            {
                GameState = GameState.Loss;

                return;
            }
        }
    }
}
