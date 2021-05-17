using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(BoardManager))]
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(PiecesManager))]
    [RequireComponent(typeof(ScoreManager))]
    [RequireComponent(typeof(SelectionManager))]
    [RequireComponent(typeof(MovingManager))]
    [RequireComponent(typeof(GameStateManager))]
    public class GameManager : MonoBehaviour, IInitilizable
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public BoardManager BoardManager => this.gameObject.GetComponent<BoardManager>();
        public GameObjectsManager GameObjectsManager => this.gameObject.GetComponent<GameObjectsManager>();
        public UserInterfaceManager UserInterfaceManager => this.gameObject.GetComponent<UserInterfaceManager>();
        public PiecesManager PiecesManager => this.gameObject.GetComponent<PiecesManager>();
        public ScoreManager ScoreManager => this.gameObject.GetComponent<ScoreManager>();
        public SelectionManager SelectionManager => this.gameObject.GetComponent<SelectionManager>();
        public MovingManager MovingManager => this.gameObject.GetComponent<MovingManager>();
        public GameStateManager GameStateManager => this.gameObject.GetComponent<GameStateManager>();

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
        }

        public void Initialize()
        {
            BoardManager.Initialize();
            GameObjectsManager.Initialize();
            UserInterfaceManager.Initialize();
            PiecesManager.Initialize();
            ScoreManager.Initialize();
            SelectionManager.Initialize();
            MovingManager.Initialize();
            GameStateManager.Initialize();
        }
    }
}
