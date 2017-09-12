using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DistCqrs.Core.Command;
using DistCqrs.Core.Resolve;
using DistCqrs.Sample.Domain;

namespace DistCqrs.Sample.Service.Resolve
{
    public class RootTypeResolver : IRootTypeResolver
    {
        private readonly IDictionary<Type, Type> mappings;

        public RootTypeResolver()
        {
            mappings = new Dictionary<Type, Type>();

            var allCmds = this.GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(BaseCommand)));

            foreach (var cmdType in allCmds)
            {
                var entityNamespace = cmdType.Namespace.Replace("Commands", "");

                var tokens = entityNamespace.Split('.');
                var entityTypeFullName = entityNamespace + tokens[tokens.Length - 1];

                var type = Type.GetType(entityTypeFullName);
                mappings.Add(type,cmdType);
            }
        }

        public Type GetRootType(ICommand cmd)
        {
            return mappings[cmd.GetType()];
        }
    }
}
