using EnumExtension;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InputHistory : Node
    {
        //[Signal]
        //public delegate void HistoryChangedEventHandler();

        //[Signal]
        //public delegate void HistoryChanged(InputHistoryItem next);




    public List<InputHistoryItem> _history = new List<InputHistoryItem>();
        
    public void add(Godot.Collections.Array<int> keys, Godot.Collections.Array<int> mods, int count)
    {
        if (_history.Count >= 20)
        {
            _history.RemoveAt(0);
        }
        var temp = new InputHistoryItem();
        {
            temp.keys = keys;
            temp.mods = mods;
            temp.count = count;
        }
        _history.Add(temp);
    } 
}
