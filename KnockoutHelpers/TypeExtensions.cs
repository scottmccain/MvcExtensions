﻿using System;

namespace KnockoutHelpers
{
    public static class TypeExtensions
    {
        public static bool IsNumericType(this Type type)
        {
            while (true)
            {
                if (type == null)
                {
                    return false;
                }

                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Byte:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.SByte:
                    case TypeCode.Single:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return true;
                    case TypeCode.Object:
                        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof (Nullable<>)) return false;
                        type = Nullable.GetUnderlyingType(type);
                        continue;
                }
                return false;
            }
        }
    }
}