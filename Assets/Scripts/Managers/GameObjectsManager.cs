﻿using Models;
using UnityEngine;
using Utils;
using Views;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private BoardView _boardViewPrefab;
        [SerializeField]
        private SquareView _squareViewPrefab;

        private BoardView _boardViewInstance;

        public void Initialize()
        {
            _boardViewInstance = CreateBoard();
            _boardViewInstance.gameObject.transform.SetParent(GameManager.Instance.UserInterfaceManager.CanvasInstance.transform);

            RedrawCompleted();
            _boardViewInstance.RedrawCompleted += RedrawCompleted;
        }
        public void UnInitialize()
        {
        }

        private void RedrawCompleted()
        {
            CenterBoardViewInstancePosition();
        }

        private void CenterBoardViewInstancePosition()
        {
            _boardViewInstance.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public SquareView CreateSquare(SquareModel squareModel)
        {
            var squareViewInstance = Instantiate(_squareViewPrefab);
            squareViewInstance.Initialize(squareModel);
            return squareViewInstance;
        }
        public BoardView CreateBoard()
        {
            var boardViewInstance = Instantiate(_boardViewPrefab);
            return boardViewInstance;
        }
    }
}