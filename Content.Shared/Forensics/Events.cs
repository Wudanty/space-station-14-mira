using Content.Shared.DoAfter;
using Content.Shared.Inventory;
using Robust.Shared.Serialization;

namespace Content.Shared.Forensics;

[ByRefEvent]
public readonly record struct ForensicScannerBeforeDoAfterEvent;

[Serializable, NetSerializable]
public sealed partial class ForensicScannerDoAfterEvent : SimpleDoAfterEvent
{
}

[Serializable, NetSerializable]
public sealed partial class ForensicPadDoAfterEvent : DoAfterEvent
{
    [DataField("sample", required: true)] public  string Sample = default!;

    private ForensicPadDoAfterEvent()
    {
    }

    public ForensicPadDoAfterEvent(string sample)
    {
        Sample = sample;
    }

    public override DoAfterEvent Clone() => this;
}

[Serializable, NetSerializable]
public sealed partial class CleanForensicsDoAfterEvent : SimpleDoAfterEvent
{
}

/// <summary>
/// An event to apply DNA evidence from a donor onto some recipient.
/// </summary>
[ByRefEvent]
public record struct TransferDnaEvent()
{
    /// <summary>
    /// The entity donating the DNA.
    /// </summary>
    public EntityUid Donor;

    /// <summary>
    /// The entity receiving the DNA.
    /// </summary>
    public EntityUid Recipient;

    /// <summary>
    /// Can the DNA be cleaned off?
    /// </summary>
    public bool CanDnaBeCleaned = true;
}

/// <summary>
/// Raised on an entity when its DNA has been changed.
/// </summary>
[ByRefEvent]
public record struct GenerateDnaEvent()
{
    /// <summary>
    /// The entity getting new DNA.
    /// </summary>
    public EntityUid Owner;

    /// <summary>
    /// The generated DNA.
    /// </summary>
    public required string DNA;
}

/// <summary>
/// An event to check if the fingerprint is accessible.
/// </summary>
public sealed class TryAccessFingerprintEvent : CancellableEntityEventArgs, IInventoryRelayEvent
{
    SlotFlags IInventoryRelayEvent.TargetSlots => SlotFlags.WITHOUT_POCKET;

    /// <summary>
    ///     Entity that blocked access.
    /// </summary>
    public EntityUid? Blocker;
}
