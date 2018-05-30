using ElasticSearchApp.Common;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchApp.Accounts
{
	public static class AccountMappingExtensions
	{
		public const string ACCOUNTS_INDEX_NAME = "accounts";
		const string REMOVE_NUMBERS_NORMALIZER_NAME = "removeNumbersNormalizer";
		const string REMOVE_NUMBERS_CHAR_FILTER_NAME = "removeNumberCharFilter";

		public static AnalysisDescriptor CreateRemoveNumbersNormalizer(this AnalysisDescriptor analysisDescriptor)
		{
			return analysisDescriptor.Normalizers(norm => norm
								.Custom(REMOVE_NUMBERS_NORMALIZER_NAME, cstNorm => cstNorm
									 .CharFilters("removeNumbers")));
		}

		public static AnalysisDescriptor CreateRemoveNumbersCharFilters(this AnalysisDescriptor analysisDescriptor)
		{
			return analysisDescriptor.CharFilters(cFilter => cFilter
						   .Mapping(REMOVE_NUMBERS_CHAR_FILTER_NAME, mpf => mpf
								.Mappings("1=>", "2=>", "3=>", "4=>", "5=>", "6=>", "7=>", "8=>", "9=>")));
		}
	}

	class AccountMapping : IndexBuilder<Account>
	{
		public string IndexName { get; } = AccountMappingExtensions.ACCOUNTS_INDEX_NAME;

		public IPromise<IIndexSettings> ConfigureIndexSettings(IndexSettingsDescriptor indexSettingsDescriptor)
		{
			return indexSettingsDescriptor.Analysis(an => an
											.CreateRemoveNumbersCharFilters()
											.CreateRemoveNumbersNormalizer());
		}

		public ITypeMapping MapAccount(TypeMappingDescriptor<Account> typeMappingDescriptor)
		{
			return typeMappingDescriptor.Properties(accountProps => accountProps
											.CreateNumber(account => account.AccountNumber, NumberType.Long)
											.CreateNumber(account => account.Balance, NumberType.Long)
											.CreateTextFieldWithKeyword(account => account.FirstName)
											.CreateTextFieldWithKeyword(account => account.LastName)
											.CreateNumber(account => account.Age, NumberType.Byte)
											.CreateTextFieldWithKeyword(account => account.Gender)
											.CreateTextFieldWithKeyword(account => account.Address)
											.CreateTextFieldWithKeyword(account => account.City)
											.CreateTextFieldWithKeyword(account => account.State)
											.CreateTextFieldWithKeyword(account => account.Employer)
											.CreateTextFieldWithKeyword(account => account.Email));
		}
	}
}
