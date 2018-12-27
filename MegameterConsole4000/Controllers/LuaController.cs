using System;
using MegameterConsole4000.LuaLINQ;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegameterConsole.Types;
using NLua;
using System.Web.Mvc;
using MegameterConsole4000.Types.Enums;
using MegameterConsole4000.Database;
using MegameterConsole4000.Types.Internal;

namespace MegameterConsole.Controllers
{
    public struct Result
    {
        public int Code;
        public string Message;
        public object Data;
        public Result(int Code, string Message, object Data)
        {
            this.Code = Code;
            this.Message = Message;
            this.Data = Data;
        }
        public bool OK()
        {
            return this.Code > 199 && this.Code < 299;
        }

    }
    public class LuaController : Controller
    {
        public Dictionary<int, Lua> Sessions
        {
            get
            {
                return Database.Store(HttpContext).Sessions;
            }
            set
            {
                Database.Store(HttpContext).Sessions = value;
            }
        }
        private Dictionary<int, System.Type> TYPE_MAP = new Dictionary<int, System.Type>()
        {

        };

        public JsonResult Result(int Code = 200, string Message = "OK", object Data = null)
        {
            return Json(new Result(Code, Message, Data));
        }
        public LuaController()
        {
            var types = new System.Type[]
            {
                typeof(Customer),
                typeof(Order),
                typeof(Invoice),
                typeof(LineItem),
                typeof(Product),
                typeof(Warehouse),
                typeof(PurchaseOrder),
                typeof(Supplier),
                typeof(Employee),
                typeof(Address)
            };
            var index = 0;
            foreach (var type in types)
            {
                TYPE_MAP.Add(++index, type);
            }
        }

        private System.Type GetType(ObjectTypes type)
        {
            return TYPE_MAP[(int)type];
        }



        public new JsonResult Json(object data)
        {
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult StartSession()
        {
            var ID = Sessions.Count;
            Sessions.Add(ID, new Lua());
            Sessions[ID].LoadCLRPackage();
            Sessions[ID].DoFile(HttpContext.Server.MapPath("~/MyLua.lua"));
            return Result(Data: ID);
        }
        public ActionResult RequestSessionState(ObjectTypes type, int ID, int sessionID, string statePath)
        {
            if (!Sessions.ContainsKey(sessionID))
            {
                return new HttpUnauthorizedResult();
            }
            var session = Sessions[sessionID];

            if (!HasType(type))
            {
                return new HttpNotFoundResult();
            }
            var stateType = this.GetType(type);


            return Json(new { state = session[statePath] });


        }
        public ActionResult CloseSession(int sessionID)
        {
            if (!Sessions.ContainsKey(sessionID))
            {
                return new HttpUnauthorizedResult();
            }
            Sessions[sessionID].Close();
            Sessions.Remove(sessionID);
            return Result();
        }
        private bool HasType(ObjectTypes type)
        {
            return Enum.GetValues(typeof(ObjectTypes)).Length >= (int)type
                        &&
                    type > 0;
        }
        public ActionResult RequestState(string script, ObjectTypes type, int sessionID)
        {
            if (!Sessions.ContainsKey(sessionID))
            {
                return new HttpUnauthorizedResult();
            }
            var session = Sessions[sessionID];
            session.DoString(script);
            if (!HasType(type))
            {
                return Result(400, "Bad Request (type not found)");
            }
            if (session["where"] == null || session["set"] == null)
            {
                return Result(400, "Bad Request (missing 'where' or 'set' values)");
            }
            var scriptFunc = session["where"] as LuaFunction;
            Func<object, bool> predicate = delegate (object input)
            {
                bool result = false;
                try
                {
                    result = (bool)scriptFunc.Call(input)[0];
                } catch(Exception e)
                {
                    
                }
                return result;
            };
            var statePath = (string)session["set"];
            session["set"] = null;

            switch (type)
            {
                case ObjectTypes.Address:

                    session[statePath] = Database.LWhere<Address>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Invoice:
                    session[statePath] = Database.LWhere<Invoice>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Order:
                    session[statePath] = Database.LWhere<Order>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Product:
                    session[statePath] = Database.LWhere<Product>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Customer:
                    session[statePath] = Database.LWhere<Customer>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Warehouse:
                    session[statePath] = Database.LWhere<Warehouse>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Employee:
                    session[statePath] = Database.LWhere<Employee>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.Supplier:
                    session[statePath] = Database.LWhere<Supplier>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                case ObjectTypes.LineItem:
                    session[statePath] = Database.LWhere<LineItem>(scriptFunc).ToLuaEnumerable();
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
                default:
                    session[statePath] = null;
                    return Result(200, "OK", new
                    {
                        data = session[statePath],
                        meta = new TypeDescription(session[statePath].GetType()).MemberDescriptions,
                        statePath
                    });
            }

        }
        public ActionResult REPL(string script, int sessionID)
        {
            if (!Sessions.ContainsKey(sessionID))
            {
                return new HttpUnauthorizedResult();
            }
            var session = Sessions[sessionID];
         
            object result = null;
            try
            {
                result = session.DoString(script);
            } catch(Exception e)
            {
                return Result(500, "ERROR", new { e.Message, e.Data, e.Source });
            }
            return Json(result);
        }
        [Route("GetObject")]

        public ActionResult GetObject(ObjectTypes type)
        {
            Func<JsonResult> executeJsonResult = delegate ()
            {
                var typeDescription = new MegameterConsole4000.Types.Internal.TypeDescription(
                               this.GetType(type)
                           );
                return Json(
                     new
                     {
                         members = typeDescription,
                         descriptions = typeDescription.MemberDescriptions
                     }
                    );
            };
            return
                (
                    HasType(type)
                ) ?
                executeJsonResult()
                 :
                (ActionResult)new HttpNotFoundResult();
        }
    }
}