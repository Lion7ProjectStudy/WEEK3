using UnityEngine;
using NaughtyAttributes; // 필수!

[System.Serializable]
public class DroptableData
{
    [Expandable] // 아이템 데이터(SO)도 바로 펼쳐보고 싶다면 추가 (선택사항)
    public ItemData itemData;

    [MinValue(0), AllowNesting] // 리스트 안에서 작동하려면 AllowNesting 필수
    public float rate; // 가중치
    
    [AllowNesting] // 리스트 내부 표시를 위해 필수
    public float probability; // 자동 계산될 확률
}