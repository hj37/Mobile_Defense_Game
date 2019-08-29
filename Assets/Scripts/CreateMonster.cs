using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {

    //private GameManager.instance GameManager.instance;

    public List<GameObject> respawnSpotList;
    //list를 잘 지원하고 잇음 
    public GameObject monster1Prefab;
    public GameObject monster2Prefab;

    private GameObject monsterPrefab;

    private float lastSpawnTime;
    private int spawnCount = 0;

    void Start () { //생성자가 start보다 먼저 실행이 됨 
       //Find함수 권장하지 않음 
        monsterPrefab = monster1Prefab;
        lastSpawnTime = Time.time;
	}
	
	void Update () {    //반복적으로업데이트를 함 
		if(GameManager.instance.round <= GameManager.instance.totalRound)
        {
            float timeGap = Time.time - lastSpawnTime;
            if(((spawnCount == 0 && timeGap > GameManager.instance.roundReadyTime) // 새 라운드가 시작
                || timeGap > GameManager.instance.spawnTime)
                && spawnCount < GameManager.instance.spawnNumber)
            {
                lastSpawnTime = Time.time;
                int index = Random.Range(0, 4);
                //문제점 배열을 이용해서 개선하게 될 것임 
                GameObject respawnSpot = respawnSpotList[index];//리스폰 지역이 많을때는 매우 비효율적임 리스트로 해결할 수 있음 
                //자료의 유형에 따라 어떤 자료구조가 좋을지 고민을 해보는게 중요함 
                Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
                spawnCount += 1;
            }
            if(spawnCount == GameManager.instance.spawnNumber &&
               GameObject.FindGameObjectWithTag("Monster") == null)
            {
                if(GameManager.instance.totalRound == GameManager.instance.round)
                {
                    GameManager.instance.gameClear();
                    GameManager.instance.round += 1;
                    return;
                }
                GameManager.instance.clearRound();
                spawnCount = 0;
                lastSpawnTime = Time.time;

                if(GameManager.instance.round == 4)
                {
                    monsterPrefab = monster2Prefab;
                    GameManager.instance.spawnTime = 2.0f;
                    GameManager.instance.spawnNumber = 10;
                }
            }
        }
	}
}
