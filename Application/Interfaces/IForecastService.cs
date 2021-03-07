using Application.DTO;
using Countries.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IForecastService
    {
        WeatherForecastDTO GetForecasts();
    }
}
