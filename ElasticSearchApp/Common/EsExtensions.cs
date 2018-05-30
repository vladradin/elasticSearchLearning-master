using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ElasticSearchApp.Common
{
	public static class EsExtensions
	{
		const string REMOVE_NUMBERS_NORMALIZER_NAME = "removeNumbersNormalizer";
		const string REMOVE_NUMBERS_CHAR_FILTER_NAME = "removeNumberCharFilter";

		public static AnalysisDescriptor CreateRemoveNumbersNormalizer(this AnalysisDescriptor analysisDescriptor)
		{
			return analysisDescriptor.Normalizers(norm => norm
								.Custom(REMOVE_NUMBERS_NORMALIZER_NAME, cstNorm => cstNorm
									 .CharFilters(REMOVE_NUMBERS_CHAR_FILTER_NAME)));
		}

		public static AnalysisDescriptor CreateRemoveNumbersCharFilters(this AnalysisDescriptor analysisDescriptor)
		{
			return analysisDescriptor.CharFilters(cFilter => cFilter
						   .Mapping(REMOVE_NUMBERS_CHAR_FILTER_NAME, mpf => mpf
								.Mappings("1=>", "2=>", "3=>", "4=>", "5=>", "6=>", "7=>", "8=>", "9=>")));
		}

		public static PropertiesDescriptor<T> CreateTextFieldWithKeyword<T>(this PropertiesDescriptor<T> textDescriptor, Expression<Func<T, object>> fieldPath)
			where T : class
		{
			return textDescriptor.Text(nameDesc => nameDesc
									 .Name(fieldPath)
									 .Fields(f => f
										 .Keyword(pk => pk
											 .Name("keyword")
											 .Normalizer(REMOVE_NUMBERS_NORMALIZER_NAME)
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
											 .Normalizer(REMOVE_NUMBERS_NORMALIZER_NAME)
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
											 .Normalizer(REMOVE_NUMBERS_NORMALIZER_NAME)
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
