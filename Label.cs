using EnumExtension;
using Godot;
using inputreader;
using System;
using System.Collections.Generic;
using System.Linq;

public class Label : Godot.Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public TileSet tileset = GD.Load<TileSet>("nicetileset.tres");
    public PackedScene InputItem = GD.Load<PackedScene>("InputItemUI.tscn");
    public Node HistoryContainer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        HistoryContainer = this.GetNode("History");
        foreach (int value in Enumerable.Range(1, 20))
        {
            HistoryContainer.AddChild(InputItem.Instance());
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        InputReader parent = this.GetParent<InputReader>();
        if (parent != null)
        {
            this.Text = $"{parent.count} {parent.Directions} {parent.Buttons}\n";
            this.Text += "===\n";
            if (parent.InputHistory._history.Count == 0)
            {
                return;
            }
            this.Text += string.Join("", parent.History.Reverse());
            var reversed = parent.InputHistory._history.Reverse<InputHistoryItem>().ToList();
            for (int i = 0; i < parent.InputHistory._history.Count; i++)
            {
                var node = HistoryContainer.GetChild(i);
                var item = reversed[i];
                ((InputItemUI)node).update(item);
            }
            //var spri = InputItem.Instance();
            //this.AddChild(spri);
        }
    }
}
