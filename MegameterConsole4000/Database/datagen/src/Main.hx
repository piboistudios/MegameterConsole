package;

using Lambda;
using StringTools;

import js.node.Fs;
import js.node.Require;

typedef AddressDataObject = {
	var addresses:Array<Dynamic>;
	var attribution:Dynamic;
}

typedef DbType = {
	var ID:Int;
	var EnteredDate:Date;
	var EnteredBy:Int;
	var ModifiedBy:Int;
	var ModifiedDate:Date;
}

typedef Contact = {
	> DbType,
	var Name:String;
	var Info:Dynamic;
}

typedef Address = {
	> DbType,
	var ReferenceID:Int;
	var TypeID:Int;
	var Lines:Array<String>;
	var City:String;
	var StateOrProvince:String;
	var Country:String;
	var PostalCode:String;
}

typedef Customer = {
	> Contact,
	var CompanyName:String;
	var Credits:Float;
}

typedef Employee = {
	> Contact,
	var Username:String;
	var Password:String;
}

typedef Supplier = {
	> Contact,
}

typedef Database = {
	var addresses:Array<Address>;
	var customers:Array<Customer>;
	var employees:Array<Employee>;
	var suppliers:Array<Supplier>;
	var products:Array<Product>;
	var warehouses:Array<Warehouse>;
	var orders:Array<Order>;
	var lineItems:Array<LineItem>;
	var invoices:Array<Invoice>;
	var __meta:Dynamic;
}

typedef Dimensions = {
	var Height:Float;
	var Length:Float;
	var Width:Float;
}

typedef Product = {
	> DbType,
	var Name:String;
	var UnitPrice:Float;
	var Weight:Float;
	var Dimensions:Dimensions;
	var Discontinued:Bool;
	var SupplierID:Int;
	var SupplierProductCode:String;
}

typedef ProductCount = {
	var ProductID:Int;
	var Count:Int;
}

typedef Warehouse = {
	> DbType,
	var AddressID:Int;
	var OnHandCounts:Array<ProductCount>;
	var OnOrderCounts:Array<ProductCount>;
	var OnBackorderCounts:Array<ProductCount>;
}

typedef Order = {
	> DbType,
	var CustomerID:Int;
	var ShipToID:Int;
	var BillToID:Int;
	var ParentID:Int;
	var WarehouseID:Int;
	var Type:Int;
}

typedef LineItem = {
	> DbType,
	var OrderID:Int;
	var ProductID:Int;
	var LineNo:Int;
	var QtyOrdered:Int;
	var QtyShipped:Int;
	var QtyBackordered:Int;
}

typedef Invoice = {
	> DbType,
	var OrderID:Int;
}

class Main {
	public static function main() {
		// generateAddresses();
		var args = Sys.args();
		var command = args[0];
		switch (command) {
			case "read":
				readDB();
			case "init":
				makeDB();
			default:
				trace('Command unrecognized: $command (parameters: $args)', {
					className: "_NETDBGenerator"
				});
		}
	}

	static var orderTypes = {
		Order: 1,
		Cancelled: 2,
		ReferenceOnly: 3,
		Backorder: 4,
		Dropship: 5
	}
	static var objectTypes = {
		Customer: 1,
		Order: 2,
		Invoice: 3,
		LineItem: 4,
		Product: 5,
		Warehouse: 6,
		PurchaseOrder: 7,
		Supplier: 8,
		Employee: 9,
		Address: 10
	}
	static var amounts = ["addresses" => 10];
	static var addresses:Array<Address> = [];
	static var customers:Array<Customer> = [];
	static var numeral:Dynamic = Require.require('numeral');
	static var peopleDataObjects:Array<Dynamic> = [];
	static var customerDataObjects:Array<Dynamic> = [];
	static var supplierDataObjects:Array<Dynamic> = [];
	static var employeeDataObjects:Array<Dynamic> = [];
	static var productDataObjects:Array<Dynamic> = [];
	static var database:Database = {
		addresses: new Array<Address>(),
		customers: new Array<Customer>(),
		employees: new Array<Employee>(),
		suppliers: new Array<Supplier>(),
		warehouses: new Array<Warehouse>(),
		products: new Array<Product>(),
		orders: new Array<Order>(),
		lineItems: new Array<LineItem>(),
		invoices: new Array<Invoice>(),
		__meta: {}
	};

