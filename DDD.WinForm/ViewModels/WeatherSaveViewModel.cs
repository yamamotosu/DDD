using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Helpers;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;
using DDD.Infrastracture.SQLite;
using DDD.Infrastructure.SQLite;
using System;
using System.ComponentModel;

namespace DDD.WinForm.ViewModels
{
    public class WeatherSaveViewModel : ViewModelBase       //WeatherSaveViewModelはViewModelBaseを継承している
    {
        private IWeatherRepository _weather;
        private IAreasRepository _areas;

        public WeatherSaveViewModel()
            : this(new WeatherSQLite(), new AreasSQLite())
        {
        }

        public WeatherSaveViewModel(                        //コンストラクタに引数１、weatherと、2、areasを定義
            IWeatherRepository weather,
            IAreasRepository areas)
        {
            _weather = weather;
            _areas = areas;

            DataDateValue = GetDateTime();
            SelectedCondition = Condition.Sunny.Value;
            TemperatureText = string.Empty;

            foreach (var area in _areas.GetData())      //foreachループ
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }
        }
        public object SelectedAreaId { get; set; }
        public DateTime DataDateValue { get; set; }
        public object SelectedCondition { get; set; }
        public string TemperatureText { get; set; }
        public BindingList<AreaEntity> Areas { get; set; }
         = new BindingList<AreaEntity>();
        public BindingList<Condition> Conditions { get; set; }
         = new BindingList<Condition>(Condition.ToList());

        public string TemperatureUnitName => Temperature.UnitName;

        public void Save()
        {
            Guard.IsNull(SelectedAreaId, "エリアを選択してください");   //エリアが何も選択されてないなら、エリアを選択してくださいを表示
            var temperature =
                Guard.IsFloat(TemperatureText, "温度の入力に誤りがあります");    //Float型なら、温度の入力に誤りがありますを表示

            var entity = new WeatherEntity(
                Convert.ToInt32(SelectedAreaId),
                DataDateValue,
                Convert.ToInt32(SelectedCondition),
                temperature);


           _weather.Save(entity);                                           
        }
    }
}
