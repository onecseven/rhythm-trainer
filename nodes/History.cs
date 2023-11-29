using Godot;
using System;
using System.Linq;
using static InputHistory;

public class History : VFlowContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public PackedScene InputItem = GD.Load<PackedScene>("nodes/InputItemControl/InputItemUI.tscn");
    public int index = 0;
    
    private void indexUp()
    {
        index = index + 1;
        if (index > 19)
        {
            index = 0;
        }
    }
    public override void _Ready()
    {
        foreach (int value in Enumerable.Range(1, 20))
        {
            this.AddChild(InputItem.Instance());
        }
    }

    public void handleHistoryChange(Godot.Collections.Array<int> k, Godot.Collections.Array<int> m, int c)
    {
        Node last = this.GetChild(19);
        last.QueueFree();
        var item = InputItem.Instance();
        ((InputItemUI)item).update(k, m, c);
        this.AddChild(item);
        this.MoveChild(item, 0);
    }
    //var reversed = parent.InputHistory._history.Reverse<InputHistoryItem>().ToList();
    //for (int i = 0; i<parent.InputHistory._history.Count; i++)
    //{
    //  var node = HistoryContainer.GetChild(i);
    //  var item = reversed[i];
    //  ((InputItemUI) node).update(item);
    //}

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
