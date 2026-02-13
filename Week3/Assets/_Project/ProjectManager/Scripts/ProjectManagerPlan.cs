using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

[CreateAssetMenu(fileName = "New Project Plan", menuName = "Project Manager/Plan")]
public class ProjectManagerPlan : ScriptableObject
{
    // --- UI 섹션: 작업 내용 ---
    [BoxGroup("작업 내용")]
    [Label("Edit Mode")]
    public bool isEditMode; // Summary 수정 권한 제어

    [BoxGroup("작업 내용")]
    [TextArea(5, 10)] 
    [EnableIf("isEditMode")] // Edit Mode가 켜져야만 입력 가능
    public string summary;

    // --- UI 섹션: 진행률 ---
    [BoxGroup("진행률")]
    [ProgressBar("Progress", 1f, EColor.Green)] // 0~1 사이의 진행바
    public float progress;

    // --- UI 섹션: 세부 목표 ---
    [BoxGroup("세부 목표")]
    public List<ProjectTask> tasks = new List<ProjectTask>();

    // --- 기능: 진행률 계산 및 갱신 ---
    [Button("새로고침")] // 수동 갱신 버튼
    public void UpdateProgress()
    {
        if (tasks == null || tasks.Count == 0)
        {
            progress = 0f;
            return;
        }

        float doneCount = tasks.Count(t => t.isDone);
        progress = doneCount / tasks.Count;
    }

    // 인스펙터 값이 변경될 때마다 자동으로 진행률 계산
    private void OnValidate()
    {
        UpdateProgress();
    }
}