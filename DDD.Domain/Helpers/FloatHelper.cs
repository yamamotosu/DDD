using System;

namespace DDD.Domain.Helpers
{
    public static class FloatHelper
    {
        public static string RoundString(this float value, int decimalPoint)        //入力された値を、小数点以下2桁切り捨て表示する丸め文字の定義
        {
            var temp = Convert.ToSingle(Math.Round(value, decimalPoint));           
            return temp.ToString("F" + decimalPoint);
        }
    }
}
