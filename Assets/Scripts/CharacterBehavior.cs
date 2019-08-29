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
    
	void Start () {
        characterStat = gameObject.GetComponent<CharacterStat>();
        //GameManager.instance = GameObject.Find("Game Manager").GetComponent<GameManager.instance>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void attack(int damage)  //가장 핵심인 부분 실제로 총알이 발사되는 
    {
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioSource.clip);
        GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);    //총알 객체 만든부분 
        currentBullet.GetComponent<BulletBehavior>().bulletStat = new BulletStat(10+characterStat.level*3,characterStat.damage) ;
        //객체지향 프로그래밍 기법 
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
