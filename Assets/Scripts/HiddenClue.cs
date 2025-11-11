using UnityEngine;

public class HiddenClue : MonoBehaviour
{
    public GameObject clueToHide; // 숨길 단서 오브젝트
    private bool isVisible = false; // 단서가 현재 보이는지 여부

    private void Awake()
    {
        if (clueToHide != null)
        {
            clueToHide.SetActive(false); // 시작 시 단서 숨기기
        }
        else
        {
            Debug.LogWarning($"clueToHide 오브젝트가 할당되지 않음");
        }
    }

    // 단서 보이기
    public void ShowClue()
    {
        if (isVisible) return;

        if (clueToHide != null)
        {
            clueToHide.SetActive(true);
            isVisible = true;
        }
    }

    // 단서 숨기기
    public void HideClue()
    {
        if (!isVisible) return;

        if (clueToHide != null)
        {
            clueToHide.SetActive(false);
            isVisible = false;
        }
    }
}