using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InvenItem // 인벤토리 한 칸의 정보를 담는 클래스
{
    public ItemData data;
    public int count;

    public InvenItem(ItemData _data, int _count)
    {
        data = _data;
        count = _count;
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [Header("현재 플레이어 캐릭터")]
    public Player player;

    private List<InvenItem> inventory = new List<InvenItem>();
    private float gold = 0;
    private int exp = 0;

    [Header("UI")]
    public GameObject mainUI;
    public TextMeshProUGUI money;
    public ItemSlot[] slots;
    public TextMeshProUGUI moneyResult;
    public ItemSlot[] ResultSlots;
    public GameObject gamover;
    public GameObject resultUI;
    public GameObject rst;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            gold = 0;
            inventory.Clear();
        }
        else if (instance != this)
        {
            // [수정됨] 이제 SceneLink가 Start()에서 모든 걸 넘겨줄 것이므로,
            // Awake에서는 복잡한 복사 과정 없이 중복된 매니저만 파괴하면 됩니다.
            Destroy(gameObject); 
            return;
        }
    }

    // [수정됨] SceneLink가 호출해 주는 이 함수에서 모든 연결을 끝냅니다.
    public void UpdateSceneReferences(SceneLink links)
    {
        // 1. UI 오브젝트 주소 갱신
        this.mainUI = links.mainUI;
        this.money = links.money;
        this.moneyResult = links.moneyResult;
        this.gamover = links.gamover;
        this.resultUI = links.resultUI;
        this.rst = links.rst;
        
        // [중요] 슬롯 배열들도 여기서 갱신해 주어야 합니다! (기존 코드 누락)
        this.slots = links.slots; 
        this.ResultSlots = links.ResultSlots;

        // 2. 재시작 버튼 코드로 연결 (GameObject.Find 사용 안 함!)
        // rst가 Button 컴포넌트를 가졌거나, 그 자식 중에 Button이 있다면 찾아냅니다.
        Button rstBtn = this.rst.GetComponent<Button>();
        if (rstBtn == null) rstBtn = this.rst.GetComponentInChildren<Button>(true);

        if (rstBtn != null)
        {
            rstBtn.onClick.RemoveAllListeners();
            rstBtn.onClick.AddListener(() => SceneOver("Stage1")); // 씬 이름은 상황에 맞게 변경
            Debug.Log("리스타트 버튼 연결 성공!");
        }
        else
        {
            Debug.LogWarning("rst 오브젝트에서 Button 컴포넌트를 찾지 못했습니다.");
        }

        mainUI.SetActive(true);
        gamover.SetActive(false);
        resultUI.SetActive(false);
        rst.SetActive(false);
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void GetItem(ItemData item)
    {
        switch(item.type)
        {
            case ItemType.Gold:
                gold += item.rate;
                money.text = gold.ToString();
                break;
            case ItemType.Exp:
                ItemAdd(item);
                break;
            case ItemType.Expendable:
                ItemAdd(item);
                
                break;
            case ItemType.Mag:
                ItemAdd(item);
                
                break;
        }        
    }

    // 씬이 바뀔 때마다 실행될 참조 갱신 함수
    public void UpdateReferences()
    {
        // 2. 비활성화된 버튼까지 포함해서 씬 내의 Restart 버튼을 찾습니다.
        // [팁] 버튼에 'RestartButton' 같은 태그를 붙여두면 찾기 훨씬 쉽습니다.
        Button rstBtn = GameObject.Find("RestartButton")?.GetComponent<Button>();

        if (rstBtn != null)
        {
            // 3. 기존에 걸려있을지도 모르는 연결을 청소하고 새로 연결 (중요!)
            rstBtn.onClick.RemoveAllListeners();
            rstBtn.onClick.AddListener(() => SceneOver("Stage1"));
        }
    }

    public void ItemAdd(ItemData newItem)
    {
        // 1. 이미 인벤토리에 있는지 확인 (itemCode로 비교)
        InvenItem existItem = inventory.Find(x => x.data.itemCode == newItem.itemCode);

        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            // 2. 새로 추가 (단, 슬롯이 3개 제한이므로 체크)
            if (inventory.Count >= 3) 
            {
                Debug.Log("인벤토리 꽉 참!");
                return;
            }
            inventory.Add(new InvenItem(newItem, 1));
        }

        RefreshUI(); // UI 업데이트 호출
    }

    void RefreshUI()
    {
        // 인벤토리 리스트 내용에 맞춰 UI 슬롯 갱신
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
                slots[i].UpdateSlot(inventory[i]);
            else
                slots[i].ClearSlot();
        }
    }

    // 3. 결과 UI를 채우는 함수
    void ResultUI()
    {
        if (ResultSlots == null || ResultSlots.Length == 0) return;

        for (int i = 0; i < ResultSlots.Length; i++)
        {
            // inventory.Clear()가 되기 전이므로 데이터가 남아있음
            if (i < inventory.Count)
            {
                ResultSlots[i].UpdateSlot(inventory[i]);
            }
            else
            {
                ResultSlots[i].ClearSlot();
            }
        }
    }


    public void EnterHole()
    {
        gamover.SetActive(true);

        Invoke(nameof(RstGame), 2.0f);
    }

    public void EnterHome()
    {
        resultUI.SetActive(true);

        ResultUI();
        moneyResult.text = gold.ToString();
        // 1. 인벤토리에 쌓인 아이템들을 하나씩 꺼내서 효과 적용
        foreach (InvenItem invenItem in inventory)
        {
            ApplyItemEffect(invenItem.data, invenItem.count);
        }
        mainUI.SetActive(false);

        Invoke(nameof(RstGame), 2.0f);
    }

    // 아이템 타입별 실제 효과 적용 로직
    private void ApplyItemEffect(ItemData data, int count)
    {
        // 실제 상승 수치 = 아이템 고유 수치(rate) * 획득 개수(count)
        float totalRate = data.rate * count;

        switch (data.type)
        {
            case ItemType.Gold:
                gold += (int)totalRate;                
                Debug.Log($"골드 {totalRate} 획득!");
                break;

            case ItemType.Mag:
                // 플레이어 스크립트에 구현된 자석 범위 상승 함수 호출
                player.MagRangeUp(totalRate); 
                Debug.Log($"자석 범위 {totalRate} 상승!");
                break;

            case ItemType.Expendable:
                // 소모품은 창고로 보내거나 별도의 처리를 수행
                Debug.Log($"{data.itemName} {count}개 창고로 이동");
                break;
                
            case ItemType.Exp:
                exp += (int)totalRate;
                break;
        }
    }

    void RstGame()
    {        
        rst.SetActive(true);
    }

    public void SceneOver(string scene)
    {
        // 처리가 끝났으므로 인벤토리 비우기 및 UI 갱신
        CancelInvoke();
        inventory.Clear();
        RefreshUI(); // 슬롯들을 ClearSlot 시킴

        Time.timeScale = 1f; // 리스타트 시 시간 속도 초기화 필수!
        SceneManager.LoadScene(scene);
    }
}