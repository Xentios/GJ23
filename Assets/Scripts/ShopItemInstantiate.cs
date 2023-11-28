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

    private void OnEnable()
    {
        StartCoroutine(OnEnableSpawn());
        GetComponent<AudioSource>().PlayDelayed(1f);
    }

    private IEnumerator OnEnableSpawn()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            Instantiate(itemPrefab, spawnPoint, Random.rotation).name = "Spike " + i + 1;

           
            yield return new WaitForSeconds(3f / SpawnCount);
        }
    }
}
