using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUpDown : MonoBehaviour
{
    // Start is called before the first frame update

    float hoverSpeed = 2.6f;
    float hoverAmplitude = 0.07f;
    private float rand;
    public float standardY;

    private void Start() {
        standardY = transform.position.y;
        rand = Random.Range(0, 5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = transform.position.x;
        float y = standardY + Mathf.Sin(rand + Time.timeSinceLevelLoad * hoverSpeed) * hoverAmplitude;
        float z = transform.position.z;

        transform.position = new Vector3(x, y, z);
    }
}
