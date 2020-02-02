using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eState
{
    State1,
    State2,
}

public class StatePattern : MonoBehaviour
{
    public eState StateNow;
    private StateContext StateContext = new StateContext();

    void Start()
    {
        SetState(eState.State1);
    }

    public void SetState(eState state)
    {
        switch (state)
        {
            case eState.State1:
                StateContext.SetState(new State1(this));
                break;
            case eState.State2:
                StateContext.SetState(new State2(this));
                break;
        }
    }

    void Update()
    {
        StateContext.StateUpdate();
    }
}

/// <summary>
/// 中央管理器
/// </summary>
public class StateContext
{
    private IState State = null;
    public void SetState(IState state)
    {
        if (State != null)
        {
            State.StateEnd();
        }
        State = state;
        State.StateStart();
    }

    public void StateUpdate()
    {
        if (State != null)
        {
            State.StateUpdate();
        }
    }
}

public abstract class IState
{
    protected StatePattern StatePattern = null;
    public IState(StatePattern statePattern)
    {
        StatePattern = statePattern;
    }

    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
}

public class State1 : IState
{
    public State1(StatePattern statePattern) : base(statePattern)
    {
        StatePattern.StateNow = eState.State1;
    }

    public override void StateStart()
    {
        Debug.Log("State1 Start");
    }

    public override void StateUpdate()
    {
        Debug.Log("State1 Update");
        if (Input.GetKeyDown(KeyCode.A))
        {
            StatePattern.SetState(eState.State2);
        }
    }

    public override void StateEnd()
    {
        Debug.Log("State1 End");
    }
}

public class State2 : IState
{
    public State2(StatePattern statePattern) : base(statePattern)
    {
        StatePattern.StateNow = eState.State2;
    }

    public override void StateStart()
    {
        Debug.Log("State2 Start");
    }

    public override void StateUpdate()
    {
        Debug.Log("State2 Update");
        if (Input.GetKeyDown(KeyCode.D))
        {
            StatePattern.SetState(eState.State1);
        }
    }

    public override void StateEnd()
    {
        Debug.Log("State2 End");
    }
}