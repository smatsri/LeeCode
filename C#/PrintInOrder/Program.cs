using static System.Console;

public class Foo
{

    readonly ManualResetEvent firstEvent = new (false);
    readonly ManualResetEvent secondEvent = new (false);



    public void First(Action printFirst)
    {
        printFirst();
        firstEvent.Set();
    }

    public void Second(Action printSecond)
    {
        firstEvent.WaitOne();
        printSecond();
        secondEvent.Set();
    }

    public void Third(Action printThird)
    {
        secondEvent.WaitOne();
        printThird();
    }
}