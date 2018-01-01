using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.WebUI.Tests.Helpers
{
    class SessionMock : ISession
    {
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();

        public IEnumerable<string> Keys => throw new NotImplementedException();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync()
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value)
        {
            
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            value = null;
            return false;
        }
    }
}
