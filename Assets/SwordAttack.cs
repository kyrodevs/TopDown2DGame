using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    Vector2 rightAtackOffset;
    public Collider2D swordCollider;
    // Start is called before the first frame update
    private void Start()
    {
        rightAtackOffset = transform.position; 
    }
    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAtackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAtackOffset.x * -1, rightAtackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Enter");
        if(other.tag == "Enemy")
        {
            //Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }
}
