using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    SOLID,
    KILL,
    NONSOLID
}

[RequireComponent(typeof(Collider2D))]
public class BlockSwitch : MonoBehaviour
{
    [SerializeField] private BlockState[] states;
    private BlockState state;
    private Collider2D c2d;
    void Awake()
    {
        c2d = GetComponent<Collider2D>();
        SwitchState(0);
    }
    
    public void SwitchState(int stateOrder)
    {
        state = states[stateOrder];
        
        switch(state)
        {
        case BlockState.SOLID:
            c2d.enabled = true;
            break;
        case BlockState.KILL:
            c2d.enabled = true;
            break;
        case BlockState.NONSOLID:
            c2d.enabled = false;
            break;
        }
    }
    
    public bool IsKillBlock()
    {
        return state == BlockState.KILL;
    }
}
