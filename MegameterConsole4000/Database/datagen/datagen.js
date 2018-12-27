// Generated by Haxe 3.4.7
(function () { "use strict";
var $estr = function() { return js_Boot.__string_rec(this,''); };
function $extend(from, fields) {
	function Inherit() {} Inherit.prototype = from; var proto = new Inherit();
	for (var name in fields) proto[name] = fields[name];
	if( fields.toString !== Object.prototype.toString ) proto.toString = fields.toString;
	return proto;
}
var HxOverrides = function() { };
HxOverrides.__name__ = true;
HxOverrides.strDate = function(s) {
	var _g = s.length;
	switch(_g) {
	case 8:
		var k = s.split(":");
		var d = new Date();
		d["setTime"](0);
		d["setUTCHours"](k[0]);
		d["setUTCMinutes"](k[1]);
		d["setUTCSeconds"](k[2]);
		return d;
	case 10:
		var k1 = s.split("-");
		return new Date(k1[0],k1[1] - 1,k1[2],0,0,0);
	case 19:
		var k2 = s.split(" ");
		var y = k2[0].split("-");
		var t = k2[1].split(":");
		return new Date(y[0],y[1] - 1,y[2],t[0],t[1],t[2]);
	default:
		throw new js__$Boot_HaxeError("Invalid date format : " + s);
	}
};
HxOverrides.cca = function(s,index) {
	var x = s.charCodeAt(index);
	if(x != x) {
		return undefined;
	}
	return x;
};
HxOverrides.iter = function(a) {
	return { cur : 0, arr : a, hasNext : function() {
		return this.cur < this.arr.length;
	}, next : function() {
		return this.arr[this.cur++];
	}};
};
var Lambda = function() { };
Lambda.__name__ = true;
Lambda.find = function(it,f) {
	var v = $iterator(it)();
	while(v.hasNext()) {
		var v1 = v.next();
		if(f(v1)) {
			return v1;
		}
	}
	return null;
};
var Main = function() { };
Main.__name__ = true;
Main.main = function() {
	var args = process.argv.slice(2);
	var command = args[0];
	switch(command) {
	case "init":
		Main.makeDB();
		break;
	case "read":
		Main.readDB();
		break;
	default:
		haxe_Log.trace("Command unrecognized: " + command + " (parameters: " + Std.string(args) + ")",{ fileName : "Main.hx", lineNumber : 134, className : "Main", methodName : "main", customParams : [{ className : "_NETDBGenerator"}]});
	}
};
Main.makeDB = function() {
	var peopleData = js_node_Fs.readFileSync("./data/people.json");
	Main.peopleDataObjects = JSON.parse(peopleData);
	var customerData = js_node_Fs.readFileSync("./data/customers.json");
	Main.customerDataObjects = JSON.parse(customerData);
	var supplierData = js_node_Fs.readFileSync("./data/suppliers.json");
	Main.supplierDataObjects = JSON.parse(supplierData);
	var employeeData = js_node_Fs.readFileSync("./data/people.json");
	Main.employeeDataObjects = JSON.parse(employeeData).results;
	var productData = js_node_Fs.readFileSync("./data/products.json");
	Main.productDataObjects = JSON.parse(productData);
	Main.database.addresses = Main.get_NETAddresses();
	Main.database.customers = Main.get_NETCustomers();
	Main.database.employees = Main.get_NETEmployees();
	Main.database.suppliers = Main.get_NETSuppliers();
	Main.database.products = Main.get_NETProducts();
	var availableReferenceTypes = [Main.objectTypes.Customer,Main.objectTypes.Employee,Main.objectTypes.Supplier];
	var _g = 0;
	var _g1 = Main.database.addresses;
	while(_g < _g1.length) {
		var address = _g1[_g];
		++_g;
		var container = [];
		var typeId = availableReferenceTypes[Math.floor(Math.random() * availableReferenceTypes.length)];
		if(typeId == Main.objectTypes.Customer) {
			container = Main.database.customers;
		} else if(typeId == Main.objectTypes.Employee) {
			container = Main.database.employees;
		} else if(typeId == Main.objectTypes.Supplier) {
			container = Main.database.suppliers;
		}
		address.ReferenceID = Math.floor(Math.random() * container.length);
		address.TypeID = typeId;
	}
	Main.database.warehouses = Main.generate_NETWarehouses();
	var orderData = Main.generate_NETOrders();
	Main.database.orders = orderData.orders;
	Main.database.invoices = orderData.invoices;
	Main.database.lineItems = orderData.lineItems;
	var _g2 = new haxe_ds_StringMap();
	var value = Main.database.addresses.length;
	if(__map_reserved["addresses"] != null) {
		_g2.setReserved("addresses",value);
	} else {
		_g2.h["addresses"] = value;
	}
	var value1 = Main.database.customers.length;
	if(__map_reserved["customers"] != null) {
		_g2.setReserved("customers",value1);
	} else {
		_g2.h["customers"] = value1;
	}
	var value2 = Main.database.suppliers.length;
	if(__map_reserved["suppliers"] != null) {
		_g2.setReserved("suppliers",value2);
	} else {
		_g2.h["suppliers"] = value2;
	}
	var value3 = Main.database.products.length;
	if(__map_reserved["products"] != null) {
		_g2.setReserved("products",value3);
	} else {
		_g2.h["products"] = value3;
	}
	var value4 = Main.database.orders.length;
	if(__map_reserved["orders"] != null) {
		_g2.setReserved("orders",value4);
	} else {
		_g2.h["orders"] = value4;
	}
	var value5 = Main.database.lineItems.length;
	if(__map_reserved["lineItems"] != null) {
		_g2.setReserved("lineItems",value5);
	} else {
		_g2.h["lineItems"] = value5;
	}
	var value6 = Main.database.invoices.length;
	if(__map_reserved["invoices"] != null) {
		_g2.setReserved("invoices",value6);
	} else {
		_g2.h["invoices"] = value6;
	}
	var value7 = Main.database.warehouses.length;
	if(__map_reserved["warehouses"] != null) {
		_g2.setReserved("warehouses",value7);
	} else {
		_g2.h["warehouses"] = value7;
	}
	var value8 = Main.database.employees.length;
	if(__map_reserved["employees"] != null) {
		_g2.setReserved("employees",value8);
	} else {
		_g2.h["employees"] = value8;
	}
	Main.database.__meta.counts = _g2;
	js_node_Fs.writeFileSync("../../database.json",JSON.stringify(Main.database,null,"    "));
	haxe_Log.trace("Database generated.",{ fileName : "Main.hx", lineNumber : 234, className : "Main", methodName : "makeDB"});
};
Main.generate_NETOrders = function() {
	var retVal = { orders : [], lineItems : [], invoices : []};
	var _g = 1;
	while(_g < 500) {
		var i = _g++;
		var oid = Main.orders++;
		var order_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : oid, CustomerID : Math.floor(Math.random() * Main.database.customers.length), ShipToID : Math.floor(Math.random() * Main.database.addresses.length), BillToID : Math.floor(Math.random() * Main.database.addresses.length), ParentID : -1, WarehouseID : Math.floor(Math.random() * Main.database.warehouses.length), Type : Main.orderTypes.Order};
		var wasInvoiced = Math.random() * 100 > 30;
		if(wasInvoiced) {
			var invoice_NETOBject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : Main.invoices++, OrderID : oid};
			retVal.invoices.push(invoice_NETOBject);
		}
		var _g2 = 1;
		var _g1 = Math.floor(Math.random() * 10);
		while(_g2 < _g1) {
			var _i = _g2++;
			var lineItem_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : Main.lineItems++, LineNo : _i, OrderID : oid, ProductID : Math.floor(Math.random() * Main.database.products.length), QtyOrdered : Math.floor(Math.random() * 100 > 90 ? Math.random() * 10 : Math.random() * 100), QtyShipped : 0, QtyBackordered : 0};
			var _g4 = 1;
			var _g3 = lineItem_NETObject.QtyOrdered;
			while(_g4 < _g3) {
				var __i = _g4++;
				if(Math.random() * 100 > 10) {
					lineItem_NETObject.QtyShipped++;
				} else {
					lineItem_NETObject.QtyBackordered++;
					var backorder_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : Main.orders++, CustomerID : Math.floor(Math.random() * Main.database.customers.length), ShipToID : order_NETObject.BillToID, BillToID : order_NETObject.ShipToID, ParentID : order_NETObject.ID, WarehouseID : order_NETObject.WarehouseID, Type : Main.orderTypes.Backorder};
					var backorderedLineItem_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : Main.lineItems++, LineNo : 1, OrderID : Main.orders, ProductID : lineItem_NETObject.ProductID, QtyOrdered : 1, QtyShipped : 0, QtyBackordered : 0};
					retVal.orders.push(backorder_NETObject);
					retVal.lineItems.push(backorderedLineItem_NETObject);
				}
			}
			retVal.lineItems.push(lineItem_NETObject);
		}
		retVal.orders.push(order_NETObject);
	}
	return retVal;
};
Main.generate_NETWarehouses = function() {
	var retVal = [];
	var _g = 1;
	while(_g < 30) {
		var i = _g++;
		var getProductCountPair = (function() {
			return function() {
				return { ProductID : Math.floor(Math.random() * Main.productDataObjects.length), Count : Math.floor(Math.random() * 9999)};
			};
		})();
		var warehouse_NETObject = [{ AddressID : Math.floor(Math.random() * Main.addressCount), EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : i, OnHandCounts : [], OnOrderCounts : [], OnBackorderCounts : []}];
		var _g1 = 1;
		while(_g1 < 3) {
			var _i = _g1++;
			var _g3 = 1;
			var _g2 = Math.floor(Math.random() * Main.productDataObjects.length);
			while(_g3 < _g2) {
				var __i = _g3++;
				switch(_i) {
				case 1:
					warehouse_NETObject[0].OnOrderCounts.push(getProductCountPair());
					break;
				case 2:
					warehouse_NETObject[0].OnHandCounts.push(getProductCountPair());
					break;
				case 3:
					warehouse_NETObject[0].OnBackorderCounts.push(getProductCountPair());
					break;
				}
			}
		}
		var addressToUpdate = Lambda.find(Main.database.addresses,(function(warehouse_NETObject1) {
			return function(address) {
				return address.ID == warehouse_NETObject1[0].AddressID;
			};
		})(warehouse_NETObject));
		if(addressToUpdate != null) {
			addressToUpdate.ReferenceID = warehouse_NETObject[0].ID;
			addressToUpdate.TypeID = Main.objectTypes.Warehouse;
		}
		retVal.push(warehouse_NETObject[0]);
	}
	return retVal;
};
Main.get_NETProducts = function() {
	var retVal = [];
	var _g1 = 1;
	var _g = Main.productDataObjects.length;
	while(_g1 < _g) {
		var i = _g1++;
		var product = Main.productDataObjects[i];
		var product_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : i, Name : product.Name, UnitPrice : product.UnitPrice, Weight : product.Weight, Dimensions : { Length : product.Length, Width : product.Width, Height : product.Height}, Discontinued : product.Discontinued, SupplierID : 0, SupplierProductCode : product.SupplierProductCode};
		retVal.push(product_NETObject);
	}
	return retVal;
};
Main.readDB = function() {
	var database = js_node_Fs.readFileSync("./output/database.json");
	var databaseObject = JSON.parse(database);
	haxe_Log.trace(databaseObject.addresses.slice(0,3),{ fileName : "Main.hx", lineNumber : 405, className : "Main", methodName : "readDB"});
};
Main.get_NETSuppliers = function() {
	var retVal = [];
	var _g1 = 1;
	var _g = Main.supplierDataObjects.length;
	while(_g1 < _g) {
		var i = _g1++;
		var supplier = Main.supplierDataObjects[i];
		var supplier_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : i, Name : supplier.company, Info : { phone : supplier.phone, distro_center : supplier.address, email : supplier.email}};
		retVal.push(supplier_NETObject);
	}
	return retVal;
};
Main.get_NETEmployees = function() {
	var retVal = [];
	var _g1 = 1;
	var _g = Main.employeeDataObjects.length;
	while(_g1 < _g) {
		var i = _g1++;
		var employee = Main.employeeDataObjects[i];
		var employee_NETObject = { EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), ID : i, Name : "" + employee.name.last + ", " + employee.name.first, Username : employee.login.username, Password : employee.login.sha256, Info : { "email" : employee.email, "phone" : employee.phone, "cell" : employee.cell, "dob" : employee.dob, "date_hired" : employee.registered.date}};
		retVal.push(employee_NETObject);
	}
	return retVal;
};
Main.get_NETCustomers = function() {
	var retVal = [];
	var _g1 = 1;
	var _g = Main.customerDataObjects.length;
	while(_g1 < _g) {
		var i = _g1++;
		var customer = Main.customerDataObjects[i];
		var customer_NETObject = { ID : i, EnteredDate : new Date(), EnteredBy : 3960, ModifiedBy : 3960, ModifiedDate : new Date(), CompanyName : customer.company, Name : customer.name, Credits : (Math.random() * 100 > 50 ? -1 : 1) * Main.numeral(customer.balance).value(), Info : { phone : customer.phone, office : customer.address, email : customer.email}};
		retVal.push(customer_NETObject);
	}
	return retVal;
};
Main.get_NETAddresses = function() {
	var addressData = js_node_Fs.readFileSync("./data/addresses.json");
	var addressDataObject = JSON.parse(addressData);
	var retVal = [];
	var index = 0;
	var _g = 0;
	var _g1 = addressDataObject.addresses;
	while(_g < _g1.length) {
		var dataAddress = _g1[_g];
		++_g;
		var address_NETObject = { ID : index++, EnteredDate : new Date(), EnteredBy : 3960, ModifiedDate : new Date(), ModifiedBy : 3960, ReferenceID : 0, TypeID : 0, Lines : [], City : "", StateOrProvince : "", Country : "", PostalCode : ""};
		address_NETObject.City = dataAddress.city;
		address_NETObject.StateOrProvince = dataAddress.state;
		address_NETObject.PostalCode = dataAddress.postalCode;
		address_NETObject.Lines = [dataAddress.address1,dataAddress.address2];
		retVal.push(address_NETObject);
	}
	Main.addressCount = index + 1;
	return retVal;
};
Math.__name__ = true;
var Std = function() { };
Std.__name__ = true;
Std.string = function(s) {
	return js_Boot.__string_rec(s,"");
};
var haxe_io_Output = function() { };
haxe_io_Output.__name__ = true;
var _$Sys_FileOutput = function(fd) {
	this.fd = fd;
};
_$Sys_FileOutput.__name__ = true;
_$Sys_FileOutput.__super__ = haxe_io_Output;
_$Sys_FileOutput.prototype = $extend(haxe_io_Output.prototype,{
	writeByte: function(c) {
		js_node_Fs.writeSync(this.fd,String.fromCharCode(c));
	}
	,writeBytes: function(s,pos,len) {
		var data = s.b;
		return js_node_Fs.writeSync(this.fd,new js_node_buffer_Buffer(data.buffer,data.byteOffset,s.length),pos,len);
	}
	,writeString: function(s) {
		js_node_Fs.writeSync(this.fd,s);
	}
	,flush: function() {
		js_node_Fs.fsyncSync(this.fd);
	}
	,close: function() {
		js_node_Fs.closeSync(this.fd);
	}
});
var haxe_io_Input = function() { };
haxe_io_Input.__name__ = true;
var _$Sys_FileInput = function(fd) {
	this.fd = fd;
};
_$Sys_FileInput.__name__ = true;
_$Sys_FileInput.__super__ = haxe_io_Input;
_$Sys_FileInput.prototype = $extend(haxe_io_Input.prototype,{
	readByte: function() {
		var buf = new js_node_buffer_Buffer(1);
		try {
			js_node_Fs.readSync(this.fd,buf,0,1,null);
		} catch( e ) {
			if (e instanceof js__$Boot_HaxeError) e = e.val;
			if(e.code == "EOF") {
				throw new js__$Boot_HaxeError(new haxe_io_Eof());
			} else {
				throw new js__$Boot_HaxeError(haxe_io_Error.Custom(e));
			}
		}
		return buf[0];
	}
	,readBytes: function(s,pos,len) {
		var data = s.b;
		var buf = new js_node_buffer_Buffer(data.buffer,data.byteOffset,s.length);
		try {
			return js_node_Fs.readSync(this.fd,buf,pos,len,null);
		} catch( e ) {
			if (e instanceof js__$Boot_HaxeError) e = e.val;
			if(e.code == "EOF") {
				throw new js__$Boot_HaxeError(new haxe_io_Eof());
			} else {
				throw new js__$Boot_HaxeError(haxe_io_Error.Custom(e));
			}
		}
	}
	,close: function() {
		js_node_Fs.closeSync(this.fd);
	}
});
var haxe_IMap = function() { };
haxe_IMap.__name__ = true;
var haxe_Log = function() { };
haxe_Log.__name__ = true;
haxe_Log.trace = function(v,infos) {
	js_Boot.__trace(v,infos);
};
var haxe_ds_StringMap = function() {
	this.h = { };
};
haxe_ds_StringMap.__name__ = true;
haxe_ds_StringMap.__interfaces__ = [haxe_IMap];
haxe_ds_StringMap.prototype = {
	setReserved: function(key,value) {
		if(this.rh == null) {
			this.rh = { };
		}
		this.rh["$" + key] = value;
	}
};
var haxe_io_Bytes = function(data) {
	this.length = data.byteLength;
	this.b = new Uint8Array(data);
	this.b.bufferValue = data;
	data.hxBytes = this;
	data.bytes = this.b;
};
haxe_io_Bytes.__name__ = true;
haxe_io_Bytes.alloc = function(length) {
	return new haxe_io_Bytes(new ArrayBuffer(length));
};
haxe_io_Bytes.ofString = function(s) {
	var a = [];
	var i = 0;
	while(i < s.length) {
		var c = s.charCodeAt(i++);
		if(55296 <= c && c <= 56319) {
			c = c - 55232 << 10 | s.charCodeAt(i++) & 1023;
		}
		if(c <= 127) {
			a.push(c);
		} else if(c <= 2047) {
			a.push(192 | c >> 6);
			a.push(128 | c & 63);
		} else if(c <= 65535) {
			a.push(224 | c >> 12);
			a.push(128 | c >> 6 & 63);
			a.push(128 | c & 63);
		} else {
			a.push(240 | c >> 18);
			a.push(128 | c >> 12 & 63);
			a.push(128 | c >> 6 & 63);
			a.push(128 | c & 63);
		}
	}
	return new haxe_io_Bytes(new Uint8Array(a).buffer);
};
haxe_io_Bytes.ofData = function(b) {
	var hb = b.hxBytes;
	if(hb != null) {
		return hb;
	}
	return new haxe_io_Bytes(b);
};
haxe_io_Bytes.fastGet = function(b,pos) {
	return b.bytes[pos];
};
var haxe_io_Eof = function() {
};
haxe_io_Eof.__name__ = true;
haxe_io_Eof.prototype = {
	toString: function() {
		return "Eof";
	}
};
var haxe_io_Error = { __ename__ : true, __constructs__ : ["Blocked","Overflow","OutsideBounds","Custom"] };
haxe_io_Error.Blocked = ["Blocked",0];
haxe_io_Error.Blocked.toString = $estr;
haxe_io_Error.Blocked.__enum__ = haxe_io_Error;
haxe_io_Error.Overflow = ["Overflow",1];
haxe_io_Error.Overflow.toString = $estr;
haxe_io_Error.Overflow.__enum__ = haxe_io_Error;
haxe_io_Error.OutsideBounds = ["OutsideBounds",2];
haxe_io_Error.OutsideBounds.toString = $estr;
haxe_io_Error.OutsideBounds.__enum__ = haxe_io_Error;
haxe_io_Error.Custom = function(e) { var $x = ["Custom",3,e]; $x.__enum__ = haxe_io_Error; $x.toString = $estr; return $x; };
var js__$Boot_HaxeError = function(val) {
	Error.call(this);
	this.val = val;
	this.message = String(val);
	if(Error.captureStackTrace) {
		Error.captureStackTrace(this,js__$Boot_HaxeError);
	}
};
js__$Boot_HaxeError.__name__ = true;
js__$Boot_HaxeError.wrap = function(val) {
	if((val instanceof Error)) {
		return val;
	} else {
		return new js__$Boot_HaxeError(val);
	}
};
js__$Boot_HaxeError.__super__ = Error;
js__$Boot_HaxeError.prototype = $extend(Error.prototype,{
});
var js_Boot = function() { };
js_Boot.__name__ = true;
js_Boot.__unhtml = function(s) {
	return s.split("&").join("&amp;").split("<").join("&lt;").split(">").join("&gt;");
};
js_Boot.__trace = function(v,i) {
	var msg = i != null ? i.fileName + ":" + i.lineNumber + ": " : "";
	msg += js_Boot.__string_rec(v,"");
	if(i != null && i.customParams != null) {
		var _g = 0;
		var _g1 = i.customParams;
		while(_g < _g1.length) {
			var v1 = _g1[_g];
			++_g;
			msg += "," + js_Boot.__string_rec(v1,"");
		}
	}
	var d;
	var tmp;
	if(typeof(document) != "undefined") {
		d = document.getElementById("haxe:trace");
		tmp = d != null;
	} else {
		tmp = false;
	}
	if(tmp) {
		d.innerHTML += js_Boot.__unhtml(msg) + "<br/>";
	} else if(typeof console != "undefined" && console.log != null) {
		console.log(msg);
	}
};
js_Boot.__string_rec = function(o,s) {
	if(o == null) {
		return "null";
	}
	if(s.length >= 5) {
		return "<...>";
	}
	var t = typeof(o);
	if(t == "function" && (o.__name__ || o.__ename__)) {
		t = "object";
	}
	switch(t) {
	case "function":
		return "<function>";
	case "object":
		if(o instanceof Array) {
			if(o.__enum__) {
				if(o.length == 2) {
					return o[0];
				}
				var str = o[0] + "(";
				s += "\t";
				var _g1 = 2;
				var _g = o.length;
				while(_g1 < _g) {
					var i = _g1++;
					if(i != 2) {
						str += "," + js_Boot.__string_rec(o[i],s);
					} else {
						str += js_Boot.__string_rec(o[i],s);
					}
				}
				return str + ")";
			}
			var l = o.length;
			var i1;
			var str1 = "[";
			s += "\t";
			var _g11 = 0;
			var _g2 = l;
			while(_g11 < _g2) {
				var i2 = _g11++;
				str1 += (i2 > 0 ? "," : "") + js_Boot.__string_rec(o[i2],s);
			}
			str1 += "]";
			return str1;
		}
		var tostr;
		try {
			tostr = o.toString;
		} catch( e ) {
			return "???";
		}
		if(tostr != null && tostr != Object.toString && typeof(tostr) == "function") {
			var s2 = o.toString();
			if(s2 != "[object Object]") {
				return s2;
			}
		}
		var k = null;
		var str2 = "{\n";
		s += "\t";
		var hasp = o.hasOwnProperty != null;
		for( var k in o ) {
		if(hasp && !o.hasOwnProperty(k)) {
			continue;
		}
		if(k == "prototype" || k == "__class__" || k == "__super__" || k == "__interfaces__" || k == "__properties__") {
			continue;
		}
		if(str2.length != 2) {
			str2 += ", \n";
		}
		str2 += s + k + " : " + js_Boot.__string_rec(o[k],s);
		}
		s = s.substring(1);
		str2 += "\n" + s + "}";
		return str2;
	case "string":
		return o;
	default:
		return String(o);
	}
};
var js_node_Fs = require("fs");
var js_node_buffer_Buffer = require("buffer").Buffer;
function $iterator(o) { if( o instanceof Array ) return function() { return HxOverrides.iter(o); }; return typeof(o.iterator) == 'function' ? $bind(o,o.iterator) : o.iterator; }
var $_, $fid = 0;
function $bind(o,m) { if( m == null ) return null; if( m.__id__ == null ) m.__id__ = $fid++; var f; if( o.hx__closures__ == null ) o.hx__closures__ = {}; else f = o.hx__closures__[m.__id__]; if( f == null ) { f = function(){ return f.method.apply(f.scope, arguments); }; f.scope = o; f.method = m; o.hx__closures__[m.__id__] = f; } return f; }
String.__name__ = true;
Array.__name__ = true;
Date.__name__ = ["Date"];
var __map_reserved = {};
Main.orderTypes = { Order : 1, Cancelled : 2, ReferenceOnly : 3, Backorder : 4, Dropship : 5};
Main.objectTypes = { Customer : 1, Order : 2, Invoice : 3, LineItem : 4, Product : 5, Warehouse : 6, PurchaseOrder : 7, Supplier : 8, Employee : 9, Address : 10};
Main.amounts = (function($this) {
	var $r;
	var _g = new haxe_ds_StringMap();
	if(__map_reserved["addresses"] != null) {
		_g.setReserved("addresses",10);
	} else {
		_g.h["addresses"] = 10;
	}
	$r = _g;
	return $r;
}(this));
Main.addresses = [];
Main.customers = [];
Main.numeral = require("numeral");
Main.peopleDataObjects = [];
Main.customerDataObjects = [];
Main.supplierDataObjects = [];
Main.employeeDataObjects = [];
Main.productDataObjects = [];
Main.database = { addresses : [], customers : [], employees : [], suppliers : [], warehouses : [], products : [], orders : [], lineItems : [], invoices : [], __meta : { }};
Main.lineItems = 0;
Main.invoices = 0;
Main.orders = 0;
Main.addressCount = 0;
Main.main();
})();
