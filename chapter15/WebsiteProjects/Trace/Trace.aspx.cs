using System;
using System.Web;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hitAWebservice();
        getSomeData();
        doSomeProcessing();
        breakSomething();
        displayTheResults();
    }

    private void getSomeData()
    {
        writeTrace(true);
        simulateWaiting(8000);
        writeTrace(false);
    }

    private void hitAWebservice()
    {
        writeTrace(true);
        Trace.Write("A message to demonstrate tracing.");
        simulateWaiting(2000);
        writeTrace(false);
    }

    private void doSomeProcessing()
    {
        writeTrace(true);
        simulateWaiting(1000);
        writeTrace(false);
    }

    private void displayTheResults()
    {
        writeTrace(true);
        simulateWaiting(500);
        writeTrace(false);
    }

    private void breakSomething()
    {
        writeTrace(true);
        try
        {
            int superBig = int.MaxValue;
            superBig += 1;
        }
        catch (Exception ex)
        {
            Trace.Warn("Exception", "Oops", ex);
        }
    }

    /// <summary>
    /// Utility function to write a trace message on entering
    /// or leaving a procedure.
    /// </summary>
    /// <param name="enteringFunction">
    /// True if enterinf the function,
    /// False if leaving it.
    /// </param>
    private void writeTrace(bool enteringFunction)
    {
        if (!Trace.IsEnabled) 
            return;

        string callingFunctionName = "Undetermined method";
        string action = enteringFunction ? "Entering" : "Exiting";
        try
        {
            //Determine the name of the calling function.
            System.Diagnostics.StackTrace stackTrace =
                new System.Diagnostics.StackTrace();
            callingFunctionName = 
                stackTrace.GetFrame(1).GetMethod().Name;
        }
        catch { }
        Trace.Write(action, callingFunctionName);
    }

    /// <summary>
    /// Wait a bit.
    /// </summary>
    /// <param name="waitTime">Time in milliseconds to wait.</param>
    private void simulateWaiting(int waitTime)
    {
        System.Threading.Thread.Sleep(waitTime);
    }
}