using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    int dimFlag = 0; // 3 for +x, 1 for -x, -1 for +z, -3 for -z
    bool moving = false;
    bool falling = false;

    int x, y, z;

    private void Awake() {
        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);
        z = (int)Mathf.Round(transform.position.z);
    }

    private void LateUpdate() {

        GetComponent<HoverUpDown>().enabled = !moving && !falling;

        // If there is no cube below, fall down until it meets floor
        if(y > 0 && !CubeArray.posCube[x, y - 1, z]) {
            falling = true;
            Debug.Log(new Vector3(x, transform.position.y - 0.1f, z));
            transform.position = new Vector3(x, transform.position.y - 0.1f, z);
        }
        else {
            if(falling) {
                transform.position = new Vector3(x, y, z);
                falling = false;
                GetComponent<HoverUpDown>().standardY = y;
            }
        }
    }

    public void StartMove(int d) {
        dimFlag = d;
        switch(dimFlag) {
            case 3:
                StartCoroutine(MoveX(1));
                break;
            case 1:
                StartCoroutine(MoveX(-1));
                break;
            case -1:
                StartCoroutine(MoveZ(1));
                break;
            case -3:
                StartCoroutine(MoveZ(-1));
                break;
        }
    }

    IEnumerator MoveX(int direction) 
    {
        moving = true;
        dimFlag = 2 + direction;

        if(x + direction < 0) CubeArray.movable = false;

        if(CubeArray.movable)         
            if(CubeArray.posCube[x + direction, y, z])
                Physics.OverlapSphere(new Vector3(x + direction, y, z), 0.1f)[0].gameObject.GetComponent<CubeCollision>().StartMove(dimFlag);

        if(CubeArray.movable) {
            float ft = x;
            for(int i = 0; i < 10; i++)
            {
                ft += direction*0.1f;
                transform.position = new Vector3(ft, transform.position.y, z);
                yield return null;
            }
            transform.position = new Vector3(x + direction, transform.position.y, z);
            x += direction;
        }
        moving = false;
    }

    IEnumerator MoveZ(int direction) 
    {
        moving = true;
        dimFlag = -2 + direction;

        if(z + direction < 0) CubeArray.movable = false;

        if(CubeArray.movable)
            if(CubeArray.posCube[x, y, z + direction])
                Physics.OverlapSphere(new Vector3(x, y, z+direction), 0.1f)[0].gameObject.GetComponent<CubeCollision>().StartMove(dimFlag);

        if(CubeArray.movable) {
            float ft = z;
            for(int i = 0; i < 10; i++)
            {
                ft += direction*0.1f;
                transform.position = new Vector3(x, transform.position.y, ft);
                yield return null;
            }
            transform.position = new Vector3(x, transform.position.y, z + direction);
            z += direction;
        }
        moving = false;
    }
}
