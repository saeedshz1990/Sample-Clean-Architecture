using System;


public static class Convertor
{
    public static short ToShort(this object input)
    {
        short result = 0;
        if (input != null)
            short.TryParse(input.ToString(), out result);
        return result;
    }

    public static int ToInt(this object input)
    {
        int result = 0;
        if (input != null)
            int.TryParse(input.ToString(), out result);
        return result;
    }
    public static decimal ToDecimal(this object input)
    {
        decimal result = 0;
        if (input != null)
            decimal.TryParse(input.ToString(), out result);
        return result;
    }
    public static long ToLong(this object input)
    {
        long result = 0;
        if (input != null)
            long.TryParse(input.ToString(), out result);
        return result;
    }

    public static int ToIntRemoveComma(this object input)
    {
        int result = 0;
        if (input != null)
            int.TryParse(input.ToString().Replace(",", ""), out result);
        return result;
    }



    public static float ToFloat(this object input)
    {
        float result = 0;
        if (input != null)
            float.TryParse(input.ToString(), out result);
        return result;
    }

    public static byte ToByte(this object input)
    {
        byte result = 0;
        if (input != null)
            byte.TryParse(input.ToString(), out result);
        return result;
    }

    public static bool ToBool(this object input)
    {
        bool result = false;
        if (input != null)
            bool.TryParse(input.ToString(), out result);
        return result;
    }

    public static string ToHex(this object input)
    {
        return string.Format("{0:X}", input);
    }
    public static DateTime ToDateTime(this object input)
    {
        DateTime result = DateTime.Now;
        if (input != null)
            DateTime.TryParse(input.ToString(), out result);
        return result;
    }

    public static DateTime ToDate(this object input)
    {
        DateTime result = DateTime.Now;
        if (input != null)
            DateTime.TryParse(input.ToString(), out result);
        return result.Date;
    }

}

