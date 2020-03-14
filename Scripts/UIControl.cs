using Godot;
using System;
using EventCallback;

public class UIControl : CanvasLayer
{
    //The UI screens that need to be controlled
    Node2D menuPanel, winPanel, losePanel, creditsPanel, uiPanel;
    //The buttons used for all the screens
    Button startBtn, exitBtn, smallExitBtn, menuBtn, creditsBtn;
    //The main background
    Sprite background;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        UnitDeathEvent.RegisterListener(UnitDeath);
        WinEvent.RegisterListener(WinTriggered);

        //Grab all the references for the scenes
        menuPanel = GetNode<Node2D>("Menu");
        winPanel = GetNode<Node2D>("Win");
        losePanel = GetNode<Node2D>("Lose");
        creditsPanel = GetNode<Node2D>("Credits");
        uiPanel = GetNode<Node2D>("UI");
        background = GetNode<Sprite>("Background");


        //Grab a refference to all the buttons that we have
        startBtn = GetNode<Button>("Start");
        exitBtn = GetNode<Button>("Exit");
        menuBtn = GetNode<Button>("Menubtn");
        smallExitBtn = GetNode<Button>("SmallExit");
        creditsBtn = GetNode<Button>("Creditsbtn");

        //The buttons for the menu
        startBtn.Connect("pressed", this, nameof(ShowUI));
        exitBtn.Connect("pressed", this, nameof(ExitGame));
        menuBtn.Connect("pressed", this, nameof(ShowMenu));
        smallExitBtn.Connect("pressed", this, nameof(ExitGame));
        creditsBtn.Connect("pressed", this, nameof(ShowCredits));

        //Hide all the scenes at start up
        HideAll();
        ShowMenu();
    }
    private void HideAll()
    {
        //Hide all the UI screens
        menuPanel.Hide();
        winPanel.Hide();
        losePanel.Hide();
        creditsPanel.Hide();
        uiPanel.Hide();
        //Hide the background
        background.Hide();
        //Hide all the buttons
        startBtn.Hide();
        exitBtn.Hide();
        menuBtn.Hide();
        smallExitBtn.Hide();
        creditsBtn.Hide();
    }
    private void ShowMenu()
    {
        //Send message that the ui is changed
        MainEvent mei = new MainEvent();
        mei.menuBtnPressed = true;
        mei.FireEvent();
        //Hide all the UI elements
        HideAll();
        //Show the buttons and menu 
        startBtn.Show();
        creditsBtn.Show();
        exitBtn.Show();
        menuPanel.Show();
        background.Show();
    }
    private void ShowUI()
    {          //Hide all the UI elements
        HideAll();
        //Show the UIPanel
        //Show the small exit button
        smallExitBtn.Show();

        uiPanel.Show();
        //Send message that the ui is changed
        MainEvent mei = new MainEvent();
        mei.startBtnPressed = true;
        mei.FireEvent();
    }
    private void ShowWin()
    {
        HideAll();
        winPanel.Show();
        menuBtn.Show();
    }
    private void ShowLose()
    {
        HideAll();
        losePanel.Show();
        menuBtn.Show();
    }
    private void ShowCredits()
    {
        HideAll();
        creditsPanel.Show();
        background.Show();
        menuBtn.Show();
    }
    private void ExitGame()
    {
        //Exit the game
        GetTree().Quit();
    }
    private void UnitDeath(UnitDeathEvent ude)
    {
        if (ude.UnitNode.IsInGroup("Player")) ShowLose();
    }

    private void WinTriggered(WinEvent we)
    {
        ShowWin();
    }
    public override void _ExitTree()
    {
        UnitDeathEvent.UnregisterListener(UnitDeath);
        WinEvent.UnregisterListener(WinTriggered);

    }
}
