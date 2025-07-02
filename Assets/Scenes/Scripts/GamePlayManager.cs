using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;

    public PlayerBodyController headControler;
    public PlayerBodyController wholeBodyController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
}
