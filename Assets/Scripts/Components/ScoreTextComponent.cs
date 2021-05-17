using Managers;
using TMPro;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreTextComponent : BaseComponent
    {
        private TextMeshProUGUI _text;

        private string _defaultValue = $"{nameof(GameManager.Instance.ScoreManager.Score)}: ";

        protected override void Initialize()
        {
            _text = this.gameObject.GetComponent<TextMeshProUGUI>();

            Redraw(GameManager.Instance.ScoreManager.Score);
        }

        protected override void UnInitialize()
        {
        }

        protected override void Subscribe()
        {
            GameManager.Instance.ScoreManager.ScoreChanged += ScoreManagerOnScoreChanged;
        }
        protected override void UnSubscribe()
        {
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
