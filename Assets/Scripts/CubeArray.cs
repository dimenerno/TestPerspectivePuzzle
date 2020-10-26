using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeArray : MonoBehaviour
{
    public static bool[, ,] posCube = new bool[20, 20, 20];
    public static bool movable = true;

    void Update()
    {   
        for(int i = 0; i < 20; i++)
            for(int j = 0; j < 20; j++)
                for(int k = 0; k < 20; k++) posCube[i, j, k] = false;
        GameObject[] arrCube = GameObject.FindGameObjectsWithTag("Cube");
        for(int i = 0; i < arrCube.Length; i++)
        {
            Vector3 pos = arrCube[i].transform.position;
            posCube[(int)Mathf.Round(pos.x), (int)Mathf.Round(pos.y), (int)Mathf.Round(pos.z)] = true;
        }
    }
}
