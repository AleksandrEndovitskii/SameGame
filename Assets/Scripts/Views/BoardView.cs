using System.Collections.Generic;
using Components;
using Managers;
using Models;

namespace Views
{
    public class BoardView : BaseComponent
    {
        private List<List<SquareView>> _squareViewInstances = new List<List<SquareView>>();

        private BoardModel _boardModel;

        protected override void Initialize()
        {
            BoardModelChanged(GameManager.Instance.BoardManager.BoardModel);
        }
        protected override void UnInitialize()
        {
            Clear();
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
            Clear();
            Fill();
        }

        private void Clear()
        {
            for (var i = 0; i < _squareViewInstances.Count; i++)
            {
                for (var j = 0; j < _squareViewInstances[i].Count; j++)
                {
                    Destroy(_squareViewInstances[i][j]);
                }
            }

            _squareViewInstances.Clear();
        }
        private void Fill()
        {
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
            _boardModel = GameManager.Instance.BoardManager.BoardModel;

            Redraw();
        }
    }
}
