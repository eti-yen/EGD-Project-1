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
        
        events.Add(player + " win " + time);
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
        time = 1000;
        player = 0;
        events = new List<string>();
        died = false;
    }
    
    private static EventSingleton instance;
    
    private int player;
    private int time;
    private bool died;
    private string ip;
    private string playerName;
    private List<string> events;
    
}
