﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit;
namespace Project
{
    using SharpDX.Toolkit.Graphics;
    public class Assets
    {
        LabGame game;

        public Assets(LabGame game)
        {
            this.game = game;
        }

        // Dictionary of currently loaded models.
        // New/existing models are loaded by calling GetModel(modelName, modelMaker).
        public Dictionary<String, MyModel> modelDict = new Dictionary<String, MyModel>();

        // Load a model from the model dictionary.
        // If the model name hasn't been loaded before then modelMaker will be called to generate the model.
        public delegate MyModel ModelMaker();

		/// <summary>
		/// Get a model by name.
		/// </summary>
		/// <param name="modelName">Name of model.</param>
		/// <param name="modelMaker">Maker class used to make model.</param>
		/// <returns>A model.</returns>
        public MyModel GetModel(String modelName, ModelMaker modelMaker)
        {
            if (!modelDict.ContainsKey(modelName))
            {
                modelDict[modelName] = modelMaker();
            }
            return modelDict[modelName];
        }

		/// <summary>
		/// Create a cube with one texture for all faces.
		/// </summary>
		/// <param name="textureName">Texture file to use.</param>
		/// <param name="size">Size of cube.</param>
		/// <returns>A textured cube.</returns>
        public MyModel CreateTexturedCube(String textureName, float size)
        {
            return CreateTexturedCube(textureName, new Vector3(size, size, size));
        }

