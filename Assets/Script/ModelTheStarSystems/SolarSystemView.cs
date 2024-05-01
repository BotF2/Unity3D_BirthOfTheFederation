using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using GalaxyMap;
using System.Linq;
using System.Net;

namespace Assets.Core
{
    public class SolarSystemView : MonoBehaviour 
    {
        private   GameManager gameManager;
        public GameObject emptyGO;
        public SolarSystem solarSystem;
        public Sprite[] starSprites; 
        public Sprite[] planetMoonSprites;
        public Sprite[] solSprites;
        public Sprite earthMoonSprite;
        private string[] systemDataArray;
        public ulong zoomLevels = 150000000000; // times 1 billion zoom
        float planetMoonScale = 0.25f;
        //Galaxy ourGalaxy;
        public float galacticTime = 0;
        private int deltaTime =1000;
        private int earthSpriteCounter = 0;
        Dictionary<OrbitalGalactic, GameObject> orbitalGameObjectMap; // put in the orbital sprit and get the game object
        public static Dictionary<int, string[]> systemDataDictionary = new Dictionary<int, string[]>();

        void Start()
        {
            gameManager = GameManager.Instance;
            systemDataDictionary = GalaxyView.SystemDataDictionary; // this really works
        }
        void Update()
        {
            galacticTime = galacticTime + deltaTime;
            if (solarSystem == null)
                return;
            else
            {
                OrbitalGalactic sunOrbitalGalactic = solarSystem.Children[0];

                for (int i = 0; i < sunOrbitalGalactic.Children.Count; i++)
                {
                    sunOrbitalGalactic.Children[i].Update(galacticTime); // update offset angle for orbital
                    UpdateSprites(sunOrbitalGalactic.Children[i]); // UpdateSprites looks a moon in children of the orbital sent to it
                }
            }

        }

