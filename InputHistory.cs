using EnumExtension;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inputreader
{
    public enum Mods
    {
        UP=12,
        DOWN=13, 
        LEFT=14,
        RIGHT=15,
    }

    public class utils
    {
        public static TileSet tiles = GD.Load<TileSet>("nicetileset.tres");

        public static bool SameArrayCheck(Godot.Collections.Array<int> a1, Godot.Collections.Array<int> a2)
        {
            return (a1.Count == a2.Count && a1.All(i => a2.Contains(i)));
        }
        public static string stringifyButtons(Array<int> values)
        {
            if (values.Count == 0) return "N";
            if (values.Any(item => item > 10))
            {
                return string.Join("", values.Select(key => ((inputreader.Mods)key).Name()));
            }
            else
            {
                return string.Join("", values.Select(key => ((inputreader.Keys)key).Name()));
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
                tilename = ((inputreader.Mods)i).Name();
            }
            else
            {
                tilename = ((inputreader.Keys)i).Name();
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

    public class InputHistory : Node2D
    {
        [Signal]
        public delegate void NewHistoryItemAdded();
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
}
