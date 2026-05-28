# Code Analysis

**Question 1:**

Given the following:

 class Animal
 {
    public virtual string speak(int x) { return "silence"; }
 }

 class Cat : Animal
 {
      public string speak(int x) { return "meow"; }
 }

 class Dog : Animal
 {
      public string speak(short x) { return "bow-wow"; }
 }

Question: Explain why the block below does not emit “bow-wow”:
          Animal  d = new Dog(); 
          Console.Write(d.speak(0)); 

**Answer:** 
The first reason is that d.speak(0) is passing in an integer as a parameter. So it will not be picked up by speak(short x) function in the Dog class. Additionally it is missing an override keyword in the Dog class for it to work properly.


**Question 2**

Given the following:
 class A
 {
     public int a { get; set; }
     public int b { get; set; }
}

class B
{
    public const A a;  
    public B()  { a.a = 10; }
}

int main()
{
    B b = new B();
    Console.WriteLine("%d %d\n", b.a.a, b.a.b);
    return 0;
}

Question: Outline any issues/concerns with the implemented code.

**Answer**

Right away I can see that b.a.b is not instantiated, and does not have a default value. This will most likely throw a null reference exception when attempting the Console.WriteLine. public const A a; line seems suspicious. I'm not sure that can be a constant. Usually I see basic data types in constant. It could be declared as readonly. Another issue is that public const A a; is also never instantiated with any values, so a.a = 10 would likely also throw a null reference exception.



### Startup Configuration

Make sure the Startup Configuration looks as follows:

![Startup Configuration](startup-configuration.png)

The solution should be configured to run multiple startup projects:
- **BackendAPI1** - Start (https)
- **BackendAPI2** - Start (https)
- **FrontendAPI** - Start (https)
- **WebApp** - Start (https)

To configure this in Visual Studio:
1. Right-click on the solution in Solution Explorer
2. Select __Configure Startup Projects...__
3. Select __Multiple startup projects__
4. Set the Action to "Start" for all four projects
5. Click Apply and OK



