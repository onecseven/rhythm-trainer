using EnumExtension;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct InputHistoryItem
{
    public Godot.Collections.Array<int> keys;
    public Godot.Collections.Array<int> mods;
    public int count;

    public InputHistoryItem(Godot.Collections.Array<int> k, Godot.Collections.Array<int> m, int c)
    {
        this.keys = k;
        this.mods = m;
        this.count = c;
    }
    public static bool operator ==(InputHistoryItem first, InputHistoryItem second)
    {
        return (first.count == second.count && Godot.Collections.Array.Equals(first.keys, second.keys) && Godot.Collections.Array.Equals(first.mods, second.mods));
    }

    public static bool operator !=(InputHistoryItem first, InputHistoryItem second)
    {
        return (first.count == second.count && Godot.Collections.Array.Equals(first.keys, second.keys) && Godot.Collections.Array.Equals(first.mods, second.mods));
    }

    public override bool Equals(object obj)
    {
        if (!(obj is InputHistoryItem)) return false;
        else
        {
            var casted = (InputHistoryItem)obj;
            return (this.count == casted.count
                && this.keys.Count == casted.keys.Count
                && this.mods.Count == casted.mods.Count
                && this.keys.All(i => casted.keys.Contains(i))
                && this.mods.All(i => casted.mods.Contains(i))
                );
        }
    }
}