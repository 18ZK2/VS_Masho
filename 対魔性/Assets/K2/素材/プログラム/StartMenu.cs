using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    GameManager_ gm;
    bool isPlayed = false;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager_>();
    }
    public void GoToScene(string sceneName)
    {
        if (isPlayed) return;
        gm.StartCoroutine(gm.WipeLoadScene(sceneName));
        isPlayed = true;
    }
}
