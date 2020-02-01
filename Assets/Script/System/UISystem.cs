using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : SingletonMono<UISystem>
{
    [SerializeField]
    private Text TextGameTime = null;

    [SerializeField]
    private GameObject GameObjectWinGorup = null;

    [SerializeField]
    private GameObject GameObjectLosGroup = null;

    void Start()
    {
        
    }

    public void ShowGameTime(int time)
    {
        TextGameTime.text = time.ToString();
    }

    public void ShowWin()
    {
        GameObjectWinGorup.SetActive(true);
    }

    public void ShowLose()
    {
        GameObjectLosGroup.SetActive(true);
    }
}
