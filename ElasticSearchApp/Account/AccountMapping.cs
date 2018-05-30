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
