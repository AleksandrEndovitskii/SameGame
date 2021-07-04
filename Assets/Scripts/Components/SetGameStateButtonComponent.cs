using Managers;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class SetGameStateButtonComponent : ButtonComponent
    {
        [SerializeField]
        private GameState _gameState;

        public override void ButtonOnClick()
        {
            GameManager.Instance.GameStateManager.GameState.Value = _gameState;
        }
    }
}
