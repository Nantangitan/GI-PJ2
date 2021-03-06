﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Toolkit;

namespace Project
{

    //Class representing the islands and the undersea terrain
    public class Land : GameObject
    {
        public Vector3[][] heights;

        public Land(LabGame game, int degree)
        {
            this.game = game;
            this.pos = new Vector3(0, 0, 0);        //The world is centred at 0,0,0
            type = GameObjectType.Terrain;          //Terrain is terrain
            myModel = game.assets.CreateWorldBase(degree);  //Generate a terrain model
            GetParamsFromModel();
            
        }

        public override void Update(GameTime gametime)
        {
            //Terrain never changes
        }

		// Check if a point collides with the land
		public bool isColidingTerrain(Vector3 pt, float collisionRadius) {
			// Check if this point is colliding with any point in the terrain.
			Vector3[][] map = this.myModel.modelMap;
			Vector2 directionP = new Vector2(0.0f,0.0f), directionM = new Vector2(0.0f,0.0f);
			directionP.X = pt.X;
			directionP.Y = pt.Y;
			for (int i = 0; i < map.Length; i++) {
					for (int j = 0; j < map[i].Length; j++) { 
					directionM.X = map[i][j].X;
					directionM.Y = map[i][j].Y;
					// Calculate distance and return true if within collision radius
					if (Vector3.Distance(map[i][j], pt) <= collisionRadius || 
						(Vector2.Distance(directionP, directionM) < collisionRadius && pt.Z > map[i][j].Z)) {
						return true;
					}
				}
			}
			return false;
		}

		// Check if a point collides with the land
		public bool isColidingEdge(Vector3 pt, float collisionRadius) {
			// Stay within boudaries
			if (pt.X <= -game.edgemax || pt.X >= game.edgemax) {
				return true;
			}
			if (pt.Y <= -game.edgemax || pt.Y >= game.edgemax) {
				return true;
			}
			return false;
		}

		// Check if a point collides with the land
		public bool isColiding(Vector3 pt, float collisionRadius) {
			return isColidingEdge(pt, collisionRadius) || isColidingTerrain(pt, collisionRadius);
		}
    }
}
