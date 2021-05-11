using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(UserInterfaceManager))]
    [RequireComponent(typeof(BoardManager))]
    public class GameManager : MonoBehaviour, IInitilizable
    {
        // static instance of GameManager which allows it to be accessed by any other script
        public static GameManager Instance;

        public UserInterfaceManager UserInterfaceManager => this.gameObject.GetComponent<UserInterfaceManager>();
        public BoardManager BoardManager => this.gameObject.GetComponent<BoardManager>();

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
        }
    }
}
