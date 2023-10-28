using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool<T>
{
    private Func<T> _initializeFunction;
    private Action<T> _getAction;
    private Action<T> _returnAction;
    private Queue<T> _objectsPool = new Queue<T>();
    private List<T> _activeObjectsList = new List<T>();



    public ObjectsPool(
        Func<T> initializeFunction,
        Action<T> getAction = null,
        Action<T> returnAction = null,
        int initializeCount = 0
    )
    {
        _initializeFunction = initializeFunction;
        _getAction = getAction;
        _returnAction = returnAction;

        if (_initializeFunction == null)
        {
            Debug.LogError("ObjectsPool: Initiate function is null");
            return;
        }

        for (int i = 0; i < initializeCount; i++)
            Return(_initializeFunction());
    }

    public T Get()
    {
        var usingObject = _objectsPool.Count > 0 ? _objectsPool.Dequeue() : _initializeFunction();
        _getAction?.Invoke(usingObject);
        _activeObjectsList.Add(usingObject);
        
        return usingObject;
    }

    public void Return(T usingObject)
    {
        _returnAction?.Invoke(usingObject);
        _objectsPool.Enqueue(usingObject);
        _activeObjectsList.Remove(usingObject);
    }

    public void ReturnAll() => _activeObjectsList.ForEach(Return);
    public int ActiveCount => _activeObjectsList.Count;
    public int TotalCount => _objectsPool.Count;
}