using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;  
    public GameObject gameOver;
    public GameObject key;
    public GameObject door;
    public Transform spawnPoint;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (key == null)
        {
            Destroy(key);
        }

        if (door == null)
        {
            Destroy(door);
        }
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        GameObject levelPreab = Resources.Load<GameObject>($"Levels/Level{currentLevel}");
        GameObject child = Instantiate(levelPreab);
        
        //Functions are using child here
        KeyAndDoorFinder(child);
        //Level2
        SpecialKeyObstracleAndKeyFinder(child);
        ButtonObstracleFinder(child);
        ButtonCover(child);
        
        //Level3
        SetPlayerSpawnPoint(child, currentLevel);
        
        HideGameOverScreen();  
    }

    public void SetPlayerSpawnPoint(GameObject level, int currentLevel)
    {

        if (currentLevel == 3)
        {
            // For Level 3 spawnposition
            spawnPoint = level.transform.Find("SpawnPoint"); 
        }
        if (spawnPoint != null)
        {
            PlayerBodyController.instance.transform.position = spawnPoint.position;
        }
       
    }

    public void KeyAndDoorFinder(GameObject level)
    {
        key = level.transform.Find("Key").gameObject;
        Debug.Log("Calling here");
        door = level.transform.Find("Door").gameObject;

    }

    public void SpecialKeyObstracleAndKeyFinder(GameObject level)
    {
        LevelData.instance.specialKey = level.transform.Find("SpecialKey")?.gameObject;
        LevelData.instance.specialKeyObstracle = level.transform.Find("SpecialKeyObstracle")?.gameObject;
    }

    public void ButtonCover(GameObject level)
    {
        LevelData.instance.buttonCover = level.transform.Find("Cover")?.gameObject;
    }

    public void ButtonObstracleFinder(GameObject level)
    {
        LevelData.instance.buttonObstracle1 = level.transform.Find("buttonObstracle1")?.gameObject;
        LevelData.instance.buttonObstracle2 = level.transform.Find("buttonObstracle2")?.gameObject;
        LevelData.instance.buttonObstracle3 = level.transform.Find("buttonObstracle3")?.gameObject;
        LevelData.instance.buttonObstracle4 = level.transform.Find("buttonObstracle4")?.gameObject;
        
    }
    
    public void ShowGameOverScreen()
    {
        Debug.Log("Game Over");
        var clone = gameOver.transform.Find("GameOver");  
        var child = clone.GetComponent<Image>();
        child.gameObject.SetActive(true);  
    }

    public void HideGameOverScreen()
    {
        var clone = gameOver.transform.Find("GameOver");
        var child = clone.GetComponent<Image>();
        child.gameObject.SetActive(false);  
    }

    public void RestartGame()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
       
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel",currentLevel);
        SceneManager.LoadScene("Level");


    }

  

  
}
