using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _maxScoreText;
    [SerializeField] private PlayerInput _playerInput;
    private int _count;
    private int _maxScore;
    private void OnEnable() => _playerInput.OnTouchUp += AddCount;

    private void OnDisable()
    {
        _playerInput.OnTouchUp -= AddCount;
        TryUpdateMaxScore();
        ClearScore();
    }

    private void TryUpdateMaxScore()
    {
        if (_count <= _maxScore) return;
        _maxScore = _count;
        _maxScoreText.text = "Max Score: " + _maxScore;
    }

    public void AddCount()
    {
        _count++;
        UpdateText();
    }

    private void ClearScore()
    {
        _count = 0;
        UpdateText();
    }

    private void UpdateText() => _text.text = _count.ToString();
}