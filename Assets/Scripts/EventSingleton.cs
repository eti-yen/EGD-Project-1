using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class EventSingleton
{
    public static EventSingleton GetInstance()
    {
        if(instance == null)
        {
            instance = new EventSingleton();
        }
        
        return instance;
    }
    
    public int GetPlayer()
    {
        return player;
    }
    
    public void SwapPlayer()
    {
        player = 1 - player;
        score = 0;
    }
    
    public void SetName(string name)
    {
        playerName = name;
    }
    
    public void SetTime(int time)
    {
        this.time = time;
    }
    
    public void Goal()
    {
        if(!died)
        {
            events.Add(player + " 1cc");
        }
        
        score = Math.Max(time, 0);
        events.Add(player + " win " + score);
    }
    
    public int GetScore()
    {
        return score;
    }
    
    public void AddDeath()
    {
        events.Add(player + " die");
        died = true;
    }
    
    public void SendEvents()
    {
        IEnumerable<string> namedEvents = events.Select(e => playerName + " " + e);
        File.WriteAllLines(Application.dataPath + "/" + ip, namedEvents.ToArray());
        events = new List<string>();
        died = false;
    }
    
    public void Tick()
    {
        --time;
    }
    
    private EventSingleton()
    {
        playerName = "";
        ip = "events.txt";
        time = 0;
        player = 0;
        events = new List<string>();
        died = false;
        score = 0;
    }
    
    private static EventSingleton instance;
    
    private int player;
    private int time;
    private bool died;
    private string ip;
    private string playerName;
    private List<string> events;
    private int score;
}
