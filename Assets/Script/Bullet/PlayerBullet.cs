using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField]
    private int playerAttackPower;

    public void Init(int attack)
    {
        playerAttackPower = attack;
    }

    // Update is called once per frame
    void Update()
    {
        base.ShotMove();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BossEnemy"))
        {
            BossEnemy boss = other.GetComponent<BossEnemy>();
            boss.OnDmage(Player.Instance.PlayerAttackPower);
            Destroy(gameObject);
        }

        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
