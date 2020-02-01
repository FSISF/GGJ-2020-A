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

public enum eTimeState
{
    Timing,
    End,
}

public class GameStateManager : SingletonMono<GameStateManager>
{
    public eGameState GameStateNow;
    public eTimeState TimeStateNow;
    public System.IDisposable GameTimer = null;

    private GameStateContext GameStateContext = new GameStateContext();
    private TimeStateContext TimeStateContext = new TimeStateContext();

    void Start()
    {
        SetGameState(eGameState.Idle);
        SetTimeState(eTimeState.Timing);
    }

    void Update()
    {
        GameStateContext.StateUpdate();
        TimeStateContext.StateUpdate();
    }

    public void SetGameState(eGameState gameState)
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

    public void SetTimeState(eTimeState timeState)
    {
        switch (timeState)
        {
            case eTimeState.Timing:
                TimeStateContext.SetState(new TimeStateTiming());
                break;
            case eTimeState.End:
                TimeStateContext.SetState(new TimeStateEnd());
                break;
        }
    }
}

#region GameState
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
            GameStateManager.Instance.SetGameState(eGameState.Idle);
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
        UISystem.Instance.ShowWin();
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
        UISystem.Instance.ShowLose();
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}
#endregion

#region TimeState
public class TimeStateContext
{
    private ITimeState TimeState = null;

    public void SetState(ITimeState timeState)
    {
        if (TimeState != null)
        {
            TimeState.StateEnd();
        }
        TimeState = timeState;
        TimeState.StateStart();
    }

    public void StateUpdate()
    {
        if (TimeState != null)
        {
            TimeState.StateUpdate();
        }
    }
}

public abstract class ITimeState
{
    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
}

public class TimeStateTiming : ITimeState
{
    private float timer = 0;
    private float Timer
    {
        get
        {
            return timer;
        }
        set
        {
            timer = value;
            UISystem.Instance.ShowGameTime(Mathf.FloorToInt(timer));
        }
    }

    public override void StateStart()
    {
        Timer = 180;
    }

    public override void StateUpdate()
    {
        if (GameStateManager.Instance.GameStateNow == eGameState.GameOver)
        {
            GameStateManager.Instance.SetGameState(eGameState.GameOver);
        }

        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = 0;
            GameStateManager.Instance.SetGameState(eGameState.GameWin);
            GameStateManager.Instance.SetTimeState(eTimeState.End);
        }
    }

    public override void StateEnd()
    {
    }
}

public class TimeStateEnd : ITimeState
{
    public override void StateStart()
    {
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}
#endregion