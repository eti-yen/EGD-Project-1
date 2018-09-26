using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    [SerializeField] private Vector3 step;
    [SerializeField] private int[] keys;
    [SerializeField] private int[] values;
    [SerializeField] private int length;
    
    private Dictionary<int, int> penalties;
    
    void Awake()
    {
        penalties = new Dictionary<int, int>();
        for(int i = 0; i < keys.Length && i < values.Length; ++i)
        {
            penalties[keys[i]] = values[i];
        }
    }
    
    public Vector3 GetStep()
    {
        return step;
    }
    
    public int GetIndex(int key)
    {
        if(penalties.ContainsKey(key))return penalties[key];
        
        if(key < 0)return 0;
        if(key >= length - 1)return -1;
        return -2;
    }
    public int GetLength()
    {
        return length;
    }
}
