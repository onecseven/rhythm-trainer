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


    public List<InputHistoryItem> _history = new List<InputHistoryItem>() { new InputHistoryItem(new Godot.Collections.Array<int>(), new Godot.Collections.Array<int>(), 1) };
    public InputHistoryItem last { get => _history[0]; }

    public int chargeCount {
        get
        {
            int chargeCount = 0;
            for (int i = _history.Count - 1; i >= 0; i--)
            {
                var item = _history[i];
                if (utils.HasChargeInput(item.mods))
                {
                    GD.Print(string.Join(" ", item.mods));
                    chargeCount += item.count;
                }
                else return chargeCount;
            }
            return chargeCount;
        }
    }
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
