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

        protected override void Initialize()
        {
            Redraw(GameManager.Instance.GameStateManager.GameState.Value);
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            _gameStateManagerOnGameStateChangedSubscription = GameManager.Instance.GameStateManager.GameState.Subscribe(GameStateManagerOnGameStateChanged);
        }
        protected override void UnSubscribe()
        {
            _gameStateManagerOnGameStateChangedSubscription?.Dispose();
        }

        private void Redraw(GameState gameState)
        {
            this.gameObject.SetActive(gameState == _gameState);
        }

        private void GameStateManagerOnGameStateChanged(GameState gameState)
        {
            Redraw(gameState);
        }
    }
}
