using System;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Components
{
    public class GameStateActivationComponent : BaseComponent
    {
        [SerializeField]
        private GameState _gameState;

        private IDisposable _gameStateManagerOnGameStateChangedSubscription;

        public override void Initialize()
        {
            if (GameManager.Instance.GameStateManager.GameState != null)
            {
                GameStateManagerOnGameStateChanged(GameManager.Instance.GameStateManager.GameState.Value);
            }
            else
            {
                GameStateManagerOnGameStateChanged(GameState.NotStarted);
            }
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            if (GameManager.Instance.GameStateManager.GameState != null)
            {
                _gameStateManagerOnGameStateChangedSubscription = GameManager.Instance.GameStateManager.GameState.Subscribe(GameStateManagerOnGameStateChanged);
            }
        }
        public override void UnSubscribe()
        {
            _gameStateManagerOnGameStateChangedSubscription?.Dispose();
        }

        private void Redraw(GameState gameState)
        {
            var isActive = gameState == _gameState;
            this.gameObject.SetActive(isActive);
        }

        private void GameStateManagerOnGameStateChanged(GameState gameState)
        {
            Redraw(gameState);
        }
    }
}
