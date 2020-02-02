using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private Button ButtonStart = null;

    void Start()
    {
        ButtonStart.onClick.AddListener(DoStart);
    }

    private void DoStart()
    {
        SceneManager.LoadScene(1);
    }
}