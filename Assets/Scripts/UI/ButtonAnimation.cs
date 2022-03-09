using DG.Tweening;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;

    public void Play() => transform.DOPunchRotation(_rotation, _duration);
}