        public void TurnOffSolarSystemview(Galaxy galaxy, int solarSystemID)
        {
            //ourGalaxy = galaxy;
            while (transform.childCount > 0) // delelt old systems from prior update
            {
                Transform child = transform.GetChild(0);
                child.SetParent(null); // decreases number of children in while loop
                Destroy(child.gameObject);
            }
            solarSystem = null;
        }
        //public void ShowSolarSystemView(Galaxy galaxy, int solarSystemID) // called from gameManager with the input galaxy
        //{
        //    while (transform.childCount > 0) // delelt old systems from prior update
        //    {
        //        Transform child = transform.GetChild(0);
        //        child.SetParent(null); // decreases number of children in while loop
        //        Destroy(child.gameObject);
        //    }
        //    //orbitalGameObjectMap = new Dictionary<OrbitalGalactic, GameObject>();
        //    //solarSystem = SolarSystems[0];
        //    solarSystem = ourGalaxy.SolarSystems[solarSystemID];
        //    for (int i = 0; i < solarSystem.Children.Count; i++)
        //    {
        //        this.MakeSpritesForOrbital(this.transform, solarSystem.Children[i]);
        //    }
        //}
        public void ShowNextSolarSystemView(int buttonSystemID)
        {
            while (transform.childCount > 0) // transform is the SSView child of the solar system button, delelt old systems from prior update
            {
                Transform child = transform.GetChild(0);
                child.SetParent(null); // decreases number of children in while loop
                Destroy(child.gameObject);
            }
            gameManager.ChangeSystemClicked(buttonSystemID, this);
            gameManager.SwitchtState(GameManager.State.GALACTIC_MAP_INIT, 0);
            systemDataArray = systemDataDictionary[buttonSystemID];
            var mySolarSystem = new SolarSystem();
            mySolarSystem.LoadSystem(systemDataArray);
            solarSystem = mySolarSystem;

            for (int i = 0; i < solarSystem.Children.Count; i++)
            {
                this.LoadSpritesForOrbital(this.transform, solarSystem.Children[i]);
            }

        }
        private void LoadSpritesForOrbital(Transform transformParent, OrbitalGalactic orbitalG)
        {       
           
            if (orbitalGameObjectMap == null)
                orbitalGameObjectMap = new Dictionary<OrbitalGalactic, GameObject>() { { orbitalG, emptyGO } };
            else
                orbitalGameObjectMap.Add(orbitalG, emptyGO);
            emptyGO.transform.SetParent(transformParent, false);           
            emptyGO.transform.position = orbitalG.Position / zoomLevels; 
            emptyGO.layer = 3; // Star System layer
            emptyGO.name = "Orbital";
            SpriteRenderer renderer = emptyGO.AddComponent<SpriteRenderer>();
            renderer.transform.localScale = new Vector3(planetMoonScale, planetMoonScale, planetMoonScale);
            if (int.Parse(systemDataArray[0]) == 0)
                this.LoadEarthSprites(transformParent, orbitalG, renderer);
            else
            {
                switch (orbitalG.GraphicID)
                {
                    case 0:
                        string starColor = systemDataArray[7];
                        gameObject.name = "Star";
                        switch (starColor)
                        {
                            case "Blue":
                                renderer.sprite = starSprites[0];
                                break;
                            case "Orange":
                                renderer.sprite = starSprites[1];
                                break;
                            case "Red":
                                renderer.sprite = starSprites[2];
                                break;
                            case "White":
                                renderer.sprite = starSprites[3];
                                break;
                            case "Yellow":
                                renderer.sprite = starSprites[4];
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1 + (int)PlanetType.H_uninhabitable:
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(0, 4)];
                        break;
                    case 1 + (int)PlanetType.J_gasGiant:
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(5, 10)];
                        break;
                    case 1 + (int)PlanetType.M_habitable:
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(11, 16)];
                        break;
                    case 1 + (int)PlanetType.L_marginalForLife:
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(17, 22)];
                        break;
                    case 1 + (int)PlanetType.K_marsLike:
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(23, 28)];
                        break;
                    case 1 + (int)PlanetType.Moon: // Our moons are only defined as orbitalgalactic and not as planet so do not really have a planet type
                        renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(29, 34)];
                        break;
                    default:
                        break;                    
                }
            }

            for (int i = 0; i < orbitalG.Children.Count; i++)
            {
                LoadSpritesForOrbital(gameObject.transform, orbitalG.Children[i]);
            } 
            
        }
        private void LoadEarthSprites(Transform transformParent, OrbitalGalactic orbitalG, SpriteRenderer renderer)
        {
            if (orbitalG.GraphicID == 1 + (int)PlanetType.Moon)
            {
                if (orbitalG.Parent.GraphicID == 7)
                {
                    renderer.sprite = earthMoonSprite;
                }
                
                else   
                renderer.sprite = planetMoonSprites[UnityEngine.Random.Range(29, 34)];
            }
            else
            {
                switch (earthSpriteCounter)
                {
                    case 0:
                        renderer.sprite = starSprites[4];
                        //renderer.transform.localPosition = Vector3.zero;
                        earthSpriteCounter++;
                        break;
                    case 1:
                        renderer.sprite = solSprites[0];
                        earthSpriteCounter++;
                        break;
                    case 2:
                        renderer.sprite = solSprites[1];
                        earthSpriteCounter++;
                        break;
                    case 3:
                        renderer.sprite = solSprites[2];
                        orbitalG.GraphicID = 7;
                        //GameObject gameObject = new GameObject();
                        ////if (orbitalGameObjectMap == null)
                        ////    orbitalGameObjectMap = new Dictionary<OrbitalGalactic, GameObject>() { { orbitalG, gameObject } };
                        ////else
                        //    //orbitalGameObjectMap.Add(orbitalG, gameObject);
                        //gameObject.transform.SetParent(transformParent, false);
                        //gameObject.transform.Position = orbitalG.Position / zoomLevels;
                        //gameObject.layer = 5; // UI
                        //gameObject.Name = "Orbital";
                        //// orbitalGameObjectMap.Add(orbitalG, gameObject);

                        //SpriteRenderer moonRenderer = gameObject.AddComponent<SpriteRenderer>();
                        //moonRenderer.transform.localScale = new Vector3(planetMoonScale, planetMoonScale, planetMoonScale);
                        //OrbitalGalactic moon = new OrbitalGalactic();
                        //orbitalG.AddChild(moon);
                        //moon.OrbitalDistance = 500000000;
                        //moon.TimeToOrbit = moon.OrbitTimeForDistance() / 10;
                        //moonRenderer.sprite = earthMoonSprite;
                        earthSpriteCounter++;
                        break;
                    case 4:
                        renderer.sprite = solSprites[3];
                        earthSpriteCounter++;
                        break;
                    case 5:
                        renderer.sprite = solSprites[4];
                        earthSpriteCounter++;
                        break;
                    case 6:
                        renderer.sprite = solSprites[5];
                        earthSpriteCounter++;
                        break;
                    case 7:
                        renderer.sprite = solSprites[6];
                        earthSpriteCounter++;
                        break;
                    case 8:
                        renderer.sprite = solSprites[7];
                        earthSpriteCounter = 0;
                        break;
                    case 9:
                        earthSpriteCounter = 0;
                        break;
                    default:
                        break;
                }
            }
        }
        private void MakeSpritesForOrbital(Transform transformParent, OrbitalGalactic orbitalG)
        {
            //CameraManagerGalactica cameraManagerGalactic = new CameraManagerGalactica();
            GameObject gameObject = new GameObject();
            //orbitalGameObjectMap[orbitalG] = gameObject; // update map
            //gameObject.layer = 30; // galactic
            gameObject.transform.SetParent(transformParent, false);
            // set Position in 3D
            gameObject.transform.position = orbitalG.Position / zoomLevels; // cut down scale of system to view
            SpriteRenderer spritView = gameObject.AddComponent<SpriteRenderer>();
            spritView.transform.localScale = new Vector3(planetMoonScale, planetMoonScale, planetMoonScale);
            spritView.sprite = planetMoonSprites[orbitalG.GraphicID];
            orbitalGameObjectMap.Add(orbitalG, gameObject);

            for (int i = 0; i < orbitalG.Children.Count; i++)
            {
                MakeSpritesForOrbital(gameObject.transform, orbitalG.Children[i]);
                //spritView.transform.LookAt();
            }
        }
        void UpdateSprites(OrbitalGalactic orbital) //, float time)
        {
            GameObject gameObject = orbitalGameObjectMap[orbital];
            gameObject.transform.position = orbital.Position / zoomLevels;
            for (int i = 0; i < orbital.Children.Count; i++)
            {
                UpdateSprites(orbital.Children[i]);
            }
        }
        public void SetZoomLevel(ulong zl)
        {
            zoomLevels = zl;
            //Update planet postions and scale graphics to still see planet sprites as a few pix
        }
    }
}
