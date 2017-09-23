using System;
using System.Collections.Generic;
using System.Reflection;
using DistCqrs.Core.Command;
using DistCqrs.Core.Resolve;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Sample.Domain;

namespace DistCqrs.Sample.Service.Resolve
{
    public class RootTypeResolver : IRootTypeResolver
    {
        private readonly IDictionary<Type, Type> mappings;

        public RootTypeResolver()
        {
            this.mappings = new Dictionary<Type, Type>();

            var assemblies = new[] {typeof(BaseCommand).GetTypeInfo().Assembly};
            var commandToEntityMappings =
                ResolveUtils.GetCommandToEntityMappings(assemblies);

            foreach (var mapping in commandToEntityMappings)
            {
                mappings.Add(mapping.CommandType, mapping.EntityType);
            }
        }

        public Type GetRootType(ICommand cmd)
        {
            return mappings[cmd.GetType()];
        }
    }
}