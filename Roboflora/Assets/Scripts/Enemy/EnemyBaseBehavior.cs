using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBaseBehavior : MonoBehaviour
{
    public int health;
    public int speed;

    public abstract void GetHurt(int damage);

    public abstract void Die();
    

}
