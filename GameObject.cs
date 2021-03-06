﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using Windows.UI.Input;
using Windows.UI.Core;

namespace Project
{
    using SharpDX.Toolkit.Graphics;

    //All the enemy types
    public enum GameObjectType
    {
        None, Player, Enemy, Ocean, Terrain
    }

    // Super class for all game objects.
    abstract public class GameObject
    {
        public MyModel myModel;                             //Renderable model if applicable
        public LabGame game;                                //Master game variable
        public GameObjectType type = GameObjectType.None;   //Objecttype descriptor defaults to none
        public Vector3 pos;                                 //Position of object in the world if applicable
        public Matrix World;                                //World for objects rendered with custom shader
        public Matrix WorldInverseTranspose;                //WorldInverseTranspose for objects rendered with custom shader
        public Effect effect;                               //Custom shader if applicable
        public BasicEffect basicEffect;                     //Basiceffect shader (Used for some objects)

        //Update method required to be implemented by all subclasses
        public abstract void Update(GameTime gametime);

        //The almighty draw method, all hail
        public void Draw(GameTime gametime)
        {
            // Some objects such as the Enemy Controller have no model and thus will not be drawn
            if (myModel != null)
            {
                //If the model is an externally loaded model
                if (myModel.wasLoaded)
                {
                    //Coloured models use the custom shader
                    if (myModel.modelType == ModelType.Colored)
                    {
                        this.effect.Parameters["View"].SetValue(game.camera.View);
                        this.effect.Parameters["Projection"].SetValue(game.camera.Projection);
                        this.effect.Parameters["World"].SetValue(Matrix.Identity);
                        this.effect.Parameters["cameraPos"].SetValue(game.camera.pos);
                        this.effect.Parameters["worldInvTrp"].SetValue(WorldInverseTranspose);
                        game.lighting.SetLighting(this.effect);
                        myModel.model.Draw(game.GraphicsDevice,
                            Matrix.Identity, game.camera.View, game.camera.Projection);
                    }

                    //Textured models use basiceffect
                    if (myModel.modelType == ModelType.Textured)
                    {
                        this.basicEffect.World = Matrix.Identity;
                        this.basicEffect.Projection = game.camera.Projection;
                        this.basicEffect.View = game.camera.View;
                        myModel.model.Draw(game.GraphicsDevice,
                            basicEffect.World, basicEffect.View, basicEffect.Projection);
                    }
                    
                }

                //If the model was internally constructed
                else
                {
                    //Coloured models use the custom shader
                    if (myModel.modelType == ModelType.Colored)
                    {
                        game.lighting.SetLighting(this.effect);
                        this.effect.Parameters["World"].SetValue(Matrix.Identity);
                        this.effect.Parameters["View"].SetValue(game.camera.View);
                        this.effect.Parameters["Projection"].SetValue(game.camera.Projection);
                        this.effect.Parameters["cameraPos"].SetValue(game.camera.pos);
                        this.effect.Parameters["worldInvTrp"].SetValue(WorldInverseTranspose);
                    }
                    //Textured models use basiceffect
                    else
                    {
                        this.basicEffect.LightingEnabled = true;

                        basicEffect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                        basicEffect.DirectionalLight0.Enabled = true;
                        basicEffect.DirectionalLight0.DiffuseColor = new Vector3(0.6f, 0.6f, 0.6f);
                        basicEffect.DirectionalLight0.Direction = new Vector3(0, 0, 1f);
                        basicEffect.DirectionalLight0.SpecularColor = new Vector3(0.1f, 0.1f, 0.166f);

                        basicEffect.View = game.camera.View;
                        basicEffect.Projection = game.camera.Projection;
                    }

                    // Setup the vertices
                    game.GraphicsDevice.SetVertexBuffer(0, myModel.vertices, myModel.vertexStride);
                    game.GraphicsDevice.SetVertexInputLayout(myModel.inputLayout);

                    // Apply the custom effect technique and draw the object
                    if (myModel.modelType == ModelType.Colored)
                    {
                        effect.CurrentTechnique.Passes[0].Apply();
                    }
                    //Apple the basiceffect technique and draw the object
                    else
                    {
                        basicEffect.CurrentTechnique.Passes[0].Apply();
                    }
                    game.GraphicsDevice.Draw(PrimitiveType.TriangleList, myModel.vertices.ElementCount);
                }
            }
        }

        //Get relevant parameters from the model at initialisation
        public void GetParamsFromModel()
        {
            //Custom effect parameters for coloured models
            if (myModel.modelType == ModelType.Colored) {
                effect = game.Content.Load<Effect>("MultiPoint");
                this.effect.Parameters["View"].SetValue(game.camera.View);
                this.effect.Parameters["Projection"].SetValue(game.camera.Projection);
                this.effect.Parameters["World"].SetValue(Matrix.Identity);

            }
            //BasicEffect parameters for textured models
            else if (myModel.modelType == ModelType.Textured) {
                basicEffect = new BasicEffect(game.GraphicsDevice)
                {
                    View = game.camera.View,
                    Projection = game.camera.Projection,
                    World = Matrix.Identity,
                    Texture = myModel.Texture,
                    TextureEnabled = true,
                    VertexColorEnabled = false
                };
            }
        }

        /*
         * Explanation as to why some objects use one shader and others use basiceffect.
         * Because of the bright lights caused by exploding enemies when they die we opted
         * to have players and enemies shaded by basiceffect, this allows them to be clearly
         * seen over the top of the explosion without the player becoming disoriented.
         */

        // These virtual voids allow any object that extends GameObject to respond to tapped and manipulation events
        public virtual void Tapped(GestureRecognizer sender, TappedEventArgs args)
        {

        }

        public virtual void OnManipulationStarted(GestureRecognizer sender, ManipulationStartedEventArgs args)
        {

        }

        public virtual void OnManipulationUpdated(GestureRecognizer sender, ManipulationUpdatedEventArgs args)
        {

        }

        public virtual void OnManipulationCompleted(GestureRecognizer sender, ManipulationCompletedEventArgs args)
        {

        }
    }
}
