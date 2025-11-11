using System.Collections;
using UnityEngine;
using Seagull.Interior_04E.SceneProps;

public class DoorSequenceManager : MonoBehaviour
{
    public Rotatable doorRotator;
    public Transform playerTransform; // 플레이어 위치

    public Vector3 targetPosition; // 텔레포트 할 위치
    public Vector3 targetRotation; // 텔레포트 후 플레이어의 회전값

    public float animationDuration = 2.0f; // 애니메이션 지속 시간

    private bool isOpening = false;  // 문이 열리는 중인지 여부

    // 문 열기 애니메이션 및 텔레포트 시퀀스 시작
    public void StartDoorSequence()
    {
        // 문이 열리는 중이 아닐 때만 실행
        if (!isOpening)
        {
            StartCoroutine(OpenAndTeleport());
        }
    }

    // 문 열기 애니메이션 및 텔레포트 코루틴
    private IEnumerator OpenAndTeleport()
    {
        isOpening = true;
        float elapsedTime = 0f;                  // 경과 시간
        float startValue = doorRotator.rotation; // 현재 값 (시작 시 1 = Closed)
        float endValue = 0f;                     // 목표 값 (0 = Open)

        // 문 열기 애니메이션
        while (elapsedTime < animationDuration)
        {
            doorRotator.rotation = Mathf.Lerp(startValue, endValue, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorRotator.rotation = endValue;

        // 목표 지점으로 텔레포트
        playerTransform.position = targetPosition;

        // 설정한 회전값으로 플레이어 회전
        playerTransform.rotation = Quaternion.Euler(targetRotation);

        isOpening = false;
    }
}