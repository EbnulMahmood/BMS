namespace BMS.CoreBusiness.Enums
{
    public enum DevTaskStatus
    {
        Pending = 10,
        Running = 20,
        NeedToReview = 30,
        ReviewRunning = 40,
        NeedToTest = 50,
        TestRunning = 60,
        BugFound = 70,
        Done = 80,
        Canceled = 90,
        NextDevelopment = 100,
        FutureDevelopment = 110,
        All = 120
    }
}
