using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public LevelGenerator lg;
    public Text levelNumberText;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        levelNumberText.text = "Level " + lg.currentLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
