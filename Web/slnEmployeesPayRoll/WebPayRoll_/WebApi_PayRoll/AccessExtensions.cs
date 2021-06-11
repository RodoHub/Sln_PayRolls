﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace WebPayRoll_
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;
    using Models;

    /// <summary>
    /// Extension methods for Access.
    /// </summary>
    public static partial class AccessExtensions
    {
            /// <summary>
            /// Logins a User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='loginEntity'>
            /// Entity for parametizing this action
            /// </param>
            public static Responser Login(this IAccess operations, AMLoginEntity loginEntity)
            {
                return Task.Factory.StartNew(s => ((IAccess)s).LoginAsync(loginEntity), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Logins a User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='loginEntity'>
            /// Entity for parametizing this action
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Responser> LoginAsync(this IAccess operations, AMLoginEntity loginEntity, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.LoginWithHttpMessagesAsync(loginEntity, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Logs Off a User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// ID
            /// </param>
            public static Responser LogOff(this IAccess operations, string id)
            {
                return Task.Factory.StartNew(s => ((IAccess)s).LogOffAsync(id), operations, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Logs Off a User
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// ID
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Responser> LogOffAsync(this IAccess operations, string id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.LogOffWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
