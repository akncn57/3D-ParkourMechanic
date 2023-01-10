using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] private List<ParkourAction> parkourActions;
    [SerializeField] private EnvironmentScanner environmentScanner;
    [SerializeField] private PlayerStateMachine playerStateMachine;
    private bool _isJumping;
    private bool _inAction;

    private void Awake()
    {
        playerStateMachine.inputReader.JumpEvent += InputReaderOnJumpEvent;
    }

    private void OnDisable()
    {
        playerStateMachine.inputReader.JumpEvent -= InputReaderOnJumpEvent;
    }

    private void Update()
    {
        if (_isJumping && !_inAction)
        {
            var hitData = environmentScanner.ObstacleCheck();
            
            if (hitData.ForwardHitFound)
            {
                foreach (var action in parkourActions)
                {
                    if (action.CheckIfPossible(hitData, playerStateMachine.transform))
                    {
                        StartCoroutine(DoParkourAction(action));
                        break;
                    }
                }
            }   
        }
    }

    private IEnumerator DoParkourAction(ParkourAction action)
    {
        _inAction = true;
        playerStateMachine.SetHasControl(false);
        
        playerStateMachine.animator.CrossFade(action.AnimationName, 0.2f);
        
        yield return new WaitForEndOfFrame();
        
        var animationState = playerStateMachine.animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName)) Debug.LogError("Parkour animation is wrong!");
        yield return new WaitForSeconds(animationState.length);
        
        playerStateMachine.SetHasControl(true);
        _inAction = false;
        _isJumping = false;
    }
    
    private void InputReaderOnJumpEvent()
    {
        _isJumping = true;
    }
}
