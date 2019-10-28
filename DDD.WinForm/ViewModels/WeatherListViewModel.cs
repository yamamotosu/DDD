using DDD.Domain.Repositories;
using DDD.Infrastracture.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public sealed class WeatherListViewModel : ViewModelBase        //WeatherListViewModelはViewModelBaseを継承している
    {
        private IWeatherRepository _weather;

        public WeatherListViewModel()
            :this(new WeatherSQLite())
        {
        }

        public WeatherListViewModel(IWeatherRepository weather)     
        {
            _weather = weather;

            foreach(var entity in _weather.GetData())               //foreachループ
            {
                Weathers.Add(new WeatherListViewModelWeather(entity));
            }
        }

        public BindingList<WeatherListViewModelWeather>
            Weathers
        { get; set; }
            = new BindingList<WeatherListViewModelWeather>();
    }
}
