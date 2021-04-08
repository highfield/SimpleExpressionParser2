using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet.Core.Expression
{
    public static class XSolverHelpers
    {

        public static bool AsBool(object? data)
        {
            switch (data)
            {
                case null: throw new NullReferenceException(nameof(AsBool));
                case bool b: return b;
                case double d: return d != 0;
                case int i: return i != 0;
                case DateTime dt: throw new InvalidCastException($"Cannot convert a {typeof(DateTime).Name} to a {typeof(bool).Name}.");
                case DateTimeOffset dto: throw new InvalidCastException($"Cannot convert a {typeof(DateTimeOffset).Name} to a {typeof(bool).Name}.");
                case string s: return s.Length != 0;
                default: return default(bool);
            }
        }


        public static double AsDouble(object? data)
        {
            switch (data)
            {
                case null: throw new NullReferenceException(nameof(AsDouble));
                case bool b: return b ? 1.0 : 0.0;
                case double d: return d;
                case int i: return i;
                case DateTime dt: throw new InvalidCastException($"Cannot convert a {typeof(DateTime).Name} to a {typeof(double).Name}.");
                case DateTimeOffset dto: throw new InvalidCastException($"Cannot convert a {typeof(DateTimeOffset).Name} to a {typeof(double).Name}.");
                case string s: double.TryParse(s, out double value); return value;
                default: return default(double);
            }
        }


        public static bool Match(object? da, object? db)
        {
            if (da == null || db == null)
            {
                return da == null && db == null;
            }
            else if (da is bool && db is bool)
            {
                return (bool)da == (bool)db;
            }
            else if (da is double && db is double)
            {
                return (double)da == (double)db;
            }
            else if (da is int && db is int)
            {
                return (int)da == (int)db;
            }
            else if (da is string && db is string)
            {
                return (string)da == (string)db;
            }
            else if (da is DateTime && db is DateTime)
            {
                return (DateTime)da == (DateTime)db;
            }
            else if (da is DateTimeOffset && db is DateTimeOffset)
            {
                return (DateTimeOffset)da == (DateTimeOffset)db;
            }
            else
            {
                double na = AsDouble(da);
                double nb = AsDouble(db);
                return na == nb;
            }
        }


        public static int Compare(object? da, object? db)
        {
            if (da == null || db == null)
            {
                if (da == null)
                {
                    return db == null ? 0 : -1;
                }
                else
                {
                    return 1;
                }
            }
            else if (da is bool && db is bool)
            {
                return ((bool)da).CompareTo((bool)db);
            }
            else if (da is double && db is double)
            {
                return ((double)da).CompareTo((double)db);
            }
            else if (da is int && db is int)
            {
                return ((int)da).CompareTo((int)db);
            }
            else if (da is string && db is string)
            {
                return ((string)da).CompareTo((string)db);
            }
            else if (da is DateTime && db is DateTime)
            {
                return ((DateTime)da).CompareTo((DateTime)db);
            }
            else if (da is DateTimeOffset && db is DateTimeOffset)
            {
                return ((DateTimeOffset)da).CompareTo((DateTimeOffset)db);
            }
            else
            {
                double na = AsDouble(da);
                double nb = AsDouble(db);
                return na.CompareTo(nb);
            }
        }

    }
}
