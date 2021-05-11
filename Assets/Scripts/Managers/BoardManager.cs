using UnityEngine;
using Utils;

namespace Managers
{
    public class BoardManager : MonoBehaviour, IInitilizable
    {
        [SerializeField]
        private int _rowsCount;
        [SerializeField]
        private int _cellsCount;

        private BoardModel _boardModel;

        public void Initialize()
        {
            _boardModel = new BoardModel(_rowsCount, _cellsCount);
        }
    }
}
