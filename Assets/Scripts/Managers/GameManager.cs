using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(BoardManager))]
    [RequireComponent(typeof(GameObjectsManager))]
    [RequireComponent(typeof(PiecesManager))]
    [RequireComponent(typeof(ScoreManager))]
    public class GameManager : MonoBehaviour, IInitilizable
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public UserInterfaceManager UserInterfaceManager => this.gameObject.GetComponent<UserInterfaceManager>();
        public BoardManager BoardManager => this.gameObject.GetComponent<BoardManager>();
        public GameObjectsManager GameObjectsManager => this.gameObject.GetComponent<GameObjectsManager>();
        public PiecesManager PiecesManager => this.gameObject.GetComponent<PiecesManager>();
        public ScoreManager ScoreManager => this.gameObject.GetComponent<ScoreManager>();

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
            UserInterfaceManager.Initialize();
            BoardManager.Initialize();
            GameObjectsManager.Initialize();
            PiecesManager.Initialize();
            ScoreManager.Initialize();
        }
    }
}
