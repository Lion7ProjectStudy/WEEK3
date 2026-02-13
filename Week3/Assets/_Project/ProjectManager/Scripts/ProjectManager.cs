using System;
using UnityEngine;
using NaughtyAttributes;

[Serializable]
public class ProjectTask
{
    public string taskName; // 작업 이름
    public bool isDone;     // 완료 여부
}

public class ProjectManager : MonoBehaviour
{
    [BoxGroup("현재 플랜")]
    [Expandable] // 핵심: SO의 내용을 이 컴포넌트 인스펙터에 펼쳐서 보여줌
    public ProjectManagerPlan goalAsset;
}