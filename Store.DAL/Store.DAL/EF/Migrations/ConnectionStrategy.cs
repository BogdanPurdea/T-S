using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Store.DAL.EF
{
    public class ConnectionStrategy : ExecutionStrategy
    {
        public ConnectionStrategy(DbContext context) :
        this(context, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        public ConnectionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) :
            base(context, maxRetryCount, maxRetryDelay)
        {
        }

        public ConnectionStrategy(ExecutionStrategyDependencies dependencies) :
            this(dependencies, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        public ConnectionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) :
            base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            return true;
        }
    }
}
