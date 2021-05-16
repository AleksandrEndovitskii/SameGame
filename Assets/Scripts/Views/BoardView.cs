using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Managers;
using Models;
using UnityEngine;

namespace Views
{
    public class BoardView : BaseComponent
    {
        public Action RedrawCompleted = delegate {  };

        private List<List<SquareView>> _squareViewInstances = new List<List<SquareView>>();

        private BoardModel _boardModel;

        protected override void Initialize()
        {
            BoardManagerOnBoardModelChanged(GameManager.Instance.BoardManager.BoardModel);
        }
        protected override void UnInitialize()
        {
            Clear();
        }

        protected override void Subscribe()
        {
            GameManager.Instance.BoardManager.BoardModelChanged += BoardManagerOnBoardModelChanged;
        }
        protected override void UnSubscribe()
        {
        }

        private void Redraw()
        {
            Clear();
            Fill();

            Resize();
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
            for (var i = 0; i < _boardModel.SquareModels.Count; i++)
            {
                var squareViewsRow = new List<SquareView>();
                _squareViewInstances.Add(squareViewsRow);
                for (var j = 0; j < _boardModel.SquareModels[i].Count; j++)
                {
                    var squareModel = _boardModel.SquareModels[j][i];
                    var squareViewInstance = GameManager.Instance.GameObjectsManager.CreateSquare(squareModel);
                    squareViewInstance.gameObject.transform.SetParent(this.gameObject.transform);
                    _squareViewInstances[i].Add(squareViewInstance);
                }
            }
        }

        private void SetPositions()
        {
            for (var i = 0; i < _squareViewInstances.Count; i++)
            {
                for (var j = 0; j < _squareViewInstances[i].Count; j++)
                {
                    var squareViewInstance = _squareViewInstances[i][j];
                    squareViewInstance.gameObject.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(i * squareViewInstance.Width, j * squareViewInstance.Height);
                }
            }
        }

        private void Resize()
        {
            var width = 0f;
            var height = 0f;

            if (_squareViewInstances.Count > 0 &&
                _squareViewInstances[0].Count > 0)
            {
                // TODO: not sure in this implementation
                // TODO: can calculate width and height only if all rows have same length and all cells have same length
                var squareViewInstance = _squareViewInstances[0][0];
                width = squareViewInstance.Width * _squareViewInstances.Count;
                height = squareViewInstance.Height * _squareViewInstances[0].Count;
            }

            this.gameObject.GetComponent<RectTransform>()
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            this.gameObject.GetComponent<RectTransform>()
                .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

            StartCoroutine(RunActionAfterFrame(() =>
            {
                RedrawCompleted.Invoke();

                SetPositions();
            }));
        }

        private void BoardManagerOnBoardModelChanged(BoardModel boardModel)
        {
            _boardModel = GameManager.Instance.BoardManager.BoardModel;

            Redraw();
        }

        private IEnumerator RunActionAfterFrame(Action action)
        {
            yield return new WaitForEndOfFrame();

            action?.Invoke();
        }
    }
}
