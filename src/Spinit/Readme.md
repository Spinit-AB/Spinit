## The idea

Spinit is a light weight project with some handy utilities and extensions made to make your life as a developer just a little bit easier. Spinit does not require any other dependencies than the framework itself.

## Getting started

To install Spinit, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

<div class="nuget-badge">
        <p><code>PM&gt; Install-Package Spinit</code></p>
</div>

## Embedded resource reader

A small utility to help extract text or stream from embedded resources

    // finds all embedded resources in the specified assembly under the Embedded namespace.
    var resources = EmbeddedResourceReader.CreateFromType<SomeClassInTheAssemblyIWantToAccess>()
                                          .AddResourceFilesMatchingNamespace("Embedded");
    var resource = resources.Single(r => r.ResourceKey.EndsWith("MyFile.sql"));
    string text = resource.Read();
    var somethingElse = resource.Read(stream => ReadSomethingElseFromTheStream(stream));

## EncryptionDecryptionProvider

A class that uses machine config to provide encryption and decryption of strings.

    var provider = new EncryptionDecryptionProvider();
    string encrypted = provider.Encrypt("my value");
    string decrypted = provider.Decrypt(encrypted);

## Extensions

### List extensions

    // Exclude
    var list = new[] { 1, 2, 3 };
    var listWithoutTwo = list.Exclude(2);

    // Distinct
    var people = new List<Person>
    {
        new Person(1, "Adam", "Adamsson"),
        new Person(1, "Adam", "Adamsson"),
        new Person(2, "Bertil", "Bertilsson")
    };
    var peopleWithDistinctId = people.DistinctBy(x => x.Id);

### Enum extensions

    // Next, gives you the next enum. If last, you get the first
    TestEnum second = TestEnum.First.Next();

    // SkipsItems, skips specified number of enums. If it passes the end, it starts over
    TestEnum third = TestEnum.First.SkipItems(2);

    // HasFlag, evaluates if a specified bitflag is set
    bool hasFlag = (TestFlags.FirstBit | TestFlags.ThirdBit).HasFlag(TestFlags.FirstBit);

### Expression extensions

    // GetName, gets a property name (by expression) as a string
    Expression<Func<PersonTestClass, PersonTestClass>> expression = @class => @class.FirstName;
    string propertyName = expression.GetName(); // returns "FirstName"

### Generic extensions

    // With/Return, safely evaluates a deep property chain, returning default value if finding null somewhere on the way
    var greatGrandFathersName = person.With(x => x.Father)
    								  .With(x => x.Fater)
    								  .With(x => x.Father)
    								  .Return(x => x.Firstname);

    // ParseName, will parse a first name and last name with correct whitespace adjustments if either names don't exist
    string fullName = person.ParseName(x => x.FirstName, x => x.LastName);

### Integer extensions

    // ToEnum, will cast an integer to an enum
    TestEnum secondEnum = 2.ToEnum<TestEnum>();

    // IsOdd, evaluates if value is odd
    bool 1.IsOdd();

    // IsEven, evaluates if value is even
    bool 2.IsEven();

### Object extensions

    // IsNull, will evaluate if an object is null
    bool isNull = myObject.IsNull();

    // IsNotNull, will evaluate if an object is not null
    bool isNotNull = myObject.IsNotNull();

### String extensions

    // IsNullOrEmpty, will evaluate if the string is null or empty
    bool isNullOrEmpty = "".IsNullOrEmpty();

    // IsNotNullOrEmpty, will evaluate if the string is not null or empty
    bool isNotNullOrEmpty = " ".IsNotNullOrEmpty();

    // FormatWith, will given a format and parameters format a string
    string formatted = "{0} {1}-{2}".FormatWith("This range is", 0, "100");

    // ReplaceWith, will given a format and an anonymous object as parameters, using tokens, format a string
    string result = "{First} and then {Second}".ReplaceWith(new { First = 1, Second = "two" });

    // ToDecimal/ToDecimalOrDefault, helps parse string to decimal (. and , allowed as separators)
    decimal value = "15.23".ToDecimal()
    decimal value = "15.23".ToDecimalOrDefault();
    decimal value = "15.23".ToDecimalOrDefault(100);

    // ToInt/ToIntOrDefault, helps parse string to int
    int value = "15".ToInt()
    int value = "15".ToIntOrDefault();
    int value = "15".ToIntOrDefault(100);

    // ToBool/ToBoolOrDefault, helps parse string to bool
    bool value = "true".ToInt()
    bool value = "true".ToIntOrDefault();
    bool value = "hello".ToIntOrDefault(true);

### Type extensions

    // HasAttribute, will evaluate if a type has a given attribute
    bool hasAttribute = typeof(TestClass).HasAttribute<TestAttribute>();
    bool hasAttribute = typeof(TestClass).HasAttribute(typeof(TestAttribute));

## Enumerations

