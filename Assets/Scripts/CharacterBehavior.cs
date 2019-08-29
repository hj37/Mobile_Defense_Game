using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBehavior : MonoBehaviour {    //캐릭터의 행동을 정의하는 스크립트 

    private CharacterStat characterStat;    //캐릭터 스텟 
   // private GameManager.instance GameManager.instance;

    public GameObject bullet;
    private Animator animator;
    private AudioSource audioSource;

    private GameObject bulletObjectPool;
    private ObjectPooler bulletObjectPooler;


    
	void Start () {
        characterStat = gameObject.GetComponent<CharacterStat>();
        //GameManager.instance = GameObject.Find("Game Manager").GetComponent<GameManager.instance>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        if(gameObject.name.Contains("Character 1"))
        {
            bulletObjectPool = GameObject.Find("Bullet1 Object Pool");//하나씩 꺼내서 발사
        }
        else if(gameObject.name.Contains("Character 2"))
        {
            bulletObjectPool = GameObject.Find("Bullet2 Object Pool");
        }
        bulletObjectPooler = bulletObjectPool.GetComponent<ObjectPooler>();
    }   //싱글톤이 아니라 각각 찾아서 함 (싱글톤은 스크립트가 더 많아질수도 있음 

    public void attack(int damage)  //가장 핵심인 부분 실제로 총알이 발사되는 
    {
        GameObject bullet = bulletObjectPooler.getObject();    //총알 객체 만든부분
        if (bullet == null) return;
        bullet.transform.position = gameObject.transform.position;
        bullet.GetComponent<BulletBehavior>().bulletStat 
            = new BulletStat(10+characterStat.level*3,characterStat.damage) ;
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioSource.clip);
        bullet.GetComponent<BulletBehavior>().Spawn();

        //객체지향 프로그래밍 기법
        GameManager.instance.bulletAddCount++;
    }
	
	void Update () {
		
	}

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject(-1) == true) return;
        if (EventSystem.current.IsPointerOverGameObject(0) == true) return;
        if (characterStat.canLevelUp(GameManager.instance.seed))
        {
            characterStat.increaseLevel();
            GameManager.instance.seed -= characterStat.upgradeCost;
            GameManager.instance.updateText();
        }
    }
}
