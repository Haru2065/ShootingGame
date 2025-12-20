using UnityEngine;

[CreateAssetMenu(menuName = "Skill/CircleShot")]
public class CircleShotSkill : SkillBase
{
    [Header("‰~Œ`’e‚ÌƒvƒŒƒnƒu")]
    public GameObject CircleBulletPrefab;

    [Header("”­Ë‚·‚é’e‚Ì”")]
    public int BulletCount = 12;

    [Header("’e‚Ì‘¬“x")]
    public float bulletSpeed;

    public override void Active(Player player)
    {
        float angleStep = 360f / BulletCount;

        //ƒvƒŒƒCƒ„[‚ÌUŒ‚—Í~ƒXƒLƒ‹”{—¦
        float totalAttackPower = player.PlayerAttackPower * SkillAttackPower;

        for (int i = 0; i < BulletCount; i++)
        {
            float angle = angleStep * i;

            //‰ñ“]‚ğ‚½‚¹‚Ä¶¬
            Quaternion rot = Quaternion.Euler(0, 0, angle);

            GameObject bulletObj = Instantiate(CircleBulletPrefab,player.transform.position, rot);

            Bullet bullet = bulletObj.GetComponent<Bullet>();

            if(bullet != null)
            {
                bullet.Init(totalAttackPower);
            }
        }

         
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
