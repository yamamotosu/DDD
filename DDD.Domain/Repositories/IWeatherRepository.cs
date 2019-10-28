
using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Repositories
{
    public interface IWeatherRepository     //インターフェース　IWeatherRepositoryを定義
    {
        WeatherEntity GetLatest(int areaId);
        IReadOnlyList<WeatherEntity> GetData();
        void Save(WeatherEntity weather);
    }
}
