using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectButton : MonoBehaviour
{
    [SerializeField, Header("スキルアイコン")]
    private Image iconImage;

    [SerializeField, Header("スキル名テキスト")]
    private TextMeshProUGUI skillNameText;

    //スキルを管理している親クラスSO
    private SkillBase skill;

    //選択するUIスクリプト
    private SkillSelectUI selectUI;

    /// <summary>
    /// ボタンにスキル情報をセットする
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="ui"></param>
    public void Setup(SkillBase skill, SkillSelectUI ui)
    {
        this.skill = skill;
        this.selectUI = ui;

        iconImage.sprite = skill.icon;
        skillNameText.text = skill.SkilName;
    }

    /// <summary>
    /// スキルが選ばれたらスキル設定してゲームを再開
    /// </summary>
    public void OnClick()
    {
        selectUI.OnSkillSelected(skill);
    }
}
