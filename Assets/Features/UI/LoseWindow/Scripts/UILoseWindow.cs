using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UILoseWindow : UIWindow
{
    public event EventHandler RestartButtonClickEvent;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonClickHandler);
    }

    public override void Hide()
    {
        
    }

    public override void Show()
    {
        
    }

    public void SetScore(int value)
    {
        scoreText.text = value.ToString();
    }

    private void OnRestartButtonClickHandler()
    {
        RestartButtonClickEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartButtonClickEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
