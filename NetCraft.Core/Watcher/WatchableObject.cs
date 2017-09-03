using System;

public class WatchableObject
{
    public readonly int objectType { get; }
    public readonly int dataValueId { get; }
    public Object watchedObject { get; set; }
    public bool isWatching { set; }

    public WatchableObject(int objectType, int dataValueId, Object watchedObject)
	{
        this.objectType = objectType;
        this.dataValueId = dataValueId;
        this.watchedObject = watchedObject;
        this.isWatching = true;
	}
}
