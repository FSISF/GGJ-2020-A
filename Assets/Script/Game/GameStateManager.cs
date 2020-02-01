using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eGameState
{
    Idle,
    Punish,
    GameWin,
    GameOver,
}

public class GameStateManager : SingletonMono<GameStateManager>
{
    public eGameState GameStateNow;

    private GameStateContext GameStateContext = new GameStateContext();

    void Start()
    {
        
    }

    void Update()
    {
        GameStateContext.StateUpdate();
    }
}

public class GameStateContext
{
    private IGameState GameState = null;

    public void SetState(IGameState gameState)
    {
        if (GameState != null)
        {
            GameState.StateEnd();
        }
        GameState = gameState;
        GameState.StateStart();
    }

    public void StateUpdate()
    {
        GameState.StateUpdate();
    }
}

public abstract class IGameState
{
    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
}

public class GameStateIdle : IGameState
{
    public override void StateStart()
    {
        GameStateManager.Instance.GameStateNow = eGameState.Idle;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}

public class GameStatePunish : IGameState
{
    public override void StateStart()
    {
        GameStateManager.Instance.GameStateNow = eGameState.Punish;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}

public class GameStateGameWin : IGameState
{
    public override void StateStart()
    {
        GameStateManager.Instance.GameStateNow = eGameState.GameWin;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}

public class GameStateGameOver : IGameState
{
    public override void StateStart()
    {
        GameStateManager.Instance.GameStateNow = eGameState.GameOver;
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}