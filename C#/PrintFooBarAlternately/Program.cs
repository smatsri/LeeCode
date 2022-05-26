using static System.Console;

var foobar = new Solution.FooBar(2);

Task.Run(() => foobar.Foo(() => Write("foo")));
Task.Run(() => foobar.Bar(() => Write("bar")));

ReadLine();