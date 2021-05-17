using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private Canvas _canvasPrefab;
        [SerializeField]
        private EventSystem _eventSystemPrefab;

        private Canvas CanvasInstance;
        private EventSystem EventSystemInstance;

        public void Initialize()
        {
            CanvasInstance = Instantiate(_canvasPrefab);
            EventSystemInstance = Instantiate(_eventSystemPrefab);
        }
        public void UnInitialize()
        {
            Destroy(EventSystemInstance);
            Destroy(CanvasInstance);
        }
    }
}
