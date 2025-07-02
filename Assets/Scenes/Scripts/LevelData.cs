using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelData : MonoBehaviour
{
    public static LevelData instance;
    
    //Level2
    public GameObject specialKeyObstracle;
    public GameObject specialKey;
    public GameObject buttonObstracle1;
    public GameObject buttonObstracle2;
    public GameObject buttonObstracle3;
    public GameObject buttonObstracle4;
    public GameObject buttonCover;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SceneLoader.instance.key != null && SceneLoader.instance.door != null)
        {
            SceneLoader.instance.key.SetActive(true);
            SceneLoader.instance.door.SetActive(false);
        }

        //Level 2 
        if (buttonCover == null)
        {
            Destroy(buttonCover);
        }
        else
        {
            buttonCover.SetActive(true);
        }
        
        if (specialKey != null)
        {
            specialKey.SetActive(true);
        }

        if (specialKey == null)
        {
            Destroy(specialKey);
        }

        if (buttonObstracle1 == null)
        {
            Destroy(buttonObstracle1);
        } 
        else if (buttonObstracle2 == null)
        {
            Destroy(buttonObstracle2);
        } 
        else if (buttonObstracle3 == null)
        {
            Destroy(buttonObstracle3);
        } 
        else if (buttonObstracle4 == null)
        {
            Destroy(buttonObstracle4);
        }
      
    }

   public void KeyObtained(GameObject key,GameObject door)
   {
       if (SceneLoader.instance.key != null && SceneLoader.instance.door != null)
       {
          key.SetActive(false);
          door.SetActive(true);
       }

   }

   public void Level2ObstracleManuplation(GameObject obstracle,GameObject special_Key)
   {
       if (specialKeyObstracle && specialKey == null)
       {
           Destroy(obstracle);
           Destroy(special_Key);
       }
       else
       {
           obstracle.SetActive(false);
           special_Key.SetActive(false);
       }
   }

   public void Level2ButtonPressed(GameObject buttonObstracle1,GameObject buttonObstracle2,GameObject buttonObstracle3,GameObject
       buttonObstracle4)
   {
       if (buttonObstracle1 == null && buttonObstracle2 == null && buttonObstracle3 == null && buttonObstracle4 == null)
       {
            Destroy(buttonObstracle1);
            Destroy(buttonObstracle2);
            Destroy(buttonObstracle3);
            Destroy(buttonObstracle4);
       }
       else
       {
           buttonObstracle1.SetActive(false);
           buttonObstracle2.SetActive(false);    
           buttonObstracle3.SetActive(false);    
           buttonObstracle4.SetActive(false);    
       }
   }

   public void Level2ButtonCover(GameObject buttonCover)
   {
       buttonCover.SetActive(false);
   }
}
