using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiraokaFlashController : MonoBehaviour
{
    [SerializeField] float distance = 750f;
    [SerializeField] Tile on = null, off = null;

    float camSize;
    Transform player;
    Tilemap map;
    BoundsInt bound;
    IEnumerator flashJudge()
    {
        /*Debug.Log("Y");
        Debug.Log(bound.max.y);
        Debug.Log(bound.min.y);
        Debug.Log("X");
        Debug.Log(bound.max.x);
        Debug.Log(bound.min.x);*/
        while (player != null)
        {
            Vector3 pPos = player.transform.position;
            int yMin = (int)(pPos.y - camSize) / 16;
            int yMax = (int)(pPos.y + camSize) / 16;
            int xMin = (int)(pPos.x - camSize) / 16;
            int xMax = (int)(pPos.x + camSize) / 16;
            //Debug.Log("YMin Ymax" + yMin.ToString() + yMax.ToString());
            //Debug.Log("XMin YMax" + xMin.ToString() + xMax.ToString());
            for (int y = yMin; y < yMax; y++)
            {
                for (int x = xMin; x < xMax; x++)
                {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    Vector3 tp = new Vector3(x * 16, y * 16, 0);
                    bool t = map.HasTile(tilePos);
                    if (t)
                    {

                        float diff = (player.position - tp).magnitude;
                        Tile tile = (diff < distance) ? on : off;
                        map.SetTile(tilePos, tile);
                        //tile.sprite = 
                    }
                }
                yield return new WaitForSeconds(0.0025f);
            }
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        camSize = Camera.main.orthographicSize;
        map = GetComponent<Tilemap>();
        //タイルマップがある範囲を取得
        bound = map.cellBounds;
        StartCoroutine(flashJudge());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
