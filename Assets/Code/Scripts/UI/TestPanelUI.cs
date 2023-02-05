using System;
using TMPro;
using UnityEngine;
using Code.Scripts.StateMachine;

public class TestPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text infoText;

    private void Update()
    {
        SetTestPanelUI();
    }

    private void SetTestPanelUI()
    {
        var currentState = StateMachines.CurrentState;
        infoText.text = "Current State : " + currentState + " | Date : " + DateTime.Now.Date;
    }
}
