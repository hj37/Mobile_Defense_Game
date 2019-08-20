﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour {

    private GameManager gameManager;

    public List<GameObject> respawnSpotList;
    //list를 잘 지원하고 잇음 
    public GameObject monster1Prefab;
    public GameObject monster2Prefab;

    private GameObject monsterPrefab;

    private float lastSpawnTime;
    private int spawnCount = 0;

    void Start () {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        monsterPrefab = monster1Prefab;
        lastSpawnTime = Time.time;
	}
	
	void Update () {    //반복적으로업데이트를 함 
		if(gameManager.round <= gameManager.totalRound)
        {
            float timeGap = Time.time - lastSpawnTime;
            if(((spawnCount == 0 && timeGap > gameManager.roundReadyTime) // 새 라운드가 시작
                || timeGap > gameManager.spawnTime)
                && spawnCount < gameManager.spawnNumber)
            {
                lastSpawnTime = Time.time;
                int index = Random.Range(0, 4);
                //문제점 배열을 이용해서 개선하게 될 것임 
                GameObject respawnSpot = respawnSpotList[index];//리스폰 지역이 많을때는 매우 비효율적임 리스트로 해결할 수 있음 
                //자료의 유형에 따라 어떤 자료구조가 좋을지 고민을 해보는게 중요함 
                Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
                spawnCount += 1;
            }
            if(spawnCount == gameManager.spawnNumber &&
               GameObject.FindGameObjectWithTag("Monster") == null)
            {
                if(gameManager.totalRound == gameManager.round)
                {
                    gameManager.gameClear();
                    gameManager.round += 1;
                    return;
                }
                gameManager.clearRound();
                spawnCount = 0;
                lastSpawnTime = Time.time;

                if(gameManager.round == 4)
                {
                    monsterPrefab = monster2Prefab;
                    gameManager.spawnTime = 2.0f;
                    gameManager.spawnNumber = 10;
                }
            }
        }
	}
}
