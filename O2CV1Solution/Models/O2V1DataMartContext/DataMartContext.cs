using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Xml.Linq;
using Korzh.EasyQuery;

namespace O2V1Web.Models.O2V1DataMartContext {
#region NorthWindContext
    [Table("Customers")]
    [DisplayColumn("Name")]
    [EqEntity(DisplayName = "Client")]
    public class Customer {
        [Key]
        [Column("Customer ID")]
        public string Id { get; set; }

        [Required]
        [Column("Company Name")]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        public virtual Address Address { get; set; }
    
        public virtual Contact Contact { get; set; }
    }

    [ComplexType]
    [DisplayColumn("Name")]
    public class Contact {
        [Column("Contact Name")]
        public string Name { get; set; }

        [Column("Contact Title")]
        [MaxLength(30)]
        public string Title { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Fax")]
        public string Fax { get; set; }

    }

    [ComplexType]
    [DisplayColumn("Street")]
    public class Address {
        [Column("Address")]
        public string Street { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Region")]
        public string Region { get; set; }

        [Column("Postal Code")]
        public string PostalCode { get; set; }

        [Column("Country")]
        [EqListValueEditor]
        public string Country { get; set; }
    }

    [Table("Products")]
    [DisplayColumn("Name")]
    public class Product {
        [Key]
        [Column("Product ID", Order = 0)]
        public int Id { get; set; }

        [Column("Product Name", Order = 1)]
        public string Name { get; set; }

        [Column("Supplier ID")]
        [ScaffoldColumn(false)]
        public int? SupplierId { get; set; }

        [Column("English Name")]
        public string EnglishName { get; set; }

        [Column("Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        [Column("Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Column("Units In Stock")]
        public short? UnitsInStock { get; set; }

        [Column("Units On Order")]
        public short? UnitsOnOrder { get; set; }

        [Column("Reorder Level")]
        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }
    }

