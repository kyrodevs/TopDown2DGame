using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    public GameObject bullet;
    public GameObject bulletParent;
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate = 1;
    private float nextFireTime;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (distance < lineOfSight && distance > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.fixedDeltaTime);
        } else if (distance <= shootingRange && nextFireTime<Time.time) {
            if (!playerController.dead )
             Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
             nextFireTime = Time.time + fireRate;
            
            
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}


