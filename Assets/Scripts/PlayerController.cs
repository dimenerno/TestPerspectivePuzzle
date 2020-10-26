using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int dimFlag = 0; // 3 for +x, 1 for -x, -1 for +z, -3 for -z
    bool shiftPressed = false;
    bool moving = false;
    bool falling = false;
    bool doneMoving = false;

    int x, y, z;

    private void Start() {
        x = (int)Mathf.Round(transform.position.x);
        y = (int)Mathf.Round(transform.position.y);
        z = (int)Mathf.Round(transform.position.z);
    }

    void CheckFloor() {
        int cameraState = Camera.main.gameObject.GetComponent<CameraController>().cameraState;
        if (cameraState == 0 && !falling)
        {
            if (y > 0 && !CubeArray.posCube[x, y - 1, z])
            {
                StartCoroutine(MoveY());
            }
        }

        if (cameraState == 1 && !falling)
        {
            if (y > 0)
            {
                int i;
                for (i = 0; i < 20; i++)
                {
                    if (CubeArray.posCube[x, y - 1, i])
                    {
                        transform.position = new Vector3(x, y, i);
                        z = i;
                        break;
                    }
                }
                if (i == 20)
                {
                    StartCoroutine(MoveY());
                }
            }
        }
        if (cameraState == 2 && !falling)
        {
            if (y > 0)
            {
                int i;
                for (i = 0; i < 20; i++)
                {
                    if (CubeArray.posCube[i, y - 1, z])
                    {
                        transform.position = new Vector3(i, y, z);
                        x = i;
                        break;
                    }
                }
                if (i == 20)
                {
                    StartCoroutine(MoveY());
                }
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int cameraState = Camera.main.gameObject.GetComponent<CameraController>().cameraState;
        CubeArray.movable = true;

        if (Input.GetKey(KeyCode.LeftShift)) shiftPressed = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) shiftPressed = false;
        // Moveset
        if (!moving && !falling)
        {
            if (cameraState == 1) {
                if(Input.GetKeyDown(KeyCode.A)) { StartCoroutine(MoveX(-1)); };
                if(Input.GetKeyDown(KeyCode.D)) { StartCoroutine(MoveX(1)); };
            }
            else if (cameraState == 2) {
                if(Input.GetKeyDown(KeyCode.A)) { StartCoroutine(MoveZ(-1)); };
                if(Input.GetKeyDown(KeyCode.D)) { StartCoroutine(MoveZ(1)); };
            }
            else {
                if (Input.GetKeyDown(KeyCode.D)) { StartCoroutine(MoveX(1)); }
                if (Input.GetKeyDown(KeyCode.A)) { StartCoroutine(MoveX(-1));  }
                if (Input.GetKeyDown(KeyCode.W)) { StartCoroutine(MoveZ(1));  }
                if (Input.GetKeyDown(KeyCode.S)) { StartCoroutine(MoveZ(-1));  }
            }
        }

        if(doneMoving) {
            CheckFloor();
            doneMoving = false;
        }
        GetComponent<HoverUpDown>().enabled = !moving && !falling;
    }

    IEnumerator MoveX(int direction)
    {
        moving = true;
        dimFlag = 2 + direction;

        if (Physics.OverlapSphere(new Vector3(x+direction, -1, z), 0.2f).Length == 0) CubeArray.movable = false;

        if (CubeArray.movable)
        {
            if (CubeArray.posCube[x + direction, y, z])
            {
                if (shiftPressed) StartCoroutine(Jump());
                else Physics.OverlapSphere(new Vector3(x + direction, y, z), 0.1f)[0].gameObject.GetComponent<CubeCollision>().StartMove(dimFlag);
            }
        }

        if (CubeArray.movable)
        {
            float ft = x;
            for (int i = 0; i < 10; i++)
            {
                ft += direction * 0.1f;
                transform.position = new Vector3(ft, transform.position.y, z);
                yield return null;
            }
            transform.position = new Vector3(x + direction, transform.position.y, z);
            x += direction;
            doneMoving = true;
        }
        moving = false;
    }

    IEnumerator MoveZ(int direction)
    {
        moving = true;
        dimFlag = -2 + direction;

        if (Physics.OverlapSphere(new Vector3(x, -1, z + direction), 0.2f).Length == 0) CubeArray.movable = false;

        if (CubeArray.movable)
        {
            if (CubeArray.posCube[x, y, z + direction])
            {
                if (shiftPressed) StartCoroutine(Jump());
                else Physics.OverlapSphere(new Vector3(x, y, z + direction), 0.1f)[0].gameObject.GetComponent<CubeCollision>().StartMove(dimFlag);
            }
        }

        if (CubeArray.movable)
        {
            float ft = z;
            for (int i = 0; i < 10; i++)
            {
                ft += direction * 0.1f;
                transform.position = new Vector3(x, transform.position.y, ft);
                yield return null;
            }
            transform.position = new Vector3(x, transform.position.y, z + direction);
            z += direction;
            doneMoving = true;
        }
        moving = false;
    }

    IEnumerator MoveY()
    {
        falling = true;
        float ft = y;
        for (int i = 0; i < 10; i++)
        {
            ft -= 0.1f;
            transform.position = new Vector3(x, ft, z);
            yield return null;
        }
        transform.position = new Vector3(x, y - 1, z);
        GetComponent<HoverUpDown>().standardY = y - 1;
        y--;
        falling = false;
    }


    IEnumerator Jump()
    {
        moving = true;
        float ft = y;
        for (int i = 0; i < 10; i++)
        {
            ft += 0.1f;
            transform.position = new Vector3(x, ft, z);
            yield return null;
        }

        transform.position = new Vector3(x, y + 1, z);
        GetComponent<HoverUpDown>().standardY = y + 1;
        y++;
        moving = false;
    }
}
