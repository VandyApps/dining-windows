using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public class CountryCapitals
{
   
   private string _Name;
   
   private string _CityName;
   
   public string Name
   {
      get
      {
         return _Name;
      }
      set
      {
         _Name = value;
      }
   }
   
   public string CityName
   {
      get
      {
         return _CityName;
      }
      set
      {
         _CityName = value;
      }
   }
   
   public IEnumerable<CountryCapitals> GetResults(Countries context)
   {
      return from eachCountry in context.Country
		join eachCity in context.City on  new { Condition0 = eachCountry.Id } equals  new { Condition0 = eachCity.CountryId } 
		 where eachCity.IsCapital  ==  true  
		select new CountryCapitals  {  Name = eachCountry.Name,  CityName = eachCity.Name};
   }
}

