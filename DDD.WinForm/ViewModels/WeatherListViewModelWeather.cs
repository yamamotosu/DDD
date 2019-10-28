using DDD.Domain.Entities;

namespace DDD.WinForm.ViewModels
{
    public sealed class WeatherListViewModelWeather
    {
        private WeatherEntity _entity;      // WeatherEntityを省略した　_entityを定義
        public WeatherListViewModelWeather(WeatherEntity entity)    //引数entityを、_entityに代入
        {
            _entity = entity;
        }

        public string AreaId => _entity.AreaId.DisplayValue;        //　_entityのAreaIdの値を実装
        public string AreaName => _entity.AreaName;                 //　_entityの地域名を実装
        public string DataDate => _entity.DataDate.ToString();      //　_entityの日付名を実装
        public string Condition => _entity.Condition.DisplayValue;  //  _entityの状態（晴れ、雨、曇り、不明）の実装
        public string Temperature => _entity.Temperature.DisplayValueWithUnitSpace;     //　_entityの温度を実装
    }
}
