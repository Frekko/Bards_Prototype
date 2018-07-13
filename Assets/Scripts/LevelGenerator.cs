using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator : MonoBehaviour
{
    private class LevelEventPair
    {
        public int Beat;
        public LevelEvent Event;
//        public float NewScale;

        public LevelEventPair(int beat, LevelEvent lEvent)
        {
            Beat = beat;
            Event = lEvent;
//            NewScale = scale;
        }
    }

    public int SongTempo;

    private Vector3 _roadSize;

    public GameObject StraightRoadPiece;
    public GameObject TurnRoadPiece;
    public GameObject ObstacleObject;
    public GameObject CoinObject;
    public GameObject LineRendObject;
    public GameObject LineRendObject2;
    public GameObject IdealBeatPoint;

    public ProtagonistController Player;

    private GameObject _lastRoadObject;

//    private Dictionary<int, GameObject> _turnLocationDict = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> _roadDict = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> _bonusRoadDict = new Dictionary<int, GameObject>();

    private List<LevelEventPair> _levelLayout = new List<LevelEventPair>();
    private List<LevelEventPair> _bonusLevelLayout = new List<LevelEventPair>();
    private List<int> _obstaclePositions = new List<int>();
    private List<int> _coinLayout = new List<int>();

    private bool _isRoadRight = true;

    private int _directionSign
    {
        get { return _isRoadRight ? 1 : -1; }
    }

    private float _playerBeatDistance;


    private enum LevelEvent
    {
        Start,
        Turn,
        Enemy,
        Obstacle,
        Finish,
    }

    void GenerateMainRoad(int startingHalfBeat = 16)
    {
        _levelLayout.Add(new LevelEventPair(startingHalfBeat, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 2, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 3, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 5, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 7, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 9, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 11, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 13, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 16, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 18, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 19, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 21, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 23, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 25, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 27, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 29, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 32, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 34, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 35, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 37, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 39, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 41, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 43, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 45, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 48, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 50, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 51, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 53, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 55, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 57, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 59, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 61, LevelEvent.Turn));
        //вот тут можно бонусную

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 73, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 80, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 88, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 96, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 102, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 103, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 104, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 105, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 112, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 120, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 128, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 136, LevelEvent.Turn));

        //        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 141, LevelEvent.Turn));
        //        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 142, LevelEvent.Turn));
        //        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 143, LevelEvent.Turn));
        //        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 144, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 144, LevelEvent.Turn));

