using UnityEngine;
using Utils;
using Views;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private SquareView _squareViewPrefab;

        public void Initialize()
        {
        }
        public void UnInitialize()
        {
        }

        public SquareView CreateSquare()
        {
            var squareViewInstance = Instantiate(_squareViewPrefab);
            return squareViewInstance;
        }
    }
}