using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace WebAPI.Data.Models;

public abstract class Model
{
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> DatabaseGeneratedProperties = new();
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new();
    private static readonly ConcurrentDictionary<RuntimeTypeHandle, object?> DefaultValues = new();
    private int? _requestedHashCode;

    private static IEnumerable<PropertyInfo> GetDatabaseGeneratedProperties(Type type)
    {
        return DatabaseGeneratedProperties.GetOrAdd(type.TypeHandle, typeHandle =>
        {
            return type.GetProperties().Where(p => p.GetCustomAttributes<DatabaseGeneratedAttribute>(true).Any(a => a.DatabaseGeneratedOption != DatabaseGeneratedOption.None)).ToArray();
        });
    }

    private static IEnumerable<PropertyInfo> GetKeyProperties(Type type)
    {
        return KeyProperties.GetOrAdd(type.TypeHandle, typeHandle =>
        {
            return type.GetProperties().Where(p => p.GetCustomAttributes<KeyAttribute>(true).Any()).ToArray();
        });
    }

    private static object? GetDefaultValues(Type type)
    {
        return DefaultValues.GetOrAdd(type.TypeHandle, typeHandle =>
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        });
    }

    private static bool IsDefaultValue(object? value)
    {
        if (value == null)
        {
            return true;
        }
        return value.Equals(GetDefaultValues(value.GetType()));
    }

    public bool IsTransient()
    {
        var generatedProperties = GetDatabaseGeneratedProperties(GetType());

        if (!generatedProperties.Any())
        {
            return false;
        }

        if (generatedProperties.Any(p => p.GetValue(this) == null))
        {
            return true;
        }

        return generatedProperties.Any(p => IsDefaultValue(p.GetValue(this)));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Model model)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        if (model.IsTransient() || IsTransient())
        {
            return false;
        }
        else
        {
            var keyProperties = GetKeyProperties(GetType());
            if (keyProperties.Any(p => p.GetValue(this) == null))
            {
                return true;
            }

            foreach (var keyProperty in keyProperties)
            {
                var thisValue = keyProperty.GetValue(this);
                if (thisValue == null)
                {
                    return false;
                }

                if (!thisValue.Equals(keyProperty.GetValue(model)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
            {
                var keyProperties = GetKeyProperties(GetType());
                if (keyProperties.Any(p => p.GetValue(this) == null))
                {
                    return base.GetHashCode();
                }

                foreach (var keyProperty in keyProperties)
                {
                    var thisValue = keyProperty.GetValue(this);
                    if (thisValue == null)
                    {
                        return base.GetHashCode();
                    }

                    var hashCode = thisValue.GetHashCode() ^ 31;

                    if (_requestedHashCode.HasValue)
                    {
                        _requestedHashCode ^= hashCode;
                    }
                    else
                    {

                        _requestedHashCode = hashCode;
                    }
                }
            }
            return _requestedHashCode ?? base.GetHashCode();
        }
        else
        {
            return base.GetHashCode();
        }
    }

    public static bool operator ==(Model left, Model right)
    {
        if(Equals(left, null))
        {
            return Equals(right, null);
        }
        else
        {
            return left.Equals(right);
        }
    }

    public static bool operator !=(Model left, Model right)
    {
        return !(left == right);
    }
}
