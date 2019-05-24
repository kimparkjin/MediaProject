using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public Transform BulletTransform;

    public Vector3 Direction;

    public int Damage = 10;

    public float Speed;

    public float LifeTime = 5.0f;

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
        if (other.CompareTag("CHARACTER"))
        {
            _destroy();
        }
    }

    private void _destroy()
    {
        Destroy(gameObject);
    }
}
