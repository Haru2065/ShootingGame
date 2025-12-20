using System.Collections.Generic;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    [SerializeField, Header("スキルデータベース")]
    private SkillDataBase skillDatabase;

    [SerializeField, Header("スキルボタンのプレハブ")]
    private SkillSelectButton buttonPrefab;

    [SerializeField, Header("ボタンの配置先")]
    private Transform buttonParent;

    //プレイヤーのスキルをコントロールしているスクリプト
    private PlayerSkillController playerSkillController;

    private void Awake()
    {
        //最初は選択画面を非表示
        gameObject.SetActive(false);
    }

    public void Show(PlayerSkillController controller)
    {
        //プレイヤーのスキルをコントロールしているスクリプトを取得
        playerSkillController = controller;

        //スキル選択画面を表示
        gameObject.SetActive(true);

        //ゲームを停止
        Time.timeScale = 0f;

        //スキルボタンを生成
        CreateSkillButtons();
    }

    /// <summary>
    /// スキルボタンを生成するメソッド
    /// </summary>
    private void CreateSkillButtons()
    {
        //既存ボタンを一度非表示
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        //ランダムで3つ選ぶ
        List<SkillBase> randomSkills = new List<SkillBase>(skillDatabase.skillList);

        for (int i = 0; i < 3; i++)
        {
            SkillBase skill = randomSkills[Random.Range(0, randomSkills.Count)];
            randomSkills.Add(skill);

            SkillSelectButton button = Instantiate(buttonPrefab, buttonParent);

            button.Setup(skill, this);
        }
    }

    /// <summary>
    /// スキルが選択されたときのメソッド
    /// </summary>
    /// <param name="skill"></param>
    public void OnSkillSelected(SkillBase skill)
    {
        playerSkillController.SetSkill(skill);
        
        //ゲームを再開
        Time.timeScale = 1f;

        //スキル選択画面を非表示
        gameObject.SetActive(false);
    }
}
