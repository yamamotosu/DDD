﻿using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DDD.Infrastracture.SQLite
{
    public class WeatherSQLite : IWeatherRepository         //IWeatherRepositoryを実装したWeatherSQLite
    { 
        public WeatherEntity GetLatest(int areaId)          //SQL文
        {
            string sql = @"
select DataDate,
       Condition,
       Temperature
from Weather
where AreaId = @AreaId
order by DataDate desc
LIMIT 1
";
            return SQLiteHelper.QuerySingle(        
                sql,
                new List<SQLiteParameter>
                {
                    new SQLiteParameter("@AreaID",areaId)
                }.ToArray(),

                reader =>
                {
                    return new WeatherEntity(                               //戻り値とし、id、日時、状態、温度を読み込む
                             areaId,
                             Convert.ToDateTime(reader["DataDate"]),
                             Convert.ToInt32(reader["Condition"]),
                             Convert.ToSingle(reader["Temperature"]));
                },
                null);
        }

        public IReadOnlyList<WeatherEntity> GetData()       //SQL文　場所名と日時、状態、温度を結合
        {
            string sql = @"
select A.AreaId,
         ifnull(B.AreaName,'') as AreaName,
         A.DataDate,
         A.Condition,
         A.Temperature
from Weather A
left outer join Areas B
on A.AreaId = B.AreaId
";

            return SQLiteHelper.Query(sql,
                reader =>
                {
                    return new WeatherEntity(
                             Convert.ToInt32(reader["AreaId"]),
                             Convert.ToString(reader["AreaName"]),
                             Convert.ToDateTime(reader["DataDate"]),
                             Convert.ToInt32(reader["Condition"]),
                             Convert.ToSingle(reader["Temperature"]));
                });
        }

        public void Save(WeatherEntity weather)
        {                                            //SQL　挿入文
            string insert = @"                     
insert into Weather
(AreaId,DataDate,Condition,Temperature)
values
(@AreaId,@DataDate,@Condition,@Temperature)
";                                                   //SQL   更新文
            string update = @"                      
update Weather 
set Condition = @Condition,
    Temperature = @Temperature
where AreaId = @AreaId
and DataDate = @DataDate
";
            var args = new List<SQLiteParameter>
            {
                new SQLiteParameter("@AreaId",weather.AreaId.Value),
                new SQLiteParameter("@DataDate",weather.DataDate),
                new SQLiteParameter("@Condition",weather.Condition.Value),
                new SQLiteParameter("@Temperature",weather.Temperature.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());           //SQL 挿入文と更新文のパラメータ要素の取得
        }
    }
}
