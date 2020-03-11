using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField] string sceneName = "Map";
    GameManager gm;

    public void GoToScene()
    {
        gm = gameObject.AddComponent<GameManager>();
        StartCoroutine(gm.WipeLoadScene(sceneName));
    }
}
