using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine.UI;
using System.Linq;

namespace Assets.Core
{
    public class Ship : MonoBehaviour
    {

        public Combat combat;
        public CivEnum _civEnum;
        public ShipType _shipType;
        public TechLevel _techLevel;
        public int _shieldsMaxHealth; // set in ShipData.txt
        public int _hullMaxHealth;
        public int _torpedoDamage; // update with data of torpedo that hits
        public int _beamDamage;
        public int _cost;
        public Rigidbody _rigidbody;
        private bool _isFriend;
        private bool _notInThisFight;
        private new Rigidbody rigidbody;
        //private GameObject shipGameObject;
        private Transform _farTarget;
        private Transform _nearTarget;
        private Transform _currentTarget;
        private bool isFarTargetSet = false;
       // public float combatAreaOffset = -5000f;
        private float turnRate = 1f;
        private bool lockTurn = true;
        private int _shieldsCurrentHealth;
        private float _shieldsRegeneratRate = 4f; // of Shields
        private float _speedBooster = 15000f;
       // public bool allStop = false;
        private int _sheildsRegenerateAmount = 1;
        private GameObject _shields;
        public bool shieldsAreUp;
        // public Image _hullHealthImage;
        public GameObject _warpCoreBreach;
        public GameObject _ringExplosion;
        private bool _isTorpedo;
        private Transform beamTargetTransform; // Torpedo targeting is in PhotonTorpedo.cs as the torpedo moves

        private List<GameObject> theLocalTargetList;
        private float diff = 0;
        private Vector3[] linePositions = new Vector3[2]; // beam line render points in update
        //private int _torpedoWarhead;
        //private int _beamPower;
        public int _layer;
        public GameObject torpedoPrefab; // set in prefab of ships code in inspector
        public GameObject beamPrefab;
        private GameObject beamObject;
        public GameObject shieldPrefab;
        public GameObject explosionPrefab;

        private AudioSource theSource;
        private AudioSource theNextSource;
        public AudioClip clipTorpedoFire;
        public AudioClip clipExplodTorpedo;
        public AudioClip clipBeamWeapon;
        public AudioClip clipWarpCoreBreach;

        // private Renderer rend; // not working 

        // public Material _hitMaterial;
        //List<Design> shipDesign = new List<Design>();
        //Material _orgMaterial;
        //Renderer _renderer;