    [Table("Orders")]
    [DisplayColumn("Name")]
    public class Order {
        [Key]
        [Column("Order ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [NotMapped]
        public string Name {
            get {
                return string.Format("{0:0000}-{1:yyyy-MM-dd}", this.Id, this.OrderDate);
            }
        }

        //[Required]
        //[Column("Customer ID")]
        //[ScaffoldColumn(false)]
        //public string CustomerId { get; set; }

        [Column("Employee ID")]
        [ScaffoldColumn(false)]
        public int? EmployeeRefId { get; set; }

        public virtual Ship Ship { get; set; }

        [Column("Order Date")]
        [Display(Name = "Ordered")]
        public DateTime? OrderDate { get; set; }

        [Column("Required Date")]
        [Display(Name = "Required")]
        public DateTime? RequiredDate { get; set; }

        [Column("Shipped Date")]
        [Display(Name = "Shipped")]
        public DateTime? ShippedDate { get; set; }

        [DataType("number")]
        public decimal? Freight { get; set; }

        //[ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("EmployeeRefId")]
        public virtual Employee Employee { get; set; }

        [Column("Ship Via")]
        [ScaffoldColumn(false)]
        public int? Via { get; set; }

        public virtual Product Product { get; set; }
    }

    [Table("Employees")]
    [DisplayColumn("FirstName")]
    public class Employee {
        [Key]
        [Column("Employee ID")]
        public int Id { get; set; }

        [Required]
        [Column("Last Name")]
        [MaxLength(20)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Column("First Name")]
        [MaxLength(20)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        public string Title { get; set; }

        [Column("Birth Date")]
        [Display(Name = "Birth date")]
        public DateTime? BirthDate { get; set; }

        [Column("Hire Date")]
        public DateTime? HireDate { get; set; }

        public virtual Address Address { get; set; }

        [Column("Home Phone")]
        [MaxLength(24)]
        public string HomePhone { get; set; }

        [MaxLength(4)]
        public string Extension { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Photo { get; set; }

        public string Notes { get; set; }

        [Column("Reports To")]
        [ScaffoldColumn(false)]
        public int? ReportsTo { get; set; }

        [ForeignKey("ReportsTo")]
        public Employee Manager { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }

    [ComplexType]
    public class Ship {
        [Column("Ship Name")]
        public string Name { get; set; }

        [Column("Ship Address")]
        public string Address { get; set; }

        [Column("Ship City")]
        public string City { get; set; }

        [Column("Ship Region")]
        public string Region { get; set; }

        [Column("Ship Postal Code")]
        public string PostalCode { get; set; }

        [Column("Ship Country")]
        public string Country { get; set; }
    }

    public class NorthwindContext : DbContext {
        static NorthwindContext() {
            Database.SetInitializer<NorthwindContext>(new NWindInitializer());
        }

        public NorthwindContext()
            : base() {
        }

        public NorthwindContext(string connectionString)
            : base(connectionString) { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    public class NWindInitializer : DropCreateDatabaseIfModelChanges<NorthwindContext> {
        protected override void Seed(NorthwindContext context) {
            LoadCustomers(context);
            LoadEmployees(context);
            LoadProducts(context);
            LoadOrders(context);
        }

        XElement LoadFile(string fileName) {
            string dataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            string path = System.IO.Path.Combine(dataPath, fileName);
            return XDocument.Load(path).Root;
        }

        void LoadCustomers(NorthwindContext context) {
            XElement root = LoadFile("customer.txt");
            foreach (XElement element in root.Elements("Result")) {
                Customer customer = new Customer {
                    Id = element.StringValue("CustomerID"),
                    Name = element.StringValue("CompanyName"),
                    Contact = new Contact {
                        Name = element.StringValue("ContactName"),
                        Title = element.StringValue("ContactTitle"),
                        Phone = element.StringValue("Phone"),
                        Fax = element.StringValue("Fax"),
                    },
                    Address = new Address {
                        Street = element.StringValue("Address"),
                        City = element.StringValue("City"),
                        PostalCode = element.StringValue("PostalCode"),
                        Region = element.StringValue("Region"),
                        Country = element.StringValue("Country")
                    }
                };
                context.Customers.Add(customer);
            }
        }

        void LoadProducts(NorthwindContext context) {
            XElement root = LoadFile("product.txt");          
            foreach (XElement element in root.Elements("Result")) {
                Product product = new Product {
                    Id = element.IntValue("ProductID"),
                    Discontinued = element.BoolValue("Discontinued"),
                    Name = element.StringValue("ProductName"),
                    EnglishName = element.StringValue("ProductName"),
                    QuantityPerUnit = element.StringValue("QuantityPerUnit"),
                    ReorderLevel = element.ShortValue("ReorderLevel"),
                    UnitPrice = element.DecimalValue("UnitPrice"),
                    UnitsInStock = element.ShortValue("UnitsInStock"),
                    UnitsOnOrder = element.ShortValue("UnitsOnOrder"),
                };
                context.Products.Add(product);
            }
        }

        void LoadEmployees(NorthwindContext context) {
            Dictionary<int, int> d = new Dictionary<int, int>();
            XElement root = LoadFile("employee.txt");
            foreach (XElement element in root.Elements("Result")) {
                Employee employee = new Employee {
                    Id = element.IntValue("EmployeeID"),
                    LastName = element.StringValue("LastName"),
                    FirstName = element.StringValue("FirstName"),
                    Title = element.StringValue("Title"),
                    BirthDate = element.DateTimeValue("BirthDate"),
                    HireDate = element.DateTimeValue("HireDate"),
                    Address = new Address {
                        Street = element.StringValue("Address"),
                        City = element.StringValue("City"),
                        PostalCode = element.StringValue("PostalCode"),
                        Country = element.StringValue("Country"),
                        Region = element.StringValue("Region"),
                    },
                    Notes = element.StringValue("Notes"),
                    //ReportsTo = element.IntValue("ReportsTo")
                };
                d[employee.Id] = element.IntValue("ReportsTo");
                context.Employees.Add(employee);
            }
            foreach (Employee emp in context.Employees) {
                emp.ReportsTo = d[emp.Id];
            }
        }

        void LoadOrders(NorthwindContext context) {
            XElement root = LoadFile("order.txt");
            int productId = 0;
            foreach (XElement element in root.Elements("Result")) {
                string customerId = element.StringValue("CustomerID");
                var customer = context.Customers.Find(customerId);
                //string productId = element.StringValue("CustomerID");
                //var product = context.Products.Find(productId);
                Order order = new Order {
                    Id = element.IntValue("OrderID"),
                    Customer = customer,
                    //Product = product,
                    //Customer = context.Customers.FirstOrDefault(c => c.Id == element.StringValue("CustomerID")),
                    EmployeeRefId = element.IntValue("EmployeeID"),
                    OrderDate = element.DateTimeValue("OrderDate"),
                    RequiredDate = element.DateTimeValue("RequiredDate"),
                    ShippedDate = element.DateTimeValue("ShippedDate"),
                    Freight = element.DecimalValue("Freight"),
                    Via = element.IntValue("ShipVia"),
                    Product = context.Products.Find(++productId),
                    Ship = new Ship {
                        Name = element.StringValue("ShipName"),
                        Address = element.StringValue("ShipAddress"),
                        City = element.StringValue("ShipCity"),
                        PostalCode = element.StringValue("ShipPostalCode"),
                        Country = element.StringValue("ShipCountry"),
                    }
                };
                context.Orders.Add(order);
            }
        }


    }

    public static class Extensions {
        public static string StringValue(this XElement element, string name) {
            XElement child = element.Element(name);
            return child == null ? string.Empty : child.Value;
        }

        public static int IntValue(this XElement element, string name) {
            XElement child = element.Element(name);
            if (child == null)
                return 0;
            int result = 0;
            int.TryParse(child.Value, out result);
            return result;
        }

        public static bool BoolValue(this XElement element, string name) {
            XElement child = element.Element(name);
            if (child == null)
                return false;
            bool result = false;
            bool.TryParse(child.Value, out result);
            return result;
        }

        public static decimal DecimalValue(this XElement element, string name) {
            XElement child = element.Element(name);
            return child == null ? 0 : decimal.Parse(child.Value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static short ShortValue(this XElement element, string name) {
            XElement child = element.Element(name);
            return child == null ? (short)0 : short.Parse(child.Value);
        }

        public static DateTime DateTimeValue(this XElement element, string name) {
            XElement child = element.Element(name);
            if (child == null)
                return DateTime.Now;
            DateTime result = DateTime.Now;
            DateTime.TryParse(child.Value, out result);
            return result;
        }

    }
#endregion

#region LinqContext
    public class Country {
        public Country() {
            Cities = new List<City>();
        }
        public string CountryName { get; set; }
        public ICollection<City> Cities { get; private set; }
    }

    public class City {
        public string CityName { get; set; }
        public Country Country { get; set; }
    }

    public class LinqContext {
        public LinqContext() {
            Country ru = new Country { CountryName = "Russia" };
            Country ua = new Country { CountryName = "Ukraine" };
            Countries = new List<Country> { ru, ua };
            Cities = new List<City> {
                new City { CityName = "Kiev", Country = ua },
                new City { CityName = "Donetsk", Country = ua },
                new City { CityName = "Moscow", Country = ru },
                new City { CityName = "Spb", Country = ru }
            };
        }
        public List<Country> Countries { get; private set; }
        public List<City> Cities { get; private set; }
    }
#endregion
}