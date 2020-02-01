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
        SetState(eGameState.Idle);
    }

    void Update()
    {
        GameStateContext.StateUpdate();
    }

    public void SetState(eGameState gameState)
    {
        switch (gameState)
        {
            case eGameState.Idle:
                GameStateContext.SetState(new GameStateIdle());
                break;
            case eGameState.Punish:
                GameStateContext.SetState(new GameStatePunish());
                break;
            case eGameState.GameWin:
                GameStateContext.SetState(new GameStateGameWin());
                break;
            case eGameState.GameOver:
                GameStateContext.SetState(new GameStateGameOver());
                break;
        }
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
        if (GameState != null)
        {
            GameState.StateUpdate();
        }
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
    private System.IDisposable PunishTimer = null;
    public override void StateStart()
    {
        GameStateManager.Instance.GameStateNow = eGameState.Punish;

        PunishTimer = Usefull.Timer(10, () =>
        {
            GameStateManager.Instance.SetState(eGameState.Idle);
        });
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
        if (PunishTimer != null)
        {
            PunishTimer.Dispose();
        }
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