using System;
using Managers;
using TMPro;
using UniRx;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreTextComponent : BaseComponent
    {
        private TextMeshProUGUI _text;

        private readonly string _defaultValue = $"{nameof(GameManager.Instance.ScoreManager.Score)}: ";
        private IDisposable _scoreManagerOnScoreChangedSubscription;

        protected override void Initialize()
        {
            _text = this.gameObject.GetComponent<TextMeshProUGUI>();

            Redraw(GameManager.Instance.ScoreManager.Score.Value);
        }
        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            _scoreManagerOnScoreChangedSubscription = GameManager.Instance.ScoreManager.Score.Subscribe(ScoreManagerOnScoreChanged);
        }
        protected override void UnSubscribe()
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
