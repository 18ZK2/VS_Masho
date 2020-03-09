using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshot : MonoBehaviour
{
    [SerializeField] public Transform targetObj;
    [SerializeField] private TextAlignment distanceUI;
    public float MAX_DISTANCE=500;
    private float colliderOffset;
    private Animator shoter=null;
    public GameObject tumura;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        colliderOffset = GetComponent<CapsuleCollider>().radius + targetObj.GetComponent<CapsuleCollider>().radius;
        shoter = GetComponent<Animator>();
        Vector3 respown_t = GameObject.Find("発射機").transform.position;
        x = respown_t.x;
        y = respown_t.y;
        z = respown_t.z;
    } 

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, targetObj.position) - colliderOffset;
        Debug.Log(distance);
        
        if (distance <= 400)
        {
            shoter.SetBool("distance",true);
           

        }
        else if (distance > 400)
        {
            shoter.SetBool("distance",false);
        }
     }
    void animation_event()
    {
        for (int i=0; i> 5; i++)
        {
            GameObject enemy = Instantiate(tumura);
            enemy.transform.position = new Vector3(x, y, z);
        }
    }
}
