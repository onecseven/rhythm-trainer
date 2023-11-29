using Godot;
using System;

public class Keys : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Godot.Collections.Array<int> last = new Godot.Collections.Array<int>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private void replace(Godot.Collections.Array<int> current)
    {
        for (int i = 0; i < last.Count; i++)
        {
            TextureRect node = this.GetChild<TextureRect>(i);
            var key = current[i];
            node.Texture = utils.numToSprite(key);
        }
    }

    private void killdren()
    {
        foreach (Node item in this.GetChildren())
        {
            item.QueueFree();
        }
    }

    public void update(Godot.Collections.Array<int> current)
    {
        if (current.Count == 0) killdren();
        if (utils.SameArrayCheck(last, current)) return;
        if (last.Count > 0 && current.Count == last.Count) replace(current);
        else //further optimizations if we check for less children or more
        {
            killdren();
            foreach (int i in current)
            {
                var temp = new TextureRect();
                temp.Texture = utils.numToSprite(i);
                this.AddChild(temp);
            }
        }
        last = current;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
