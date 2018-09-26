using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using System.Text;

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
    
    public int GetTime()
    {
        return time;
    }
    
    public void Goal()
    {
        if(deaths == 0)
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
    
    public void SetIp(string address)
    {
        ip = address;
    }
    
    public void AddDeath()
    {
        events.Add(player + " die");
        deaths += 1;
    }
    
    public int GetDeaths()
    {
        return deaths;
    }
    
    public void SendEvents()
    {
        try
        {
            TcpClient client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();

            IEnumerable<string> namedEvents = events.Select(e => playerName + " " + e);
            foreach(string s in namedEvents)
            {
                byte[] data = Encoding.ASCII.GetBytes(s + "\n");
                stream.Write(data, 0, data.Length);
            }
            
            stream.Close();
            client.Close();
        }
        catch(Exception){}   

        events = new List<string>();
        deaths = 0;
    }
    
    public void Tick()
    {
        --time;
    }
    
    private EventSingleton()
    {
        playerName = "";
        ip = "127.0.0.1";
        time = 0;
        player = 0;
        events = new List<string>();
        score = 0;
        deaths = 0;
    }
    
    private static EventSingleton instance;
    
    private int player;
    private int time;
    private int deaths;
    private string ip;
    private string playerName;
    private List<string> events;
    private int score;
    private static int port = 8192;
}
