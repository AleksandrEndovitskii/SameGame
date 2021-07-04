using Helpers;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Views;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class PieceButtonComponent : ButtonComponent
    {
        [SerializeField]
        private PieceView _pieceView;

        public override void ButtonOnClick()
        {
            if (_pieceView == null)
            {
                Debug.LogError($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()} aborted" +
                               $"\n {nameof(_pieceView)} == null");

                return;
            }

            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");

            GameManager.Instance.SelectionManager.Select(_pieceView.PieceModel.Value);
        }
    }
}
