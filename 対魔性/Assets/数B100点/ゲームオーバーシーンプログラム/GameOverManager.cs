using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    GameManager gm;
    bool loaded = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !loaded)
        {
            loaded = true;
            if (GameManager.nowscene != null) gm.StartCoroutine(gm.WipeLoadScene(GameManager.nowscene));//SceneManager.LoadScene(GameManager.nowscene);
            else gm.StartCoroutine(gm.WipeLoadScene("Title"));
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}
