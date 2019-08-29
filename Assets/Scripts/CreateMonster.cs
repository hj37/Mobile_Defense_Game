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

    private int spawnCount = 0;
    private IEnumerator coroutine;

    void Start() { //생성자가 start보다 먼저 실행이 됨 
                   //Find함수 권장하지 않음 
        monsterPrefab = monster1Prefab;
        coroutine = process();
        StartCoroutine(coroutine);
    }

    void Create()
    {
        int index = Random.Range(0, 4);
        //문제점 배열을 이용해서 개선하게 될 것임 
        GameObject respawnSpot = respawnSpotList[index];//리스폰 지역이 많을때는 매우 비효율적임 리스트로 해결할 수 있음 
                                                        //자료의 유형에 따라 어떤 자료구조가 좋을지 고민을 해보는게 중요함 
        Instantiate(monsterPrefab, respawnSpot.transform.position, Quaternion.identity);
        GameManager.instance.monsterAddCount++;
        spawnCount += 1;
    }//몬스터가 리스폰지역에 출몰하는거 

    IEnumerator process()
    {
        while (true)
        {
            if (GameManager.instance.round > GameManager.instance.totalRound) StopCoroutine(coroutine);
            if (spawnCount < GameManager.instance.spawnNumber)
            {
                Create();
            }
            if (spawnCount == GameManager.instance.spawnNumber && GameObject.FindGameObjectWithTag("Monster") == null)
            {
                if (GameManager.instance.totalRound == GameManager.instance.round)
                {
                    GameManager.instance.gameClear();
                    GameManager.instance.round += 1;
                }
                else
                {
                    GameManager.instance.clearRound();
                    spawnCount = 0;

                    if (GameManager.instance.round == 4)
                    {
                        monsterPrefab = monster2Prefab;
                        GameManager.instance.spawnTime = 2.0f;
                        GameManager.instance.spawnNumber = 10;
                    }
                }
            }
            if (spawnCount == 0) yield return new WaitForSeconds(GameManager.instance.roundReadyTime);
            else yield return new WaitForSeconds(GameManager.instance.spawnTime);
        }
    }

}