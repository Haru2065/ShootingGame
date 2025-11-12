using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    // Update is called once per frame
    void Update()
    {
        base.ShotMove();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("<color=green>[“G‚É“–‚½‚Á‚½!]</color>");
            Destroy(gameObject);
        }
    }
}
