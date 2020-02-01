using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMove : MonoBehaviour
{
    public float DragSpeed = 2;
    private Vector3 DragOrigin;

    private float LimitX = 19.2f * 2;

    void Start()
    {
    }

    void Update()
    {
        if (GameStateManager.Instance.GameStateNow != eGameState.Idle)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - DragOrigin);
        Vector3 move = new Vector3(pos.x * DragSpeed, 0, 0);

        transform.Translate(move, Space.World);
        if (Mathf.Abs(transform.position.x) >= LimitX)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * LimitX, 0, -10f);
        }
    }
}
