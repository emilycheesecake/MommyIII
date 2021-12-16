using Godot;
using System;

public class Global : Node2D
{
	
	public Node CurrentScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Viewport root = GetTree().GetRoot();
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
		//Go to test scene
		GotoScene("res://Test.tscn");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public void GotoScene(string path)
	{
		//Defer load to later time to prevent deleting scene while it's code is still running
		CallDeferred(nameof(DeferredGotoScene), path);
	}
	
	public void DeferredGotoScene(string path)
	{
		CurrentScene.Free();
		
		var nextScene = (PackedScene)GD.Load(path);
		
		CurrentScene = nextScene.Instance();
		
		GetTree().GetRoot().AddChild(CurrentScene);
		GetTree().SetCurrentScene(CurrentScene);
	}
}
