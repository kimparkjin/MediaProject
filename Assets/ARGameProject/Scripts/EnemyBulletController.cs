using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public Transform BulletTransform;

    public Vector3 Direction;

    public int Damage = 1;

    public float Speed;

    public float LifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("_destroy", LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        BulletTransform.Translate(Direction * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            _destroy();
        }
    }

    private void _destroy()
    {
        Destroy(gameObject);
    }
}
