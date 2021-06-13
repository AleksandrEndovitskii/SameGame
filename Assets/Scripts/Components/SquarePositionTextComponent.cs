using TMPro;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SquarePositionTextComponent : BaseComponent
    {
        [SerializeField]
        private SquareView _squareView;

        private TextMeshProUGUI _text;

        public override void Initialize()
        {
            _text = this.gameObject.GetComponent<TextMeshProUGUI>();
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }

        private void Start()
        {
            if (_squareView == null)
            {
                Debug.LogError($"{this.GetType().Name}.{nameof(Start)} aborted" +
                               $"\n {nameof(_squareView)} == null");

                return;
            }

            // TODO: move this from Start to _squareView.IndexChanged subscription
            _text.text = _squareView.Position.ToString();
        }
    }
}
