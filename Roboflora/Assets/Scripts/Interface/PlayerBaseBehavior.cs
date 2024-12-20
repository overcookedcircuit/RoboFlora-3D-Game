using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseBehavior : MonoBehaviour
{
    public abstract void TakeDamage(int damage);
    public abstract void Die();
}
