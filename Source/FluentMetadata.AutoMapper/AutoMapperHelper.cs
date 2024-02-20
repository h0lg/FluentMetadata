using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace FluentMetadata.AutoMapper
{
    /// <summary>
    /// Helpers for getting 
    /// </summary>
    public static class MapperConfigurationExtensions
    {
        public static Func<MapperConfiguration> GetMapperConfiguration = () =>
        {
            var getConfig = $"{nameof(MapperConfigurationExtensions)}.{nameof(GetMapperConfiguration)}";

            throw new NotImplementedException($"Before using {getConfig}, initialize it with a function giving " +
                $"{typeof(MapperConfigurationExtensions).Namespace} access to your " +
                $"{typeof(MapperConfiguration).Namespace}.{nameof(MapperConfiguration)} " +
                $"somewhat like this:{Environment.NewLine + getConfig} = () => yourAutoMapperConfig;");
        };

        /// <summary>
        /// Gets the mapped members for a source/destination type pair.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        /// <param name="source">The source Type.</param>
        /// <param name="destination">The destination Type.</param>
        /// <returns></returns>
        public static IEnumerable<MemberMap> GetMemberMapsOf(this MapperConfiguration config, Type source, Type destination)
        {
            foreach (var propertyMap in config.GetRelevantMappedMembersOf(source, destination))
            {
                if (propertyMap.SourceMember != null)
                {
                    yield return new MemberMap
                    {
                        SourceName = propertyMap.SourceMembers.Count > 1 ?
                            propertyMap.SourceMembers.Aggregate(string.Empty, (result, svr) => result + svr.Name) :
                            propertyMap.SourceMember.Name,
                        DestinationName = propertyMap.DestinationName
                    };
                }
                else if (propertyMap.ValueResolverConfig != null)
                {
                    yield return new MemberMap
                    {
                        SourceName = propertyMap.ValueResolverConfig.SourceMemberName
                            ?? ExpressionHelper.GetPropertyName(propertyMap.ValueResolverConfig.SourceMember),
                        DestinationName = propertyMap.DestinationName
                    };
                }
            }
        }

        /// <summary>
        /// Gets the mapped members for a source/destination type pair
        /// leaving out mapped members that are irrelevant.
        /// </summary>
        /// <param name="config">The mapping configuration.</param>
        /// <param name="source">The source type.</param>
        /// <param name="destination">The destination type.</param>
        /// <returns></returns>
        private static IEnumerable<PropertyMap> GetRelevantMappedMembersOf(this MapperConfiguration config, Type source, Type destination)
        {
            var typeMap = config.FindTypeMapFor(source, destination);
            // filter by non-ignored PropertyMaps
            return typeMap != null ? typeMap.PropertyMaps.Where(m => !m.Ignored) : Enumerable.Empty<PropertyMap>();
        }
    }
}