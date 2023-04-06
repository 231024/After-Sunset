using System;
using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Data _data;
    private Controllers _controllers;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _controllers = new Controllers();
        //new GameInitialization(_controllers, _data);
        _controllers.Initialization();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        _controllers.Execute(deltaTime);
    }

    private void FixedUpdate()
    {
        var deltaTime = Time.deltaTime;
        _controllers.FixedExecute(deltaTime);;
    }

    private void LateUpdate()
    {
        var deltaTime = Time.deltaTime;
        _controllers.LateExecute(deltaTime);
    }

    private void OnDestroy()
    {
        _controllers.Cleanup();
        Destroy(this);
    }
}
