using Managers;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class PieceButtonComponent : BaseComponent
    {
        [SerializeField]
        private PieceView _pieceView;

        private Button _button;

        protected override void Initialize()
        {
            _button = this.gameObject.GetComponent<Button>();
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            _button.onClick.AddListener(ButtonOnClick);
        }
        protected override void UnSubscribe()
        {
            _button.onClick.RemoveListener(ButtonOnClick);
        }

        private void ButtonOnClick()
        {
            if (_pieceView == null)
            {
                Debug.LogError($"{this.GetType().Name}.{nameof(ButtonOnClick)} aborted" +
                               $"\n {nameof(_pieceView)} == null");

                return;
            }

            GameManager.Instance.PiecesManager.Remove(_pieceView.PieceModel);
        }
    }
}