
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Pickers : MonoBehaviour
{
    [SerializeField]
    protected ShopRequest shopRequest;

    [SerializeField]
    protected Image image;

    [SerializeField]
    protected TextMeshProUGUI textField;

    private AudioSource menuSound;
    private void Awake()
    {
        menuSound = GetComponent<AudioSource>();
        OnAwakeActions();
    }
    private void OnEnable()
    {
        transform.localScale = Vector3.up;
        transform.DOScale(1f, 0.8f).SetEase(Ease.InOutElastic).SetDelay(0.2f);
        menuSound.PlayDelayed(0.42f);
        OnEnableActions();
    }

    protected virtual void OnEnableActions() { }
    protected virtual void OnAwakeActions() { }
}