	static function makeDB() {
		var peopleData:String = cast Fs.readFileSync('./data/people.json');
		peopleDataObjects = haxe.Json.parse(peopleData);

		var customerData:String = cast Fs.readFileSync('./data/customers.json');
		customerDataObjects = haxe.Json.parse(customerData);

		var supplierData:String = cast Fs.readFileSync('./data/suppliers.json');
		supplierDataObjects = haxe.Json.parse(supplierData);

		var employeeData:String = cast Fs.readFileSync('./data/people.json');
		employeeDataObjects = haxe.Json.parse(employeeData).results;

		var productData:String = cast Fs.readFileSync('./data/products.json');
		productDataObjects = haxe.Json.parse(productData);

		database.addresses = get_NETAddresses();
		database.customers = get_NETCustomers();
		database.employees = get_NETEmployees();
		database.suppliers = get_NETSuppliers();
		database.products = get_NETProducts();

		var availableReferenceTypes = [objectTypes.Customer, objectTypes.Employee, objectTypes.Supplier];
		for (address in database.addresses) {
			var container:Array<Dynamic> = [];
			var typeId = availableReferenceTypes[Math.floor(Math.random() * availableReferenceTypes.length)];

			if (typeId == objectTypes.Customer)
				container = database.customers;
			else if (typeId == objectTypes.Employee)
				container = database.employees;
			else if (typeId == objectTypes.Supplier)
				container = database.suppliers;
			address.ReferenceID = Math.floor(Math.random() * container.length);
			address.TypeID = typeId;
		}
		database.warehouses = generate_NETWarehouses();
		var orderData = generate_NETOrders();
		database.orders = orderData.orders;
		database.invoices = orderData.invoices;
		database.lineItems = orderData.lineItems;
		database.__meta.counts = [
			"addresses" => database.addresses.length,
			"customers" => database.customers.length,
			"suppliers" => database.suppliers.length,
			"products" => database.products.length,
			"orders" => database.orders.length,
			"lineItems" => database.lineItems.length,
			"invoices" => database.invoices.length,
			"warehouses" => database.warehouses.length,
			"employees" => database.employees.length
		];
		Fs.writeFileSync('../../database.json', haxe.Json.stringify(database, null, "    "));
		trace("Database generated.");
	}

	static var lineItems = 0;
	static var invoices = 0;
	static var orders = 0;

	static function generate_NETOrders():{orders:Array<Order>, lineItems:Array<LineItem>, invoices:Array<Invoice>} {
		var retVal:{
			orders:Array<Order>,
			lineItems:Array<LineItem>,
			invoices:Array<Invoice>
		} = {
			orders: new Array<Order>(),
			lineItems: new Array<LineItem>(),
			invoices: new Array<Invoice>()
		};
		for (i in 1...500) {
			var oid = orders++;
			var order_NETObject:Order = {
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				ID: oid,
				CustomerID: Math.floor(Math.random() * database.customers.length),
				ShipToID: Math.floor(Math.random() * database.addresses.length),
				BillToID: Math.floor(Math.random() * database.addresses.length),
				ParentID: -1,
				WarehouseID: Math.floor(Math.random() * database.warehouses.length),
				Type: orderTypes.Order
			}
			var wasInvoiced = Math.random() * 100 > 30;
			if (wasInvoiced) {
				var invoice_NETOBject:Invoice = {
					EnteredDate: Date.now(),
					EnteredBy: 3960,
					ModifiedBy: 3960,
					ModifiedDate: Date.now(),
					ID: invoices++,
					OrderID: oid
				};
				retVal.invoices.push(invoice_NETOBject);
			}
			for (_i in 1...Math.floor(Math.random() * 10)) {
				var lineItem_NETObject:LineItem = {
					EnteredDate: Date.now(),
					EnteredBy: 3960,
					ModifiedBy: 3960,
					ModifiedDate: Date.now(),
					ID: lineItems++,
					LineNo: _i,
					OrderID: oid,
					ProductID: Math.floor(Math.random() * database.products.length),
					QtyOrdered: Math.floor(Math.random() * 100 > 90 ? Math.random() * 10 : Math.random() * 100),
					QtyShipped: 0,
					QtyBackordered: 0,
				};
				for (__i in 1...lineItem_NETObject.QtyOrdered) {
					if (Math.random() * 100 > 10) {
						lineItem_NETObject.QtyShipped++;
					} else {
						lineItem_NETObject.QtyBackordered++;
						var backorder_NETObject:Order = {
							EnteredDate: Date.now(),
							EnteredBy: 3960,
							ModifiedBy: 3960,
							ModifiedDate: Date.now(),
							ID: orders++,
							CustomerID: Math.floor(Math.random() * database.customers.length),
							ShipToID: order_NETObject.BillToID,
							BillToID: order_NETObject.ShipToID,
							ParentID: order_NETObject.ID,
							WarehouseID: order_NETObject.WarehouseID,
							Type: orderTypes.Backorder
						};
						var backorderedLineItem_NETObject:LineItem = {
							EnteredDate: Date.now(),
							EnteredBy: 3960,
							ModifiedBy: 3960,
							ModifiedDate: Date.now(),
							ID: lineItems++,
							LineNo: 1,
							OrderID: orders,
							ProductID: lineItem_NETObject.ProductID,
							QtyOrdered: 1,
							QtyShipped: 0,
							QtyBackordered: 0,
						}
						retVal.orders.push(backorder_NETObject);
						retVal.lineItems.push(backorderedLineItem_NETObject);
					}
				}
				retVal.lineItems.push(lineItem_NETObject);
			}
			retVal.orders.push(order_NETObject);
		}
		return retVal;
	}

