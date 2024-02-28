using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiWeather.Models
{
    public class PronosticoDia
    {
        public string nombreCiudad {  get; set; }

        public string Dia {  get; set; }
        public int TemperaturaMin {  get; set; }

        public int TemperaturaMax { get; set; }
        
       
    }
    public class PronosticoCiudad
    {
        public string nombreCiudad { get; set; }

        public int TemperaturaActual { get; set; }

        public int TemperaturaMinima {  get; set; }

        public int TemperaturaMaxima { get; set; }

        public List<PronosticoDia> PronosticoProximosDias { get; set; }

      
    }

 




}