//        //кусок можно на бонусную дорожку
//        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 148, LevelEvent.Turn));
//        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 149, LevelEvent.Turn));
//        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 150, LevelEvent.Turn));
//        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 151, LevelEvent.Turn));
//        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 152, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 160, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 164, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 165, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 166, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 167, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 168, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 176, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 180, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 181, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 182, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 183, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 184, LevelEvent.Turn));


        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 188, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 189, LevelEvent.Turn));
        _levelLayout.Add(new LevelEventPair(startingHalfBeat + 191, LevelEvent.Turn));
        //_levelLayout.Add(new LevelEventPair(startingHalfBeat + 192, LevelEvent.Turn));

        _levelLayout.Add(new LevelEventPair(220, LevelEvent.Finish));
    }

    void GenerateBonus(int startingHalfBeat = 16)
    {
//        int startingQuarterBeat = 16;
//        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat, LevelEvent.Start));
//        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + 8, LevelEvent.Turn));
//        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + 24, LevelEvent.Finish));

        int startingQuarterBeat = 138*2;
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + startingHalfBeat*2, LevelEvent.Start));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 3)*2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 6) * 2, LevelEvent.Turn));

        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 9) * 2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 10)*2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 11)*2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 12)*2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 13)*2, LevelEvent.Turn));
      //  _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 14)*2, LevelEvent.Turn));
        _bonusLevelLayout.Add(new LevelEventPair(startingQuarterBeat + (startingHalfBeat + 14)*2, LevelEvent.Finish));

        _coinLayout.Add(startingQuarterBeat + (startingHalfBeat + 10)*2);
        _coinLayout.Add(startingQuarterBeat + (startingHalfBeat + 12) * 2);
    }

    void GenerateObstacles(int startingHalfBeat = 16)
    {
        _obstaclePositions.Add(startingHalfBeat + 35);
        _obstaclePositions.Add(startingHalfBeat + 39);
        _obstaclePositions.Add(startingHalfBeat + 43);
    
        _obstaclePositions.Add(startingHalfBeat + 51);
        _obstaclePositions.Add(startingHalfBeat + 55);
        _obstaclePositions.Add(startingHalfBeat + 59);

        _obstaclePositions.Add(startingHalfBeat + 189);
        _obstaclePositions.Add(startingHalfBeat + 192);
    }

    void Awake()
    {
//        Random r = new Random();
        int startingBeat = 16;

        GenerateMainRoad(startingBeat);
        GenerateBonus(startingBeat);
        GenerateObstacles(startingBeat);

//        _levelLayout.Add(new LevelEventPair(startingBeat, LevelEvent.Turn));
//        _levelLayout.Add(new LevelEventPair(100, LevelEvent.Finish));
//        _bonusLevelLayout.Add(new LevelEventPair(startingBeat, LevelEvent.Start));
//        _bonusLevelLayout.Add(new LevelEventPair(startingBeat + 9, LevelEvent.Turn));
//        _bonusLevelLayout.Add(new LevelEventPair(startingBeat + 26, LevelEvent.Finish));
//        _obstaclePositions.Add(16);
//        for (int i = startingBeat + 3; i < startingBeat + 8; i++)
//        {
//            _coinLayout.Add(i);
//        }
//        for (int i = 1; i < 100; i ++)
//        {
//            _levelLayout.Add(new LevelEventPair(i, LevelEvent.Turn));
//        }
//        _levelLayout.Add(new LevelEventPair(100, LevelEvent.Finish));

        _levelLayout = _levelLayout.OrderBy(x => x.Beat).ToList();
        _bonusLevelLayout = _bonusLevelLayout.OrderBy(x => x.Beat).ToList();

        //var roadLayout = _levelLayout.Where(x => x.Event == LevelEvent.Turn).ToList(); //LINQ

        Physics2D.gravity = new Vector2(0, 0);
    }

    // Use this for initialization
    void Start()
    {
        _lastRoadObject = GameObject.Find("Starting Point");
//        _turnLocationDict.Add(0, _lastRoadObject);
        _roadDict.Add(0, _lastRoadObject);
        _roadSize = _lastRoadObject.GetComponent<Collider2D>().bounds.extents;
        Vector3 bonusRoadSize = new Vector3(_roadSize.x / 2, _roadSize.y / 2, _roadSize.z / 2);

        Player.VelocityComponent = _roadSize.x * SongTempo / 60f;

        int currentHalfBeat = 1;
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        float obstacleOffset = 0.2f; //СДВИГ ОБСТАКЛА
        _isRoadRight = true;
//        bool writeDebug = true;
        foreach (LevelEventPair levelEventPair in _levelLayout.Where(x => x.Event == LevelEvent.Turn || x.Event == LevelEvent.Finish).ToList())
        {
            while (currentHalfBeat <= levelEventPair.Beat)
            {
                Vector3 lastElementPosition = _lastRoadObject.transform.position;
//                if (writeDebug)
//                    Debug.Log(currentHalfBeat);
                Vector3 roadPiecePosition = new Vector3(lastElementPosition.x + _directionSign * _roadSize.x, lastElementPosition.y + _roadSize.y);
                if (currentHalfBeat == levelEventPair.Beat && levelEventPair.Event == LevelEvent.Finish)
                {
                    _lastRoadObject = GameObject.Find("Finish Point");
                    _lastRoadObject.transform.position = roadPiecePosition;
                }
                else
                {
                    _lastRoadObject = Instantiate(StraightRoadPiece, roadPiecePosition, Quaternion.AngleAxis(_directionSign * 45, Vector3.forward));
                }
                _roadDict.Add(currentHalfBeat, _lastRoadObject);
                if (_obstaclePositions.Contains(currentHalfBeat))
                {
                    Vector3 obstaclePosition = new Vector3(roadPiecePosition.x + obstacleOffset * _directionSign,
                        roadPiecePosition.y + obstacleOffset);
                    Instantiate(ObstacleObject, obstaclePosition,
                        Quaternion.AngleAxis(90 - _directionSign * 45, Vector3.forward));
                }
                currentHalfBeat++;
            }
//            writeDebug = false;
            _isRoadRight = !_isRoadRight;
        }

        LineRenderer lineRend = LineRendObject.GetComponent<LineRenderer>();
//        Debug.Log(_roadDict.Values.Count);
        lineRend.positionCount = _roadDict.Values.Count;
        lineRend.SetPositions(_roadDict.Values.Select(x => x.transform.position).ToArray());
        foreach (KeyValuePair<int, GameObject> valuePair in _roadDict)
        {
            Vector3 pos = valuePair.Value.transform.position;
            Instantiate(IdealBeatPoint, pos, Quaternion.AngleAxis(0, Vector3.forward));
        }

        if (_bonusLevelLayout.Count == 0)
        {
            return;
        }
        GameObject lastBonusRoadObject = _roadDict[_bonusLevelLayout[0].Beat / 2];
        Vector3 bonusRoadStartPosition = lastBonusRoadObject.transform.position;
        Vector3 nextRoadElementPosition = _roadDict[_bonusLevelLayout[0].Beat / 2 + 1].transform.position;
        lastBonusRoadObject = Instantiate(TurnRoadPiece, bonusRoadStartPosition, Quaternion.AngleAxis(_directionSign * 45, Vector3.forward));
        lastBonusRoadObject.transform.localScale = new Vector3(0.5f, 0.5f);
        _isRoadRight = nextRoadElementPosition.x < bonusRoadStartPosition.x;
        int currentQuarterBeat = _bonusLevelLayout[0].Beat;
        foreach (LevelEventPair levelEventPair in _bonusLevelLayout.Where(x => x.Event == LevelEvent.Turn || x.Event == LevelEvent.Finish).ToList())
        {
            while (currentQuarterBeat <= levelEventPair.Beat)
            {
                Vector3 lastBonusElementPosition = lastBonusRoadObject.transform.position;
                Vector3 roadPiecePosition = new Vector3(lastBonusElementPosition.x + _directionSign * bonusRoadSize.x, lastBonusElementPosition.y + bonusRoadSize.y);
                lastBonusRoadObject = Instantiate(TurnRoadPiece, roadPiecePosition, Quaternion.AngleAxis(_directionSign * 45, Vector3.forward));
                _bonusRoadDict.Add(currentQuarterBeat, lastBonusRoadObject);
                lastBonusRoadObject.transform.localScale = new Vector3(0.5f, 0.5f);
                if (_coinLayout.Contains(currentQuarterBeat))
                {
                    Instantiate(CoinObject, lastBonusRoadObject.transform.position,
                        Quaternion.AngleAxis(0, Vector3.forward));
                }
                currentQuarterBeat++;
            }
            if (levelEventPair.Event == LevelEvent.Turn)
            {
                _isRoadRight = !_isRoadRight;
                
            }
        }

        LineRenderer lineRend2 = LineRendObject2.GetComponent<LineRenderer>();
                Debug.Log(_bonusRoadDict.Values.Count);
        lineRend2.positionCount = _bonusRoadDict.Values.Count;
        lineRend2.SetPositions(_roadDict.Values.Select(x => x.transform.position).ToArray());
        foreach (KeyValuePair<int, GameObject> valuePair in _bonusRoadDict)
        {
            Vector3 pos = valuePair.Value.transform.position;
            Instantiate(IdealBeatPoint, pos, Quaternion.AngleAxis(0, Vector3.forward));
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}