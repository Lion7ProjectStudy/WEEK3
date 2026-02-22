using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;     // 자석 범위, 공격력 등
    public float baseValue;        // 기본 수치
    public float increasePerLevel; // 레벨당 상승폭
    public int maxLevel;           // 최대 강화 레벨
}
