using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PawnScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve stepCurve;
    [SerializeField] private float stepTime = 1.5f;
    [SerializeField] private Text text;
    [SerializeField] private string winScene;
    [SerializeField] private int movement;
    [SerializeField] private bool debugMovement;
    
    private float triggerTime;
    private int space;
    private int target;
    private int phase = 0;
    private Board board;
    private string playerName;
    private Vector3 basePosition;
    private new Animation animation;
    
    void Start()
    {
        space = 0;
        SetName("Player 1");
        board = (Board)Object.FindObjectOfType(typeof(Board));
        basePosition = transform.position;
        animation = GetComponent<Animation>();
    }
    
    void Update()
    {
        if(debugMovement)
        {
            debugMovement = false;
            Move(movement);
        }
        float currentTime = Time.time;
        if(phase == 1)
        {
            Vector3 offset = stepCurve.Evaluate((currentTime - triggerTime)/stepTime) * board.GetStep() * (target - space);
            transform.position = offset + basePosition;
            
            if(currentTime - triggerTime >= stepTime)
            {
                phase = 2;
                triggerTime = currentTime;
                basePosition = transform.position;
            }
        }
        else if(phase == 2)
        {
            space = target;
            if(board.GetIndex(space) == -1)
            {
                Win();
                phase = 0;
            }
            else if(board.GetIndex(space) == -2)
            {
                phase = 0;
            }
            else
            {
                target = board.GetIndex(space);
                phase = 3;
                animation.Play();
            }
        }
        else if(phase == 3)
        {
            Vector3 offset = stepCurve.Evaluate((currentTime - triggerTime)/stepTime) * board.GetStep() * (target - space);
            transform.position = offset + basePosition;
            
            if(currentTime - triggerTime >= stepTime)
            {
                phase = 0;
                triggerTime = currentTime;
                basePosition = transform.position;
                space = target;
            }
        }
    }
    
    void Win()
    {
        SceneManager.LoadScene(winScene);
    }
    
    public void SetName(string playerName)
    {
        this.playerName = playerName;
        if(text)
        {
            text.text = this.playerName;
        }
    }
    
    public string GetName(string playerName)
    {
        return playerName;
    }
    
    public void Move(int distance)
    {
        target = space + distance;
        if(target > board.GetLength())
        {
            target = board.GetLength();
        }
        else if(target < 0)
        {
            target = 0;
        }
        triggerTime = Time.time;
        phase = 1;
        animation.Play();
    }
}
