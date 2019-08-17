using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//VO라고 표현하기도 함 Value Object 데이터의 속성으로만 사용하기 위해서 
public class BulletStat
{
   
    public float speed { get; set; }
    public int damage { get; set; }

    public BulletStat(float speed,int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
   
}
