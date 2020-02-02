using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        idle,
        damage,
        cover,
        pretend,
    }
    public EnemyState enemyStateNow;
    public int number;                  //犯人編號
    public int speed;                   //移動速度
    public float damageTime;            //累積正在破壞時間
    public float damageCompleteTime;    //破壞完成時間
    private float coverTime;            //累積正在掩護時間
    private Vector2 originalEnemyPosition;  //開始遊戲時的位置
    private float idleTime;             //閒置時間累計
    private int idleMaxTime;            //最大閒置時間
    private float lastTime = 0;
    public int cam;
    private int derect = 1;
    public Image rightNotice;
    public Image leftNotice;

    void Start()
    {
        enemyStateNow = EnemyState.idle;
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
            case EnemyState.idle:
                Enemy_idle();
                break;
            case EnemyState.damage:
                Enemy_damage();
                break;
            case EnemyState.cover:
                Enemy_cover();
                break;
            case EnemyState.pretend:
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
            int i = Random.Range(0, 99);
            if(i <=80)
                enemyStateNow = EnemyState.damage;
            else if (i>80)
                enemyStateNow = EnemyState.cover;
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
                if(Mathf.Abs(cam-number)  == 1)
                    Notice();
                break;
            case 3:
                Notice();
                break;
        }
        if (cam - number == 0)
        {
            enemyStateNow = EnemyState.pretend;
        }
        if (damageTime >= damageCompleteTime)
        {
            GameObject.Find("wall2").GetComponent<LongPressEffect>().level += 1;
            damageTime = 0;
            enemyStateNow = EnemyState.idle;
            leftNotice.enabled = false;
            rightNotice.enabled = false;
        }
    }

    void Enemy_cover()
    {
        // 假裝挖吸引注意
        Debug.Log("假裝挖掘");

        if (cam == number)
            enemyStateNow = EnemyState.idle;
        
        coverTime += Time.deltaTime;
        if (coverTime >= 30) {
            if (Mathf.Abs(cam - number) >= 1)
                Notice();            
        }

    }
    void Enemy_pretend()
    {
        Debug.Log(cam);
        if (Mathf.Abs( cam- number) >=1)
        {
            enemyStateNow = EnemyState.damage;
        }
        transform.position = originalEnemyPosition;
    }

    void Notice() {
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
