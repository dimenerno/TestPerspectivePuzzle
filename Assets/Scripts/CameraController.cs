using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject posOrigin;
    public GameObject posRight;
    public GameObject posFront;

    public int cameraState = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            cameraState = (++cameraState) % 3;
            switch(cameraState) {
                case 0:
                    StartCoroutine(MoveCamera(posOrigin.transform.position, posOrigin.transform.rotation));
                    break;
                case 1:
                    StartCoroutine(MoveCamera(posFront.transform.position, posFront.transform.rotation));
                    break;
                case 2:
                    StartCoroutine(MoveCamera(posRight.transform.position, posRight.transform.rotation));
                    break;
            }
        }
    }

    private IEnumerator MoveCamera(Vector3 finalPos, Quaternion finalAng)
    {
            Vector3 startingPos = transform.position;
            Quaternion startingAng = transform.localRotation;
            float elapsedTime = 0;
            
            while (elapsedTime < 1f)
            {
                transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / 1f));
                transform.localRotation = Quaternion.Slerp(startingAng, finalAng, (elapsedTime / 1f));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    }
}
