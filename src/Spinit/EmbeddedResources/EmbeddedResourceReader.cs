using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Spinit.EmbeddedResources
{
    public class EmbeddedResourceReader : IEnumerable<ResourceFile>
    {
        protected EmbeddedResourceReader(Assembly assembly)
        {
            Assembly = assembly;
            Resources = new List<ResourceFile>();
            AllResourceNames = Assembly.GetManifestResourceNames();
            AssemblyName = Assembly.GetName()
                                   .Name;
        }

        public int Count
        {
            get { return Resources.Count; }
        }

        protected Assembly Assembly { get; set; }

        protected IList<string> AllResourceNames { get; set; }

        protected string AssemblyName { get; set; }

        protected IList<ResourceFile> Resources { get; set; }

        public static EmbeddedResourceReader Create()
        {
            return new EmbeddedResourceReader(Assembly.GetEntryAssembly());
        }

        public static EmbeddedResourceReader Create(Assembly assembly)
        {
            return new EmbeddedResourceReader(assembly);
        }

        public static EmbeddedResourceReader CreateFromType<T>()
        {
            return new EmbeddedResourceReader(typeof(T).GetTypeInfo().Assembly);
        }

        public IEnumerator<ResourceFile> GetEnumerator()
        {
            return Resources.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual EmbeddedResourceReader AddResourceFilesMatchingNamespace(params string[] namespaceSegments)
        {
            var namespaceToMatch = GetFullNamespace(namespaceSegments);
            var result = AllResourceNames.Where(x => x.StartsWith(namespaceToMatch))
                                         .OrderBy(x => x);
            foreach (var item in result)
            {
                Resources.Add(new ResourceFile(Assembly, item));
            }

            return this;
        }

        public virtual EmbeddedResourceReader AddResourceFilesMatchingRegexPattern(string pattern)
        {
            var result = AllResourceNames.Where(x => Regex.IsMatch(x, pattern))
                                         .OrderBy(x => x);
            foreach (var item in result)
            {
                Resources.Add(new ResourceFile(Assembly, item));
            }

            return this;
        }

        public virtual EmbeddedResourceReader AddResourceFilesWhereResourceKeyMatches(Func<string, bool> predicate)
        {
            var result = AllResourceNames.Where(predicate)
                                         .OrderBy(x => x);
            foreach (var item in result)
            {
                Resources.Add(new ResourceFile(Assembly, item));
            }

            return this;
        }

        public virtual IEnumerable<string> Read()
        {
            return Resources.Select(x => x.Read());
        }

        public virtual IEnumerable<TResult> Read<TResult>(Func<Stream, TResult> resourceReader)
        {
            return Resources.Select(x => x.Read(resourceReader));
        }

        public virtual void ProcessAll(Action<Stream> action)
        {
            foreach (var resourceFile in Resources)
            {
                resourceFile.Process(action);
            }
        }

        protected virtual string GetFullNamespace(params string[] namespaceSegments)
        {
            var relativeNamespace = string.Join(".", namespaceSegments);
            return string.Format("{0}.{1}", AssemblyName, relativeNamespace);
        }
    }
}