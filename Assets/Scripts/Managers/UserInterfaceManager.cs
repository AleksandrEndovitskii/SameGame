using Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class UserInterfaceManager : BaseMonoBehaviour
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private EventSystem _eventSystemPrefab;

        private Canvas _canvasInstance;
        private EventSystem _eventSystemInstance;

        public override void Initialize()
        {
            if (_canvasInstance == null)
            {
                _canvasInstance = Instantiate(_canvasPrefab);
            }
            if (_eventSystemInstance == null)
            {
                _eventSystemInstance = Instantiate(_eventSystemPrefab);
            }

            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();

            if (_eventSystemInstance != null)
            {
                Destroy(_eventSystemInstance.gameObject);
                _eventSystemInstance = null;
            }
            if (_canvasInstance != null)
            {
                Destroy(_canvasInstance.gameObject);
                _canvasInstance = null;
            }
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }
    }
}
