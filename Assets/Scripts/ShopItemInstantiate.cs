using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemInstantiate : MonoBehaviour
{
    [SerializeField]
    private GameObject itemPrefab;

    [SerializeField]
    public float RandomSpawnForce;

    [SerializeField]
    public int SpawnCount;

    private Vector3 spawnPoint;


    private void Awake()
    {
        spawnPoint = transform.GetChild(0).transform.position;
    }

    private IEnumerator Start()
    {
        for (int i = 0; i < SpawnCount; i++)
        {            
            Instantiate(itemPrefab, spawnPoint, Random.rotation);
            
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
            yield return new WaitForSeconds(3f/SpawnCount);
        }
    }
}
