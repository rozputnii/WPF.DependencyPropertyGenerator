﻿//HintName: MyControl.WeakEvents.Completed.generated.cs
#nullable enable

namespace H.Generators.IntegrationTests
{
    public partial class MyControl
    {
        private class CompletedWeakEventManager : global::System.Windows.WeakEventManager
        {
            private CompletedWeakEventManager()
            {
            }

            public static void AddHandler(object? source, global::System.EventHandler? handler)
            {
                if (source == null)
                    throw new global::System.ArgumentNullException(nameof(source));
                if (handler == null)
                    throw new global::System.ArgumentNullException(nameof(handler));

                CurrentManager.ProtectedAddHandler(source, handler);
            }

            public static void RemoveHandler(object? source, global::System.EventHandler? handler)
            {
                if (source == null)
                    throw new global::System.ArgumentNullException(nameof(source));
                if (handler == null)
                    throw new global::System.ArgumentNullException(nameof(handler));

                CurrentManager.ProtectedRemoveHandler(source, handler);
            }

            internal static CompletedWeakEventManager CurrentManager
            {
                get
                {
                    var managerType = typeof(CompletedWeakEventManager);
                    var manager = (CompletedWeakEventManager)GetCurrentManager(managerType);
                    if (manager == null)
                    {
                        manager = new CompletedWeakEventManager();
                        SetCurrentManager(managerType, manager);
                    }

                    return manager;
                }
            }

            protected override void StartListening(object? source)
            {
                (source as MyControl)!.Completed += OnCompleted;
            }

            protected override void StopListening(object? source)
            {
                (source as MyControl)!.Completed -= OnCompleted;
            }

            internal void OnCompleted(object? sender, global::System.EventArgs args)
            {
                DeliverEvent(sender, args);
            }
        }

        /// <summary>
        /// </summary>
	    public event global::System.EventHandler? Completed
	    {
		    add => CompletedWeakEventManager.AddHandler(null, value);
		    remove => CompletedWeakEventManager.RemoveHandler(null, value);
	    }

        /// <summary>
        /// A helper method to raise the Completed event.
        /// </summary>
	    internal void RaiseCompletedEvent(object? sender)
	    {
		    CompletedWeakEventManager.CurrentManager.OnCompleted(sender, global::System.EventArgs.Empty);
	    }
    }
}