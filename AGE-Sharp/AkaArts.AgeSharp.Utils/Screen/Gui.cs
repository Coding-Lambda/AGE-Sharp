﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AkaArts.AgeSharp.Utils.Screen
{
    public abstract class Gui
    {

        private List<IGuiEntity> GuiEntities = new List<IGuiEntity>();
        private List<IGuiEntity> GuiEntitiesToUpdate = new List<IGuiEntity>();

        private bool IsLoaded = false;

        public bool IsActive = true;

        private float opacity;

        public float Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                this.opacity = MathHelper.Clamp(value, 0, 1);
            } 
        }

        public Gui()
        {
            this.Opacity = 1;
        }

        abstract internal void Initialize();

        internal void LoadContent()
        {

            foreach(IGuiEntity entity in this.GuiEntities){

                entity.LoadContent();

            }

            IsLoaded = true;

        }

        internal void Update(GameTime time)
        {

            GuiEntitiesToUpdate.Clear();

            foreach (IGuiEntity entity in this.GuiEntities)
            {
                GuiEntitiesToUpdate.Add(entity);
            }

            foreach (IGuiEntity entity in GuiEntitiesToUpdate)
            {

                entity.Update(time);

            }

        }

        internal void Draw(SpriteBatch batch)
        {

            foreach (IGuiEntity entity in this.GuiEntities)
            {

                entity.Draw(batch);

            }

        }

        internal void UnloadContent()
        {

            foreach (IGuiEntity entity in this.GuiEntities)
            {

                entity.UnloadContent();

            }

        }

        public void AddGuiEntity(IGuiEntity entity)
        {

            if (this.GuiEntities.Contains(entity))
            {
                return;
            }

            this.GuiEntities.Add(entity);

            if (this.IsLoaded)
            {
                entity.LoadContent();
            }
            
        }

        public void RemoveGuiEntity(IGuiEntity entity)
        {

            this.GuiEntities.Remove(entity);

            entity.UnloadContent();

        }

    }
}