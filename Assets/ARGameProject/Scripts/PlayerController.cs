using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int HP = 5;

    public GameObject ARCamera;

    public GameObject Bullet;

    public bool B_Fire = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(B_Fire == true)
        {
            _generateBullet();
        }
    }

    private void _generateBullet()
    {
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        var bullet = GameObject.Instantiate(Bullet, ARCamera.transform.position + ARCamera.transform.forward * 0.3f, Quaternion.identity);
        bullet.GetComponent<PlayerBulletController>().Direction = ARCamera.transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BULLET"))
        {
            HP -= other.gameObject.GetComponent<EnemyBulletController>().Damage;
            if (HP == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