A class to replace enums when more context is needed

    // Declaring an enumeration
    public class Status : Enumeration<Status>
    {
    	public static Status Registered = new Status(1, "User has registered");
    	public static Status LoggedIn = new Status(2, "User has logged in");
    	public static Status SignedOut = new Status(3, "User has signed out");

    	private Status(int value, string displayName) : base(value, displayName) { }
    }

    // Using an enumeration
    Status firstStatus =  Status.Registered;
    long value = firstStatus.Value;
    string displayName = firstStatus.DisplayName;
    IEnumerable<Status> allStatuses = Enumeration.GetAll<Status>();
    Status firstStatus = Enumeration.FromValue<Status>(1);
    IEnumerable<Status> statuses = Enumeration.FromValues<Status>(new[] { 1, 2 });
    Status firstStatus = Enumeration.FromDisplayName<Status>("User has registered");

    // Switch function
    var text = Enumeration<Status>.Switch(value, _case =>
    {
        _case.Of(Status.Registered, (en) => en.DisplayName);
        _case.Of(Status.LoggedIn, (en) => en.DisplayName);
    }, "default");

    // Swith action
    Enumeration<Status>.Switch(value, _case =>
    {
        _case.Of(Status.First, (en) => { output += en.DisplayName; });
        _case.Of(Status.Second, (en) => { output += en.DisplayName; });
        _case.Of(Status.Third, (en) => { output += en.DisplayName; });
    }, "default");

## EnumerationFlags

A class to replace enum flags when more context is needed

    // Declaring an enumeration
    public class Access : Enumeration<Access>
    {
    	public static Access AdminModule = new Access(1, "Admin module");
    	public static Access OrderModule = new Access(2, "Order module");
    	public static Access HRModule = new Access(4, "HR module");

    	private Access(int value, string displayName) : base(value, displayName) { }
    }

    // Creating an enumeration flags object
    var myAccess = EnumerationFlags.Create(Access.AdminModule, Access.OrderModule);

    // Using flags
    bool hasAdminAccess = myAccess.Has(Access.AdminModule);
    bool hasAdminOrHRAccess = myAccess.HasAny(new []{ Access.AdminModule, Access.HRModule});
    bool hasAdminAndHRAccess = myAccess.HasAll(new []{ Access.AdminModule, Access.HRModule});
    foreach(var access in myAccess)
    {
    	//Do something fun
    }
    EnumerationFlags<Access> flags = EnumerationFlags<Access>.FromValue(3);

## ListBuilder

A class that implementes `IListBuilder<T> : IList<T>` to help working with lists

    // Make list
    ListBuilder<int> list = new ListBuilder<int>();
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2 });
    ListBuilder<int> list = new ListBuilder<int>().Append(1).Append(2);
    ListBuilder<int> list = new ListBuilder<int>().Append(new[] { 1, 2 });
    ListBuilder<int> list = new ListBuilder<int>().Append(people, x => x.Age);
    ListBuilder<int> list = new ListBuilder<int>().Append(people, (person, index) => (person.Age + index));

    // Indexed access (default = Name)
    ListBuilder<Person> list = new ListBuilder<Person>().Append(new Person{ Name = "Adam", Age = 30})
    													.Append(new Person{ Name = "David", Age = 40});
    var david = list["David"];

    // Custom indexed access
    ListBuilder<Person> list = new ListBuilder<Person>("Role").Append(new Person{ Name = "Adam", Role = "HR", Age = 30})
                                        	 				  .Append(new Person{ Name = "David", Role = "Management", Age = 40});
    var adam = list["HR"];

    // Iterate over multiple lists
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2, 3 });
    IEnumerable<int> twoCombined = list.And(list)
    								   .ForEach((a, b) => a + b);
    IEnumerable<int> threeCombined = list.And(list)
    									 .And(list)
    									 .ForEach((a, b, c) => a + b + c);

    // Built in Where() and OfType() returns ListBuilder
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2, 3 }).Where(x => x > 1);
    ListBuilder<int> list = new ListBuilder<object>().Append(1).Append(2).OfType<int>();

    // Random and shuffle
    int item = new ListBuilder<int>(new[] { 1, 2, 3 }).GetRandom();
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2, 3 }).GetRandomItems();
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2, 3 }).GetRandomItems(quantity: 2);
    ListBuilder<int> list = new ListBuilder<int>(new[] { 1, 2, 3 }).Shuffle();

## Sequence

A class to work with sequences
// Repeat, runs given action a specified number of times
Sequence.Repeat(() => { DoSomething(); }, 10);

## Extended enumerable

A class that implements `IEnumerable<T>` that can contain paging and sorting information

    // Create extended enumerable
    var list = new ExtendedEnumerable<Person>(people /*items*/, 150 /*total count*/, 1 /*page*/, 50 /*page size*/, "FirstName" /*sorted by*/, false /*sorted ascending*/);

    // Using extended enumerable
    long totalCount = list.TotalCount;
    int page = list.Page;
    int pageSize = list.PageSize;
    int numberOfPages = list.NumberOfPages;
    bool hasPreviousPage = list.HasPreviousPage;
    bool hasNextpage = list.HasNextPage;
    string sortedBy = list.SortedBy;
    bool isSortedAscending = list.SortedAscending;
    bool isPaged = list.IsPaged;
    bool isSorted = list.IsSorted;
