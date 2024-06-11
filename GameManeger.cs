using Godot;
using System;

public partial class GameManeger : Node
{
	[Export]
	public Node2D DialogUI;  // Reference to the dialog UI control
	
	[Export]
	public Npc currentNpc;
	
	bool dialog = false;

	public override void _Ready()
	{
		DialogUI.Visible = false;  // Hide the dialog UI initially
	}

	// Call this method when the player interacts with an NPC
	public void InteractWithNpc(Npc npc)
	{
		currentNpc = npc;
		//dialog = true;
		ShowDialog();
	}
	 public override void _Process(double delta)
	{
		// Voer hier je logica uit die elke frame moet worden uitgevoerd
		if (dialog) {
			ShowDialog();
			GD.Print("ShowDialog");
			}
	}
	// Show the dialog UI with the NPC's dialog
	public void ShowDialog()
	{
		if (currentNpc != null && DialogUI != null)
		{
			DialogUI.Visible = true;

			// Update the dialog UI with the NPC's dialog
			RichTextLabel dialogText = DialogUI.GetNode<RichTextLabel>("DialogText");
			dialogText.Text = $"Talking to {currentNpc.Npcname}";

			// Assuming you have buttons for questions
			Button question1 = DialogUI.GetNode<Button>("Question1");
			Button question2 = DialogUI.GetNode<Button>("Question2");
			Button question3 = DialogUI.GetNode<Button>("Question3");

			question1.Text = currentNpc.GetQuestion(0);
			question2.Text = currentNpc.GetQuestion(1);
			question3.Text = currentNpc.GetQuestion(2);

			question1.Visible = currentNpc.Questions.Length > 0;
			question2.Visible = currentNpc.Questions.Length > 1;
			question3.Visible = currentNpc.Questions.Length > 2;
		}
	}

	// Call this method to hide the dialog UI
	public void HideDialog()
	{
		DialogUI.Visible = false;
	}

	// Handle the player's selected question
	public void AskQuestion(int questionIndex)
	{
		if (currentNpc != null)
		{
			RichTextLabel dialogText = DialogUI.GetNode<RichTextLabel>("DialogText");
			dialogText.Text = currentNpc.GetAnswer(questionIndex);
		}
	}
}
