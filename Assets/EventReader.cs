using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventReader : MonoBehaviour
{
    [SerializeField]private float stepTime;
    [SerializeField]private PawnScript[] pawns;
    
    private float lastStep;
    
    private List<EventArgs> args;
    void Start()
    {
        args = new List<EventArgs>(EventManager.events);
        lastStep = Time.time;
    }
    
    void Update()
    {
        if(Time.time - lastStep >= stepTime)
        {
            lastStep = Time.time;
            if(args.Count > 0)
            {
                EventArgs e = args[0];
                args.RemoveAt(0);
                process(e);
            }
            else
            {
                end();
            }
        }
    }
    
    void end()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void process(EventArgs args)
    {
        switch(args.eventName)
        {
        case "die":
            pawns[args.player].SetName(args.playerName);
            pawns[args.player].Move(-1);
            break;
            
        case "1cc":
            pawns[args.player].SetName(args.playerName);
            pawns[args.player].Move(-30);
            break;

        case "win":
            int score = Int32.Parse(args.args[0]);
            score /= 12;
            pawns[args.player].SetName(args.playerName);
            pawns[args.player].Move(score);
            break;

        default:
            Debug.Log("Player " + args.player + " has changed their name to " + args.playerName + ".");
            Debug.Log("Unrecognised event \""+ args.eventName + "\".");
            break;
        }
        
        
    }
}
