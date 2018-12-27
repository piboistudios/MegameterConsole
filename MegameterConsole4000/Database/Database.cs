using MegameterConsole.Types;
using MegameterConsole4000.LuaLINQ;
using MegameterConsole4000.Types.Attributes;
using MegameterConsole4000.Types.Enums;
using MegameterConsole4000.Types.Interfaces;
using MegameterConsole4000.Types.Internal;
using Newtonsoft.Json;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegameterConsole4000.Database
{
    public class Database
    {
        public Dictionary<int, NLua.Lua> Sessions { get; set; }
        public Address[] addresses;
        public Customer[] customers;
        public Employee[] employees;
        public Invoice[] invoices;
        public LineItem[] lineItems;
        public Order[] orders;
        public Product[] products;
        public PurchaseOrder[] purchaseOrders;
        public Supplier[] suppliers;
        public Warehouse[] warehouses;
        static Database store;
        public static Database Store(HttpContextBase context)
        {
            if (store == null)
            {

                var databaseContent = System.IO.File.ReadAllText(context.Server.MapPath("~/database.json"));
                store = Newtonsoft.Json.JsonConvert.DeserializeObject<Database>(databaseContent);
                store.Sessions = new Dictionary<int, Lua>();
            }
            return store;
        }
        private static OType[] ExchangeTypes<IType, OType>(IType[] input)
        {
            return JsonConvert.DeserializeObject<OType[]>(JsonConvert.SerializeObject(input));
        }

        public static DType GetByID<DType>(int ID) where DType : IDbType
        {
            return Where<DType>(dbItem => dbItem.ID == ID)[0];
        }
        public static DType[] LWhere<DType>(LuaFunction predicate) where DType : IDbType
        {
            var DbAttribute = (DbCollection)Attribute.GetCustomAttribute(typeof(DType), typeof(DbCollection));
            if (DbAttribute == null)
            {
                return null;
            }
            else
            {

                switch (DbAttribute.Type)
                {
                    case ObjectTypes.Address:
                        return ExchangeTypes<Address, DType>(store.addresses.LWhere(predicate).ToArray());
                    case ObjectTypes.Invoice:
                        return ExchangeTypes<Invoice, DType>(store.invoices.LWhere(predicate).ToArray());
                    case ObjectTypes.Order:
                        return ExchangeTypes<Order, DType>(store.orders.LWhere(predicate).ToArray());
                    case ObjectTypes.Product:
                        return ExchangeTypes<Product, DType>(store.products.LWhere(predicate).ToArray());
                    case ObjectTypes.Customer:
                        return ExchangeTypes<Customer, DType>(store.customers.LWhere(predicate).ToArray());
                    case ObjectTypes.Warehouse:
                        return ExchangeTypes<Warehouse, DType>(store.warehouses.LWhere(predicate).ToArray());
                    case ObjectTypes.Employee:
                        return ExchangeTypes<Employee, DType>(store.employees.LWhere(predicate).ToArray());
                    case ObjectTypes.Supplier:
                        return ExchangeTypes<Supplier, DType>(store.suppliers.LWhere(predicate).ToArray());
                    case ObjectTypes.LineItem:
                        return ExchangeTypes<LineItem, DType>(store.lineItems.LWhere(predicate).ToArray());
                    default:
                        return null;
                }
            }
        }
        public static DType[] Where<DType>(Func<IDbType, bool> predicate) where DType : IDbType
        {
            var DbAttribute = (DbCollection)Attribute.GetCustomAttribute(typeof(DType), typeof(DbCollection));
            if (DbAttribute == null)
            {
                return null;
            }
            else
            {

                switch (DbAttribute.Type)
                {
                    case ObjectTypes.Address:
                        return ExchangeTypes<Address, DType>(store.addresses.Where(address => predicate((IDbType)address)).ToArray());
                    case ObjectTypes.Invoice:
                        return ExchangeTypes<Invoice, DType>(store.invoices.Where(invoice => predicate((IDbType)invoice)).ToArray());
                    case ObjectTypes.Order:
                        return ExchangeTypes<Order, DType>(store.orders.Where(order => predicate((IDbType)order)).ToArray());
                    case ObjectTypes.Product:
                        return ExchangeTypes<Product, DType>(store.products.Where(product => predicate((IDbType)product)).ToArray());
                    case ObjectTypes.Customer:
                        return ExchangeTypes<Customer, DType>(store.customers.Where(customer => predicate((IDbType)customer)).ToArray());
                    case ObjectTypes.Warehouse:
                        return ExchangeTypes<Warehouse, DType>(store.warehouses.Where(warehouse => predicate((IDbType)warehouse)).ToArray());
                    case ObjectTypes.Employee:
                        return ExchangeTypes<Employee, DType>(store.employees.Where(employee => predicate((IDbType)employee)).ToArray());
                    case ObjectTypes.Supplier:
                        return ExchangeTypes<Supplier, DType>(store.suppliers.Where(supplier => predicate((IDbType)supplier)).ToArray());
                    case ObjectTypes.LineItem:
                        return ExchangeTypes<LineItem, DType>(store.lineItems.Where(lineItem => predicate((IDbType)lineItem)).ToArray());
                    default:
                        return null;
                }
            }
        }

    }
}