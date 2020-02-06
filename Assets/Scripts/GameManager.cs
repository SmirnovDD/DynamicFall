using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public LevelGenerator lg;
    public TextMeshProUGUI levelNumberText;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        levelNumberText.text = "Level " + lg.currentLevel.ToString();

        if (Screen.height > 1980)
            Camera.main.fieldOfView = 70;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
