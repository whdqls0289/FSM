using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Playerstate 
{ 
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK
}
public class FSMManager : MonoBehaviour
{


    public Playerstate currentState;
    public Playerstate startState;
    public Transform Marker;

    Dictionary<Playerstate, PlayerFSMState> states
        = new Dictionary<Playerstate, PlayerFSMState>();

    private void Awake()
    {
        Marker = GameObject.FindGameObjectWithTag("Marker").transform;

        states.Add(Playerstate.IDLE,GetComponent<PlayerIDLE>());
        states.Add(Playerstate.RUN, GetComponent<PlayerRUN>());
        states.Add(Playerstate.CHASE, GetComponent<PlayerCHASE>());
        states.Add(Playerstate.ATTACK, GetComponent<PlayerATTACK>());

        states[startState].enabled = true;
    }

    public void SetState(Playerstate newstate)
    {
        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }
        states[newstate].enabled = true;
    }

    // Use this for initialization
    void Start()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 1000))
            {
                Marker.position = hit.point;

                SetState(Playerstate.RUN);
            }
        }
    }
}