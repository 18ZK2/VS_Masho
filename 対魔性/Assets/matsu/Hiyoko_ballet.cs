using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiyoko_ballet : MonoBehaviour
{
    public float Hiyo_ballet_damege = 1;
    // Start is called before the first frame update

    void Start()
    {

    }
    private void OnParticleCollision(GameObject obj)
    {
        if (obj.tag == "Player")
        {
            obj.GetComponent<PlayerContloller>().Damage(Hiyo_ballet_damege);
        }
        else if(obj.tag == "Gimmick")
        {
            obj.GetComponent<GimmickContloller>().HP -= Hiyo_ballet_damege;
        }
        Debug.Log("hitDeath");
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
