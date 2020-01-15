using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Text levelNum;
    public int currentLevel;
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Number", 1);
        levelNum.text = "Level " + currentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
