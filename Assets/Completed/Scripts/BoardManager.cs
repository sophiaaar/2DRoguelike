﻿using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed
	
{
	
	public class BoardManager : MonoBehaviour
	{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count
		{
			public int minimum; 			//Minimum value for our Count class.
			public int maximum; 			//Maximum value for our Count class.
			
			
			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}

        public VariableManager varMan;
		public int columns = 8; 										//Number of columns in our game board.
		public int rows = 8;											//Number of rows in our game board.
		public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
		public Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
        public Count crateCount = new Count(1, 2);                      //Lower and upper limit for our random number of reawrd crates per level. [EDITED BY SOPHIA FOR TUTORIAL PURPOSES]
        public GameObject exit;											//Prefab to spawn for exit.
        public GameObject xmasExit;                                     //Prefab to spawn for exit.
        public GameObject[] floorTiles;									//Array of floor prefabs.
		public GameObject[] wallTiles;                                  //Array of wall prefabs.
        public GameObject[] floorXmasTiles;                             //Array of floor prefabs.
        public GameObject[] wallXmasTiles;                              //Array of wall prefabs.
        public GameObject[] foodTiles;									//Array of food prefabs.
        public GameObject[] rewardTiles;                                //Array of reward crate prefabs.
        public GameObject[] foodXmasTiles;							    //Array of food prefabs.
        public GameObject[] enemyTiles;                                 //Array of enemy prefabs.
        public GameObject[] enemyXmasTiles;                             //Array of Xmas enemy prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
        public GameObject[] outerXmasWallTiles;                         //Array of outer tile prefabs.

        private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();   //A list of possible locations to place tiles.
		
		//Clears our list gridPositions and prepares it to generate a new board.
		void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 1; x < columns-1; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 1; y < rows-1; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;
			
			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = -1; x < columns + 1; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = -1; y < rows + 1; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					
					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(x == -1 || x == columns || y == -1 || y == rows)
						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance =
						Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}

        void FestiveBoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = -1; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = -1; y < rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorXmasTiles[Random.Range(0, floorXmasTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = outerXmasWallTiles[Random.Range(0, outerXmasWallTiles.Length)];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition ()
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}
		
		
		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
		{
			//Choose a random number of objects to instantiate within the minimum and maximum limits
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}
		
		
		//SetupScene initializes our level and calls the previous functions to lay out the game board
		public void SetupScene (int level)
		{
            if (varMan == null)
            {
                varMan = GameObject.FindGameObjectWithTag("VariableManager").GetComponent<VariableManager>();
            }

            //Creates the (X) outer walls and floor where X is the current theme being used.

            if (varMan.fesiveSkin)
            {
                FestiveBoardSetup();
            }
            else
                BoardSetup ();
			
			//Reset our list of gridpositions.
			InitialiseList ();

            //Instantiate a random number of (X) wall tiles based on minimum and maximum, at randomized positions where X is the current theme being used.

            if (varMan.fesiveSkin)
            {
                LayoutObjectAtRandom(wallXmasTiles, wallCount.minimum, wallCount.maximum);
            }
                else
                    LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
            //Instantiate a random number of (X) food tiles based on minimum and maximum, at randomized positions where X is the current theme being used.
            if (varMan.fesiveSkin)
            {
                LayoutObjectAtRandom(foodXmasTiles, foodCount.minimum, foodCount.maximum);
            }
            else
                LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);

            //Instantiate a random number of reward crate tiles based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom(rewardTiles, 1, 5);

            //Determine number of enemies based on current level number, based on a logarithmic progression
            int enemyCount = (int)Mathf.Log(level, 2f);

            //Instantiate a random number of (X) enemies based on minimum and maximum, at randomized positions where X is the current theme being used.
            if (varMan.fesiveSkin)
            {
                LayoutObjectAtRandom(enemyXmasTiles, enemyCount, enemyCount);
            }
            else
                LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);

            //Instantiate the (X) exit tile in the upper right hand corner of our game board where X is the current theme being used.
            if (varMan.fesiveSkin)
            {
                Instantiate(xmasExit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
            }
            else
                Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        }
	}
}