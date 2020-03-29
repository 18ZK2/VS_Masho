using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadController : MonoBehaviour
{
    public bool isLaunch = false;
    [SerializeField] float launchPow = 200f;
    [SerializeField] GameObject gravitiBall = null, launchPoint = null;

    bool sleeped = false;
    IEnumerator Launch()
    {
        while (true)
        {
            GameObject gb = null;
            if (isLaunch)
            {
                gb = Instantiate(gravitiBall, launchPoint.transform);
                Destroy(gb, 10f);
            }
            yield return new WaitForSeconds(2f);
            Vector2 vec = Random.Range(-1, 1f) * Vector2.right;
            vec += Random.Range(-1, 1f) * Vector2.up;
            gb.GetComponent<Rigidbody2D>().AddForce(vec * launchPow, ForceMode2D.Impulse);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Launch());
    }
}
