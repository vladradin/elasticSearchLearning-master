using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchApp
{
    interface IndexBuilder<T>
		where T:class
    {
		string IndexName { get; }
		IPromise<IIndexSettings> ConfigureIndexSettings(IndexSettingsDescriptor indexSettingsDescriptor);
		ITypeMapping MapAccount(TypeMappingDescriptor<T> typeMappingDescriptor);
	}
}
