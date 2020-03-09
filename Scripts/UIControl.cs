using Godot;
using System;
using EventCallback;

public class UIControl : Control
{
    //The UI screens that need to be controlled
    Node2D menuPanel, uiPanel, winPanel, losePanel, creditsPanel;
//The buttons used for all the screens
    Button startBtn, exitBtn, menuBtn;
    //The main background
    Sprite background;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        //Grab all the references for the scenes
        menuPanel = GetNode<Node2D>("Menu");
        uiPanel = GetNode<Node2D>("UI");
        winPanel = GetNode<Node2D>("Win");
        losePanel = GetNode<Node2D>("Lose");
        creditsPanel = GetNode<Node2D>("Credits");
        background = GetNode<Sprite>("Background");

        //Grab a refference to all the buttons that we have
        startBtn = GetNode<Button>("Menubtn");
        exitBtn = GetNode<Button>("Exit");
        menuBtn = GetNode<Button>("Menubtn");

//The buttons for the menu
        startBtn.Connect("pressed", this, nameof(ShowUI));
        exitBtn.Connect("pressed", this, nameof(ExitGame));
        menuBtn.Connect("pressed", this, nameof(ShowMenu));

        //Hide all the scenes at start up
        HideAll();
        ShowMenu();
    }
    private void HideAll()
    {
        //Hide all the UI screens
        menuPanel.Hide();
        uiPanel.Hide();
        winPanel.Hide();
        losePanel.Hide();
        creditsPanel.Hide();
        background.Hide();
    }

    private void ShowMenu()
    {
        //Show the menu as a defualt start up
        menuPanel.Show();
        background.Show();
    }

    private void ShowUI()
    {

    }

    private void ShowWin()
    {

    }

    private void ShowLose()
    {

    }

    private void ShowCredits()
    {

    }
    private void ExitGame()
    {
        //Exit the game
        GetTree().Quit();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
