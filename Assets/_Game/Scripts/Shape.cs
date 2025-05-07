using UnityEngine;
using DG.Tweening;

public class Shape : MonoBehaviour
{
    [SerializeField] private Transform _crane;

    [SerializeField] private float _cyclelength = 1;

    void Start()
    {
        transform.DOMove(new Vector3(0,25,0), _cyclelength).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);

        _crane.DORotate(new Vector3(0, 360, 0), _cyclelength * 0.5f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }


}
