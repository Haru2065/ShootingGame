using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBarController : MonoBehaviour
{

    [SerializeField, Header("敵のHPBar")]
    private Slider enemyHPBar;

    [SerializeField,Header("敵スクリプト")]
    private Enemy enemy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (enemy != null)
        {
            enemyHPBar.maxValue = enemy.EnemyCurrentHP;
            enemyHPBar.value = enemy.EnemyCurrentHP;

            InitializeHP();
        }
        else
        {
            Debug.Log("ない");
        }
    }

    public void InitializeHP()
    {
        
        // HPバーの子要素であるCanvas（またはSlider）をアクティブにする
        // ここでは親オブジェクト（ボス）にアタッチされている前提で、HPバーの子要素を操作
        if (enemyHPBar.transform.parent.gameObject != gameObject)
        {
            enemyHPBar.transform.parent.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
