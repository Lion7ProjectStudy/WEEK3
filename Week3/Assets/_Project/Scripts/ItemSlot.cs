using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image iconImage;          // 아이템 아이콘 Image
    public TextMeshProUGUI countText; // 개수 표시 TMP
    
    [HideInInspector]
    public int currentItemCode = -1; // 현재 슬롯에 담긴 아이템 코드 (-1은 빈 슬롯)
    public int itemCount = 0;

    public void UpdateSlot(InvenItem item)
    {
        currentItemCode = item.data.itemCode;
        
        // 아이콘 활성화 및 교체 (itemIcon은 GameObject이므로 GetComponent<Image>().sprite 등으로 접근 필요)
        iconImage.gameObject.SetActive(true);
        iconImage.sprite = item.data.itemIcon;

        itemCount = item.count;
        
        // 개수 업데이트 (1개 초과일 때만 표시하는 것이 실무 UI 센스!)
        countText.text = itemCount > 1 ? itemCount.ToString() : "";
    }

    public void ClearSlot()
    {
        currentItemCode = -1;
        iconImage.gameObject.SetActive(false);
        countText.text = "";
    }
}