using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject Bullet;

    public GameObject Player;

    public int HP = 100;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("_phaseOne", 5.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BULLET"))
        {
            HP -= other.gameObject.GetComponent<PlayerBulletController>().Damage;
            if (HP == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void _phaseOne()
    {
        var bullet = GameObject.Instantiate(Bullet, transform.position + transform.forward, Quaternion.identity);
        bullet.GetComponent<EnemyBulletController>().Direction = Vector3.Normalize(Player.transform.position - transform.position);
    }
}
