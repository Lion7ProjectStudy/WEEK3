# 📘 NaughtyAttributes 전체 명령어 가이드 (실무용)

유니티 인스펙터를 커스텀 에디터 코딩 없이 확장해주는 라이브러리입니다.

---

## 1. 시각적 요소 (Layout & Grouping)
인스펙터의 가독성을 높여 기획서처럼 만들어줍니다.

| 속성 (Attribute) | 설명 |
| :--- | :--- |
| `[Header("제목")]` | 섹션 제목을 표시 (유니티 기본보다 깔끔함) |
| `[HorizontalLine]` | 수평선을 그어 섹션 구분 (색상, 두께 조절 가능) |
| `[BoxGroup("그룹명")]` | 변수들을 박스로 묶어 시각화 |
| `[Foldout("그룹명")]` | 접고 펼 수 있는 그룹 생성 |
| `[TabGroup("탭이름")]` | 탭 인터페이스로 변수 정리 |
| `[Label("한글이름")]` | 변수명 대신 보여줄 라벨 지정 |

---

## 2. 특수 입력 및 데이터 (Inputs)
데이터 입력을 직관적으로 바꿉니다.

| 속성 (Attribute) | 설명 |
| :--- | :--- |
| `[ResizableTextArea]` | 텍스트 박스 크기를 자유롭게 조절 (줄글 기획용) |
| `[ReorderableList]` | 리스트의 항목을 드래그로 순서 변경 가능 |
| `[MinMaxSlider(0, 100)]` | 최소/최대 범위를 슬라이더로 조절 |
| `[Dropdown("함수/리스트명")]` | 지정한 목록에서 선택하는 드롭다운 생성 |
| `[ProgressBar("이름", 100)]` | 수치 데이터를 게이지 바 형태로 표시 |
| `[InputKey]` | 키보드 입력을 받는 필드 생성 |

---

## 3. 조건부 제어 (Conditionals)
상황에 따라 인스펙터를 동적으로 변화시킵니다.

| 속성 (Attribute) | 설명 |
| :--- | :--- |
| `[ShowIf("bool변수")]` | 조건이 참일 때만 표시 |
| `[HideIf("bool변수")]` | 조건이 참일 때 숨김 |
| `[EnableIf("bool변수")]` | 조건이 참일 때만 수정 가능 |
| `[DisableIf("bool변수")]` | 조건이 참일 때 수정 불가 (회색 비활성화) |
| `[ReadOnly]` | 값은 보여주되 절대 수정 불가 |

---

## 4. 유효성 검사 및 실행 (Validation & Buttons)
실수를 방지하고 즉시 테스트하게 해줍니다.

| 속성 (Attribute) | 설명 |
| :--- | :--- |
| `[Button("버튼명")]` | 인스펙터에 버튼을 만들어 함수 즉시 실행 |
| `[OnValueChanged("함수")]` | 값이 바뀔 때마다 지정한 함수 실행 |
| `[ValidateInput("함수")]` | 입력값이 올바른지 체크 (잘못되면 경고 표시) |
| `[InfoBox("내용")]` | 정보/경고 메시지 박스 출력 |
| `[Required]` | 레퍼런스(에셋)가 비어있으면 빨간색 경고 표시 |

---

## 5. 실무 활용 예시 (Quick Code)

/*
using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class MyProjectManager : MonoBehaviour
{
    [Header("📋 기획 문서")]
    [InfoBox("이곳에 기획 내용을 작성하세요.")]
    [ResizableTextArea] public string doc;

    [HorizontalLine(color: EColor.Green)]
    
    [BoxGroup("상태 설정")]
    [ProgressBar("진행도", 100, EColor.Blue)] public float progress = 50f;
    
    [BoxGroup("상태 설정")]
    public bool isLocked;

    [EnableIf("isLocked")]
    [ReadOnly] public string secretKey = "AX-1234";

    [Button("기획 데이터 출력")]
    private void PrintData() => Debug.Log(doc);
}
*/