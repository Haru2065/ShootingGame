using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキルゲージUIを管理するクラス
/// 撃破数に応じたチャージ表示
/// スキル使用可能状態の通知
/// スキル使用中の残り時間表示
/// </summary>
public class SkillGaugeUI : MonoBehaviour
{
    private static SkillGaugeUI instance;

    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    public static SkillGaugeUI Instance => instance;

    [SerializeField, Header("スキルチャージゲージ")]
    [Tooltip("撃破数に応じて溜まるスキルゲージ")]
    private Slider skillChargeSlider;

    [SerializeField, Header("スキル使用時間ゲージ")]
    [Tooltip("スキル使用中の残り時間を表示するゲージ")]
    private Slider skillTimerSlider;

    [SerializeField, Header("スキル使用可能UI")]
    [Tooltip("Eキー使用可能を示すUI")]
    private GameObject skillReadyUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 初期状態ではスキルは溜まっていない
        skillChargeSlider.value = 0f;

        // 使用可能UI・タイマーは非表示
        skillReadyUI.SetActive(false);
        skillTimerSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// スキルチャージ量を更新（0〜1）
    /// </summary>
    public void UpdateCharge(float value)
    {
        skillChargeSlider.value = Mathf.Clamp01(value);
    }

    /// <summary>
    /// スキルが使用可能かどうかをUIに反映
    /// </summary>
    public void SetCharged(bool charged)
    {
        skillReadyUI.SetActive(charged);
    }

    /// <summary>
    /// スキル使用開始時に呼ばれる
    /// </summary>
    public void ShowSkillTimer(float maxTime)
    {
        skillTimerSlider.gameObject.SetActive(true);
        skillTimerSlider.value = 1f;
    }

    /// <summary>
    /// スキル使用中の残り時間更新（0〜1）
    /// </summary>
    public void UpdateSkillTimer(float value)
    {
        skillTimerSlider.value = Mathf.Clamp01(value);
    }

    /// <summary>
    /// スキル終了時のUIリセット
    /// </summary>
    public void ResetAll()
    {
        skillChargeSlider.value = 0f;
        skillTimerSlider.gameObject.SetActive(false);
        skillReadyUI.SetActive(false);
    }
}
