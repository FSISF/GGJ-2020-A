using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int derect = 1;
    public Camera cam;

    void Start()
    {
        enemyStateNow = enemyState.idle;
        originalEnemyPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (idleTime - lastTime >= 3 || idleTime == 0)
        {
            lastTime = idleTime;
            int p = Random.Range(0, 99);
            if (p <= 40)
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            else if (40 < p && p <= 80)
                transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (idleTime == 0)
        {
            idleMaxTime = Random.Range(5, 10);
        } 
        idleTime += Time.deltaTime;
        Debug.Log(idleTime);
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
        switch (damageTime / 5)
        {
            case 2:
                Debug.Log("隔壁出現提示");
                break;
            case 3:
                Debug.Log("全房出現提示");
                break;
        }
        Debug.Log(damageTime);
        if (Vector2.Distance(transform.position, cam.transform.position) <= Screen.width / 2)
        {
            enemyStateNow = enemyState.pretend;
        }
        if (damageTime >= damageCompleteTime)
        {
            damagedLevel += 1;
            damageTime = 0;
            enemyStateNow = enemyState.idle;
        }
    }

    void Enemy_cover()
    {
        // 假裝挖吸引注意
        Debug.Log("假裝挖掘");
        coverTime += Time.deltaTime;
        if (coverTime >= 30) {
            Debug.Log("全房出現提示");
        }

    }
    void Enemy_pretend()
    {
        if (Vector2.Distance(transform.position, cam.transform.position) > Screen.width / 2)
        {
            enemyStateNow = enemyState.damage;
        }
        transform.position = originalEnemyPosition;
        Debug.Log("吹口哨動畫");
    }
}
