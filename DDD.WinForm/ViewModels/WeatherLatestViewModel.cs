using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastracture.SQLite;
using DDD.Infrastructure.SQLite;
using System;
using System.ComponentModel;

namespace DDD.WinForm.ViewModels
{
    public class WeatherLatestViewModel : ViewModelBase     // WeatherLatestViewModelはViewModelBaseを継承している
    {
        private IWeatherRepository _weather;                // _weatherの定義
        IAreasRepository _areas;                            //　_areasの定義

        public WeatherLatestViewModel() : this(new WeatherSQLite(), new AreasSQLite())
        {
        }

        public WeatherLatestViewModel(IWeatherRepository weather,
            IAreasRepository areas)
        {
            _weather = weather;             //weatherを _weatherに代入
            _areas = areas;                 //areasを _areasに代入

            foreach(var area in _areas.GetData())           //foreachループ
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }
        }

        private object _selectedAreaIdText;
        public object SelectedAreaId
        {
            get { return _selectedAreaIdText; }
            set
            { 
                SetProperty(ref _selectedAreaIdText, value);
            }
        }

        public string _dataDateText = string.Empty;
        public string DataDateText
        {
            get { return _dataDateText; }
            set
            {
                SetProperty(ref _dataDateText, value);
            }
        }

        public string _conditionText = string.Empty;
        public string ConditionText
        {
            get { return _conditionText; }
            set
            {
                SetProperty(ref _conditionText, value);
            }
        }
        public string _temperatureText = string.Empty;
        public string TemperatureText
        {
            get { return _temperatureText; }
            set
            {
                SetProperty(ref _temperatureText, value);
            }
        }

        public BindingList<AreaEntity> Areas { get; set; }
        = new BindingList<AreaEntity>();

        public void Search()
        {
            var entity = _weather.GetLatest(Convert.ToInt32(_selectedAreaIdText));
            if (entity == null)                     //テキストボックスに何も入力されていなければ
            {
                DataDateText = string.Empty;        //空文字を日時テキストに代入
                ConditionText = string.Empty;       //空文字を状態テキストに代入
                TemperatureText = string.Empty;     //空文字を温度テキストに代入
            }
            else                                        //nullでないなら
            { 
                DataDateText = entity.DataDate.ToString();                          //入力された文字列インスタンスを表示
                ConditionText = entity.Condition.DisplayValue;                      //入力された天気の状態を表示
                TemperatureText = entity.Temperature.DisplayValueWithUnitSpace;     //入力された温度を表示
            }
        }
    }
}
