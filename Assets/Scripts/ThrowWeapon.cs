using UnityEngine;
using System.Collections;

public class ThrowWeapon : MonoBehaviour
{

    EnemyAttacked attacked;
    float timer = 2.0f;
    Vector3 direction;
    Rigidbody2D rid;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(this.transform.rotation, new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - 1, this.transform.rotation.w), Time.deltaTime * 4);
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            rid.isKinematic = true;
            Destroy(this);
        }
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            attacked = collider.gameObject.GetComponent<EnemyAttacked>();
            attacked.KnockDownEnemy();
            rid.isKinematic = false;
            Destroy(this);
        }
        else if(collider.gameObject.tag == "Player")
        {
                
        }
        else
        {
            rid.isKinematic = true;
            Destroy(this);
        }
    }
}