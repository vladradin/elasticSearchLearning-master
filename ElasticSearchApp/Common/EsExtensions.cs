using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElasticSearchApp.Common
{
	public static class EsExtensions
	{
		public static PropertiesDescriptor<T> CreateTextFieldWithKeyword<T>(this PropertiesDescriptor<T> textDescriptor, Expression<Func<T, object>> fieldPath)
			where T : class
		{
			return textDescriptor.Text(nameDesc => nameDesc
									 .Name(fieldPath)
									 .Fields(f => f
										 .Keyword(pk => pk
											 .Name("keyword")
											 .Normalizer("removeNumbers")
											 .IgnoreAbove(256))));
		}

		public static PropertiesDescriptor<T> CreateTextFieldWithKeyword<T>(this PropertiesDescriptor<T> textDescriptor, string fieldPath)
			where T : class
		{
			return textDescriptor.Text(nameDesc => nameDesc
									 .Name(fieldPath)
									 .Fields(f => f
										 .Keyword(pk => pk
											 .Name("keyword")
											 .Normalizer("removeNumbers")
											 .IgnoreAbove(256))));
		}

		public static PropertiesDescriptor<T> CreateTextFieldWithKeyword<T>(this PropertiesDescriptor<T> textDescriptor, Expression<Func<T, object>> fieldPath, string fieldName)
			where T : class
		{
			return textDescriptor.Text(nameDesc => nameDesc
									 .Name(fieldPath)
									 .Fields(f => f
										 .Keyword(pk => pk
											 .Name("keyword")
											 .Normalizer("removeNumbers")
											 .IgnoreAbove(256))));
		}

		public static PropertiesDescriptor<T> CreateNumber<T>(this PropertiesDescriptor<T> textDescriptor, Expression<Func<T, object>> fieldPath, NumberType numberType = NumberType.Double)
				 where T : class
		{
			return textDescriptor.Number(nameDesc => nameDesc
									 .Name(fieldPath)
									 .Type(numberType)
									 .Fields(f => f
										.Number(n => n
											.Name("keyword")
											.Type(numberType))));
		}		
	}
}
