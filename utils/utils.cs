using Godot.Collections;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnumExtension;
using data;
//https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-create-a-new-method-for-an-enumeration

namespace data {

    public enum PlayerSide
    {
        P1,
        P2,
    }
    public enum Mods
    {
        UP = 12,
        DOWN = 13,
        LEFT = 14,
        RIGHT = 15,
    }
    public enum Keys
    {
        LK,
        HK,
        LP,
        MP,
        DP,
        MK,
        DI,
        HP,
    }


}

public class utils
{
    public static TileSet tiles = GD.Load<TileSet>("static/nicetileset.tres");

    public static bool SameArrayCheck(Godot.Collections.Array<int> a1, Godot.Collections.Array<int> a2)
    {
        return (a1.Count == a2.Count && a1.All(i => a2.Contains(i)));
    }
    public static string stringifyButtons(Array<int> values)
    {
        if (values.Count == 0) return "N";
        if (values.Any(item => item > 10))
        {
            return string.Join("", values.Select(key => ((data.Mods)key).Name()));
        }
        else
        {
            return string.Join("", values.Select(key => ((data.Keys)key).Name()));
        }
    }
    public static AtlasTexture numToSprite(int i)
    {
        var sprite = new Sprite();
        sprite.RegionEnabled = true;
        string tilename = "";
        if (i < 0)
        {
            GD.Print("huh");
        }
        if (i > 10)
        {
            tilename = ((data.Mods)i).Name();
        }
        else
        {
            tilename = ((data.Keys)i).Name();
        }
        return strToSprite(tilename);
    }
    public static AtlasTexture strToSprite(string nam)
    {
        var sprite = new Sprite();
        sprite.RegionEnabled = true;
        string tilename = nam;
        int tileid = tiles.FindTileByName(tilename);
        AtlasTexture atlasTexture = new AtlasTexture();
        atlasTexture.Atlas = tiles.TileGetTexture(tileid);
        atlasTexture.Region = tiles.TileGetRegion(tileid);
        return atlasTexture;
    }

    public static bool HasChargeInput(Godot.Collections.Array<int> input, PlayerSide player = PlayerSide.P1)
    {
        int chargeInput = player == PlayerSide.P1 ? (int)data.Mods.LEFT : (int)data.Mods.RIGHT;
        return input.Contains(chargeInput);
    }
}

