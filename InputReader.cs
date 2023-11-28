using EnumExtension;
using Godot;
using Godot.Collections;
using inputreader;
using System;
using System.Linq;

public class InputReader : Node2D
{
        
    public Godot.Collections.Array<int> currentKeys = new Godot.Collections.Array<int>();
    public string Buttons { get => utils.stringifyButtons(currentKeys); }
    public string Directions { get => utils.stringifyButtons(currentMods); }

    public string[] History { get => InputHistory._history.Select(item => $"{item.count} {utils.stringifyButtons(item.mods)} {utils.stringifyButtons(item.keys)}\n").ToArray(); }
    public Godot.Collections.Array<int> currentMods = new Godot.Collections.Array<int>();
    public int count = 0;
    
    public InputHistory InputHistory = new InputHistory();
    public override void _Ready()
    {
        Input.UseAccumulatedInput = false;
    }


    public override void _PhysicsProcess(float delta)
    {
        Godot.Collections.Array<int> keys = new Godot.Collections.Array<int>();
        Godot.Collections.Array<int> mods = new Godot.Collections.Array<int>();
        for (int i = 0; i < 11; i++) {
            if (Input.IsJoyButtonPressed(0, i))
            {
                keys.Add(i);
            }
        }
        for (int i = 12; i < 16; i++)
        {
            if (Input.IsJoyButtonPressed(0, i))
            {
                mods.Add(i);
            }
        }
        if (keys.Count != currentKeys.Count || mods.Count != currentMods.Count)
        {
            setNew(keys, mods);
        } else if (mods.Count == currentMods.Count && keys.Count == currentKeys.Count)
        {
            if (keys.All(key => currentKeys.Contains(key)) && mods.All(mod => currentMods.Contains(mod))) count++;
            else setNew(keys, mods);
        }

    }
    private void setNew(Array<int> keys, Array<int> mods)
    {
        InputHistory.add(currentKeys, currentMods, count);
        count = 1;
        currentKeys = keys;
        currentMods = mods;
    }
}
