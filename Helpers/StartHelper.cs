namespace TableInputIssue.Helpers;

public static class StartHelper
{
    public static string GetStart(string partitionKey) => partitionKey switch
    {
        Constants.HERE_BE_PARTITIONKEY => Constants.HERE_BE_START,
        Constants.YOU_SHALL_PARTITIONKEY => Constants.YOU_SHALL_START,
        _ => Constants.UNKNOWN,
    };
}
