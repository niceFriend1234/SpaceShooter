using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 전역변수 선언
    private float v;
    private float h;
    private float r;

    [SerializeField]
    private float moveSpeed = 3.0f;
    [SerializeField]
    private float turnSpeed = 200.0f;

    // Animator 컴포넌트를 저장할 변수 선언
    //[NonSerialized]
    [HideInInspector]
    public Animator animator;

    // Animator Hash 추출
    private readonly int hashForward = Animator.StringToHash("forward");
    private readonly int hashStrafe = Animator.StringToHash("strafe");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 매 프레임 마다 호출, 60 FPS, 불규칙한 주기, 랜더링 주기와 동일
    void Update()
    {
        InputAxis();
        Locomotion();
        Animation();
    }

    private void Animation()
    {
        // 애니메이션 파라메터 전달
        animator.SetFloat("forward", v);
        animator.SetFloat("strafe", h);
    }

    private void Locomotion()
    {
        // Vector 덧셈 연산
        // 이동 처리 로직
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        // 회전 처리 로직
        transform.Rotate(Vector3.up * Time.deltaTime * r * turnSpeed);
    }

    private void InputAxis()
    {
        // 축(Axis) 값을 받아옴. -1.0f ~ 0.0 ~ +1.0f
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        r = Input.GetAxis("Mouse X");
    }
}

/*
    Vector3.forward = Vector3(0, 0, 1)
    Vector3.up      = Vector3(0, 1, 0)
    Vector3.right   = Vector3(1, 0, 0)

    Vector3.one     = Vector3(1, 1, 1)
    Vector3.zero    = Vector3(0, 0, 0)
*/
