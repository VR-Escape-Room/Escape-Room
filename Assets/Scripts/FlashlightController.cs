using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("레퍼런스 설정")]  
    public Light spotLight; // spotLight의 light 컴포넌트
    public Transform raycastOrigin; // ray 시작점이 될 spotLight의 Transform

    [Header("ray 감지 설정")]
    public float raycastDistance = 20f; // ray 감지 거리
    public LayerMask clueLayer; // ray가 감지할 레이어

    private bool isOn = false; // 손전등 켜짐 여부
    private HiddenClue currentClue; // 현재 손전등으로 비추고 있는 단서

    void Awake()
    {
        // 시작 시 손전등 끄기
        if (spotLight != null)
        {
            spotLight.enabled = false;
        }
    }

    void Update()
    {
        // 손전등이 꺼져 있으면 현재 단서 숨김
        if (!isOn || spotLight == null || !spotLight.enabled)
        {
            if (currentClue != null)
            {
                currentClue.HideClue();
                currentClue = null;
            }
            return;
        }

        // raycast 발사
        RaycastHit hit;
        Vector3 origin = raycastOrigin != null ? raycastOrigin.position : transform.position;
        Vector3 direction = raycastOrigin != null ? raycastOrigin.forward : transform.forward;

        // ray가 단서에 닿았는지 확인
        if (Physics.Raycast(origin, direction, out hit, raycastDistance, clueLayer))
        {
            // ray가 무언가에 닿음

            HiddenClue clue = hit.collider.GetComponent<HiddenClue>();

            // 닿은 오브젝트가 단서인 경우            
            if (clue != null)
            {
                if (currentClue != clue)
                {
                    // 이전 단서를 숨기고 새 단서 표시
                    if (currentClue != null)
                        currentClue.HideClue();

                    currentClue = clue;
                    currentClue.ShowClue();
                }
            }
            // 닿은 오브젝트가 단서가 아닌 경우
            else
            {
                HideCurrentClue();
            }
        }
        // ray에 아무것도 닿지 않음
        else
        {
            HideCurrentClue();
        }
    }

    // 현재 단서 숨기기
    private void HideCurrentClue()
    {
        if (currentClue != null)
        {
            currentClue.HideClue();
            currentClue = null;
        }
    }

    // 손전등 켜기
    public void TurnOn()
    {
        isOn = true;
        if (spotLight != null)
            spotLight.enabled = true;
    }

    // 손전등 끄기
    public void TurnOff()
    {
        isOn = false;
        if (spotLight != null)
            spotLight.enabled = false;

        HideCurrentClue();
    }

    // 감지 범위 시각화 용
    void OnDrawGizmos()
    {
        if (!isOn) return;

        Vector3 origin = raycastOrigin != null ? raycastOrigin.position : transform.position;
        Vector3 direction = raycastOrigin != null ? raycastOrigin.forward : transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, direction * raycastDistance); // <-- 이 줄만 남김
    }
}