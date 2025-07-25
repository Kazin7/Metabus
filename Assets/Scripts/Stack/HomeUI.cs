using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startButton;
    protected override UIState GetUIStates()
    {
        return UIState.Home;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        startButton = transform.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnClickStartButton);
    }
    void OnClickStartButton()
    {
        uiManager.OnClickStart();
    }
}
