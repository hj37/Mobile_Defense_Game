using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    public BulletStat bulletStat
    {
        get; set;
    }

    public BulletBehavior() //꼭 생성자안에서 bulletStat을 초기화해줘야함 
    {
        bulletStat = new BulletStat(0, 0);
    }

    public GameObject character;

    void Start () {
        Destroy(gameObject, 3.0f);
	}
	
	void Update () {    //매 프레임마다 update가 실행됨 start update가 실행되기전에 초기화되야지 됨 
        transform.Translate(Vector2.right * bulletStat.speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            Destroy(gameObject);
            other.GetComponent<MonsterStat>().attacked(bulletStat.damage);
        }   
    }
}
