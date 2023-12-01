using EnumExtension;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class InputReader : Node2D
{

    //TODO add P1/P2 switcher with the help of the start button (it's 10)

    public enum ChargingState
    {
        NOT,
        CHARGING,
        CHARGED
    }
    public Godot.Collections.Array<int> currentKeys = new Godot.Collections.Array<int>();
    public Godot.Collections.Array<int> currentMods = new Godot.Collections.Array<int>();
    public int count = 0;
    public InputHistory InputHistory = new InputHistory();
    public int threshold = 45;
    
    [Signal]
    public delegate void OnNewInput(Godot.Collections.Array<int> k, Godot.Collections.Array<int> m, int c);
    
    [Signal]
    public delegate void CurrentChanged(Godot.Collections.Array<int> k, Godot.Collections.Array<int> m, int c);
    
    
    [Signal]
    public delegate void chargingStateChanged(ChargingState current);
    private void EmitChargingStateChanged(ChargingState current) => EmitSignal(nameof(chargingStateChanged), current);
    
    
    private bool boolIsP1 = true;
    public data.PlayerSide currentPlayerSide => boolIsP1 ? data.PlayerSide.P1 : data.PlayerSide.P2;
    public string Buttons { get => utils.stringifyButtons(currentKeys); }
    public string Directions { get => utils.stringifyButtons(currentMods); }
    public string[] History { get => InputHistory._history.Select(item => $"{item.count} {utils.stringifyButtons(item.mods)} {utils.stringifyButtons(item.keys)}\n").ToArray(); }

    public override void _Ready()
    {
        Input.UseAccumulatedInput = false;
        this.Connect(nameof(InputReader.CurrentChanged), this.GetChild(0), "update");
        this.Connect(nameof(InputReader.OnNewInput), this.GetChild(1), "handleHistoryChange");
        this.Connect(nameof(InputReader.chargingStateChanged), this.GetChild(2), "OnChargingStateChanged");
        this.Connect(nameof(InputReader.chargingStateChanged), this.GetChild(4), "OnChargingStateChanged");
    }
    public override void _PhysicsProcess(float delta)
    {
        Godot.Collections.Array<int> keys = new Godot.Collections.Array<int>();
        Godot.Collections.Array<int> mods = new Godot.Collections.Array<int>();
        // get the buttons we care about
        if (Input.IsJoyButtonPressed(0, 11))
        {
            GD.Print("changed");
            boolIsP1 = !boolIsP1;
        }
        for (int i = 0; i < 11; i++) {
            if (Input.IsJoyButtonPressed(0, i)){
                keys.Add(i);
            }
        }
        for (int i = 12; i < 16; i++){
            if (Input.IsJoyButtonPressed(0, i)){
                mods.Add(i);
            }
        }

        if (keys.Count != currentKeys.Count || mods.Count != currentMods.Count) {
            logNewInput(keys, mods);
        } else if (utils.SameArrayCheck(currentKeys, keys) && utils.SameArrayCheck(currentMods, mods)) {
            count++;
        } else {
            logNewInput(keys, mods);
        }
        
        EmitSignal(nameof(CurrentChanged), currentKeys, currentMods, count);
        chargeCount();
    }
    private void logNewInput(Array<int> keys, Array<int> mods)
    {
        InputHistory.add(currentKeys, currentMods, count);
        EmitSignal(nameof(OnNewInput), currentKeys, currentMods, count);
        currentKeys = keys;
        currentMods = mods;
        count = 1;
    }

    private void chargeCount()
    {
        if (utils.HasChargeInput(currentMods, currentPlayerSide)) {
            bool thresholdMet = InputHistory.chargeCount + count >= threshold;
            if (thresholdMet) {
                EmitChargingStateChanged(ChargingState.CHARGED);
            } else {
                EmitChargingStateChanged(ChargingState.CHARGING);
            };
        } else
        {
            EmitChargingStateChanged(ChargingState.NOT);
        }
    }
}
