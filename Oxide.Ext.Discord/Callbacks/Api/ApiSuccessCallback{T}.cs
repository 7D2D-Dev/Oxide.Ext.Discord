using System;
using Oxide.Ext.Discord.Entities.Api;
using Oxide.Ext.Discord.Pooling;
using Oxide.Ext.Discord.Rest.Requests;

namespace Oxide.Ext.Discord.Callbacks.Api
{
    internal class ApiSuccessCallback<T> : BaseApiCallback
    {
        private Action<T> _onSuccess;
        private T _data;

        public void Init(Request<T> request, RequestResponse response)
        {
            base.Init(request);
            _onSuccess = request.OnSuccess;
            _data = response.ParseData<T>();
        }

        protected override void HandleApiCallback()
        {
            _onSuccess.Invoke(_data);
        }

        ///<inheritdoc/>
        protected override void DisposeInternal()
        {
            DiscordPool.Free(this);
        }
        
        ///<inheritdoc/>
        protected override void EnterPool()
        {
            base.EnterPool();
            _data = default(T);
            _onSuccess = null;
        }
    }
}