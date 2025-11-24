using System;
using LinkedListLab;

var list = new MyLinkedList<string>();
list.AddFirst("b");
list.AddLast("c");
list.AddFirst("a");
list.AddLast("c");

Console.WriteLine("Items:");
foreach (var s in list) Console.Write(s + " ");
Console.WriteLine();

Console.WriteLine("Contains c: " + list.Contains("c"));
Console.WriteLine("Find c -> " + (list.Find("c")?.Value ?? "null"));
Console.WriteLine("FindLast c -> " + (list.FindLast("c")?.Value ?? "null"));

list.Remove("c");
Console.WriteLine("After Remove('c'):");
foreach (var s in list) Console.Write(s + " ");
Console.WriteLine();

list.RemoveFirst();
list.RemoveLast();
Console.WriteLine("After RemoveFirst/RemoveLast:");
foreach (var s in list) Console.Write(s + " ");
Console.WriteLine();
