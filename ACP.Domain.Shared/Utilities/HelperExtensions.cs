﻿namespace ACP.Domain.Shared.Utilities;

public static class Helper
{
    public static T ToEnum<T>(this string value) where T : struct
    {
        var defaultValue = default(T);
        try   
        {   
            T res = (T)Enum.Parse(typeof(T), value, true);

            if (!Enum.IsDefined(typeof(T), res))
            {
                return defaultValue;
            }   
            
            return res;   
        }   
        catch   
        {   
            return defaultValue;   
        }   
    }
    
    public static T ToEnum<T>(this string value, T defaultValue) where T : struct
    {
        try   
        {   
            T res = (T)Enum.Parse(typeof(T), value, true);

            if (!Enum.IsDefined(typeof(T), res))
            {
                return defaultValue;
            }   
            
            return res;   
        }   
        catch   
        {   
            return defaultValue;   
        }   
    }
}