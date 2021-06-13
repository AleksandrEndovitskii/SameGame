using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Helpers;
using Models;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameStateManager : BaseMonoBehaviour
    {
        public ReactiveProperty<GameState> GameState;

        private GameState _gameState;
        private IDisposable _gameStateOnChangedSubscription;

        public override void Initialize()
        {
            GameState = new ReactiveProperty<GameState>
            {
                Value = Utils.GameState.InProgress
            };

            Subscribe();
        }
        public override void UnInitialize()
        {
            GameState = null;

            UnSubscribe();
        }

        public override void Subscribe()
        {
            _gameStateOnChangedSubscription = GameState.Subscribe(GameStateOnChanged);

            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }
        public override void UnSubscribe()
        {
            _gameStateOnChangedSubscription?.Dispose();

            if (GameManager.Instance.PiecesManager != null)
            {
                GameManager.Instance.PiecesManager.PieceModelsRemoved -= PiecesManagerOnPieceModelsRemoved;
            }
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
                    x.SquareModel.Value.ConnectedSquareModelsOfTheSameColor.Count >= 1).ToList();
            if (pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor.Count == 0)
            {
                GameState.Value = Utils.GameState.Loss;

                return;
            }
        }
        private void GameStateOnChanged(GameState gameState)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
