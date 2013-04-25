﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Interfaces
{
    /// <summary>
    /// Defines an interface which is required by the TileEngine class to draw items on the screen.
    /// </summary>
    public interface IGameDrawable
    {
        Vector2 Origin { get; set; }

        Rectangle GetSourceRectangle(double elapsedMS);
        Texture2D GetSourceTexture(double elapsedMS);
    }
}
