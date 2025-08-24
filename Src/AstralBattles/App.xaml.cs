using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

/*namespace AstralBattles
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}*/

// Decompiled with JetBrains decompiler
// Type: AstralBattles.App
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Infrastructure;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles
{
    public partial class App : Application
    {
        private static readonly ILogger Logger = LogFactory.GetLogger<App>();
        private bool phoneApplicationInitialized;
        private bool _contentLoaded;

        public PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
            this.InitializeComponent();
            this.InitializePhoneApplication();
            if (!Debugger.IsAttached)
                return;
            Application.Current.Host.Settings.EnableFrameRateCounter = false;
            PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode)1;
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            int num = e.IsApplicationInstancePreserved ? 1 : 0;
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            App.Logger.LogWarrning("RootFrame_NavigationFailed: {0}", (object)e.Exception);
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private void Application_UnhandledException(
          object sender,
          ApplicationUnhandledExceptionEventArgs e)
        {
            App.Logger.LogWarrning("Application_UnhandledException: {0}", (object)e.ExceptionObject);
            if (Debugger.IsAttached)
                Debugger.Break();
            int num = (int)MessageBox.Show("Oops! Unhandled exception.");
        }

        private void InitializePhoneApplication()
        {
            if (this.phoneApplicationInitialized)
                return;
            this.RootFrame = new PhoneApplicationFrame();
            ((Frame)this.RootFrame).Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
            ((Frame)this.RootFrame).NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
            this.phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (this.RootVisual != this.RootFrame)
                this.RootVisual = (UIElement)this.RootFrame;
            ((Frame)this.RootFrame).Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
        }

        [DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (this._contentLoaded)
                return;
            this._contentLoaded = true;
            Application.LoadComponent((object)this, new Uri("/AstralBattles;component/App.xaml", UriKind.Relative));
        }
    }
}
