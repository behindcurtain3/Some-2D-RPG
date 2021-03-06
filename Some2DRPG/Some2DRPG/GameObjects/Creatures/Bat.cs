﻿using System;
using GameEngine;
using GameEngine.Drawing;
using GameEngine.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Some2DRPG.GameObjects.Characters;

namespace Some2DRPG.GameObjects.Creatures
{
    public class Bat : Entity
    {
        enum AttackStance { NotAttacking, Preparing, Attacking };

        private static Random randomGenerator = new Random();

        private const int ATTACK_COUNTER_LIMIT = 40;
        private const double ATTACK_DISTANCE = 40;
        private const double AGRO_DISTANCE = 200;

        public int HP { get; set; }

        private int _attackCounter = 0;
        private AttackStance _attackStance = AttackStance.NotAttacking;
        private Vector2 _attackHeight = Vector2.Zero;    
        private double _attackAngle = 0;
        private double _randomModifier;
        private float _attackSpeed = 5.4f;
        private float _moveSpeed = 1.8f;

        public Bat(float x, float y)
            :base(x, y)
        {
            this.HP = 200;
            this._randomModifier = randomGenerator.NextDouble();
            this.Visible = true;
        }

        public override void Update(GameTime gameTime, TeeEngine engine)
        {
            // Get the Hero player for interaction purposes.
            Hero player = (Hero)engine.GetEntity("Player");
            Vector2 prevPos = Pos;

            // Check if this Bat has died.
            if (HP <= 0)
            {
                this.Opacity -= 0.02f;
                this.Drawables.ResetState(CurrentDrawableState, gameTime);
                if (this.Opacity < 0) 
                    engine.RemoveEntity(this);
            }
            else
            {
                // ATTACKING LOGIC.
                if (_attackStance == AttackStance.Attacking)
                {
                    this.Pos.X -= (float) (Math.Cos(_attackAngle) * _attackSpeed);
                    this.Pos.Y -= (float) (Math.Sin(_attackAngle) * _attackSpeed);
                    this._attackHeight.Y += 30.0f / ATTACK_COUNTER_LIMIT;
                    this.Drawables.SetGroupProperty("Body", "Offset", _attackHeight);

                    if (Entity.IntersectsWith(this, "Shadow", player, "Shadow", gameTime))
                        player.HP -= 3;

                    if (_attackCounter++ == ATTACK_COUNTER_LIMIT) 
                        _attackStance = AttackStance.NotAttacking;
                }
                // ATTACK PREPERATION LOGIC.
                else if (_attackStance == AttackStance.Preparing)
                {
                    _attackHeight.Y -= 2;

                    if (_attackHeight.Y < -40)
                    {
                        _attackHeight.Y = -40;
                        _attackAngle = Math.Atan2(
                            this.Pos.Y - player.Pos.Y,
                            this.Pos.X - player.Pos.X
                            );
                        _attackStance = AttackStance.Attacking;
                        _attackCounter = 0;
                    }

                    Drawables.SetGroupProperty("Body", "Offset", _attackHeight);
                }
                // NON-ATTACKING LOGIC. PATROL AND APPROACH.
                else if (_attackStance == AttackStance.NotAttacking)
                {
                    double distance = Vector2.Distance(player.Pos, this.Pos);

                    if (distance < AGRO_DISTANCE)
                    {
                        // Move towards the player for an attack move.
                        double angle = Math.Atan2(
                            player.Pos.Y - this.Pos.Y,
                            player.Pos.X - this.Pos.X
                            );

                        // Approach Function.
                        double moveValue;
                        if (distance < ATTACK_DISTANCE)
                        {
                            _attackStance = AttackStance.Preparing;
                            moveValue = 0;
                        }
                        else
                            moveValue = _moveSpeed;

                        Pos.X += (float)(Math.Cos(angle) * moveValue);
                        Pos.Y += (float)(Math.Sin(angle) * moveValue);
                    }
                    else
                    {
                        // Perform a standard patrol action.
                        Pos.X += (float)(Math.Cos(gameTime.TotalGameTime.TotalSeconds - _randomModifier * 90) * 2);
                    }
                }

                // Determine the animation based on the change in position.
                if (Math.Abs(prevPos.X - Pos.X) > Math.Abs(prevPos.Y - Pos.Y))
                {
                    if (prevPos.X < Pos.X)
                        this.CurrentDrawableState = "Right";
                    if (prevPos.X > Pos.X)
                        this.CurrentDrawableState = "Left";
                }
                else
                {
                    if (prevPos.Y < Pos.Y)
                        this.CurrentDrawableState = "Down";
                    if (prevPos.Y > Pos.Y)
                        this.CurrentDrawableState = "Up";
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            double startTimeMS = randomGenerator.NextDouble() * 4000;

            DrawableSet.LoadDrawableSetXml(
                Drawables, 
                "Animations/Monsters/bat.anim", 
                content, startTimeMS
                );

            CurrentDrawableState = "Left";
        }
    }
}
