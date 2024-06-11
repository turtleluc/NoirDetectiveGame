using Godot;
using System;

public partial class Npc : Node
{
	[Export]
	public NodePath DialogUIPath;
	
	[Export]
	public GameManeger gameManeger;

	[Export]
	public string Npcname = "Null";

	[Export]
	public bool isKiller;

	[Export]
	public string[] Questions;

	[Export]
	public string[] Answers;

	private Node2D dialogUI;

	// Initialize the NPC's data
	public override void _Ready()
	{
		// Get the DialogUI node
		dialogUI = GetNode<Node2D>(DialogUIPath);
		dialogUI.Visible = false;  // Hide the dialog UI initially
	}

	// Get the NPC's question by index
	public string GetQuestion(int index)
	{
		if (index >= 0 && index < Questions.Length)
		{
			return Questions[index];
		}
		return "No question available.";
	}

	// Get the NPC's answer by index
	public string GetAnswer(int index)
	{
		if (index >= 0 && index < Answers.Length)
		{
			return Answers[index];
		}
		return "I don't have an answer for that.";
	}

	// Method to show the dialog UI
	private void _on_pressed_npc()
	{
		//gameManeger.currentNpc = this;
		//gameManeger.ShowDialog();
		gameManeger.InteractWithNpc(this);
		GD.Print("Button Pressed");
	}
}
