using MegameterConsole4000.Types.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reflection = System.Reflection;
namespace MegameterConsole4000.Types.Internal
{
    public enum Type
    {
        Variable = 1,
        Function

    }

    public class TypeDescription : Dictionary<string, int>
    {
        public struct MemberDescriptionObject
        {
            public string title;
            public string content;
            public string type;

            public MemberDescriptionObject(string title, string content, string type)
            {
                this.title = title;
                this.content = content;
                this.type = type;
            }
        }
        public MemberDescriptionObject GetMemberDescription(Reflection.MemberInfo member, Type type)
        {
            var retValBuilder = new { title = new StringBuilder(), content = new StringBuilder() };
            var typeName = Enum.GetName(typeof(Type), type).ToLower();
            retValBuilder.title.Append(string.Format("<span class=\"text-primary\">{0}</span> {1}", typeName, member.Name));
            if (type == Type.Function)
            {
                var methodInfo = (Reflection.MethodInfo)member;
                retValBuilder.title.Append('(');
                var parameters = methodInfo.GetParameters();
                var index = 1;
                foreach (var parameter in parameters)
                {

                    retValBuilder.title.Append(string.Format("<span class=\"text-info\">{0}</span> {1}", parameter.ParameterType, parameter.Name));
                    if (index++ != parameters.Length)
                    {
                        retValBuilder.title.Append(',');
                    }
                }
                retValBuilder.title.Append(')');
                retValBuilder.title.Append(string.Format(" <span class=\"text-primary\">returns</span> <span class=\"text-info\">{0}</span>", methodInfo.ReturnType));
            }
            retValBuilder.title.Append(": ");

            var descriptionAttribute = (MemberDescription)Attribute.GetCustomAttribute(member, typeof(MemberDescription));
            if (descriptionAttribute != null)
            {
                retValBuilder.content.Append(descriptionAttribute.Description);
            }
            return new MemberDescriptionObject(retValBuilder.title.ToString(), retValBuilder.content.ToString(), typeName);
        }
        public Dictionary<string, MemberDescriptionObject> MemberDescriptions = new Dictionary<string, MemberDescriptionObject>();
        public TypeDescription(System.Type type)
        {
            var ignoredMembers = new string[]
            {
                "ToString",
                "Equals",
                "GetHashCode",
                "GetType",
                "Finalize",
                "MemberwiseClone",
                ".ctor"
            };
            foreach (var member in
                type.GetMembers(
                    Reflection.BindingFlags.Instance |
                    Reflection.BindingFlags.Public |
                    Reflection.BindingFlags.NonPublic
                ).Where(
                    member => !ignoredMembers.Any(
                                ignoredMember => ignoredMember == member.Name
                                )
                )
            )
            {
                var memberType = member.MemberType == Reflection.MemberTypes.Method ?
                    Type.Function :
                    Type.Variable;
                this.Add(
                    member.Name,
                    (int)memberType
                );
                MemberDescriptions.Add(member.Name, this.GetMemberDescription(member, memberType));
            }

        }
    }
}
