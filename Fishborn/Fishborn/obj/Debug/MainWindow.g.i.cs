#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A0F8061315D8222ECACAC37EC73FFB8A0AAF3FC2E3BA88872171705EA6C2E71B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Fishborn;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Fishborn
{


    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 17 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Field;

#line default
#line hidden


#line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Fishlist;

#line default
#line hidden


#line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListAllFish;

#line default
#line hidden


#line 25 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Scrollbar;

#line default
#line hidden


#line 39 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button restart;

#line default
#line hidden


#line 72 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exit;

#line default
#line hidden


#line 106 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button start;

#line default
#line hidden


#line 139 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pause;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Fishborn;component/mainwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.GameScreen = ((Fishborn.MainWindow)(target));
                    return;
                case 2:
                    this.Field = ((System.Windows.Controls.Canvas)(target));
                    return;
                case 3:
                    this.Fishlist = ((System.Windows.Controls.Canvas)(target));
                    return;
                case 4:
                    this.ListAllFish = ((System.Windows.Controls.ListBox)(target));
                    return;
                case 5:
                    this.Scrollbar = ((System.Windows.Controls.Canvas)(target));
                    return;
                case 6:
                    this.restart = ((System.Windows.Controls.Button)(target));

#line 39 "..\..\MainWindow.xaml"
                    this.restart.Click += new System.Windows.RoutedEventHandler(this.restart_Click);

#line default
#line hidden
                    return;
                case 7:
                    this.exit = ((System.Windows.Controls.Button)(target));

#line 72 "..\..\MainWindow.xaml"
                    this.exit.Click += new System.Windows.RoutedEventHandler(this.exit_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.start = ((System.Windows.Controls.Button)(target));

#line 106 "..\..\MainWindow.xaml"
                    this.start.Click += new System.Windows.RoutedEventHandler(this.start_Click);

#line default
#line hidden
                    return;
                case 9:
                    this.pause = ((System.Windows.Controls.Button)(target));

#line 139 "..\..\MainWindow.xaml"
                    this.pause.Click += new System.Windows.RoutedEventHandler(this.pause_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Window GameScreen;
        internal System.Windows.Controls.Slider Survivor;
        internal System.Windows.Controls.Slider CountStage;
        internal System.Windows.Controls.Slider TimeStage;
        internal System.Windows.Controls.Slider SpeedSim;
    }
}

