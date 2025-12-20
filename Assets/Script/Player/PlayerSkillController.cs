using System.CodeDom.Compiler;
using System.Collections;
using UnityEngine;

/// <summary>
/// ローグライク用
/// スキルのチャージと発動を管理
/// </summary>
public class PlayerSkillController : MonoBehaviour
{
    [SerializeField, Header("使用するスキル")]
    private SkillBase skill;

    [SerializeField, Header("チャージに必要な撃破数")]
    private int needKillCount;

    [SerializeField, Header("チャージに必要な撃破数")]
    private float skillActiveTine;

    //現在の撃破数
    private int currentKill;

    //スキルがチャージ完了しているかどうか
    private bool isCharged;

    //スキル使用中かどうか
    private bool isSkillMode;

    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        EnemyKillCounter.Instance.OnEnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyKillCounter.Instance.OnEnemyKilled -= OnEnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
        //スキルがたまったらEが押せるようにする
        if (isCharged && !isSkillMode && Input.GetKeyDown(KeyCode.E))
        {
            EnterSkillMode();
        }

        //スキルモード中のみ右クリックで発射
        if(isSkillMode && Input.GetMouseButtonDown(1))
        {
            skill.Active(player);
        }
    }

    /// <summary>
    /// 敵を倒した時の処理
    /// </summary>
    private void OnEnemyKilled()
    {
        if (isCharged) return;

        currentKill++;

        SkillGaugeUI.Instance.UpdateCharge(
            (float)currentKill / needKillCount);

        if(currentKill >= needKillCount)
        {
            isCharged = true;
            SkillGaugeUI.Instance.SetCharged(true);
        }
    }

    /// <summary>
    /// Eキーでスキル待機状態に入る  
    /// </summary>
    private void EnterSkillMode()
    {
        isSkillMode = true;
        StartCoroutine(SkillTimerCoroutine());
        SkillGaugeUI.Instance.ShowSkillTimer(skillActiveTine);
    }

    /// <summary>
    /// スキル仕様時間管理
    /// </summary>
    private IEnumerator SkillTimerCoroutine()
    {
        float timer = skillActiveTine;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            SkillGaugeUI.Instance.UpdateSkillTimer(timer/ skillActiveTine);
            yield return null;
        }

        EndSkillMode();
    }

    /// <summary>
    /// スキル終了・再チャージ
    /// </summary>
    private void EndSkillMode()
    {
        isSkillMode = false;
        isCharged = false;
        currentKill = 0;

        SkillGaugeUI.Instance.ResetAll();
    }

    /// <summary>
    /// 選択されたスキルをセットする
    /// </summary>
    /// <param name="newSkill">新しく選択されたスキル</param>
    public void SetSkill(SkillBase newSkill)
    {
        //使用するスキルを選択したスキルに設定
        skill = newSkill;

        //チャージ状態をリセット
        isCharged = false;
        isSkillMode = false;
        currentKill = 0;

        //スキルゲージもリセットする
        SkillGaugeUI.Instance.ResetAll();
    }
}
