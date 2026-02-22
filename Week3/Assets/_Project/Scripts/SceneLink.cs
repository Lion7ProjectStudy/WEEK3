using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLink : MonoBehaviour
{
    [Header("UI")]
    public GameObject mainUI;
    public TextMeshProUGUI money;
    public TextMeshProUGUI moneyResult;
    public ItemSlot[] slots;
    public ItemSlot[] ResultSlots;
    public GameObject gamover;
    public GameObject resultUI;
    public GameObject rst;
    

    void Start()
    {
        // 시작하자마자 살아남아 있는 인스턴스에 자기 정보를 주입
        GameManager.Instance.UpdateSceneReferences(this);
    }
}
