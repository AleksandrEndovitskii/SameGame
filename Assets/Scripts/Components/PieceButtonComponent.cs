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

        public override void Initialize()
        {
            _button = this.gameObject.GetComponent<Button>();
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _button.onClick.AddListener(ButtonOnClick);
        }
        public override void UnSubscribe()
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

            Debug.Log($"{this.GetType().Name}.{nameof(ButtonOnClick)}");

            GameManager.Instance.SelectionManager.Select(_pieceView.PieceModel.Value);
        }
    }
}
