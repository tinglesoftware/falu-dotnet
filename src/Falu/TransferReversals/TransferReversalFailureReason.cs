﻿using System.Runtime.Serialization;

namespace Falu.TransferReversals
{
    /// <summary>
    /// Reason for failure of a transfer reversal.
    /// </summary>
    public enum TransferReversalFailureReason
    {
        ///
        Unknown,

        ///
        [EnumMember(Value = "insufficient_balance")]
        InsufficientBalance,

        ///
        [EnumMember(Value = "authentication_error")]
        AuthenticationError,

        ///
        Timeout,

        ///
        Other,
    }
}