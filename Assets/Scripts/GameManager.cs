using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}

    private int numberOfLevelsUnlocked = 1;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) instance = this;
        if(instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void LevelClear() {
        numberOfLevelsUnlocked++;
        Debug.Log(numberOfLevelsUnlocked);
    }

    public int GetNumberOfLevelsUnlocked() {
        return numberOfLevelsUnlocked;
    }
}
