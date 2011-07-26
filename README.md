# Blueprinting

Blueprinting is a library designed to assist in the creation of large collections of complex objects, especially 
for use in testing or database seeding tasks.

It was created mainly to simpify persistence testing of NHibernate managed classes by quickly generating valid instances 
that can be configured for specific scenarios without having to write a object create a state management code.  It is also
quite useful when configuring objects that don't expose their state directly since it uses reflection to handle most of the
state configuration.

## Installing

Blueprinting is available on NuGet and can be installed from the package manager in Visual Studio or from the package manager console with:

    PM> Install-Package Blueprinting

See Also: [Blueprinting on NuGet][nuget]

## Basic Usage

Let's say you had a simple object that looked like this:

    public class SimpleObject
    {
        public string Name { get; set; }
        public string Desc { get; protected set; }
        public int Position { get; set; }
        public int OddNumber { get; set; }
    } 

You could blueprint it with the following:

    public class SimpleObjectBlueprint : Blueprint<SimpleObject>
    {
        public override void ConfigureValidInstance()
        {
            Set(x => x.Name, "Simple Object");
            Set(x => x.Desc, "I am a simple object");
            Set(x => x.Position).StartingWith(0);
            Set(x => x.OddNumber).StartingWith(1, previous => (int) previous + 2);
        }
    }

Then, whenever you need to create a valid instance of SimpleObject, simply do the following:

    var simpleObject = Blueprints.For<SimpleObject>().Build();

Or override specific values:

    var simpleObject = Blueprints.For<SimpleObject>().Set(x => x.Desc, "Not just a simple object").Build();

## Other Features

##### Blueprint Registration

The first time you ask for a blueprint using `Blueprints.For<T>` it will automatically load any registered blueprints that can be
found in the loaded assemblies.  If you need to control which blueprints are loaded then you need to configure your own provider 
with all relevant assemblies and classes to be registered.

    Blueprints.Provider = new DefaultBlueprintProvider(
        AllBlueprints.FromThisAssembly(),
        AllBlueprints.FromAssemblyContaining<MyClass>(),
        AllBlueprints.FromAssembly(asm),
        TheBlueprint.From<MyClassBlueprint>()
    );

Removing all registered blueprints is as simple as calling `Blueprints.Clear()`.

##### Blueprint Configuration

For a simple POCO class with a number of defined property getters:

    public class AnObject
    {
        // exposed property getters
    }

The standard blueprint will look like this:

    public class AnObjectBlueprint : Blueprint<AnObject>
    {
        public override void ConfigureValidInstance()
        {
            // blueprint configuration of properties
        }
    }

The following property/blueprint configurations are supported:

*   Public Setter

        // property definition
        public string Name { get; set; } 

        // in blueprint configuration
        Set(x => x.Name, "name");        
    
*   Protected Setter

        // property definition
        public string Name { get; protected set; } 

        // in blueprint configuration
        Set(x => x.Name, "name");                 

*   Private Setter

        // property definition
        public string Name { get; private set; } 

        // in blueprint configuration
        Set(x => x.Name, "name");              

*   Property with Backing Field

        // property definition
        private string _firstName; // note: currently, backing fields need to follow this naming convention 
        public string FirstName { get { return _firstName; } } 

        // in blueprint configuration
        Set(x => x.FirstName, "first name");                   

*   Sequential Values

        // property definition
        private int Index { get; set; }    

        // in blueprint configuration
        Set(x => x.Index).StartingWith(0); 
        // subsequent values are 0, 1, 2...

*   Sequential Values with String Property

        // property definition
        private string Name { get; set; }       

        // in blueprint configuration
        Set(x => x.Name).StartingWith("name0"); 
        // subsequent values are "name0", "name1", "name2"...

*   Sequential Values Using a Generator Function

        // property definition
        private int Index { get; set; }                                   

        // in blueprint configuration
        Set(x => x.Index).StartingWith(1, previous => (int) previous * 2); 
        // subsequent values are 1, 2, 4...

*   Creating Object Graphs Using Other Blueprints

        // property definition of another object with a registered blueprint
        private AnotherObject MyObject { get; set; } 

        // in blueprint configuration
        Set(x => x.MyObject).FromBlueprint();        

##### Creating Instances and Overriding Values

To create an instance of a blueprint configured object:

    var anObject = Blueprints.For<AnObject>().Build(); 
    // or
    var anObjectBuilder = Blueprints.For<AnObject>();
    var anObject = anObjectBuilder.Build();


To override configured values, you can use the chainable `Set` operation:

    var anObject = Blueprints.For<AnObject>().
        Set(x => x.Name, "new name").
        Set(x => x.Index, 500).
        Build();

This also works with nested objects:

    var anObject = Blueprints.For<AnObject>().
        Set(x => x.AnotherObject.InnerValue.Name, "deeply nested property").
        Build();

Previously created instances can also be used to configure new instances:

    var source = Blueprints.For<AnObject>().Build();
    var customizedCopy = Blueprints.For<AnObject>().
        Copy(source).
        Set(x => x.Name, "a copy of the original").
        Build();

## Contribute

If you'd like to hack on Blueprints, start by forking my repo on GitHub:

  [https://github.com/colincasey/Blueprinting][repo]

1. Clone your fork
2. Create a thoughtfully named topic branch to contain your change
3. Hack away
4. Add tests and make sure everything still passes by running the NUnit test suite
5. If you are adding new functionality, document it in the README
6. Do not change the version number, I will do that on my end
7. If necessary, rebase your commits into logical chunks, without errors
8. Push the branch up to GitHub
9. Send me ([colincasey][github]) a pull request for your branch

== Copyright

Copyright (c) 2011 Colin Casey. See LICENSE for details.

[nuget]: http://nuget.org/List/Packages/Blueprinting
[repo]: https://github.com/colincasey/Blueprinting
[github]: http://github.com/colincasey