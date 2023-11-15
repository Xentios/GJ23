using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Shaker : MonoBehaviour
{
    [Range (0.1f,99f)]
    [SerializeField]
    public float duration=1f;

    [Range(0.1f, 10f)]
    [SerializeField]
    public float strength=1f;

    [SerializeField]
    public Vector3 strengthV3;

    [Range(0, 100)]
    [SerializeField]
    public int vibrato = 1;

    [Range(0, 360)]
    [SerializeField]
    public int randomness = 90;


    public bool snapping = false;

    public bool fadeout = false;


    public bool debugging = true;

    private Tween shakeTween;
    // Start is called before the first frame update
    void Start()
    {

        ShakeForever();
    }

    

    private void ShakeForever()
    {
        if (debugging == true) return;
        
        shakeTween=transform.DOShakePosition(duration, strengthV3* strength, vibrato, randomness, snapping, fadeout).OnComplete(() => ShakeForever());
    }

    public void StopShaking()
    {        
        shakeTween?.Kill(false);
    }

    public void StartShaking()
    {
        if (shakeTween?.active == false)
        {
            ShakeForever();
        }           
    }
}
