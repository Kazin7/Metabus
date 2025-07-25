using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI comboText;
    TextMeshProUGUI maxcomboText;
    protected override UIState GetUIStates()
    {
        return UIState.Game;
    }
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        comboText = transform.Find("ComboText").GetComponent<TextMeshProUGUI>();
        maxcomboText = transform.Find("MaxComboText").GetComponent<TextMeshProUGUI>();
    }
    public void SetUI(int score, int combo, int maxCombo)
    {
        scoreText.text = score.ToString();
        comboText.text = combo.ToString();
        maxcomboText.text = maxCombo.ToString();
    }
}
