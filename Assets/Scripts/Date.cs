[System.Serializable]
public struct Date 
{
    public int month;
    public int year;

    public static bool Released(Date date)
    {
        return date.year < TimeManager.instance.year || (date.year == TimeManager.instance.year && date.month <= TimeManager.instance.month);
    }

    public static bool Enabled(Date releaseDate, Date endDate)
    {
        bool released = Released(releaseDate);
        bool ended = endDate.year < TimeManager.instance.year || (endDate.year == TimeManager.instance.year && endDate.month <= TimeManager.instance.month);
        if (endDate.year == 0 && endDate.month == 0) ended = false;
        return (released && !ended)? true: false;
    }

    public int GetTotalMonths()
    {
        return (2000 - year) * 12 + month;
    }
}