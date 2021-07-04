using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonComponent : BaseComponent
    {
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

        public abstract void ButtonOnClick();
    }
}
