﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Interfaces;

namespace GameEngine.Tiled
{
    public class Tile : PropertyBag, IGameDrawable
    {
        public Texture2D SourceTexture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public int TileGid { get; set; }                    //Tile Global Identifier
        public string TileSetName { get; set; }

        //IGameDrawable Properties
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public int Layer { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public bool Visible { get; set; }
        public Vector2 Origin { get; set; }
        public string Group { get; set; }

        public Tile()
        {
            this.Color = Color.White;
            this.Rotation = 0;
            this.Layer = 0;
            this.SpriteEffect = SpriteEffects.None;
            this.Visible = true;
            this.Origin = new Vector2(0, 1);
            this.Group = null;
        }

        public Texture2D GetSourceTexture(GameTime GameTime)
        {
            return SourceTexture;
        }

        public Rectangle GetSourceRectangle(GameTime GameTime)
        {
            return SourceRectangle;
        }

        public override string ToString()
        {
            return string.Format("TileGid: {0}, TileSet: {1}", TileGid, TileSetName);
        }
    }
}
