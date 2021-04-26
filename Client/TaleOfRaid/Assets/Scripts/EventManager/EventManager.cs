using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
    public delegate void EventDelegate<T>(T e) where T : BaseEventType;

    Dictionary<Type, Delegate> _delegateDict = new Dictionary<Type, Delegate>();

    public void registEvent<T>(EventDelegate<T> listener) where T : BaseEventType
    {
        Delegate d;
        if (_delegateDict.TryGetValue(typeof(T), out d))
        {
            _delegateDict[typeof(T)] = Delegate.Combine(d, listener);
        }
        else {
            _delegateDict[typeof(T)] = listener;
        }
    }

    public void unRegistEvent<T>(EventDelegate<T> listener) where T : BaseEventType
    {
        Delegate d;
        if (_delegateDict.TryGetValue(typeof(T), out d))
        {
            Delegate currDel = Delegate.Remove(d, listener);
            if (currDel == null)
            {
                _delegateDict.Remove(typeof(T));
            }
            else
            {
                _delegateDict[typeof(T)] = currDel;
            }
        }
    }

    public void dispatchEvent<T>(T e) where T : BaseEventType {
        if (e == null)
        {
            throw new ArgumentNullException("dispatchEvent e");
        }
        else {
            Delegate d;
            if (_delegateDict.TryGetValue(typeof(T), out d)) {
                EventDelegate<T> callback = d as EventDelegate<T>;
                if (callback != null)
                {
                    callback(e);
                }
            }
        }
    }
}
