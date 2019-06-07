## The idea

Spinit.Data.Export is a project built to simplify exporting data into other formats. Currently this library supports exporting excel and csv data formats and data can be written both to files and byte arrays.

## Getting started

To install Spinit.Data.Export, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

<div class="nuget-badge">
        <p><code>PM&gt; Install-Package Spinit.Data.Export</code></p>
</div>

### Create an excel file

    var people = new List<Person>{ new Person(), new Person() };

    // create an excel file exporter
    var exporter = new ExcelFileExporter(new ExportService(), new ExcelPackageFactory(), new FileStreamFactory());

    // or use default services/factories
    var exporter = ExcelFileExporter.Create();

    exporter.Write(people, "C:\\myfile.xlsx");

### Create a csv file

    var people = new List<Person>{ new Person(), new Person() };

    // create a csv file exporter
    var exporter = new CsvFileExporter(new ExportService(), new FileStreamFactory());

    // or use default services/factories
    var exporter = CsvFileExporter.Create();

    exporter.Write(people, "C:\\myfile.csv");

## Customization

A common scenario is that the list-class you are using to generate the export contains properties you do not want to expose in the export. To ignore a property you can use the `ExcludeExport` like so:

    public class Person
    {
        [ExcludeExport]
        public Guid Id { get; set; }

        [DisplayName("Namn")]
        public string Name { get; set; }

        [DisplayName("Ålder")]
        public int Age { get; set; }
    }

## Dependency injection

The examples above are designed to get you started with this package quickly, but chances are you are working in an actual project and would prefer to get an exporter injected by your IOC container of choice wherever you need it.

    // Registry declarations à la Structuremap
    public class MyRegistry : Registry
    {
        public MyRegistry()
        {
            For<IExportService>().Use<ExportService>();
            For<IByteArrayExporter>().Use<ExcelByteArrayExporter>();
            For<IFileExporter>().Use<ExcelFileExporter>();
            For<IFileStreamFactory>().Use<FileStreamFactory>();
        }
    }


    // now you are free to use a IFileExporter or a IByteArrayExporter like this for instance
    public class MyController : Controller
    {
        private readonly IByteArrayExporter _byteArrayExporter;
        public MyController (IByteArrayExporter byteArrayExporter, IPersonApi personApi)
        {
            _byteArrayExporter = byteArrayExporter;
            _personApi= personApi;
        }

        public ActionResult GetFile()
        {
            var people = _personApi.GetEveryone();
            return new FileContentResult(
                _byteArrayExporter.Write(people),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "MyFile.xlsx"
            };
        }
    }

## Advanced customization

The attribute-driven approach of excluding properties forces you to know which properties to export at compile time. Fortunately, the default behaviour can be overridden in the ExportService like this:

    // Define a custom IColumnIncluder
    public class MyColumnIncluder : IColumnIncluder
    {
        public bool ShouldIncludeProperty(PropertyInfo propertyInfo)
        {
            // Decide whether property should be exported or not
        }
    }

    // Modifying the exportservice
    new ExportService
    {
        ColumnIncluder = new MyColumnIncluder();
        // Default behaviour is equivalent to: ColumnIncluder = new DefaultColumnIncluder();
    }
