using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Transform level;
    public Text levelNumber;
    public Material opaque;

    RectTransform CanvasRect;

    List<Transform> levelArray = new List<Transform>();
    List<Text> textArray = new List<Text>();

    int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        CanvasRect = this.GetComponent<RectTransform>();

        Color c = level.gameObject.GetComponent<Renderer>().sharedMaterial.color;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 5; j++)
            {
                levelArray.Add(Instantiate(level, new Vector3(j * 2.5f, 3.5f - i * 2.0f, 0), Quaternion.identity));
                textArray.Add(Instantiate(levelNumber, WorldSpaceToUI(new Vector3(j * 2.5f, 3.5f - i * 2.0f, 0)), Quaternion.identity));
                textArray[i * 5 + j].transform.SetParent(transform, false);
                textArray[i * 5 + j].color = new Color(0.37f, 0.37f, 0.51f, 0.2f);
                textArray[i * 5 + j].text = string.Format("{0}", i * 5 + j + 1);
            }
        levelArray[0].gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        levelArray[0].gameObject.GetComponent<Renderer>().material = opaque;
        textArray[0].color = new Color(1f, 1f, 1f, 1f);
        textArray[0].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    Vector3 WorldSpaceToUI(Vector3 position)
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(position);
        return new Vector3(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)), 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && key < 14)
        {
            levelArray[key].gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            levelArray[key].gameObject.GetComponent<Renderer>().material = level.gameObject.GetComponent<Renderer>().sharedMaterial;
            textArray[key].color = new Color(0.37f, 0.37f, 0.51f, 0.2f);
            textArray[key].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            key++;
            levelArray[key].gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            levelArray[key].gameObject.GetComponent<Renderer>().material = opaque;
            textArray[key].color = new Color(1f, 1f, 1f, 1f);
            textArray[key].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (Input.GetKeyDown(KeyCode.A) && key > 0)
        {
            levelArray[key].gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            levelArray[key].gameObject.GetComponent<Renderer>().material = level.gameObject.GetComponent<Renderer>().sharedMaterial;
            textArray[key].color = new Color(0.37f, 0.37f, 0.51f, 0.2f);
            textArray[key].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            key--;
            levelArray[key].gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            levelArray[key].gameObject.GetComponent<Renderer>().material = opaque;
            textArray[key].color = new Color(1f, 1f, 1f, 1f);
            textArray[key].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
    }
}