		/// <summary>
		/// Create a cube with one texture for all faces.
		/// </summary>
		/// <param name="texturePath">Texture file to use.</param>
		/// <param name="size">Size of cube.</param>
		/// <returns>A textured cube.</returns>
        public MyModel CreateTexturedCube(String texturePath, Vector3 size)
        {
            VertexPositionTexture[] shapeArray = new VertexPositionTexture[]{
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(0.0f, 1.0f)), // Front
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, -1.0f), new Vector2(1.0f, 1.0f)),

            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, 1.0f), new Vector2(1.0f, 1.0f)), // BACK
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, 1.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, 1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(0.0f, 0.0f)),

            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Vector2(0.0f, 1.0f)), // Top
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, -1.0f), new Vector2(1.0f, 1.0f)),

            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(0.0f, 0.0f)), // Bottom
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, 1.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, 1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, 1.0f), new Vector2(1.0f, 1.0f)),

            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(1.0f, 1.0f)), // Left
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, 1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, -1.0f, -1.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, 1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-1.0f, 1.0f, -1.0f), new Vector2(1.0f, 0.0f)),

            new VertexPositionTexture(new Vector3(1.0f, -1.0f, -1.0f), new Vector2(0.0f, 1.0f)), // Right
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, 1.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, -1.0f, -1.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, -1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(1.0f, 1.0f, 1.0f), new Vector2(1.0f, 0.0f)),
            };

            for (int i = 0; i < shapeArray.Length; i++)
            {
                shapeArray[i].Position.X *= size.X / 2;
                shapeArray[i].Position.Y *= size.Y / 2;
                shapeArray[i].Position.Z *= size.Z / 2;
            }

            float collisionRadius = (size.X + size.Y + size.Z) / 6 ;
            return new MyModel(game, shapeArray, texturePath, collisionRadius);
        }

		/// <summary>
		/// Create a flat world base to use.
		/// </summary>
		/// <param name="size">Size of world.</param>
		/// <returns>A world model.</returns>
        public MyModel CreateWorldBase(int size)
        {
            
            float collisionRadius = 1;
            int sidelength = (int)Math.Pow(2, size);
            int min = -sidelength / 2;
            int k = 0;
			// Data structures for generating vertice heightmap
			Vector3[][] points = new Vector3[sidelength][];
			Vector3[][] normals = new Vector3[sidelength][];
			for (int i = 0; i < sidelength; i++) {
				points[i] = new Vector3[sidelength];
				normals[i] = new Vector3[sidelength];
			}

			// TO-DO: generate a two dimensional array of vertices
			for (int i = 0; i < sidelength; i++) {
				for (int j = 0; j < sidelength; j++) {
					points[i][j] = new Vector3(i + min, j + min, 0);
					normals[i][j] = new Vector3(0, 1, 0);
				}
			}

			// TO-DO: Apply diamond square algorithm


			// TO-DO: calculate vertex normals


			// TO-DO: Refactor shapeArray code slightly to reference points[][] and normals[][] directly
            VertexPositionNormalColor[] shapeArray = new VertexPositionNormalColor[sidelength * sidelength*6];

            for (int i = 0; i < sidelength; i++)
            {
                for (int j = 0; j < sidelength; j++)
                {
                    //Each step creates a square in the map mesh
                    //Bottom triangle
                    shapeArray[k] = new VertexPositionNormalColor(new Vector3(i + min, j + min, 0), new Vector3(0, 0, 1), Color.SandyBrown);
                    shapeArray[k + 1] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min + 1, 0), new Vector3(0, 0, 1), Color.SandyBrown);
                    shapeArray[k + 2] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min, 0), new Vector3(0, 0, 1), Color.SandyBrown);

                    //Top Triangle
                    shapeArray[k + 3] = new VertexPositionNormalColor(new Vector3(i + min, j + min, 0), new Vector3(0, 0, 1), Color.SandyBrown);
                    shapeArray[k + 4] = new VertexPositionNormalColor(new Vector3(i + min, j + min + 1, 0), new Vector3(0, 0, 1), Color.SandyBrown);
                    shapeArray[k + 5] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min + 1, 0), new Vector3(0, 0, 1), Color.SandyBrown);

                    k += 6;
                }
            }


            return new MyModel(game, shapeArray, collisionRadius);
        }

		/// <summary>
		/// Create a new ocean.
		/// </summary>
		/// <param name="size">Size of ocean</param>
		/// <param name="seaLevel">Height of ocean.</param>
		/// <returns>An ocean model.</returns>
        public MyModel CreateOcean(int size, int seaLevel)
        {
            float collisionRadius = 1;
            int sidelength = (int)Math.Pow(2, size);
            int min = -sidelength / 2;
            int k = 0;

            VertexPositionNormalColor[] shapeArray = new VertexPositionNormalColor[sidelength * sidelength * 6];

            for (int i = 0; i < sidelength; i++)
            {
                for (int j = 0; j < sidelength; j++)
                {
                    //Each step creates a square in the map mesh
                    //Bottom triangle
                    shapeArray[k] = new VertexPositionNormalColor(new Vector3(i + min, j + min, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);
                    shapeArray[k + 1] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min + 1, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);
                    shapeArray[k + 2] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);

                    //Top Triangle
                    shapeArray[k + 3] = new VertexPositionNormalColor(new Vector3(i + min, j + min, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);
                    shapeArray[k + 4] = new VertexPositionNormalColor(new Vector3(i + min, j + min + 1, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);
                    shapeArray[k + 5] = new VertexPositionNormalColor(new Vector3(i + min + 1, j + min + 1, -seaLevel), new Vector3(0, 0, 1), Color.SeaGreen);

                    k += 6;
                }
            }

            return new MyModel(game, shapeArray, collisionRadius);
        }

		/// <summary>
		/// Create a new ship model.
		/// </summary>
		/// <param name="texturePath">Texture file to use.</param>
		/// <returns>A ship model.</returns>
        public MyModel CreateShip(String texturePath)
        {
            return CreateTexturedCube("player.png",1);
        }

		/// <summary>
		/// Create a new cannonball model.
		/// </summary>
		/// <returns>A cannonball model.</returns>
        public MyModel CreateCannonBall()
        {
            return CreateTexturedCube("player_projectile.png",1);
        }

		/// <summary>
		/// Create a power-up model.
		/// </summary>
		/// <param name="texturePath">Texture file to use.</param>
		/// <returns>A power-up model.</returns>
        public MyModel CreatePowerup(String texturePath)
        {
            return CreateTexturedCube(texturePath, 1);
        }

    }
}
