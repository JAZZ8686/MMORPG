using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtil{
    /// <summary>
    /// 把string转换成int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
	public static int ToInt(this string str)
    {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }

    /// <summary>
    /// 把string转换成float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp = 0;
        float.TryParse(str, out temp);
        return temp;
    }
}
