using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DistCqrs.Core.Command;
using DistCqrs.Core.Domain;

namespace DistCqrs.Core.Resolve.Helpers
{
    public class ResolveUtils 
    {
        public static IDictionary<Type, Type> GetCommandToEntityMappings(Assembly[] assemblies)
        {
            var mappings = new Dictionary<Type, Type>();

            var allHandlers = assemblies.SelectMany(
                a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Any(
                               i => i.GenericTypeArguments.Length > 0 &&
                                    i.GetGenericTypeDefinition() ==
                                    typeof(ICommandHandler<,>))));

            var allCmds = assemblies.SelectMany(
                a => a.GetTypes()
                    .Where(t => t.GetInterfaces()
                               .Contains(typeof(ICommand)))).ToList();

            foreach (var handlerType in allHandlers)
            {
                var handlerInterfaceType = handlerType.GetInterfaces().Single(
                    i => i.GenericTypeArguments.Length > 0 &&
                         i.GetGenericTypeDefinition() ==
                         typeof(ICommandHandler<,>));

                foreach (var cmdType in allCmds)
                {
                    if (handlerInterfaceType.GenericTypeArguments[1] == cmdType)
                    {
                        mappings.Add(cmdType,handlerInterfaceType.GenericTypeArguments[0]);
                    }
                }
            }

            return mappings;
        }

        public static IList<Tuple<Type, Type, Type>> GetCommandHandlerMappings(
            Assembly[] assemblies)
        {
            var mappings = new List<Tuple<Type, Type, Type>>();

            var allHandlers = assemblies.SelectMany(
                a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Any(
                               i => i.GenericTypeArguments.Length > 0 &&
                                    i.GetGenericTypeDefinition() ==
                                    typeof(ICommandHandler<,>))));

            foreach (var handlerType in allHandlers)
            {
                var handlerInterfaceType = handlerType.GetInterfaces().Single(
                    i => i.GenericTypeArguments.Length > 0 &&
                         i.GetGenericTypeDefinition() ==
                         typeof(ICommandHandler<,>));

                mappings.Add(new Tuple<Type, Type, Type>(
                    handlerInterfaceType.GenericTypeArguments[0],
                    handlerInterfaceType.GenericTypeArguments[1],
                    handlerType));
            }

            return mappings;
        }

        public static IList<Tuple<Type, Type, Type>> GetEventHandlerMappings(
            Assembly[] assemblies)
        {
            var mappings = new List<Tuple<Type, Type, Type>>();

            var allHandlers = assemblies.SelectMany(
                a => a.GetTypes()
                    .Where(t => t.GetInterfaces().Any(
                               i => i.GenericTypeArguments.Length > 0 &&
                                    i.GetGenericTypeDefinition() ==
                                    typeof(IEventHandler<,>))));

            foreach (var handlerType in allHandlers)
            {
                var handlerInterfaceType = handlerType.GetInterfaces().Single(
                    i => i.GenericTypeArguments.Length > 0 &&
                         i.GetGenericTypeDefinition() ==
                         typeof(IEventHandler<,>));

                mappings.Add(new Tuple<Type, Type, Type>(
                    handlerInterfaceType.GenericTypeArguments[0],
                    handlerInterfaceType.GenericTypeArguments[1],
                    handlerType));
            }

            return mappings;
        }
    }
}
