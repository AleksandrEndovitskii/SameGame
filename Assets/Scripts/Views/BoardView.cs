using System.Collections.Generic;
using Components;
using Managers;

namespace Views
{
    public class BoardView : BaseComponent
    {
        private List<List<SquareView>> _squareViewInstances = new List<List<SquareView>>();

        private BoardModel _boardModel;

        protected override void Initialize()
        {
            _boardModel = GameManager.Instance.BoardManager.BoardModel;

            Redraw();
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            GameManager.Instance.BoardManager.BoardModelChanged += BoardModelChanged;
        }
        protected override void UnSubscribe()
        {
            GameManager.Instance.BoardManager.BoardModelChanged -= BoardModelChanged;
        }

        private void Redraw()
        {
            _squareViewInstances = new List<List<SquareView>>();
            for (var i = 0; i < _boardModel.Squares.Count; i++)
            {
                var squareViewsRow = new List<SquareView>();
                _squareViewInstances.Add(squareViewsRow);
                for (var j = 0; j < _boardModel.Squares[i].Count; j++)
                {
                    var squareViewInstance = GameManager.Instance.GameObjectsManager.CreateSquare();
                    squareViewInstance.gameObject.transform.SetParent(this.gameObject.transform);
                    _squareViewInstances[i].Add(squareViewInstance);
                }
            }
        }

        private void BoardModelChanged(BoardModel boardModel)
        {

        }
    }
}
