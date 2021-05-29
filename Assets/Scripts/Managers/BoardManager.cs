using System.Linq;
using Helpers;
using Models;
using UniRx;
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

        public ReactiveProperty<BoardModel> BoardModel;

        public void Initialize()
        {
            BoardModel = new ReactiveProperty<BoardModel>()
            {
                Value = new BoardModel(_rowsCount, _cellsCount)
            };
            BoardModel.Subscribe(PieceModelOnChanged);
        }

        public SquareModel GetFreeSquareModel()
        {
            var squareModelsRowWithFreeSquare =
                BoardModel.Value.SquareModels.FirstOrDefault(x => x.Any(y => y.PieceModel.Value == null));
            var freeSquareModel = squareModelsRowWithFreeSquare?.FirstOrDefault(x => x.PieceModel.Value == null);

            return freeSquareModel;
        }
        private void PieceModelOnChanged(BoardModel boardModel)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
