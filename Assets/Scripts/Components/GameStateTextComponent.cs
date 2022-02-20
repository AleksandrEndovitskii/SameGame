using System;
using Managers;
using TMPro;
using UniRx;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameStateTextComponent : BaseComponent
    {
        private TextMeshProUGUI _text;

        private readonly string _defaultValue = $"{nameof(GameManager.Instance.GameStateManager.GameState)}: ";
        private IDisposable _scoreManagerOnScoreChangedSubscription;

        public override void Initialize()
        {
            _text = this.gameObject.GetComponent<TextMeshProUGUI>();

            Redraw(GameManager.Instance.ScoreManager.Score.Value);
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _scoreManagerOnScoreChangedSubscription = GameManager.Instance.ScoreManager.Score.Subscribe(ScoreManagerOnScoreChanged);
        }
        public override void UnSubscribe()
        {
            _scoreManagerOnScoreChangedSubscription?.Dispose();
        }

        private void Redraw(int score)
        {
            _text.text = _defaultValue + score;
        }

        private void ScoreManagerOnScoreChanged(int score)
        {
            Redraw(score);
        }
    }
}
