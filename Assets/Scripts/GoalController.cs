using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    public Vector3 RotateAmount;  // degrees per second to rotate in each axis. Set in inspector.

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
        {
            GameManager.instance.LevelClear();
            Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, Color.white, 1.0f);
        }
    }
}
