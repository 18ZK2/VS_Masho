using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;
using System;

public class GameManager_ : MonoBehaviour
{
    public const int treasures = 1;
    PlayerContloller pc;
    AudioSource AudioSource;
    public static string nowscene;//現在のシーン
    public static float LoadHP = 15f;
    List<GameObject> gameObjects = new List<GameObject>(); //宝箱
    public List<string[]> vs = new List<string[]>();
    bool loading = false;
    GameObject player;
    StreamWriter sw;
    StreamReader sr;
    Transform tr;

    public IEnumerator WipeLoadScene(string sceneName)
    {
        loading = true;
        //この関数よく使うのでちょっと汚くなたよ
        PostEffect pe = GameObject.Find("Main Camera").GetComponent<PostEffect>();
        for (float wipetime = 1f; wipetime > 0f; wipetime -= 0.01f)
        {
            if (pe == null) break;
            pe.wipeCircle.SetFloat("_Radius", wipetime);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        loading = false;
    }
    public void Save(int index, string value)
    {
        // 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
        sr = new StreamReader(@"saveData.csv", Encoding.GetEncoding("UTF-8"));
        int i = 0;
        string line = "";
        string[] data = null;
        List<string[]> vs = new List<string[]>();
        while ((line = sr.ReadLine()) != null)//行を読み込み
        {
            // コンソールに出力
            var arr = line.Split(','); //,で区切る
            vs.Add(arr);
            if (i == index) //index(書き込みたい行)
            {
                arr[1] = value; //value(書き込みたい値)
                data = arr;
            }
            i++;
        }
        sr.Close();
        sw = new StreamWriter(@"saveData.csv", false, Encoding.GetEncoding("UTF-8"));
        foreach(string[] v in vs)
        {
            string a = "";
            if (v[0] == data[0])
            {
                a = string.Join(",", data);
            }
            else
            {
                a = string.Join(",", v);
            }
            sw.WriteLine(a);
        }
        sw.Close();
    }
    public void Load()
    {
        TreasureBoxController tbc=null;
        int i = -8;
        // 引数説明：第1引数→ファイル読込先, 第2引数→エンコード
        sr = new StreamReader(@"saveData.csv", Encoding.GetEncoding("UTF-8"));
        string line = "";
        //List<string[]> vs = new List<string[]>();
        while ((line = sr.ReadLine()) != null)
        {
            
            var arr = line.Split(',');
            if (arr[0] == "CanUseGun") pc.canUseGun = System.Convert.ToBoolean(arr[1]);
            if (arr[0] == "CanUseAx") pc.canUseAx = System.Convert.ToBoolean(arr[1]);
            else if (arr[0] == "MaxHp") { pc.MaxPlayerHp = (float)System.Convert.ToDouble(arr[1]); pc.PlayerHp = pc.MaxPlayerHp; }
            else if (arr[0] == "speed") pc.speed = (float)System.Convert.ToDouble(arr[1]);
            else if (arr[0] == "dashPow") pc.dashPow = (float)System.Convert.ToDouble(arr[1]);
            else if (arr[0] == "MaxStamina") pc.MAX_STAMINA = (float)System.Convert.ToDouble(arr[1]);
            else if (arr[0] == "recovSpeed") pc.recoverySpeed = (float)System.Convert.ToDouble(arr[1]);
            else if (arr[0] == "Treasure" + i)
            {
                
                for(int j = 0; j < gameObjects.Count; j++) //管理番号と一致するものを探す
                {
                    tbc = gameObjects[j].GetComponent<TreasureBoxController>();
                    if (tbc.num==i) tbc.oneopened= System.Convert.ToBoolean(arr[1]);
                }
            }
            i++;
        }
        sr.Close();
    }
    public void Setup()
    {
        // ファイル書き出し
        // 現在のフォルダにsaveData.csvを出力する(決まった場所に出力したい場合は絶対パスを指定してください)
        // 引数説明：第1引数→ファイル出力先, 第2引数→ファイルに追記(true)or上書き(false), 第3引数→エンコード
        sw = new StreamWriter(@"saveData.csv", false, Encoding.GetEncoding("UTF-8"));
        string[] s1 = { "PlayerValue", "Value" };
        string[] makedData = { "makedData", "" + true };
        string[] canUseGun = { "CanUseGun", "" + false };
        string[] canUseAx = { "CanUseAx", "" + false };
        string[] MaxHp = { "MaxHp", "" + 15 };
        string[] speed = { "speed", "" + 1000 };
        string[] dashPow = { "dashPow", "" + 1000 };
        string[] MaxStamina = { "MaxStamina", "" + 200 };
        string[] recovSpeed = { "recovSpeed", "" + 2 };
        string[][] tre2 = new string[treasures][];
        for (int i = 0; i < treasures; i++)
        {
            int j = i + 1;
            string[] tre = { "Treasure" + j, "" + false };//全部初期化
            tre2[i] = tre;
        }
        string[][] b = { s1, makedData, canUseGun, canUseAx, MaxHp, speed, dashPow, MaxStamina, recovSpeed };
        string[][] a = new string[9 + treasures][];
        for (int i = 0; i < 9; i++) a[i] = b[i];
        for (int i = 9; i < 9 + treasures; i++) a[i] = tre2[i - 9];
        foreach (string[] s in a)
        {
            sw.WriteLine(string.Join(",", s));
        }
        sw.Close();
    }

    private void Awake()
    {
        if (GameObject.Find("Treasures") != null)
        {
            tr = GameObject.Find("Treasures").transform;
            foreach (Transform child in tr)
            {
                if (child.GetComponent<TreasureBoxController>().num != 0)
                {
                    gameObjects.Add(child.gameObject);
                }
            }
        }
        Save(1, "" + true);
        AudioSource = GetComponent<AudioSource>();
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            nowscene = SceneManager.GetActiveScene().name;
            pc = player.GetComponent<PlayerContloller>();
            pc.PlayerHp = LoadHP;
            Load();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Setup();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        if (Input.GetKey(KeyCode.R)&&pc!=null &&!loading) SceneManager.LoadScene(nowscene);
        if (pc != null && pc.PlayerHp <= 0)
        {
            StartCoroutine(WipeLoadScene("GameOver"));
        }
    }

    public void SoundEffect(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
