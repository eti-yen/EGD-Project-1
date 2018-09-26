using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<char> translate; 
    [SerializeField] private List<GameObject>lookup;
    [SerializeField] private string mapFile;
    [SerializeField] private float solidPC = 0.5f;
    [SerializeField] private float killPC = 0.25f;
    [SerializeField] private float minSwitchTime = 2.0f;
    [SerializeField] private float maxSwitchTime = 5.0f;
    [SerializeField] private Vector2Int offset;
    [SerializeField] private Text timer;
    [SerializeField] private Text deathCounter;
    private float lastSwitch;
    private float switchTime;
    private int lastTimer;
    void Awake()
    {
        Vector2Int size = new Vector2Int();
        if(mapFile != null && mapFile != "")
        {
            string[] rows = File.ReadAllLines(Application.dataPath + "/" + mapFile);
            
            size.y = rows.Length;
            
            for(int i = 0; i < size.y; ++i)
            {
                for(int j = 0; j < rows[i].Length; ++j)
                {
                    byte val = translateChar(rows[i][j]);
                    if(val != 255 && lookup[val] != null)
                    {
                        Vector3 ip = new Vector3(j + offset.x, size.y - 1 - i + offset.y, 0);
                        Instantiate(lookup[val], ip, Quaternion.identity, transform);
                    }
                }
            }
            
            var blockSwitches = GetComponentsInChildren<BlockSwitch>();
            foreach(BlockSwitch b in blockSwitches)
            {
                b.SwitchState(0);
            }
        }
        Camera.main.backgroundColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        
        lastSwitch = Time.time;
        switchTime = Random.Range(minSwitchTime, maxSwitchTime);
        lastTimer = (int)Time.time;
    }
    
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        float currentTime = Time.time;
        if(currentTime - lastSwitch >= switchTime)
        {
            lastSwitch = currentTime;
            switchTime = Random.Range(minSwitchTime, maxSwitchTime);
            float value = Random.value;
            int index;
            if(value < solidPC)
            {
                index = 0;
            }
            else if(value < solidPC + killPC)
            {
                index  = 1;
            }
            else
            {
                index = 2;
            }
            var blockSwitches = GetComponentsInChildren<BlockSwitch>();
            foreach(BlockSwitch b in blockSwitches)
            {
                b.SwitchState(index);
            }
            
            Camera.main.backgroundColor = new Color(randColValue, randColValue, randColValue, 1.0f);
        }
        
        if((int)Time.time - lastTimer > 0)
        {
            lastTimer = (int)Time.time;
            EventSingleton.GetInstance().Tick();
            if(EventSingleton.GetInstance().GetTime() <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        timer.text = EventSingleton.GetInstance().GetTime().ToString();
        deathCounter.text = EventSingleton.GetInstance().GetDeaths().ToString();
    }
    
    private byte translateChar(char value)
    {
        for(byte i = 0; i < translate.Count; ++i)
        {
            if(translate[i] == value)
            {
                return i;
            }
        }
        return 255;
    }
    
    private static float randColValue {get {return Random.value * 0.5f + 0.5f;}}
}
