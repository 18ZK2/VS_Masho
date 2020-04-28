using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiraokaFlashController : MonoBehaviour
{
    [SerializeField] float distance = 750f;
    [SerializeField] Tile on = null, off = null;


    Transform player;
    Tilemap map;
    BoundsInt bound;
    IEnumerator flashJudge()
    {
        while (player != null)
        {
            for (int y = bound.max.y; y >= bound.min.y; --y)
            {
                for (int x = bound.min.x; x < bound.max.x; x++)
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
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
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
