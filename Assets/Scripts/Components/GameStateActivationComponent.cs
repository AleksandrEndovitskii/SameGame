using Managers;
using UnityEngine;
using Utils;

namespace Components
{
    public class GameStateActivationComponent : BaseComponent
    {
        [SerializeField]
        private GameState _gameState;

        protected override void Initialize()
        {
            Redraw(GameManager.Instance.GameStateManager.GameState);
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            GameManager.Instance.GameStateManager.GameStateChanged += GameStateManagerOnGameStateChanged;
        }
        protected override void UnSubscribe()
        {
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
