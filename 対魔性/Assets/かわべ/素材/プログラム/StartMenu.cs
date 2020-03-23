using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField] string sceneName = "Map";
    GameManager gm;
    bool isPlayed = false;

    public void GoToScene()
    {
        if (isPlayed) return;
        gm = gameObject.AddComponent<GameManager>();
        StartCoroutine(gm.WipeLoadScene(sceneName));
        isPlayed = true;
    }
}
