using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Unit/Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    [Header("게임 오브젝트")]
    public GameObject prefab;

    [Header("세부 데이터")]
    public string enemyName;
    public float enemyHp;

    [Header("드랍 테이블")]

    [Expandable] 
    public Droptable dropTable;

    [Button("Recalculate & Sort DropTable", EButtonEnableMode.Always)]
    private void SyncDropTable()
    {
        if (dropTable != null)
        {
            dropTable.CalculateProbabilities();

            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(dropTable);
            #endif
        }
        else
        {
            Debug.LogWarning("연결된 Droptable이 없습니다!");
        }
    }
}