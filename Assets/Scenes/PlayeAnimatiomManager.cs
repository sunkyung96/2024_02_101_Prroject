using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeAnimatiomManager : MonoBehaviour
{
    public Animator animator; //�ִϸ��̼��� �����ϴ� ������Ʈ
    public PlayeAnimatiomManager stateMachine; //����ڰ� ������ ���� ����

    //�ִϸ����� �Ķ���� �̸����� ����� ����
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
        //���� ���¿� ���� �ִϸ��̼� �Ķ���� ����
        if(stateMachine.currentState !=null)
        {
            //��� bool �Ķ���͸� �ʱ�ȭ
            ResetAIIBoolParameters();

            //���� ���¿� ���� �ش��ϴ� �ִϸ��̼� �Ķ���͸� ����
            switch (stateMachine.currentState)
            {
                case IdleState;
                    //Idle ���´� �� �Ķ���Ͱ� false�� ����
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
