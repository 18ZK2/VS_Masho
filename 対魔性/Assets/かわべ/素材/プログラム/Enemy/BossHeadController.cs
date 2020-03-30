using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeadController : MonoBehaviour
{
    public bool isLaunch = false;
    [SerializeField] float launchPow = 200f, LaunchLate = 5f;
    [SerializeField] GameObject gravitiBall = null, launchPoint = null;
    [SerializeField] AudioClip SE;

    bool sleeped = false;
    AudioSource ass;

    IEnumerator Launch()
    {
        while (true)
        {
            GameObject gb = null;
            GravityBall b = null;
            if (isLaunch)
            {
                gb = Instantiate(gravitiBall, launchPoint.transform);
                b = gb.GetComponent<GravityBall>();
                b.enabled = false;
                Destroy(gb, 11.3f);
            }

            yield return new WaitForSeconds(2f);
            
            if (gb != null)
            {
                ass.PlayOneShot(SE);
                b.enabled = true;
                Vector2 vec = Random.Range(-1, 1f) * Vector2.right;
                vec += Random.Range(-1, 1f) * Vector2.up;
                gb.GetComponent<Rigidbody2D>().AddForce(vec * launchPow, ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(5f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
        StartCoroutine(Launch());
    }
}
