namespace Solution;

public class FooBar
{
    readonly AutoResetEvent fooEvent = new(false);
    readonly AutoResetEvent barEvent = new(false);
    readonly int n;

    public FooBar(int n)
    {
        this.n = n;
    }

    public void Foo(Action printFoo)
    {
        for (int i = 0; i < n; i++)
        {
            printFoo();
            fooEvent.Set();
            barEvent.WaitOne();
           
        }
    }

    public void Bar(Action printBar)
    {
        for (int i = 0; i < n; i++)
        {
            fooEvent.WaitOne();
            printBar();
            barEvent.Set();
        }
    }
}
