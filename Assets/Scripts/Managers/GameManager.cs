using System;
using Components;
using UniRx;
using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(BoardManager))]
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(PiecesManager))]
    [RequireComponent(typeof(SelectionManager))]
    [RequireComponent(typeof(MovingManager))]
    [RequireComponent(typeof(GameStateManager))]
    public class GameManager : BaseMonoBehaviour
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public BoardManager BoardManager => this.gameObject.GetComponent<BoardManager>();
        public GameObjectsManager GameObjectsManager => this.gameObject.GetComponent<GameObjectsManager>();
        public ScoreManager ScoreManager => this.gameObject.GetComponent<ScoreManager>();
        public UserInterfaceManager UserInterfaceManager => this.gameObject.GetComponent<UserInterfaceManager>();
        public PiecesManager PiecesManager => this.gameObject.GetComponent<PiecesManager>();
        public SelectionManager SelectionManager => this.gameObject.GetComponent<SelectionManager>();
        public MovingManager MovingManager => this.gameObject.GetComponent<MovingManager>();
        public GameStateManager GameStateManager => this.gameObject.GetComponent<GameStateManager>();

        private IDisposable _gameStateOnChangedSubscription;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager
                    Destroy(gameObject);
                }
            }

            Initialize();

            GameStateManager.GameState.Value = GameState.InProgress;
        }
        private void OnDestroy()
        {
            UnInitialize();

            Instance = null;
        }

        public override void Initialize()
        {
            BoardManager.Initialize();
            GameObjectsManager.Initialize();
            ScoreManager.Initialize();
            UserInterfaceManager.Initialize();
            PiecesManager.Initialize();
            SelectionManager.Initialize();
            MovingManager.Initialize();
            GameStateManager.Initialize();

            Subscribe();
        }
        public override void UnInitialize()
        {
            UnSubscribe();

            GameStateManager.UnInitialize();
            MovingManager.UnInitialize();
            SelectionManager.UnInitialize();
            ScoreManager.UnInitialize();
            PiecesManager.UnInitialize();
            UserInterfaceManager.UnInitialize();
            GameObjectsManager.UnInitialize();
            BoardManager.UnInitialize();
        }

        public override void Subscribe()
        {
            _gameStateOnChangedSubscription = GameStateManager.GameState.Subscribe(GameStateOnChanged);
        }
        public override void UnSubscribe()
        {
            _gameStateOnChangedSubscription?.Dispose();
        }

        private void GameStateOnChanged(GameState gameState)
        {
            if (gameState != GameState.InProgress)
            {
                return;
            }

            ReInitialize();
        }
    }
}