	static function generate_NETWarehouses():Array<Warehouse> {
		var retVal:Array<Warehouse> = new Array<Warehouse>();
		for (i in 1...30) {
			var getProductCountPair = function() {
				return {ProductID: Math.floor(Math.random() * productDataObjects.length), Count: Math.floor(Math.random() * 9999)}
			};
			var warehouse_NETObject:Warehouse = {
				AddressID: Math.floor(Math.random() * addressCount),
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				ID: i,
				OnHandCounts: [],
				OnOrderCounts: [],
				OnBackorderCounts: []
			};
			for (_i in 1...3)
				for (__i in 1...Math.floor(Math.random() * productDataObjects.length)) {
					switch (_i) {
						case 1:
							warehouse_NETObject.OnOrderCounts.push(getProductCountPair());
						case 2:
							warehouse_NETObject.OnHandCounts.push(getProductCountPair());
						case 3:
							warehouse_NETObject.OnBackorderCounts.push(getProductCountPair());
					}
				}

			var addressToUpdate = database.addresses.find(function(address) {
				return address.ID == warehouse_NETObject.AddressID;
			});
			if (addressToUpdate != null) {
				addressToUpdate.ReferenceID = warehouse_NETObject.ID;
				addressToUpdate.TypeID = objectTypes.Warehouse;
			}
			retVal.push(warehouse_NETObject);
		}
		return retVal;
	}

	static function get_NETProducts():Array<Product> {
		var retVal:Array<Product> = [];
		for (i in 1...productDataObjects.length) {
			var product = productDataObjects[i];
			var product_NETObject:Product = {
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				ID: i,
				Name: product.Name,
				UnitPrice: product.UnitPrice,
				Weight: product.Weight,
				Dimensions: {
					Length: product.Length,
					Width: product.Width,
					Height: product.Height,
				},
				Discontinued: product.Discontinued,
				SupplierID: 0,
				SupplierProductCode: product.SupplierProductCode
			};
			retVal.push(product_NETObject);
		}
		return retVal;
	}

	static function readDB() {
		var database:String = cast Fs.readFileSync('./output/database.json');
		var databaseObject:Database = haxe.Json.parse(database);
		trace(databaseObject.addresses.slice(0, 3));
	}

	static function get_NETSuppliers():Array<Supplier> {
		var retVal:Array<Supplier> = [];
		for (i in 1...supplierDataObjects.length) {
			var supplier = supplierDataObjects[i];
			var supplier_NETObject:Supplier = {
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				ID: i,
				Name: supplier.company,
				Info: {
					phone: supplier.phone,
					distro_center: supplier.address,
					email: supplier.email
				}
			};
			retVal.push(supplier_NETObject);
		}
		return retVal;
	}

	static function get_NETEmployees():Array<Employee> {
		var retVal:Array<Employee> = [];
		for (i in 1...employeeDataObjects.length) {
			var employee = employeeDataObjects[i];
			var employee_NETObject:Employee = {
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				ID: i,
				Name: '${employee.name.last}, ${employee.name.first}',
				Username: employee.login.username,
				Password: employee.login.sha256,
				Info: {
					"email": employee.email,
					"phone": employee.phone,
					"cell": employee.cell,
					"dob": employee.dob,
					"date_hired": employee.registered.date,
				}
			};
			retVal.push(employee_NETObject);
		}
		return retVal;
	}

	static function get_NETCustomers():Array<Customer> {
		var retVal:Array<Customer> = [];
		for (i in 1...customerDataObjects.length) {
			var customer = customerDataObjects[i];
			var customer_NETObject:Customer = {
				ID: i,
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedBy: 3960,
				ModifiedDate: Date.now(),
				CompanyName: customer.company,
				Name: customer.name,
				Credits: (Math.random() * 100 > 50 ? -1 : 1) * numeral(customer.balance).value(),
				Info: {
					phone: customer.phone,
					office: customer.address,
					email: customer.email
				}
			};
			retVal.push(customer_NETObject);
		}
		return retVal;
	}

	static var addressCount:Int = 0;

	static function get_NETAddresses():Array<Address> {
		var addressData = cast Fs.readFileSync('./data/addresses.json');
		var addressDataObject:AddressDataObject = haxe.Json.parse(addressData);

		var retVal:Array<Address> = [];
		var index = 0;
		for (dataAddress in addressDataObject.addresses) {
			var address_NETObject:Address = {
				ID: index++,
				EnteredDate: Date.now(),
				EnteredBy: 3960,
				ModifiedDate: Date.now(),
				ModifiedBy: 3960,
				ReferenceID: 0,
				TypeID: 0,
				Lines: [],
				City: "",
				StateOrProvince: "",
				Country: "",
				PostalCode: ""
			};
			address_NETObject.City = dataAddress.city;
			address_NETObject.StateOrProvince = dataAddress.state;
			address_NETObject.PostalCode = dataAddress.postalCode;
			address_NETObject.Lines = [dataAddress.address1, dataAddress.address2];
			retVal.push(address_NETObject);
		}
		addressCount = index + 1;
		return retVal;
	}
}
