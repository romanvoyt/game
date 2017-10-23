using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public Vector3 direction;

    string creator;

    EnemyAttacked attacked;
    
    float timer = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * 10 * Time.deltaTime);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetValues(Vector3 dir, string name)
    {
        direction = dir;
        creator = name;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            attacked = collision.gameObject.GetComponent<EnemyAttacked>();
            collision.gameObject.tag = "Dead";
            attacked.KillBullet();
            Destroy(this.gameObject);
        } else if (collision.gameObject.tag == "Player")
        {
            PlayerHealth.dead = true;
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
