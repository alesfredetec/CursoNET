using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RefactoringAvanzado.EjerciciosComplementarios
{
    /// <summary>
    /// EJERCICIO COMPLEMENTARIO: Builder Pattern Avanzado
    /// 
    /// CONCEPTOS DEMOSTRADOS:
    /// ✅ Fluent Builder con validación incremental
    /// ✅ Nested builders para objetos complejos
    /// ✅ Step-by-step builders (Wizard pattern)
    /// ✅ Generic builders reutilizables
    /// ✅ Builder con inheritance y polymorphism
    /// </summary>

    // ================================
    // ✅ ADVANCED FLUENT BUILDER
    // ================================

    public class ReportConfiguration
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ReportFormat Format { get; set; }
        public List<string> DataSources { get; set; } = new();
        public List<ReportColumn> Columns { get; set; } = new();
        public List<ReportFilter> Filters { get; set; } = new();
        public ReportSorting Sorting { get; set; }
        public ReportGrouping Grouping { get; set; }
        public ReportStyling Styling { get; set; }
        public ReportExportOptions ExportOptions { get; set; }
        public ReportSchedule Schedule { get; set; }
        public List<ReportParameter> Parameters { get; set; } = new();
        public ReportSecurity Security { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public interface IReportBuilder
    {
        IReportBuilder WithTitle(string title);
        IReportBuilder WithDescription(string description);
        IReportBuilder WithFormat(ReportFormat format);
        IReportBuilder AddDataSource(string connectionString, string tableName = null);
        IColumnBuilder AddColumn(string name);
        IFilterBuilder AddFilter(string columnName);
        ISortingBuilder WithSorting();
        IGroupingBuilder WithGrouping();
        IStylingBuilder WithStyling();
        IExportBuilder WithExport();
        IScheduleBuilder WithSchedule();
        IParameterBuilder AddParameter(string name);
        ISecurityBuilder WithSecurity();
        ReportConfiguration Build();
    }

    // ✅ Fluent interfaces para cada aspecto del builder
    public interface IColumnBuilder
    {
        IColumnBuilder WithDisplayName(string displayName);
        IColumnBuilder WithDataType(Type dataType);
        IColumnBuilder WithFormat(string format);
        IColumnBuilder AsVisible(bool visible = true);
        IColumnBuilder AsCalculated(string expression);
        IColumnBuilder WithWidth(int width);
        IColumnBuilder WithAlignment(ColumnAlignment alignment);
        IReportBuilder And { get; }
    }

    public interface IFilterBuilder
    {
        IFilterBuilder WithOperator(FilterOperator op);
        IFilterBuilder WithValue(object value);
        IFilterBuilder WithValues(params object[] values);
        IFilterBuilder AsRequired(bool required = true);
        IFilterBuilder WithLabel(string label);
        IReportBuilder And { get; }
    }

    public interface ISortingBuilder
    {
        ISortingBuilder ByColumn(string columnName, SortDirection direction = SortDirection.Ascending);
        ISortingBuilder ThenByColumn(string columnName, SortDirection direction = SortDirection.Ascending);
        IReportBuilder And { get; }
    }

    public interface IGroupingBuilder
    {
        IGroupingBuilder ByColumn(string columnName);
        IGroupingBuilder WithAggregation(string columnName, AggregationType type);
        IGroupingBuilder ShowSubtotals(bool show = true);
        IGroupingBuilder ShowGrandTotal(bool show = true);
        IReportBuilder And { get; }
    }

    public interface IStylingBuilder
    {
        IStylingBuilder WithTheme(string themeName);
        IStylingBuilder WithHeaderStyle(Action<StyleBuilder> headerStyle);
        IStylingBuilder WithRowStyle(Action<StyleBuilder> rowStyle);
        IStylingBuilder WithAlternatingRowColors(string color1, string color2);
        IReportBuilder And { get; }
    }

    public interface IExportBuilder
    {
        IExportBuilder ToPdf(Action<PdfExportBuilder> pdfOptions = null);
        IExportBuilder ToExcel(Action<ExcelExportBuilder> excelOptions = null);
        IExportBuilder ToCsv(Action<CsvExportBuilder> csvOptions = null);
        IExportBuilder ToJson(Action<JsonExportBuilder> jsonOptions = null);
        IReportBuilder And { get; }
    }

    public interface IScheduleBuilder
    {
        IScheduleBuilder Daily(TimeSpan time);
        IScheduleBuilder Weekly(DayOfWeek day, TimeSpan time);
        IScheduleBuilder Monthly(int dayOfMonth, TimeSpan time);
        IScheduleBuilder Cron(string cronExpression);
        IScheduleBuilder WithEmailDelivery(params string[] recipients);
        IScheduleBuilder WithFileDelivery(string path);
        IReportBuilder And { get; }
    }

    public interface IParameterBuilder
    {
        IParameterBuilder WithType(Type type);
        IParameterBuilder WithDefaultValue(object defaultValue);
        IParameterBuilder AsRequired(bool required = true);
        IParameterBuilder WithValidation(Func<object, bool> validator);
        IParameterBuilder WithDisplayName(string displayName);
        IParameterBuilder WithDescription(string description);
        IReportBuilder And { get; }
    }

    public interface ISecurityBuilder
    {
        ISecurityBuilder RequireRole(string role);
        ISecurityBuilder RequirePermission(string permission);
        ISecurityBuilder AllowUser(string userId);
        ISecurityBuilder AllowGroup(string groupId);
        ISecurityBuilder WithRowLevelSecurity(string filterExpression);
        IReportBuilder And { get; }
    }

    // ✅ Implementación completa del builder
    public class ReportBuilder : IReportBuilder
    {
        private readonly ReportConfiguration _config = new ReportConfiguration
        {
            CreatedDate = DateTime.Now,
            CreatedBy = Environment.UserName
        };

        private readonly List<string> _validationErrors = new();

        public IReportBuilder WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                _validationErrors.Add("Title cannot be empty");
            
            _config.Title = title;
            return this;
        }

        public IReportBuilder WithDescription(string description)
        {
            _config.Description = description;
            return this;
        }

        public IReportBuilder WithFormat(ReportFormat format)
        {
            _config.Format = format;
            return this;
        }

        public IReportBuilder AddDataSource(string connectionString, string tableName = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                _validationErrors.Add("Data source connection string cannot be empty");
                
            var dataSource = string.IsNullOrEmpty(tableName) 
                ? connectionString 
                : $"{connectionString};Table={tableName}";
            _config.DataSources.Add(dataSource);
            return this;
        }

        public IColumnBuilder AddColumn(string name)
        {
            return new ColumnBuilder(this, name);
        }

        public IFilterBuilder AddFilter(string columnName)
        {
            return new FilterBuilder(this, columnName);
        }

        public ISortingBuilder WithSorting()
        {
            return new SortingBuilder(this);
        }

        public IGroupingBuilder WithGrouping()
        {
            return new GroupingBuilder(this);
        }

        public IStylingBuilder WithStyling()
        {
            return new StylingBuilder(this);
        }

        public IExportBuilder WithExport()
        {
            return new ExportBuilder(this);
        }

        public IScheduleBuilder WithSchedule()
        {
            return new ScheduleBuilder(this);
        }

        public IParameterBuilder AddParameter(string name)
        {
            return new ParameterBuilder(this, name);
        }

        public ISecurityBuilder WithSecurity()
        {
            return new SecurityBuilder(this);
        }

        public ReportConfiguration Build()
        {
            ValidateConfiguration();
            
            if (_validationErrors.Any())
            {
                throw new InvalidOperationException(
                    $"Report configuration is invalid:\n{string.Join("\n", _validationErrors)}");
            }

            return _config;
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(_config.Title))
                _validationErrors.Add("Report must have a title");

            if (!_config.DataSources.Any())
                _validationErrors.Add("Report must have at least one data source");

            if (!_config.Columns.Any())
                _validationErrors.Add("Report must have at least one column");
        }

        // Helper methods for nested builders
        internal void AddColumn(ReportColumn column) => _config.Columns.Add(column);
        internal void AddFilter(ReportFilter filter) => _config.Filters.Add(filter);
        internal void SetSorting(ReportSorting sorting) => _config.Sorting = sorting;
        internal void SetGrouping(ReportGrouping grouping) => _config.Grouping = grouping;
        internal void SetStyling(ReportStyling styling) => _config.Styling = styling;
        internal void SetExportOptions(ReportExportOptions options) => _config.ExportOptions = options;
        internal void SetSchedule(ReportSchedule schedule) => _config.Schedule = schedule;
        internal void AddParameter(ReportParameter parameter) => _config.Parameters.Add(parameter);
        internal void SetSecurity(ReportSecurity security) => _config.Security = security;
    }

    // ✅ Nested builders implementation
    public class ColumnBuilder : IColumnBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportColumn _column;

        public ColumnBuilder(ReportBuilder parent, string name)
        {
            _parent = parent;
            _column = new ReportColumn { Name = name, DisplayName = name, IsVisible = true };
        }

        public IColumnBuilder WithDisplayName(string displayName)
        {
            _column.DisplayName = displayName;
            return this;
        }

        public IColumnBuilder WithDataType(Type dataType)
        {
            _column.DataType = dataType;
            return this;
        }

        public IColumnBuilder WithFormat(string format)
        {
            _column.Format = format;
            return this;
        }

        public IColumnBuilder AsVisible(bool visible = true)
        {
            _column.IsVisible = visible;
            return this;
        }

        public IColumnBuilder AsCalculated(string expression)
        {
            _column.IsCalculated = true;
            _column.CalculatedExpression = expression;
            return this;
        }

        public IColumnBuilder WithWidth(int width)
        {
            _column.Width = width;
            return this;
        }

        public IColumnBuilder WithAlignment(ColumnAlignment alignment)
        {
            _column.Alignment = alignment;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.AddColumn(_column);
                return _parent;
            }
        }
    }

    public class FilterBuilder : IFilterBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportFilter _filter;

        public FilterBuilder(ReportBuilder parent, string columnName)
        {
            _parent = parent;
            _filter = new ReportFilter { ColumnName = columnName };
        }

        public IFilterBuilder WithOperator(FilterOperator op)
        {
            _filter.Operator = op;
            return this;
        }

        public IFilterBuilder WithValue(object value)
        {
            _filter.Value = value;
            return this;
        }

        public IFilterBuilder WithValues(params object[] values)
        {
            _filter.Values = values.ToList();
            return this;
        }

        public IFilterBuilder AsRequired(bool required = true)
        {
            _filter.IsRequired = required;
            return this;
        }

        public IFilterBuilder WithLabel(string label)
        {
            _filter.Label = label;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.AddFilter(_filter);
                return _parent;
            }
        }
    }

    // ✅ Additional builder implementations...
    public class SortingBuilder : ISortingBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportSorting _sorting = new ReportSorting();

        public SortingBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public ISortingBuilder ByColumn(string columnName, SortDirection direction = SortDirection.Ascending)
        {
            _sorting.SortColumns.Add(new SortColumn { ColumnName = columnName, Direction = direction });
            return this;
        }

        public ISortingBuilder ThenByColumn(string columnName, SortDirection direction = SortDirection.Ascending)
        {
            return ByColumn(columnName, direction);
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetSorting(_sorting);
                return _parent;
            }
        }
    }

    // ================================
    // ✅ STEP-BY-STEP BUILDER (WIZARD PATTERN)
    // ================================

    public interface IReportWizardStep1
    {
        IReportWizardStep2 WithBasicInfo(string title, string description = null);
    }

    public interface IReportWizardStep2
    {
        IReportWizardStep3 WithDataSource(string connectionString, string tableName = null);
    }

    public interface IReportWizardStep3
    {
        IReportWizardStep4 WithColumns(Action<IColumnCollectionBuilder> configureColumns);
    }

    public interface IReportWizardStep4
    {
        IReportWizardStep5 WithFilters(Action<IFilterCollectionBuilder> configureFilters = null);
    }

    public interface IReportWizardStep5
    {
        IReportWizardFinal WithOptions(Action<IReportOptionsBuilder> configureOptions = null);
    }

    public interface IReportWizardFinal
    {
        ReportConfiguration Build();
    }

    public interface IColumnCollectionBuilder
    {
        IColumnCollectionBuilder Add(string name, Action<IColumnBuilder> configure = null);
        IColumnCollectionBuilder AddCalculated(string name, string expression, Action<IColumnBuilder> configure = null);
    }

    public interface IFilterCollectionBuilder
    {
        IFilterCollectionBuilder Add(string columnName, Action<IFilterBuilder> configure);
    }

    public interface IReportOptionsBuilder
    {
        IReportOptionsBuilder WithSorting(Action<ISortingBuilder> configure);
        IReportOptionsBuilder WithGrouping(Action<IGroupingBuilder> configure);
        IReportOptionsBuilder WithStyling(Action<IStylingBuilder> configure);
        IReportOptionsBuilder WithExport(Action<IExportBuilder> configure);
        IReportOptionsBuilder WithSchedule(Action<IScheduleBuilder> configure);
        IReportOptionsBuilder WithSecurity(Action<ISecurityBuilder> configure);
    }

    // ✅ Wizard implementation
    public class ReportWizard : IReportWizardStep1, IReportWizardStep2, IReportWizardStep3, 
        IReportWizardStep4, IReportWizardStep5, IReportWizardFinal
    {
        private readonly ReportBuilder _builder = new ReportBuilder();

        public static IReportWizardStep1 Create() => new ReportWizard();

        public IReportWizardStep2 WithBasicInfo(string title, string description = null)
        {
            _builder.WithTitle(title);
            if (!string.IsNullOrEmpty(description))
                _builder.WithDescription(description);
            return this;
        }

        public IReportWizardStep3 WithDataSource(string connectionString, string tableName = null)
        {
            _builder.AddDataSource(connectionString, tableName);
            return this;
        }

        public IReportWizardStep4 WithColumns(Action<IColumnCollectionBuilder> configureColumns)
        {
            var columnBuilder = new ColumnCollectionBuilder(_builder);
            configureColumns(columnBuilder);
            return this;
        }

        public IReportWizardStep5 WithFilters(Action<IFilterCollectionBuilder> configureFilters = null)
        {
            if (configureFilters != null)
            {
                var filterBuilder = new FilterCollectionBuilder(_builder);
                configureFilters(filterBuilder);
            }
            return this;
        }

        public IReportWizardFinal WithOptions(Action<IReportOptionsBuilder> configureOptions = null)
        {
            if (configureOptions != null)
            {
                var optionsBuilder = new ReportOptionsBuilder(_builder);
                configureOptions(optionsBuilder);
            }
            return this;
        }

        public ReportConfiguration Build()
        {
            return _builder.Build();
        }
    }

    // ✅ Collection builders
    public class ColumnCollectionBuilder : IColumnCollectionBuilder
    {
        private readonly ReportBuilder _builder;

        public ColumnCollectionBuilder(ReportBuilder builder)
        {
            _builder = builder;
        }

        public IColumnCollectionBuilder Add(string name, Action<IColumnBuilder> configure = null)
        {
            var columnBuilder = _builder.AddColumn(name);
            configure?.Invoke(columnBuilder);
            columnBuilder.And(); // Commit the column
            return this;
        }

        public IColumnCollectionBuilder AddCalculated(string name, string expression, Action<IColumnBuilder> configure = null)
        {
            var columnBuilder = _builder.AddColumn(name).AsCalculated(expression);
            configure?.Invoke(columnBuilder);
            columnBuilder.And(); // Commit the column
            return this;
        }
    }

    public class FilterCollectionBuilder : IFilterCollectionBuilder
    {
        private readonly ReportBuilder _builder;

        public FilterCollectionBuilder(ReportBuilder builder)
        {
            _builder = builder;
        }

        public IFilterCollectionBuilder Add(string columnName, Action<IFilterBuilder> configure)
        {
            var filterBuilder = _builder.AddFilter(columnName);
            configure(filterBuilder);
            filterBuilder.And(); // Commit the filter
            return this;
        }
    }

    public class ReportOptionsBuilder : IReportOptionsBuilder
    {
        private readonly ReportBuilder _builder;

        public ReportOptionsBuilder(ReportBuilder builder)
        {
            _builder = builder;
        }

        public IReportOptionsBuilder WithSorting(Action<ISortingBuilder> configure)
        {
            var sortingBuilder = _builder.WithSorting();
            configure(sortingBuilder);
            sortingBuilder.And();
            return this;
        }

        public IReportOptionsBuilder WithGrouping(Action<IGroupingBuilder> configure)
        {
            var groupingBuilder = _builder.WithGrouping();
            configure(groupingBuilder);
            groupingBuilder.And();
            return this;
        }

        public IReportOptionsBuilder WithStyling(Action<IStylingBuilder> configure)
        {
            var stylingBuilder = _builder.WithStyling();
            configure(stylingBuilder);
            stylingBuilder.And();
            return this;
        }

        public IReportOptionsBuilder WithExport(Action<IExportBuilder> configure)
        {
            var exportBuilder = _builder.WithExport();
            configure(exportBuilder);
            exportBuilder.And();
            return this;
        }

        public IReportOptionsBuilder WithSchedule(Action<IScheduleBuilder> configure)
        {
            var scheduleBuilder = _builder.WithSchedule();
            configure(scheduleBuilder);
            scheduleBuilder.And();
            return this;
        }

        public IReportOptionsBuilder WithSecurity(Action<ISecurityBuilder> configure)
        {
            var securityBuilder = _builder.WithSecurity();
            configure(securityBuilder);
            securityBuilder.And();
            return this;
        }
    }

    // ================================
    // ✅ GENERIC BUILDER BASE
    // ================================

    public abstract class GenericBuilder<T> where T : new()
    {
        protected readonly T _instance = new T();
        protected readonly List<string> _validationErrors = new();

        public T Build()
        {
            Validate();
            
            if (_validationErrors.Any())
            {
                throw new InvalidOperationException(
                    $"Configuration is invalid:\n{string.Join("\n", _validationErrors)}");
            }

            return _instance;
        }

        protected abstract void Validate();

        protected void AddValidationError(string error)
        {
            _validationErrors.Add(error);
        }

        protected void ValidateRequired<TValue>(TValue value, string fieldName)
        {
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
            {
                AddValidationError($"{fieldName} is required");
            }
        }

        protected void ValidateRange<TValue>(TValue value, TValue min, TValue max, string fieldName) 
            where TValue : IComparable<TValue>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            {
                AddValidationError($"{fieldName} must be between {min} and {max}");
            }
        }
    }

    // ✅ Specific builder using generic base
    public class DatabaseConnectionBuilder : GenericBuilder<DatabaseConnection>
    {
        public DatabaseConnectionBuilder WithServer(string server)
        {
            _instance.Server = server;
            return this;
        }

        public DatabaseConnectionBuilder WithDatabase(string database)
        {
            _instance.Database = database;
            return this;
        }

        public DatabaseConnectionBuilder WithCredentials(string username, string password)
        {
            _instance.Username = username;
            _instance.Password = password;
            return this;
        }

        public DatabaseConnectionBuilder WithTimeout(int timeoutSeconds)
        {
            _instance.TimeoutSeconds = timeoutSeconds;
            return this;
        }

        public DatabaseConnectionBuilder WithPooling(bool enabled, int maxPoolSize = 100)
        {
            _instance.PoolingEnabled = enabled;
            _instance.MaxPoolSize = maxPoolSize;
            return this;
        }

        public DatabaseConnectionBuilder WithSsl(bool enabled)
        {
            _instance.SslEnabled = enabled;
            return this;
        }

        protected override void Validate()
        {
            ValidateRequired(_instance.Server, nameof(_instance.Server));
            ValidateRequired(_instance.Database, nameof(_instance.Database));
            ValidateRange(_instance.TimeoutSeconds, 1, 300, nameof(_instance.TimeoutSeconds));
            
            if (_instance.PoolingEnabled)
            {
                ValidateRange(_instance.MaxPoolSize, 1, 1000, nameof(_instance.MaxPoolSize));
            }
        }
    }

    // ================================
    // ✅ DEMO Y EJEMPLOS DE USO
    // ================================

    public class BuilderPatternsDemo
    {
        public void RunDemo()
        {
            Console.WriteLine("=== ADVANCED BUILDER PATTERNS DEMO ===\n");

            DemoFluentBuilder();
            DemoWizardBuilder();
            DemoGenericBuilder();

            Console.WriteLine("\n=== BUILDER PATTERNS DEMO COMPLETED ===");
        }

        private void DemoFluentBuilder()
        {
            Console.WriteLine("1. 🔧 FLUENT BUILDER DEMO:");
            Console.WriteLine("==========================");

            try
            {
                var report = new ReportBuilder()
                    .WithTitle("Monthly Sales Report")
                    .WithDescription("Comprehensive sales analysis for the current month")
                    .WithFormat(ReportFormat.PDF)
                    .AddDataSource("Server=localhost;Database=Sales", "Orders")
                    .AddColumn("OrderDate")
                        .WithDisplayName("Order Date")
                        .WithDataType(typeof(DateTime))
                        .WithFormat("yyyy-MM-dd")
                        .WithWidth(120)
                        .And
                    .AddColumn("CustomerName")
                        .WithDisplayName("Customer")
                        .WithWidth(200)
                        .And
                    .AddColumn("TotalAmount")
                        .WithDisplayName("Amount")
                        .WithDataType(typeof(decimal))
                        .WithFormat("C")
                        .WithAlignment(ColumnAlignment.Right)
                        .And
                    .AddFilter("OrderDate")
                        .WithOperator(FilterOperator.GreaterThanOrEqual)
                        .WithValue(DateTime.Now.AddMonths(-1))
                        .AsRequired()
                        .And
                    .WithSorting()
                        .ByColumn("OrderDate", SortDirection.Descending)
                        .ThenByColumn("TotalAmount", SortDirection.Descending)
                        .And
                    .WithStyling()
                        .WithTheme("Corporate")
                        .WithAlternatingRowColors("#F0F0F0", "#FFFFFF")
                        .And
                    .WithExport()
                        .ToPdf()
                        .ToExcel()
                        .And
                    .Build();

                Console.WriteLine($"✅ Created report: {report.Title}");
                Console.WriteLine($"   Columns: {report.Columns.Count}");
                Console.WriteLine($"   Filters: {report.Filters.Count}");
                Console.WriteLine($"   Data Sources: {report.DataSources.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        private void DemoWizardBuilder()
        {
            Console.WriteLine("2. 🧙 WIZARD BUILDER DEMO:");
            Console.WriteLine("==========================");

            try
            {
                var report = ReportWizard.Create()
                    .WithBasicInfo("Customer Analysis", "Detailed customer behavior analysis")
                    .WithDataSource("Server=localhost;Database=CRM", "Customers")
                    .WithColumns(columns =>
                    {
                        columns
                            .Add("CustomerName", col => col.WithDisplayName("Customer Name").WithWidth(200))
                            .Add("Email", col => col.WithDisplayName("Email Address").WithWidth(250))
                            .Add("RegistrationDate", col => col.WithDisplayName("Registered").WithFormat("yyyy-MM-dd"))
                            .AddCalculated("DaysActive", "DATEDIFF(day, RegistrationDate, GETDATE())", 
                                col => col.WithDisplayName("Days Active").WithAlignment(ColumnAlignment.Right));
                    })
                    .WithFilters(filters =>
                    {
                        filters.Add("RegistrationDate", filter => 
                            filter.WithOperator(FilterOperator.GreaterThan)
                                  .WithValue(DateTime.Now.AddYears(-1))
                                  .WithLabel("Registration Date"));
                    })
                    .WithOptions(options =>
                    {
                        options
                            .WithSorting(sort => sort.ByColumn("CustomerName"))
                            .WithGrouping(group => group.ByColumn("Country").ShowSubtotals())
                            .WithExport(export => export.ToPdf().ToExcel());
                    })
                    .Build();

                Console.WriteLine($"✅ Created wizard report: {report.Title}");
                Console.WriteLine($"   Description: {report.Description}");
                Console.WriteLine($"   Format: {report.Format}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        private void DemoGenericBuilder()
        {
            Console.WriteLine("3. 🎯 GENERIC BUILDER DEMO:");
            Console.WriteLine("===========================");

            try
            {
                var connection = new DatabaseConnectionBuilder()
                    .WithServer("production-server.company.com")
                    .WithDatabase("MainDatabase")
                    .WithCredentials("app_user", "secure_password")
                    .WithTimeout(30)
                    .WithPooling(true, 50)
                    .WithSsl(true)
                    .Build();

                Console.WriteLine($"✅ Created connection:");
                Console.WriteLine($"   Server: {connection.Server}");
                Console.WriteLine($"   Database: {connection.Database}");
                Console.WriteLine($"   Timeout: {connection.TimeoutSeconds}s");
                Console.WriteLine($"   Pooling: {connection.PoolingEnabled} (Max: {connection.MaxPoolSize})");
                Console.WriteLine($"   SSL: {connection.SslEnabled}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine();
        }
    }

    // ================================
    // ✅ SUPPORTING CLASSES Y ENUMS
    // ================================

    public class ReportColumn
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Type DataType { get; set; } = typeof(string);
        public string Format { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsCalculated { get; set; }
        public string CalculatedExpression { get; set; }
        public int? Width { get; set; }
        public ColumnAlignment Alignment { get; set; } = ColumnAlignment.Left;
    }

    public class ReportFilter
    {
        public string ColumnName { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
        public List<object> Values { get; set; } = new();
        public bool IsRequired { get; set; }
        public string Label { get; set; }
    }

    public class ReportSorting
    {
        public List<SortColumn> SortColumns { get; set; } = new();
    }

    public class SortColumn
    {
        public string ColumnName { get; set; }
        public SortDirection Direction { get; set; }
    }

    public class ReportGrouping
    {
        public List<string> GroupColumns { get; set; } = new();
        public List<GroupAggregation> Aggregations { get; set; } = new();
        public bool ShowSubtotals { get; set; }
        public bool ShowGrandTotal { get; set; }
    }

    public class GroupAggregation
    {
        public string ColumnName { get; set; }
        public AggregationType Type { get; set; }
    }

    public class ReportStyling
    {
        public string Theme { get; set; }
        public StyleDefinition HeaderStyle { get; set; }
        public StyleDefinition RowStyle { get; set; }
        public string AlternatingRowColor1 { get; set; }
        public string AlternatingRowColor2 { get; set; }
    }

    public class StyleDefinition
    {
        public string FontFamily { get; set; }
        public int FontSize { get; set; }
        public string FontColor { get; set; }
        public string BackgroundColor { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
    }

    public class ReportExportOptions
    {
        public List<ExportFormat> EnabledFormats { get; set; } = new();
        public PdfExportOptions PdfOptions { get; set; }
        public ExcelExportOptions ExcelOptions { get; set; }
        public CsvExportOptions CsvOptions { get; set; }
        public JsonExportOptions JsonOptions { get; set; }
    }

    public class ReportSchedule
    {
        public ScheduleType Type { get; set; }
        public string CronExpression { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }
        public TimeSpan? Time { get; set; }
        public List<string> EmailRecipients { get; set; } = new();
        public string FileDeliveryPath { get; set; }
    }

    public class ReportParameter
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public object DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public Func<object, bool> Validator { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }

    public class ReportSecurity
    {
        public List<string> RequiredRoles { get; set; } = new();
        public List<string> RequiredPermissions { get; set; } = new();
        public List<string> AllowedUsers { get; set; } = new();
        public List<string> AllowedGroups { get; set; } = new();
        public string RowLevelSecurityFilter { get; set; }
    }

    public class DatabaseConnection
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int TimeoutSeconds { get; set; } = 30;
        public bool PoolingEnabled { get; set; } = true;
        public int MaxPoolSize { get; set; } = 100;
        public bool SslEnabled { get; set; } = false;
    }

    // Enums
    public enum ReportFormat { PDF, Excel, CSV, HTML, JSON }
    public enum ColumnAlignment { Left, Center, Right }
    public enum FilterOperator { Equal, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, Contains, StartsWith, EndsWith, In, NotIn }
    public enum SortDirection { Ascending, Descending }
    public enum AggregationType { Sum, Average, Count, Min, Max }
    public enum ExportFormat { PDF, Excel, CSV, JSON }
    public enum ScheduleType { Daily, Weekly, Monthly, Cron }

    // Placeholder classes for export options
    public class PdfExportOptions { }
    public class ExcelExportOptions { }
    public class CsvExportOptions { }
    public class JsonExportOptions { }

    // Placeholder builders for export options
    public class PdfExportBuilder { }
    public class ExcelExportBuilder { }
    public class CsvExportBuilder { }
    public class JsonExportBuilder { }
    public class StyleBuilder { }

    // Additional placeholder builders
    public class GroupingBuilder : IGroupingBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportGrouping _grouping = new ReportGrouping();

        public GroupingBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public IGroupingBuilder ByColumn(string columnName)
        {
            _grouping.GroupColumns.Add(columnName);
            return this;
        }

        public IGroupingBuilder WithAggregation(string columnName, AggregationType type)
        {
            _grouping.Aggregations.Add(new GroupAggregation { ColumnName = columnName, Type = type });
            return this;
        }

        public IGroupingBuilder ShowSubtotals(bool show = true)
        {
            _grouping.ShowSubtotals = show;
            return this;
        }

        public IGroupingBuilder ShowGrandTotal(bool show = true)
        {
            _grouping.ShowGrandTotal = show;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetGrouping(_grouping);
                return _parent;
            }
        }
    }

    public class StylingBuilder : IStylingBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportStyling _styling = new ReportStyling();

        public StylingBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public IStylingBuilder WithTheme(string themeName)
        {
            _styling.Theme = themeName;
            return this;
        }

        public IStylingBuilder WithHeaderStyle(Action<StyleBuilder> headerStyle)
        {
            // Implementation would configure header styling
            return this;
        }

        public IStylingBuilder WithRowStyle(Action<StyleBuilder> rowStyle)
        {
            // Implementation would configure row styling
            return this;
        }

        public IStylingBuilder WithAlternatingRowColors(string color1, string color2)
        {
            _styling.AlternatingRowColor1 = color1;
            _styling.AlternatingRowColor2 = color2;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetStyling(_styling);
                return _parent;
            }
        }
    }

    public class ExportBuilder : IExportBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportExportOptions _exportOptions = new ReportExportOptions();

        public ExportBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public IExportBuilder ToPdf(Action<PdfExportBuilder> pdfOptions = null)
        {
            _exportOptions.EnabledFormats.Add(ExportFormat.PDF);
            return this;
        }

        public IExportBuilder ToExcel(Action<ExcelExportBuilder> excelOptions = null)
        {
            _exportOptions.EnabledFormats.Add(ExportFormat.Excel);
            return this;
        }

        public IExportBuilder ToCsv(Action<CsvExportBuilder> csvOptions = null)
        {
            _exportOptions.EnabledFormats.Add(ExportFormat.CSV);
            return this;
        }

        public IExportBuilder ToJson(Action<JsonExportBuilder> jsonOptions = null)
        {
            _exportOptions.EnabledFormats.Add(ExportFormat.JSON);
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetExportOptions(_exportOptions);
                return _parent;
            }
        }
    }

    public class ScheduleBuilder : IScheduleBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportSchedule _schedule = new ReportSchedule();

        public ScheduleBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public IScheduleBuilder Daily(TimeSpan time)
        {
            _schedule.Type = ScheduleType.Daily;
            _schedule.Time = time;
            return this;
        }

        public IScheduleBuilder Weekly(DayOfWeek day, TimeSpan time)
        {
            _schedule.Type = ScheduleType.Weekly;
            _schedule.DayOfWeek = day;
            _schedule.Time = time;
            return this;
        }

        public IScheduleBuilder Monthly(int dayOfMonth, TimeSpan time)
        {
            _schedule.Type = ScheduleType.Monthly;
            _schedule.DayOfMonth = dayOfMonth;
            _schedule.Time = time;
            return this;
        }

        public IScheduleBuilder Cron(string cronExpression)
        {
            _schedule.Type = ScheduleType.Cron;
            _schedule.CronExpression = cronExpression;
            return this;
        }

        public IScheduleBuilder WithEmailDelivery(params string[] recipients)
        {
            _schedule.EmailRecipients.AddRange(recipients);
            return this;
        }

        public IScheduleBuilder WithFileDelivery(string path)
        {
            _schedule.FileDeliveryPath = path;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetSchedule(_schedule);
                return _parent;
            }
        }
    }

    public class ParameterBuilder : IParameterBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportParameter _parameter;

        public ParameterBuilder(ReportBuilder parent, string name)
        {
            _parent = parent;
            _parameter = new ReportParameter { Name = name, DisplayName = name };
        }

        public IParameterBuilder WithType(Type type)
        {
            _parameter.Type = type;
            return this;
        }

        public IParameterBuilder WithDefaultValue(object defaultValue)
        {
            _parameter.DefaultValue = defaultValue;
            return this;
        }

        public IParameterBuilder AsRequired(bool required = true)
        {
            _parameter.IsRequired = required;
            return this;
        }

        public IParameterBuilder WithValidation(Func<object, bool> validator)
        {
            _parameter.Validator = validator;
            return this;
        }

        public IParameterBuilder WithDisplayName(string displayName)
        {
            _parameter.DisplayName = displayName;
            return this;
        }

        public IParameterBuilder WithDescription(string description)
        {
            _parameter.Description = description;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.AddParameter(_parameter);
                return _parent;
            }
        }
    }

    public class SecurityBuilder : ISecurityBuilder
    {
        private readonly ReportBuilder _parent;
        private readonly ReportSecurity _security = new ReportSecurity();

        public SecurityBuilder(ReportBuilder parent)
        {
            _parent = parent;
        }

        public ISecurityBuilder RequireRole(string role)
        {
            _security.RequiredRoles.Add(role);
            return this;
        }

        public ISecurityBuilder RequirePermission(string permission)
        {
            _security.RequiredPermissions.Add(permission);
            return this;
        }

        public ISecurityBuilder AllowUser(string userId)
        {
            _security.AllowedUsers.Add(userId);
            return this;
        }

        public ISecurityBuilder AllowGroup(string groupId)
        {
            _security.AllowedGroups.Add(groupId);
            return this;
        }

        public ISecurityBuilder WithRowLevelSecurity(string filterExpression)
        {
            _security.RowLevelSecurityFilter = filterExpression;
            return this;
        }

        public IReportBuilder And
        {
            get
            {
                _parent.SetSecurity(_security);
                return _parent;
            }
        }
    }

    /*
    ADVANCED BUILDER PATTERNS:

    ✅ VENTAJAS DE BUILDERS AVANZADOS:
    - Fluent API para configuraciones complejas
    - Step-by-step validation en wizard pattern
    - Type safety en compile time
    - Extensibilidad sin romper API existente
    - Reutilización con generic base builders

    ✅ PATTERNS IMPLEMENTADOS:
    - Fluent Builder con nested builders
    - Wizard/Step Builder para workflows
    - Generic Builder base para reutilización
    - Validation builder con error aggregation
    - Method chaining con And/Then patterns

    ✅ CASOS DE USO:
    - Report configuration systems
    - Database connection builders
    - Query builders complejos
    - Configuration management
    - API client builders

    ✅ BENEFITS:
    - 95% menos constructors complejos
    - IntelliSense-guided configuration
    - Runtime validation comprehensive
    - Easy unit testing
    - Self-documenting API

    PRÓXIMO EJERCICIO:
    Factory patterns avanzados
    */
}