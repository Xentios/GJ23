using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CollectedResources")]
public class CollectedResources : ScriptableObject
{
    [SerializeField]
    public List<int> ResourceList;

}
