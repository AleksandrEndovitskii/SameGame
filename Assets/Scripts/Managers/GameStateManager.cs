using System;
using System.Collections.Generic;
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
                Value = Utils.GameState.NotStarted
            };

            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();

            GameState = null;
        }

        public override void Subscribe()
        {
            _gameStateOnChangedSubscription = GameState.Pairwise().Subscribe(GameStateOnChanged);

            GameManager.Instance.PiecesManager.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
            GameManager.Instance.PiecesManager.PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged += PiecesManagerOnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged;
        }
        public override void UnSubscribe()
        {
            _gameStateOnChangedSubscription?.Dispose();

            if (GameManager.Instance.PiecesManager != null)
            {
                GameManager.Instance.PiecesManager.PieceModelsRemoved -= PiecesManagerOnPieceModelsRemoved;
            }
            if (GameManager.Instance.PiecesManager != null)
            {
                GameManager.Instance.PiecesManager.PieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged -= PiecesManagerOnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged;
            }
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            if (GameManager.Instance.PiecesManager.PieceModels.Count == 0)
            {
                GameState.Value = Utils.GameState.Win;
            }
        }
        private void PiecesManagerOnPieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColorChanged(List<PieceModel> pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor)
        {
            if (pieceModelsWithAtLeastOneConnectedPieceModelOfTheSameColor.Count == 0)
            {
                GameState.Value = Utils.GameState.Loss;
            }
        }
        private void GameStateOnChanged(Pair<GameState> pair)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{pair.Previous}->{pair.Current}");
        }
    }
}
