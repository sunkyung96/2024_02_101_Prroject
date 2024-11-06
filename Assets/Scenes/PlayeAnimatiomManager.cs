using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeAnimatiomManager : MonoBehaviour
{
    public Animator animator; //애니메이션을 관리하는 오브젝트
    public PlayeAnimatiomManager stateMachine; //사용자가 정리한 상태 정의

    //애니메이터 파라미터 이름들을 상수로 정의
    private const string PARAM_IS_MOVING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";
    private const string PARAM_IS_JUMPING = "IsJumping";
    private const string RARAM_IS_FALLING = "IsFalling";
    private const string PARAM_ATTACK_TRIGGER= "Attack";


    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateAnimationState()
    {
        //현재 상태에 따라 애니메이션 파라미터 설정
        if(stateMachine.currentState !=null)
        {
            //모든 bool 파라미터를 초기화
            ResetAIIBoolParameters();

            //현재 상태에 따라 해당하는 애니메이션 파라미터를 설정
            switch (stateMachine.currentState)
            {
                case IdleState;
                    //Idle 상태는 모른 파라미터가 false인 상태
                    break;
                    case MovingState;
                    {

                    }

                    
            }
        }
    }
    private void TriggerAttack()
    {
        animator.SetTrigger(PARAM_ATTACK_TRIGGER);
    }
    private void ResetAIIBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
        animator.SetBool(PARAM_IS_JUMPING, false);
        animator.SetBool(RARAM_IS_FALLING, false);
    }
    
}
