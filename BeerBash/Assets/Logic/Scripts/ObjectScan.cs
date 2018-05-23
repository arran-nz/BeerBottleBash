using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScan<T>{

    GameObject targetIdentifier;



    public T GetClosestObject(Vector3 scanPosition, float radius, LayerMask layerMask)
    {
        GameObject[] objects = GetObjects(scanPosition, radius, layerMask);

        if (objects != null)
        {
            float distance = radius;
            T currentObject = objects[0].GetComponent<T>();

            for (int i = 0; i < objects.Length; i++)
            {
                float d = Vector3.Distance(scanPosition, objects[i].transform.position);

                if (d < distance)
                {
                    distance = d;
                    currentObject = objects[i].GetComponent<T>();
                }
            }


            return currentObject;
        }
        else
        {
            return default(T);
        }

    }

    private GameObject[] GetObjects(Vector3 scanPosition, float radius, LayerMask layerMask)
    {

        Collider[] colliders = Physics.OverlapSphere(scanPosition, radius, layerMask);
        if (colliders.Length > 0)
        {
            GameObject[] objectArray = new GameObject[colliders.Length];

            for (int i = 0; i < colliders.Length; i++)
            {

                if (colliders[i].GetComponent<T>() != null)
                {
                    objectArray[i] = colliders[i].gameObject;
                }
            }

            return objectArray;
        }
        else
        {
            return null;
        }

    }

}
