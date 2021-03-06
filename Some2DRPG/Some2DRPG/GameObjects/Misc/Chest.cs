﻿using System;
using GameEngine;
using GameEngine.Drawing;
using GameEngine.Extensions;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Some2DRPG.GameObjects.Characters;

namespace Some2DRPG.GameObjects.Misc
{
    public class Chest : Entity
    {
        public Chest()
        {
        }

        public Chest(float x, float y)
            :base(x,y)
        {
        }

        public override void Update(GameTime gameTime, TeeEngine engine)
        {
            Hero player = (Hero) engine.GetEntity("Player");
            KeyboardState keyboardState = Keyboard.GetState();

            if (KeyboardExtensions.GetKeyDownState(keyboardState, Keys.S, this, true)
                && Entity.IntersectsWith(this, null, player, "Shadow", gameTime))
            {
                if (CurrentDrawableState != "Open")
                {
                    Random random = new Random();

                    CurrentDrawableState = "Open";
                    Drawables.ResetState("Open", gameTime);

                    for (int i = 0; i < 10; i++)
                    {
                        Coin coin = new Coin(this.Pos.X, this.Pos.Y, 100, (CoinType)random.Next(3));
                        coin.Pos.X += (float) ((random.NextDouble() - 0.5) * 100);
                        coin.Pos.Y += (float) ((random.NextDouble() - 0.5) * 100);

                        engine.AddEntity(coin);
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            DrawableSet.LoadDrawableSetXml(Drawables, "Animations/Misc/chests.anim", content);
            CurrentDrawableState = "Closed";
        }
    }
}
