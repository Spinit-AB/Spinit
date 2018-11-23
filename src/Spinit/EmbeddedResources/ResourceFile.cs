using System;
using System.IO;
using System.Reflection;

namespace Spinit.EmbeddedResources
{
    public class ResourceFile
    {
        public ResourceFile(Assembly assembly, string resourceKey)
        {
            Assembly = assembly;
            ResourceKey = resourceKey;
            var resourceInfo = Assembly.GetManifestResourceInfo(resourceKey);
            ResourceLocation = resourceInfo.ResourceLocation;
        }

        public Assembly Assembly { get; protected set; }
        public string ResourceKey { get; protected set; }
        public ResourceLocation ResourceLocation { get; protected set; }

        public virtual string Read()
        {
            return Read(stream =>
            {
                string result;
                using (var reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }

                return result;
            });
        }

        public virtual TResult Read<TResult>(Func<Stream, TResult> reader)
        {
            TResult result;
            using (var stream = Assembly.GetManifestResourceStream(ResourceKey))
            {
                result = reader(stream);
            }

            return result;
        }

        public virtual void Process(Action<Stream> streamAction)
        {
            using (var stream = Assembly.GetManifestResourceStream(ResourceKey))
            {
                streamAction(stream);
            }
        }
    }
}