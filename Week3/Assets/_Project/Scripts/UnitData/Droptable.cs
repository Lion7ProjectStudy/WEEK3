using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewDroptable", menuName = "Unit/Droptable", order = 2)]
public class Droptable : ScriptableObject
{
    [ReorderableList] // 리스트 순서 변경 쉽게 (NaughtyAttributes 기능)
    public List<DroptableData> droptable = new List<DroptableData>();

    // 값 변경 시 자동 계산
    private void OnValidate()
    {
        CalculateProbabilities();
    }

    [Button("Force Recalculate")] // 수동으로 계산하고 싶을 때 누르는 버튼 추가
    public void CalculateProbabilities()
    {
        if (droptable == null || droptable.Count == 0) return;

        float totalRate = 0;
        foreach (var data in droptable)
        {
            if (data.rate < 0) data.rate = 0;
            totalRate += data.rate;
        }

        foreach (var data in droptable)
        {
            if (totalRate > 0)
                data.probability = (data.rate / totalRate) * 100f;
            else
                data.probability = 0;
        }
    }

    // [추가된 핵심 기능] 가중치에 따라 아이템 하나를 반환하는 함수
    public ItemData PickItem()
    {
        if (droptable == null || droptable.Count == 0) return null;

        // 1. 전체 가중치 합 계산 (매번 계산하여 안전성 확보)
        float totalRate = 0;
        foreach (var data in droptable)
        {
            totalRate += data.rate;
        }

        // 2. 랜덤 값 뽑기
        float randomValue = Random.Range(0, totalRate);

        // 3. 가중치 추첨 알고리즘
        foreach (var data in droptable)
        {
            if (randomValue <= data.rate)
            {
                return data.itemData; // 당첨된 아이템 데이터 반환
            }
            randomValue -= data.rate;
        }

        return null;
    }
}