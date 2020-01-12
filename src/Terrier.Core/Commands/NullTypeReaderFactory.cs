using Finite.Commands;
using System;

namespace Terrier.Commands
{
    public class NullTypeReaderFactory : ITypeReaderFactory
    {
        public bool TryGetTypeReader<T>(out ITypeReader<T> reader)
        {
            reader = default;
            return false;
        }

        public bool TryGetTypeReader(Type valueType, out ITypeReader reader)
        {
            reader = default;
            return false;
        }
    }
}
