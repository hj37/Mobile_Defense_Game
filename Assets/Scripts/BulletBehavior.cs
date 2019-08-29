using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public BulletStat bulletStat
    {
        get; set;
    }

    public float activeTime = 3.0f;
    public float spawnTime;


    public BulletBehavior() //꼭 생성자안에서 bulletStat을 초기화해줘야함 
    {
        bulletStat = new BulletStat(0, 0);
    }

    public GameObject character;

    public void Spawn()
    {
        gameObject.SetActive(true);
        spawnTime = Time.time;
    }

    void Start() {
        Spawn();
	}
	
	void Update () {    //매 프레임마다 update가 실행됨 start update가 실행되기전에 초기화되야지 됨 
        if (Time.time - spawnTime >= activeTime)
        {
            gameObject.SetActive(false);//비활성화 처리 
        }
        else
        {
            transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            other.GetComponent<MonsterStat>().attacked(bulletStat.damage);
        }   
    }
}
