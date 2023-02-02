﻿//HintName: MyGrid.WeakEvents.Completed.generated.cs
#nullable enable

namespace H.Generators.IntegrationTests
{
    public partial class MyGrid
    {
	    private global::Microsoft.Maui.WeakEventManager CompletedWeakEventManager { get; } = new global::Microsoft.Maui.WeakEventManager();
        /// <summary>
        /// </summary>
	    public event global::System.EventHandler? Completed
	    {
		    add => CompletedWeakEventManager.AddEventHandler(value);
		    remove => CompletedWeakEventManager.RemoveEventHandler(value);
	    }

        /// <summary>
        /// A helper method to raise the Completed event.
        /// </summary>
	    internal void RaiseCompletedEvent(object? sender)
	    {
		    CompletedWeakEventManager.HandleEvent(sender!, global::System.EventArgs.Empty!, eventName: nameof(Completed));
	    }
    }
}