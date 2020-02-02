using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public bool isRight;
    public int camNumber;
    public bool leftSwitch;
    public bool rightSwitch;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        camNumber = DragMove.Instance.NowIndex;
        Debug.Log(camNumber);
        Notice_left();
        Notice_right();
    }

    public void Notice_switch(int enemyNumber)
    {
        if (isRight)
        {
            if (enemyNumber > camNumber)
                rightSwitch = true;
            else
                rightSwitch = false;
        }
        else
        {
            if (enemyNumber < camNumber)
                leftSwitch = true;
            else
                leftSwitch = false;
        }
    }
    void Notice_left()
    {
        this.enabled = leftSwitch;
    }
    void Notice_right()
    {
        this.enabled = rightSwitch;
    }
}
