using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cet.Core
{
    public static class ReflectionHelpers
    {

        //see: http://stackoverflow.com/questions/5461295/using-isassignablefrom-with-open-generic-types
        public static bool IsAssignableToGenericType(
            Type givenType,
            Type genericType
            )
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (it.GetTypeInfo().IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.GetTypeInfo().IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type? baseType = givenType.GetTypeInfo().BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }


        public static bool IsDictionary(Type test)
        {
            if (test.IsGenericType)
            {
                Type[] args = test.GetGenericArguments();
                if (args.Length == 2)
                {
                    var tt = typeof(Dictionary<,>).MakeGenericType(args);
                    return test.IsAssignableFrom(tt);
                }
            }
            return false;
        }


        public static bool IsNullable(Type test)
        {
            return
                test.IsValueType &&
                test.IsGenericType &&
                test.GetGenericTypeDefinition() == typeof(Nullable<>);
        }


        public static object? GetValue(
            object? source,
            string path
            )
        {
            if (path.Contains('.'))
            {
                string[] parts = path.Split('.');

                for (int i = 0, length = parts.Length; i < length; i++)
                {
                    if (parts[i].Length > 0)
                    {
                        source = calc(source, parts[i]);
                    }
                }
            }
            else
            {
                source = calc(source, path);
            }
            return source;


            object? calc(object? input, string segment)
            {
                object? output = null;
                if (input != null)
                {
                    Type t = input.GetType();
                    PropertyInfo? pi = t.GetRuntimeProperty(segment);
                    if (pi != null)
                    {
                        output = pi.GetValue(input);
                    }
                    else
                    {
                        FieldInfo? fi = t.GetRuntimeField(segment);
                        if (fi != null)
                        {
                            output = fi.GetValue(input);
                        }
                        else
                        {
                            throw new NotSupportedException("Data access not supported.");
                        }
                    }
                }
                return output;
            }
        }


        public static bool TryGetValue(
            object? source,
            string path,
            out object? result
            )
        {
            //TODO: cercare di prevenire l'errore invece di catturarlo
            try
            {
                result = GetValue(source, path);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

    }
}
