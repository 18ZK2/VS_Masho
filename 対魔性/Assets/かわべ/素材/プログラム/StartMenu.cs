using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    GameManager gm;
    bool isPlayed = false;

    public void GoToScene(string sceneName)
    {
        if (isPlayed) return;
        gm = gameObject.AddComponent<GameManager>();
        StartCoroutine(gm.WipeLoadScene(sceneName));
        isPlayed = true;
    }
}
