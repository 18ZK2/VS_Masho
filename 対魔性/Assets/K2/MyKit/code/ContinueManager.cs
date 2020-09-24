using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ContinueManager : MonoBehaviour
{
    bool pushed = false;
    [SerializeField] string sceneName = "Title";
    IEnumerator Wait()
    {
        pushed = true;
        yield return new WaitForSeconds(1.5f);
        pushed = false;
    }
    void Start()
    {
        StartCoroutine(Wait());
    }
    private void Update()
    {
        if (Input.anyKeyDown && !pushed)
        {
            pushed = true;
            GameManager_ manager = GameObject.Find("GameManager").GetComponent<GameManager_>();
            manager.StartCoroutine(manager.WipeLoadScene(sceneName));
        }
    }

}
