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
            Redraw(GameManager.Instance.GameStateManager.GameState.Value);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _gameStateManagerOnGameStateChangedSubscription = GameManager.Instance.GameStateManager.GameState.Subscribe(GameStateManagerOnGameStateChanged);
        }
        public override void UnSubscribe()
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
