using System;
using System.Collections.Generic;
public class Singleton<T> where T: Singleton<T>
{
    protected static T instance = null;

    protected Singleton() {
        
    }

    public static T getInstance() {
        if (instance == null) {
            instance = Activator.CreateInstance<T>();
        }
        return instance;
    }
}