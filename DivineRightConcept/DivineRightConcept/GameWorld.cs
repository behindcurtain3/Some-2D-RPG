﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using DivineRightConcept.Generators;

namespace DivineRightConcept
{
    public class GameWorld : GameComponent
    {
        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }

        public Texture2D GroundTextures { get; set; }
        public Texture2D StickManTexture { get; set; }

        //interface for retreiving the generated minimap
        public Texture2D MiniMapTexture
        {
            get { return _miniMap; }
        }

        //contains actual tile information about the world map
        private int[][] _worldMap;

        private Texture2D _miniMap;

        public GameWorld(Game Game, int WorldWidth, int WorldHeight)
            :base(Game)
        {
            this.WorldWidth = WorldWidth;
            this.WorldHeight = WorldHeight;
        }

        public override void Initialize()
        {
            RandomWorldGenerator generator = new RandomWorldGenerator();
            _worldMap = generator.Generate(WorldWidth, WorldHeight);

            //DUMP MAP COORDINATES FOR DEBUGGING
            TextWriter writer = new StreamWriter("map_coord.txt");
            for (int j = 0; j < WorldHeight; j++)
            {
                for (int i = 0; i < WorldWidth; i++)
                    writer.Write(_worldMap[i][j].ToString());

                writer.WriteLine();
            }
            writer.Close();

            //create a small scale map for the user (The MiniMap)
            Color[] mapColors = new Color[WorldWidth * WorldHeight];
            _miniMap = new Texture2D(Game.GraphicsDevice, WorldWidth, WorldHeight, false, SurfaceFormat.Color);
            for (int i = 0; i < WorldWidth; i++)
                for (int j = 0; j < WorldHeight; j++)
                    mapColors[j * _worldMap.Length + i] = Ground.TextureColors[_worldMap[i][j]];
            _miniMap.SetData<Color>(mapColors);

            base.Initialize();
        }

        /// <summary>
        /// Backwards compatible Draw view port methods that will always attempt to automatically center the viewport on the player. This gives the 
        /// impresion that the camera is following the player around.
        /// </summary>
        public void DrawWorldViewPort(SpriteBatch SpriteBatch, int PlayerX, int PlayerY, int TileWidth, int TileHeight, Rectangle DestRectangle)
        {
            DrawWorldViewPort(SpriteBatch, PlayerX, PlayerY, PlayerX, PlayerY, TileWidth, TileHeight, DestRectangle);
        }

        /// <summary>
        /// Draws a viewport of the current game world at the specified CenterX, CenterY location. The Viewport size and location on the screen must be 
        /// specified in the DestRectangle parameter. The number of Tiles both Width-wise and Height-wise should be specified in the TileWidth and TileHeight
        /// parameters. TEMP, PlayerX and PlayerY coordinates are showm. These should be removed at a later stage when such a value would be inbuilt within this
        /// class and rendered according to whether or not it is within the viewport.
        /// </summary>
        /// <param name="SpriteBatch">SpriteBatch object with which to render the Viewport. Should have already been opened for rendering.</param>
        /// <param name="PlayerX">X Coordinate where the player currently is on the world map.</param>
        /// <param name="PlayerY">Y Coordinate where the player currently is on the world map.</param>
        /// <param name="CenterX">X Coordinate on the world map specifying where the to be drawn viewport should be rendered.</param>
        /// <param name="CenterY">Y Coordinate on the world map specifying where the to be drawn viewport should be rendered.</param>
        /// <param name="TileWidth">Number of Tiles, Width-wise that should be shown within the viewport.</param>
        /// <param name="TileHeight">Number of Tiles, Height-wise that should be shown within the viewport.</param>
        /// <param name="DestRectangle">Rectangle object specifying the render destination for the viewport. Should specify location, width and height.</param>
        public void DrawWorldViewPort(SpriteBatch SpriteBatch, int PlayerX, int PlayerY, int CenterX, int CenterY, int TileWidth, int TileHeight, Rectangle DestRectangle)
        {
            //DRAW THE WORLD MAP
            int pxTileWidth = DestRectangle.Width / TileWidth;
            int pxTileHeight = DestRectangle.Height / TileHeight;

            //determine the topleft world coordinate in the view
            int topLeftX = CenterX - (int) Math.Ceiling((double)TileWidth/2);
            int topLeftY = CenterY - (int) Math.Ceiling((double)TileHeight/2);

            //Prevent the View from going outisde of the WORLD coordinates
            if (topLeftX < 0) topLeftX = 0;
            if (topLeftY < 0) topLeftY = 0;
            if (topLeftX + TileWidth >= WorldWidth) topLeftX = WorldWidth - TileWidth;
            if (topLeftY + TileHeight >= WorldHeight) topLeftY = WorldHeight - TileHeight;

            //draw each tile
            for (int i = 0; i < TileWidth; i++)
                for (int j = 0; j < TileHeight; j++)
                {
                    int tileX = i + topLeftX;
                    int tileY = j + topLeftY;

                    Rectangle tileDestRect = new Rectangle(i * pxTileWidth, j * pxTileHeight, pxTileWidth, pxTileHeight);
                    tileDestRect.X += DestRectangle.X;
                    tileDestRect.Y += DestRectangle.Y;

                    SpriteBatch.DrawGroundTexture(GroundTextures, _worldMap[tileX][tileY], tileDestRect);
                }

            //DRAW THE USERS CHARACTER
            //The relative position of the character should always be (X,Y) - (topLeftX,TopLeftY) where topLeftX and topLeftY have already been corrected
            //in terms of the bounds of the WORLD map coordinates. This allows for panning at the edges
            SpriteBatch.Draw(StickManTexture, new Rectangle(
                (PlayerX - topLeftX) * pxTileWidth + DestRectangle.X, 
                (PlayerY - topLeftY) * pxTileHeight + DestRectangle.Y, 
                pxTileWidth, pxTileHeight), 
                Color.White);
        }
    }
}
