using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTumuraController : MonoBehaviour
{
    [SerializeField] float bombTime = 7f;


    float deadTime = 0f;
    Transform body;
    ParticleSystem.EmissionModule emmision;
    EnemyContloller ec;
    // Start is called before the first frame update
    void Start()
    {
        body = transform.Find("Body");

        var p = GetComponent<ParticleSystem>();
        emmision = p.emission;

        ec = GetComponent<EnemyContloller>();
        
    }

    // Update is called once per frame
    void Update()
    {
        deadTime += Time.deltaTime;
        body.localScale = Vector3.right * 3 * (deadTime + 1) / bombTime + new Vector3(0, 1, 1);
        emmision.rateOverTimeMultiplier = deadTime * 10f;
        if (deadTime > bombTime)
        {
            ec.Damage(ec.HP);
        }
    }
}