        private void Awake()
        {
            //_rigidbody = this.GetComponent<Rigidbody>(); // Do we need to get rigibody or is it just part of prefab ship?
            //var rigidbody = _rigibody.GetComponent<Rigidbody>();
            string[] nameArray = new string[3] { "civilization", "shipType", "era" };
            if (this.name != "Ship")
                {
                    nameArray = this.name.Split('_');              
                }
            string typeOfShip = nameArray[1];

            switch (typeOfShip.ToUpper())
            {
                case "SCOUT":
                    _shipType = ShipType.Scout;
                    break;
                case "DESTROYER":
                    _shipType = ShipType.Destroyer;
                    break;
                case "CURISER":
                    _shipType = ShipType.Cruiser;
                    break;
                case "LTCURISER":
                    _shipType = ShipType.LtCruiser;
                    break;
                case "HVYCURISER":
                    _shipType = ShipType.HvyCruiser;
                    break;
                case "TRANSPORT":
                    _shipType = ShipType.Transport;
                    break;
                case "COLONYSHIP":
                    _shipType = ShipType.Colonyship;
                    break;
                case "ONEMORE":
                    _shipType = ShipType.OneMore;
                    break;
                default:
                    break;
            }
            string civ = nameArray[0];
            //switch (civ.ToUpper())
            //{
            //    case "FED":
            //        _civilization = Civilization.FED;
            //        break;
            //    case "TERRAN":
            //        _civilization = Civilization.TERRAN;
            //        break;
            //    case "ROM":
            //        _civilization = Civilization.ROM;
            //        break;
            //    case "KLING":
            //        _civilization = Civilization.KLING;
            //        break;
            //    case "CARD":
            //        _civilization = Civilization.CARD;
            //        break;
            //    case "DOM":
            //        _civilization = Civilization.DOM;
            //        break;
            //    case "BORG":
            //        _civilization = Civilization.BORG;
            //        break;
            //    default:
            //        break;
            //}
        }
        void Start()
        {
            //_whoIAm = MyCivilization(this.Name);
            _shieldsCurrentHealth = _shieldsMaxHealth;
            //InvokeRepeating("Regenerate", _shieldsRegeneratRate, _shieldsRegeneratRate); // see Regenerate method below
            shieldsAreUp = true;

        }
        private void Update()
        {
            //if (GameManager.Instance != null && GameManager.Instance._statePassedMain_Init)
            //{
            //    if (combat.FriendCivCombatants().Contains(_civilization))
            //    {
            //        _isFriend = true;
            //        _notInThisFight = false;
            //    }
            //    else if (combat.EnemyCivCombatants().Contains(_civilization))
            //    {
            //        _isFriend = false;
            //        _notInThisFight = false;
            //    }
            //    else _notInThisFight = true;
            //}
            if (!_notInThisFight && gameObject.name.ToUpper() != "SHIP") // GameManager.Instance._statePassedCombatInit) //  || GameManager.Instance._statePassedCombatInitRight)
            {
                if (GameManager.Instance.FriendShips.Count > 0 && GameManager.Instance.EnemyShips.Count >0 && gameObject.name != "Ship")
                {
                    string whoTorpedo = gameObject.name.Substring(0, 3).ToUpper();
                    string nameTheSide;
                    if (_isFriend)
                        nameTheSide = GameManager.Instance.FriendNameArray[0].Substring(0, 3); // ToDo: account for alies with other civ names 
                    else
                        nameTheSide = GameManager.Instance.EnemyNameArray[0].Substring(0, 3);
                    nameTheSide = nameTheSide.ToUpper();

                    if (whoTorpedo == nameTheSide)
                        theLocalTargetList = GameManager.Instance.EnemyShips;
                    else
                        theLocalTargetList = GameManager.Instance.FriendShips;
                    //FindBeamTarget(theLocalTargetDictionary);
                }
 
                if (beamObject == null)
                    beamTargetTransform = null;

                if (Input.GetKeyDown(KeyCode.V))
                {
                    GameObject _tempTorpedo = Instantiate(torpedoPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    _tempTorpedo.layer = gameObject.layer + 10;
                    _tempTorpedo.tag = gameObject.name.ToUpper() + "(CLONE)"; // see Edit>Project, Tags and Layers predefined for ship names + (CLONE)
                    _tempTorpedo.AddComponent<AudioSource>().playOnAwake = false;
                    _tempTorpedo.AddComponent<AudioSource>().clip = clipTorpedoFire;
                    theSource = _tempTorpedo.GetComponent<AudioSource>();
                    theSource.PlayOneShot(clipTorpedoFire);
                    Destroy(_tempTorpedo, 8f);
                }
                if (Input.GetKeyDown(KeyCode.B))
                {
                    GameObject beamObject = Instantiate(beamPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    FindBeamTarget(theLocalTargetList);
                    linePositions[0] = this.transform.position + (transform.right * 1) + (transform.forward * 1);
                    linePositions[1] = beamTargetTransform.position + (transform.right * 1) - (transform.forward * 1);
                    beamObject.tag = gameObject.name.ToUpper() + "(CLONE)";

                    var theLine = beamObject.GetComponent<LineRenderer>();
                    theLine.SetVertexCount(2);
                    theLine.SetWidth(1f, 1f);
                    theLine.SetPosition(0, linePositions[0]);
                    theLine.SetPosition(1, linePositions[1]);
                    var boxCollider = theLine.GetComponent<BoxCollider>();
                    MeshFilter meshFilter = (MeshFilter)beamObject.GetComponent("MeshFilter");
                    Mesh mesh = meshFilter.mesh;
                    theLine.BakeMesh(mesh, true);
                    boxCollider.isTrigger = true;

                    beamObject.AddComponent<AudioSource>().playOnAwake = false;
                    beamObject.AddComponent<AudioSource>().clip = clipBeamWeapon;
                    theNextSource = beamObject.GetComponent<AudioSource>();
                    theNextSource.PlayOneShot(clipBeamWeapon);
                    OnTriggerStay(boxCollider);
                    Destroy(beamObject, 0.65f);
                }
                #region travel between targets here
                //if (!isFarTargetSet)
                //{
                //    GameObject fart = new GameObject();
                //    GameObject[] farty = new GameObject[] { fart, fart };
                //    //Transform listSONames = fart.transform;
                //    var dictionary = GameManager.Instance.GetShipTravelTargets();

                //    if (dictionary.TryGetValue(shipGameObject, out farty))
                //    {
                //        _nearTarget = farty[0].transform;
                //        _farTarget = farty[1].transform;
                //    }
                //    _currentTarget = _farTarget;
                //    isFarTargetSet = true;
                //}
                //#endregion
                //#region Alernate near and far targets
                //if (Math.Abs(transform.Position.x) <= 200) // when passing the zero point of the x axis turn lockTurn false, ready to turn
                //    lockTurn = false;

                //int leftRight = 1;
                //if (_currentTarget.Position.x < 0)
                //    leftRight = -1;
                //if ((this._currentTarget.Position.x * leftRight) - (leftRight * shipGameObject.transform.Position.x) < 100 && !lockTurn) // when near the Destination turn
                //{
                //    if (_currentTarget == _farTarget)
                //    {
                //        _currentTarget = _nearTarget;
                //        lockTurn = true;
                //    }
                //    else if (_currentTarget == _nearTarget)
                //    {
                //        _currentTarget = _farTarget;
                //        lockTurn = true;
                //    }
                //}
                #endregion
                #region turn to target
                //var targetRotation = Quaternion.LookRotation(this._currentTarget.Position - transform.Position);
                //rigidbody.MoveRotation(Quaternion.RotateTowards(shipGameObject.transform.rotation, targetRotation, turnRate));
                ////transform.Translate(Vector3.forward * 100 * Time.deltaTime * 3);
                #endregion
            }
        }
        private void FixedUpdate()
        {
            if (_shieldsCurrentHealth < 1)
                shieldsAreUp = false;
        }

        public void OnTriggerStay(Collider other) // beams
        {
            Quaternion rotationOf = Quaternion.FromToRotation(Vector3.down, transform.forward);
            string beamFiringShip = gameObject.name.ToUpper();

            if (GameManager.ShipDataDictionary.TryGetValue(beamFiringShip, out int[] _result))
            {
                _beamDamage = _result[3];
            }
            if (_beamDamage > 0 && beamTargetTransform != null)
            {
                Ship target = beamTargetTransform.GetComponent<Ship>(); // get the targeted Ship
                switch (target.shieldsAreUp)
                {
                    case true:
                        var positionOf = beamTargetTransform.position; // traget ship origin
                        _shields = Instantiate(shieldPrefab, positionOf, rotationOf) as GameObject;
                        Destroy(_shields, 2.1f);
                        target.ShieldsTakeDagame(_beamDamage);
                        _beamDamage = 0;
                        break;
                    case false:
                        target.HullTakeDamage(_beamDamage);
                        _beamDamage = 0;
                        break;
                    default:
                        break;
                }
            }
            GameObject hitShip = other.gameObject; //= other.GetComponent<GameObject>(); // *******will this work???
            hitShip.transform.parent = null; // save parent anim of ship from distroy by removing parent
            for (var i = hitShip.transform.childCount - 1; i >= 0; i--) // save children of ship by removing child's parent
            {
                // objectA is not the attached GameObject, so you can do all your checks with it.
                var objectA = hitShip.transform.GetChild(i);
                objectA.transform.parent = null;
                // remove parent ship from child cameraEmpty to save it save CameraMultiTarget function
            }
            Destroy(hitShip, 1f);
           // Destroy(other.gameObject, 1f);
        }
        public void OnCollisionEnter(Collision collision)
        {
            string nameOfImpactor = collision.gameObject.name;
            string[] _nameParts = nameOfImpactor.ToUpper().Split('_');
            string maybeTorpedo = _nameParts[1];
            if (maybeTorpedo == "TORPEDO(CLONE)")
            {

                var theOriginOf = transform.position; // for explosion below
                ContactPoint contact = collision.contacts[0];
                Quaternion rotationOf = Quaternion.FromToRotation(Vector3.down, contact.normal);
                Vector3 positionOf = contact.point;
                string weaponName = collision.gameObject.tag;
                string gameObjectName = collision.gameObject.name;
                if (gameObjectName.Contains("TORPEDO"))
                    _isTorpedo = true;

                if (GameManager.ShipDataDictionary.TryGetValue(weaponName, out int[] _result))
                {
                    if (_isTorpedo)
                        _torpedoDamage = _result[2];
                    //else
                    //_beamDamage = _result[3]; 
                }
                if (_torpedoDamage > 0) //if (_isTorpedo &&
                {
                    switch (shieldsAreUp)
                    {
                        case true:
                            theOriginOf += transform.forward * 20; // ship origin plus 20 forward for explosion
                            positionOf += transform.forward * 10;  // ship origin plus 10 forward for shields
                            _shields = Instantiate(shieldPrefab, positionOf, rotationOf) as GameObject;
                            Destroy(_shields, 2f);
                            ShieldsTakeDagame(_torpedoDamage);
                            _torpedoDamage = 0;
                            break;
                        case false:
                            HullTakeDamage(_torpedoDamage);
                            _torpedoDamage = 0;
                            break;
                        default:
                            break;
                    }
                }
                GameObject explo = Instantiate(explosionPrefab, theOriginOf, Quaternion.identity) as GameObject;
                explo.AddComponent<AudioSource>().playOnAwake = false;
                explo.AddComponent<AudioSource>().clip = clipExplodTorpedo;
                theSource = explo.GetComponent<AudioSource>();
                theSource.PlayOneShot(clipExplodTorpedo);
                Destroy(explo, 2f);
            }
        }
        public void FindBeamTarget(List<GameObject> theTargets)
        {
            var distance = Mathf.Infinity;

            foreach (var possibleTarget in theTargets)
            {
                if (possibleTarget != null)
                {
                    diff = (transform.position - possibleTarget.transform.position).sqrMagnitude;
                    if (diff < distance)
                    {
                        distance = diff;
                        beamTargetTransform = possibleTarget.transform;
                    }
                }
            }
        }
        void Regenerate()
        {
            if (_shieldsCurrentHealth < _shieldsMaxHealth)
                _shieldsCurrentHealth += _sheildsRegenerateAmount;
            if (_shieldsCurrentHealth > _shieldsMaxHealth)
            {
                _shieldsCurrentHealth = _shieldsMaxHealth;
            }
        }
        public void ShieldsTakeDagame(int damage)
        {
            _shieldsCurrentHealth -= damage;
            Debug.Log("Shields took damage");
            if (_shieldsCurrentHealth < 1)
            {
                Destroy(_shields);
                Debug.Log("Shields destroid");
            }
        }
        public void HullTakeDamage(int damage)
        {
            _hullMaxHealth -= damage;
            Debug.Log("Hull took damage");
            if (_hullMaxHealth < 1)
            {
                Destroy(transform.gameObject, 0.2f);
                if (GameManager.Instance.FriendNameArray.Contains(gameObject.name))
                {
                    var newList = GameManager.Instance.FriendNameArray.ToList();
                    newList.Remove(gameObject.name);
                    GameManager.Instance.FriendNameArray = newList.ToArray();
                    if (GameManager.Instance.FriendShips.Contains(gameObject))
                    {
                        var someKeyAndShip = GameManager.Instance.FriendShips.FirstOrDefault(o => o == gameObject);
                        GameManager.Instance.FriendShips.Remove(someKeyAndShip);
                    }
                }
                else if (GameManager.Instance.EnemyNameArray.Contains(gameObject.name))
                {
                    var newList = GameManager.Instance.EnemyNameArray.ToList();
                    newList.Remove(gameObject.name);
                    GameManager.Instance.EnemyNameArray = newList.ToArray();
                    if (GameManager.Instance.EnemyShips.Contains(gameObject))
                    {
                        var otherKeyAndShip = GameManager.Instance.EnemyShips.FirstOrDefault(o => o == gameObject);
                        GameManager.Instance.EnemyShips.Remove(otherKeyAndShip);
                    }
                }
                Debug.Log("Ship destroid");
                GameObject ringExplosion = Instantiate(_ringExplosion, transform.position, Quaternion.LookRotation(transform.up)) as GameObject;
                ringExplosion.AddComponent<AudioSource>().playOnAwake = false;
                ringExplosion.AddComponent<AudioSource>().clip = clipWarpCoreBreach;
                theSource = ringExplosion.GetComponent<AudioSource>();
                theSource.PlayOneShot(clipWarpCoreBreach);
                GameObject warpCore = Instantiate(_warpCoreBreach, transform.position, Quaternion.identity) as GameObject;
                Destroy(warpCore, 2f);
                Destroy(ringExplosion, 2f);
                Destroy(_shields, 2f);
            }
        }

        //public void SetNearTargets(Dictionary<GameObject, Transform> nearTargets)
        //{
        //    _shipsNearTargets = nearTargets; // empty dummy gameObjects as targets located where 3D ships warp in.
        //}
        //public void SetEnemyTargets(GameObject[] targets)
        //{
        //    _enemyTargets = targets; // empty dummy gameObjects as targets located where 3D ships warp in.
        //}
        public static void SetLayerRecursively(GameObject obj, int newLayer)
        {
            if (null == obj)
            {
                return;
            }

            obj.layer = newLayer;

            foreach (Transform child in obj.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
        //public static Civilization MyCivilization(string who)
        //{
        //    //string readFriendName = _friendNameArray[0].ToUpper();
        //    //string[] _collFriend = readFriendName.Split('_');
        //    //SetShipLayer(_collFriend[0], FriendOrFoe.friend);
        //    //switch (who)
        //    //{
        //    //    case "FED":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 10;
        //    //            else
        //    //                enemyShipLayer = 10;
        //    //            break;
        //    //        }
        //    //    case "TERRAN":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 11;
        //    //            else
        //    //                enemyShipLayer = 11;
        //    //            break;
        //    //        }
        //    //    case "ROM":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 12;
        //    //            else
        //    //                enemyShipLayer = 12;
        //    //            break;
        //    //        }
        //    //    case "KLING":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 13;
        //    //            else
        //    //                enemyShipLayer = 13;
        //    //            break;
        //    //        }
        //    //    case "CARD":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 14;
        //    //            else
        //    //                enemyShipLayer = 14;
        //    //            break;
        //    //        }
        //    //    case "DOM":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 15;
        //    //            else
        //    //                enemyShipLayer = 15;
        //    //            break;
        //    //        }
        //    //    case "BORG":
        //    //        {
        //    //            if (who == FriendOrFoe.friend)
        //    //                friendShipLayer = 16;
        //    //            else
        //    //                enemyShipLayer = 16;
        //    //            break;
        //    //        }
        //    //    default:
        //    //        break;
        //    //}
        //}
    }
}
