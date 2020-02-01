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
    public float damageCompleteTime;    //破壞完成時間
    public int damagedLevel;            //破壞程度階級
    private Vector2 originalEnemyPosition;
    private float idleTime;
    private int idleMaxTime;
    public int speed;
    private int derect = 1;

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
        transform.Translate(Vector2.right * Time.deltaTime * speed * derect);
        if (transform.position.x >= originalEnemyPosition.x + 4)
            derect = -1;
        else if (transform.position.x <= originalEnemyPosition.x - 4)
            derect = 1;
        if (idleTime == 0)
        {
            idleMaxTime = Random.Range(5, 10);
        } 
        idleTime += Time.deltaTime;
        Debug.Log(idleTime);
        if (idleTime >= idleMaxTime)
        {
            enemyStateNow = enemyState.damage;
            idleTime = 0;
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
        if (damageTime >= damageCompleteTime)
        {
            damagedLevel += 1;
            damageTime = 0;
            enemyStateNow = enemyState.idle;
        }
    }

    void Enemy_cover()
    {

    }
    void Enemy_pretend()
    {

        Debug.Log("吹口哨動畫");
    }
}
