using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum eDragState
{
    Idle,
    OnDrag,
    Tween,
}

public class DragMove : SingletonMono<DragMove>
{
    public eDragState DragStateNow;

    public int NowIndex = 2;
    public float[] MoveXArray = new float[5] { -19.2f * 2, -19.2f, 0f, 19.2f, 19.2f * 2 };

    public float MouseDownPosX;
    public float MouseUpPosX;

    private DragStateContext DragStateContext = new DragStateContext();

    void Start()
    {
        SetState(eDragState.Idle);
    }

    void Update()
    {
        DragStateContext.StateUpdate();
    }

    public void SetState(eDragState dragState)
    {
        switch (dragState)
        {
            case eDragState.Idle:
                DragStateContext.SetState(new DragStateIdle());
                break;
            case eDragState.OnDrag:
                DragStateContext.SetState(new DragStateOnDrag());
                break;
            case eDragState.Tween:
                DragStateContext.SetState(new DragStateTween());
                break;
        }
    }
}

public class DragStateContext
{
    private IDragState DragState = null;
    public void SetState(IDragState dragState)
    {
        if (DragState != null)
        {
            DragState.StateEnd();
        }
        DragState = dragState;
        DragState.StateStart();
    }

    public void StateUpdate()
    {
        if (DragState != null)
        {
            DragState.StateUpdate();
        }
    }
}

public abstract class IDragState
{
    public abstract void StateStart();
    public abstract void StateUpdate();
    public abstract void StateEnd();
}

public class DragStateIdle : IDragState
{
    public override void StateStart()
    {
        DragMove.Instance.DragStateNow = eDragState.Idle;
    }

    public override void StateUpdate()
    {
        if (GameStateManager.Instance.GameStateNow != eGameState.Idle)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DragMove.Instance.MouseDownPosX = Input.mousePosition.x;
            DragMove.Instance.SetState(eDragState.OnDrag);
        }
    }

    public override void StateEnd()
    {
    }
}

public class DragStateOnDrag : IDragState
{
    public override void StateStart()
    {
        DragMove.Instance.DragStateNow = eDragState.OnDrag;
    }

    public override void StateUpdate()
    {
        if (GameStateManager.Instance.GameStateNow != eGameState.Idle)
        {
            DragMove.Instance.SetState(eDragState.Idle);
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            DragMove.Instance.MouseUpPosX = Input.mousePosition.x;
            DragMove.Instance.SetState(eDragState.Tween);
        }
    }

    public override void StateEnd()
    {
    }
}

public class DragStateTween : IDragState
{
    public override void StateStart()
    {
        DragMove.Instance.DragStateNow = eDragState.Tween;
        CheckX();
    }

    private void CheckX()
    {
        float downX = DragMove.Instance.MouseDownPosX;
        float upX = DragMove.Instance.MouseUpPosX;
        if (Mathf.Abs(upX - downX) < 100)
        {
            DragMove.Instance.SetState(eDragState.Idle);
            return;
        }

        if (upX > downX && DragMove.Instance.NowIndex > 0)
        {
            DragMove.Instance.NowIndex--;
            DragMove.Instance.transform.DOMoveX(DragMove.Instance.MoveXArray[DragMove.Instance.NowIndex], 0.25f);
            DragMove.Instance.SetState(eDragState.Idle);
        }
        else if (upX < downX && DragMove.Instance.NowIndex < DragMove.Instance.MoveXArray.Length - 1)
        {
            DragMove.Instance.NowIndex++;
            DragMove.Instance.transform.DOMoveX(DragMove.Instance.MoveXArray[DragMove.Instance.NowIndex], 0.25f);
            DragMove.Instance.SetState(eDragState.Idle);
        }
        else
        {
            DragMove.Instance.SetState(eDragState.Idle);
        }
    }

    public override void StateUpdate()
    {
    }

    public override void StateEnd()
    {
    }
}