using System;
using System.Linq;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class BoardManager : MonoBehaviour, IInitilizable
    {
        public Action<BoardModel> BoardModelChanged = delegate {  };

        [SerializeField]
        private int _rowsCount;
        [SerializeField]
        private int _cellsCount;

        public BoardModel BoardModel
        {
            get
            {
                return _boardModel;
            }
            set
            {
                if (_boardModel == value)
                {
                    return;
                }

                Debug.Log($"{this.GetType().Name}.{nameof(BoardModelChanged)}");

                _boardModel = value;

                BoardModelChanged.Invoke(_boardModel);
            }
        }

        private BoardModel _boardModel;

        public void Initialize()
        {
            BoardModel = new BoardModel(_rowsCount, _cellsCount);
        }

        public SquareModel GetFreeSquareModel()
        {
            var squareModelsRowWithFreeSquare =
                BoardModel.SquareModels.FirstOrDefault(x => x.Any(y => y.PieceModel.Value == null));
            var freeSquareModel = squareModelsRowWithFreeSquare?.FirstOrDefault(x => x.PieceModel.Value == null);

            return freeSquareModel;
        }
    }
}
