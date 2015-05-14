using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SequenceTest : MonoBehaviour 
{
    public Transform cube;

    public void OnMove()
    {
        Sequence s = DOTween.Sequence();
        s.Append(cube.DOMove(new Vector3(0, 4, 0), 2).SetRelative());
        s.Append(cube.DOMove(new Vector3(0, -8, 0), 0).SetRelative());
    }
}
