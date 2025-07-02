using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int value = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
        GameObject levelPrefab = Resources.Load<GameObject>($"Levels/Level{currentLevel}");
        Instantiate(levelPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            int currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
            currentLevel++;
            PlayerPrefs.SetInt("currentLevel",currentLevel);
            SceneManager.LoadScene("Test");
        }
    }
}
