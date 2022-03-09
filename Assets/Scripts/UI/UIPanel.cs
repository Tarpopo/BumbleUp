using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private UnityEvent _onActiveAnimationEnd;
    private CanvasGroup _canvasGroup;
    private void Start() => _canvasGroup = GetComponent<CanvasGroup>();

    public void SetActive(bool active)
    {
        _canvasGroup.DOFade(active ? 1 : 0, _duration).onComplete = () =>
        {
            if (active == false) return;
            _onActiveAnimationEnd?.Invoke();
        };
        _canvasGroup.interactable = active;
        _canvasGroup.blocksRaycasts = active;
    }
}