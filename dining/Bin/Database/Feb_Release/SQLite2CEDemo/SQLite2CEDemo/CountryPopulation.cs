using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public class CountryPopulation
{
   
   private long _GroupingKeyCountryId;
   
   private System.Nullable<long> _Sum_ComputedColumn0;
   
   public long GroupingKeyCountryId
   {
      get
      {
         return _GroupingKeyCountryId;
      }
      set
      {
         _GroupingKeyCountryId = value;
      }
   }
   
   public System.Nullable<long> Sum_ComputedColumn0
   {
      get
      {
         return _Sum_ComputedColumn0;
      }
      set
      {
         _Sum_ComputedColumn0 = value;
      }
   }
   
   public IEnumerable<CountryPopulation> GetResults(Countries context)
   {
      return from eachCountry in context.Country
		join eachCity in context.City on  new { Condition0 = eachCountry.Id } equals  new { Condition0 = eachCity.CountryId } 
		group new { eachCountry,eachCity } by new { eachCity.CountryId } into grouping
		select new CountryPopulation  {GroupingKeyCountryId = grouping.Key.CountryId,Sum_ComputedColumn0 = grouping.Sum(p => p.eachCity.Population)};
   }
}

