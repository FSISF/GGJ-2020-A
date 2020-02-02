using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum enemyState
    {
        idle,
        damage,
        cover,
        pretend,
    }
    public enemyState enemyStateNow;
    private float damageTime;           //累積正在破壞時間
    private float coverTime;            //累積正在破壞時間
    public float damageCompleteTime;    //破壞完成時間
    public int damagedLevel;            //破壞程度階級
    private Vector2 originalEnemyPosition;  //開始遊戲時的位置
    private float idleTime;             //閒置時間累計
    private int idleMaxTime;            //最大閒置時間   
    public int speed;                   //移動速度
    private float lastTime = 0;
    public int cam;
    private int derect = 1;
    public int number;
    public Image rightNotice;
    public Image leftNotice;
    public GameObject abclevel;

    void Start()
    {
        enemyStateNow = enemyState.idle;
        originalEnemyPosition = transform.position;
        rightNotice = GameObject.Find("right_notice").GetComponent<Image>();
        leftNotice = GameObject.Find("left_notice").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        cam = GameObject.Find("Main Camera").GetComponent<DragMove>().NowIndex;
        switch (enemyStateNow)
        {
            case enemyState.idle:
                Enemy_idle();
                break;
            case enemyState.damage:
                Enemy_damage();
                break;
            case enemyState.cover:
                Enemy_cover();
                break;
            case enemyState.pretend:
                Enemy_pretend();
                break;
        }
    }

    void Enemy_idle()
    {
        if (transform.position.x >= originalEnemyPosition.x + 4)
            derect = -1;
        else if (transform.position.x <= originalEnemyPosition.x - 4)
            derect = 1;
        transform.Translate(Vector2.right * Time.deltaTime * speed*derect);
        if (idleTime == 0)
        {
            idleMaxTime = Random.Range(5, 10);
        } 
        idleTime += Time.deltaTime;
        if (idleTime >= idleMaxTime)
        {
            idleTime = 0;
            int i = Random.Range(0, 1);
            switch (i)
            {
                case 0:
                    enemyStateNow = enemyState.damage;
                    break;
                case 1:
                    enemyStateNow = enemyState.cover;
                    break;
            }
        }
    }

    void Enemy_damage()
    {
        transform.position = new Vector2(2.0f, -1.5f);
        damageTime += Time.deltaTime;
        Debug.Log("挖掘動畫");
        switch (Mathf.Ceil(damageTime / 5))
        {
            case 2:
                Notcie();
                Debug.Log("隔壁出現提示");
                break;
            case 3:
                Notcie();
                Debug.Log("全房出現提示");
                break;
        }
        Debug.Log(damageTime);
        if (cam - number == 0)
        {
            enemyStateNow = enemyState.pretend;
        }
        if (damageTime >= damageCompleteTime)
        {
            Debug.Log("全房出現提示");


            GameObject.Find("wall2").GetComponent<LongPressEffect>().level += 1;
            damageTime = 0;
            enemyStateNow = enemyState.idle;
            leftNotice.enabled = false;
            rightNotice.enabled = false;
        }
    }

    void Enemy_cover()
    {
        // 假裝挖吸引注意
        Debug.Log("假裝挖掘");
        coverTime += Time.deltaTime;
        if (coverTime >= 30) {
            if (cam - number == 0)
            {
                enemyStateNow = enemyState.idle;
            }
            if (Mathf.Abs(cam - number) >= 1)
                Debug.Log("全房出現提示");
        }

    }
    void Enemy_pretend()
    {
        Debug.Log(cam);
        if (Mathf.Abs( cam- number) >=1)
        {
            enemyStateNow = enemyState.damage;
        }
        transform.position = originalEnemyPosition;
    }

    void Notcie() {
        int i = cam - number;
        if (i > 0)
        {
            leftNotice.enabled = true;
            rightNotice.enabled = false;
        }
        else if (i < 0)
        {
            leftNotice.enabled = false;
            rightNotice.enabled = true;
        }
        else
        {
            leftNotice.enabled = false;
            rightNotice.enabled = false;
        }
    }
}
