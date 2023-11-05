//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using EzySlice;

///**
// * Represents a really badly written shatter script! use for reference purposes only.
// */
//public class RuntimeShatterExample : MonoBehaviour
//{
//    public GameObject parent;
//    public GameObject objectToShatter;
//    public Material crossSectionMaterial;


//    public List<GameObject> prevShatters = new List<GameObject>();

//    public GameObject[] ShatterObject(GameObject obj, Material crossSectionMaterial = null)
//    {
//        return obj.SliceInstantiate(GetRandomPlane(obj.transform.position, obj.transform.localScale),
//                                                            new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
//                                                            crossSectionMaterial);
//    }
//    //public GameObject[] ShatterObjectWithLocation(GameObject obj, Transform parent ,Material crossSectionMaterial = null)
//    //{
//    //    return obj.SliceInstantiate(GetRandomPlane(parent.position, parent.localScale),
//    //                                                        new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f),
//    //                                                        crossSectionMaterial);
//    //}

//    public EzySlice.Plane GetRandomPlane(Vector3 positionOffset, Vector3 scaleOffset)
//    {
//        Vector3 randomPosition = Random.insideUnitSphere/2;

//        //randomPosition += positionOffset;

//        Vector3 randomDirection = Random.insideUnitSphere.normalized;     
//        return new EzySlice.Plane(randomPosition, randomDirection);
//    }

//    public void RandomShatter2(int count)
//    {

//    }
//    public void RandomShatterToParent()
//    {
//        if (prevShatters.Count == 0)
//        {
//            GameObject[] shatters = ShatterObject(objectToShatter, crossSectionMaterial);

//            if (shatters != null && shatters.Length > 0)
//            {
//                objectToShatter.SetActive(false);

//                // add rigidbodies and colliders
//                foreach (GameObject shatteredObject in shatters)
//                {
//                    try
//                    {
//                        shatteredObject.AddComponent<MeshCollider>().convex = true;
//                    }
//                    catch (System.Exception)
//                    {

//                    }
//                    shatteredObject.AddComponent<Rigidbody>();
//                    shatteredObject.AddComponent<DestroyAfter2Seconds>();
//                    shatteredObject.transform.position = parent.transform.position;

//                    prevShatters.Add(shatteredObject);
//                }
//            }

//        }

//        for (int i = 0; i < prevShatters.Count; i++)
//        {
//            GameObject randomObject = prevShatters[i];


//            // otherwise, shatter the previous shattered objects, randomly picked
//            //GameObject randomObject = prevShatters[Random.Range(0, prevShatters.Count - 1)];

//            GameObject[] randShatter = ShatterObject(randomObject, crossSectionMaterial);

//            if (randShatter != null && randShatter.Length > 0)
//            {
//                randomObject.SetActive(false);

//                // add rigidbodies and colliders
//                foreach (GameObject shatteredObject in randShatter)
//                {
//                    try
//                    {
//                        shatteredObject.AddComponent<MeshCollider>().convex = true;
//                    }
//                    catch (System.Exception)
//                    {

//                    }
//                    shatteredObject.AddComponent<DestroyAfter2Seconds>();
//                    var test = shatteredObject.AddComponent<Rigidbody>();
//                    test.AddForce((Random.insideUnitSphere- parent.transform.forward*1.1f) * 300);
//                    //test.AddForce(parent.transform.forward *-100);
//                    //prevShatters.Add(shatteredObject);
//                }
//            }
//        }
//    }

//    public void DrawPlane(Vector3 position, Vector3 normal)
//    {
//        Vector3 v3;

//        if (normal.normalized != Vector3.forward)
//            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
//        else
//            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

//        var corner0 = position + v3;
//        var corner2 = position - v3;
//        var q = Quaternion.AngleAxis(90.0f, normal);
//        v3 = q * v3;
//        var corner1 = position + v3;
//        var corner3 = position - v3;

//        Debug.DrawLine(corner0, corner2, Color.green);
//        Debug.DrawLine(corner1, corner3, Color.green);
//        Debug.DrawLine(corner0, corner1, Color.green);
//        Debug.DrawLine(corner1, corner2, Color.green);
//        Debug.DrawLine(corner2, corner3, Color.green);
//        Debug.DrawLine(corner3, corner0, Color.green);
//        Debug.DrawRay(position, normal, Color.red);
//    }
//}
