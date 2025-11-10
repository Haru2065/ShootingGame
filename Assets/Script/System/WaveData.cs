using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    [Header("各ウェーブにスポーンさせる敵の情報を持ったスクリプトを参照")]
    public List<WaveStatus> waves = new List<WaveStatus>();
}
