using DDD.Domain.Helpers;

namespace DDD.Domain.ValueObjects
{
    public sealed class Temperature : ValueObject<Temperature>
    {
        public const string UnitName = "℃";         //文字列定数　℃をUnitNameに代入
        public const int DecimalPoint = 2;          //数値定数　2をDecimalPointに代入
        public Temperature(float value)
        {
            Value = value;                          //valueを引数に定義し、Valueに代入
        }

        public float Value { get; }
        public string DisplayValue
        {
            get
            {
                return Value.RoundString(DecimalPoint);     //小数点以下2桁を丸め切り捨て表示
            }
        }

        public string DisplayValueWithUnit                 
        {
            get
            {
                return Value.RoundString(DecimalPoint)      //上記コメントに　+ ℃ を入力し表示
                    + UnitName;
            }
        }

        public string DisplayValueWithUnitSpace
        {
            get
            {
                return Value.RoundString(DecimalPoint)      //上記コメントに　空文字を　加え　+　℃を入力し表示
                    + " " + UnitName;
            }
        }
        protected override bool EqualsCore(Temperature other)
        {
            return Value == other.Value;                    //戻り値のValueとotherのValueが等価がどうかを判定
        }
    }
}
