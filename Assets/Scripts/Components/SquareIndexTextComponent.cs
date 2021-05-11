using TMPro;
using UnityEngine;
using Views;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SquareIndexTextComponent : BaseComponent
    {
        [SerializeField]
        private SquareView _squareView;

        private TextMeshProUGUI _text;

        protected override void Initialize()
        {
            if (_squareView == null)
            {
                return;
            }

            _text = this.gameObject.GetComponent<TextMeshProUGUI>();
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
        }
        protected override void UnSubscribe()
        {
        }

        private void Start()
        {
            // TODO: move this from Start to _squareView.IndexChanged subscription
            _text.text = _squareView.Index.ToString();
        }
    }
}
