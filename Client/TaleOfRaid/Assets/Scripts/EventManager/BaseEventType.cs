using System;
using System.Collections.Generic;

// 事件基类
public class BaseEventType
{
    public int eventID;
}

// 之后自定义的事件类型都各自继承基类
public class DrawCardEvent : BaseEventType{
    public int cardBaseId;
}