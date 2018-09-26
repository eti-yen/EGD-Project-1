using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Linq;
using UnityEngine;

public struct EventArgs
{
    public string playerName;
    public int player;
    public string eventName;
    public string[] args;
}

public class EventManager : MonoBehaviour
{
    [SerializeField] private int portNumber;
    [SerializeField] private bool debugIP;
    IPAddress ipAddress;
    Thread listenerThread;
    
    public static List<EventArgs> events {get; private set;}

    void Start()
    {
        events = new List<EventArgs>();
        if(debugIP)
        {
            ipAddress = IPAddress.Parse("127.0.0.1");
        }
        else
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
            ipAddress = null;
            for(int i = 0; i < ipHostInfo.AddressList.Length; ++i)
            {
                if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                      ipAddress = ipHostInfo.AddressList[i];
                      break;
                }
            }
        }
        
        listenerThread = new Thread(new ThreadStart(Run)); 		
		listenerThread.IsBackground = true; 		
        listenerThread.Start(); 
    }
    
    void Run()
    {
        TcpListener listener = new TcpListener(ipAddress, portNumber);
        listener.Start();
        Debug.Log("Listening at " + ipAddress.ToString() + " on port " + portNumber);
        while(true)
        {
            try
            {
                TcpClient client = listener.AcceptTcpClient();
                
                Debug.Log("Client Connected");
                Process(client);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    
    void Process(TcpClient client)
    {
        try
        {
            NetworkStream networkStream = client.GetStream();
            StreamReader reader = new StreamReader(networkStream);
            StreamWriter writer = new StreamWriter(networkStream);
            writer.AutoFlush = true;
            
            string request = reader.ReadLine();
            while(!String.IsNullOrEmpty(request))
            {
                if(request == null) break;
                
                string[] tokens = request.Split(' ');
                EventArgs args;
                args.playerName = tokens[0];
                args.player = Int32.Parse(tokens[1]);
                args.eventName = tokens[2];
                args.args = tokens.Skip(3).ToArray();
                
                EventManager.events.Add(args);
                request = reader.ReadLine();
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            client.Close();
        }
    }
}
