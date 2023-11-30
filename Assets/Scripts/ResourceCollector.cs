using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ADV;
using DG.Tweening;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField]
    private GameEvent scoreChanged;


    [SerializeField]
    private FloatVariable score;

    [SerializeField]
    public CollectedResources CollectedResources;

    [SerializeField]
    private InputActionReference mouseMovement;

    [SerializeField]
    private LayerMask layerMask;

    private Plane visualPlane;

    private AudioSource collectionSound;
    private void Awake()
    {
        collectionSound = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        mouseMovement.action.performed += MouseMovement;
    }

    private void OnDisable()
    {
        mouseMovement.action.performed -= MouseMovement;
    }

    private void Start()
    {
        visualPlane = new Plane(Vector3.back, Vector3.zero);
    }

    private void MouseMovement(InputAction.CallbackContext callbackContext)
    {

        var mousePosition = callbackContext.ReadValue<Vector2>();
        Debugger.Log("Mouse Movement " + mousePosition, Debugger.PriorityLevel.LeastImportant);
        var ray = Camera.main.ScreenPointToRay(mousePosition);


        var modelPos = Vector3.zero;
        if (visualPlane.Raycast(ray, out float distance))
        {
            modelPos = ray.GetPoint(distance);
        }
        transform.position = modelPos;


        Collider[] results = new Collider[100];
        var count = Physics.OverlapSphereNonAlloc(modelPos, 2.2f, results, layerMask);
        for (int i = count - 1; i >= 0; i--)
        {

            var id = results[i].transform.parent.GetComponentInParent<ResourceHandler>().resourceID;
            var points = (id + 1) * 3;
            CombatControl.Main.GlobalCard.ChangeKey("Point", points);
            score.Value += points;
            CollectAnim(results[i].gameObject);
            scoreChanged.TriggerEvent();


        }

    }

    private void CollectAnim(GameObject go)
    {
        collectionSound.pitch = Random.Range(0.5f, 1f);
        collectionSound.Play();

        go.layer = 0;
        go.transform.DOScale(0, 0.95f);
        go.transform.DOBlendableLocalMoveBy(Vector3.up * 3f, 1f).OnComplete(() =>
          Destroy(go));
    }
}
