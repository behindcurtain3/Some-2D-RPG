﻿using System;
using GameEngine;
using GameEngine.Drawing;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Some2DRPG.GameObjects.Characters;

namespace Some2DRPG.GameObjects.Misc
{
    public enum CoinType { Gold, Silver, Copper };

    public class Coin : CollidableEntity
    {
        public int CoinValue { get; set; }

        public SoundEffect CoinSound { get; set; }

        public CoinType CoinType {
            get { return _coinType; }
            set
            {
                CurrentDrawableState = value.ToString();
                _coinType = value;
            }
        }

        private CoinType _coinType;

        public Coin()
        {
            Construct();
        }

        public Coin(float x, float y, int coinValue, CoinType coinType)
        {
            Construct(x, y, coinType, coinValue);
        }

        void Construct(float x=0, float y=0, CoinType coinType=CoinType.Copper, int coinValue=0)
        {
            this.Pos = new Vector2(x, y);
            this.CoinType = coinType;
            this.ScaleX = 0.7f;
            this.ScaleY = 0.7f;
            this.CoinValue = coinValue;
            this.EntityCollisionEnabled = false;
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the coin animation.
            DrawableSet.LoadDrawableSetXml(this.Drawables, "Animations/Misc/coin.anim", content );

            CoinSound = content.Load<SoundEffect>("Sounds/Coins/coin1");
        }

        public override void Update(GameTime gameTime, TeeEngine engine)
        {
            if (IsOnScreen)
            {
                float COIN_MOVE_SPEED = 5000;
                float TERMINAL_VELOCITY = 5;

                Hero player = (Hero)engine.GetEntity("Player");

                // Find the distance between the player and this coin.
                float distanceSquared = Vector2.DistanceSquared(Pos, player.Pos);

                float speed = COIN_MOVE_SPEED / distanceSquared;  // Mangitude of velocity.
                speed = Math.Min(speed, TERMINAL_VELOCITY);

                if (speed > 0.5)
                {
                    // Calculate the angle between the player and the coin.
                    double angle = Math.Atan2(
                        player.Pos.Y - this.Pos.Y,
                        player.Pos.X - this.Pos.X
                        );

                    this.Pos.X += (float)(Math.Cos(angle) * speed);        // x component.
                    this.Pos.Y += (float)(Math.Sin(angle) * speed);        // y component.

                    // Check to see if coin can be considered collected.
                    if (Entity.IntersectsWith(this, "Shadow", player, "Shadow", gameTime))
                    {
                        // CoinSound.Play(0.05f, 0.0f, 0.0f);
                        player.Coins += this.CoinValue;
                        engine.RemoveEntity(this);
                    }
                }
            }

            base.Update(gameTime, engine);
        }

        public override string ToString()
        {
            return string.Format(
                "Coin: Name={0}, CoinValue={1}, CoinType={2}, Pos={3}",
                Name,
                CoinValue,
                CoinType,
                Pos );

        }
    }
}
