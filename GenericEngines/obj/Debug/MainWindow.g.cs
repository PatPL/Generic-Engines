﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "878F38DD4C50CB0D6D7BF6EFC6E09DD8CF2D5B8E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using GenericEngines;
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


namespace GenericEngines {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GenericEngines;component/mainwindow.xaml", System.UriKind.Relative);
            
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainWindow.xaml"
            ((GenericEngines.MainWindow)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.Window_KeyUp);
            
            #line default
            #line hidden
            
            #line 8 "..\..\MainWindow.xaml"
            ((GenericEngines.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 248 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 248 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NewButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 249 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 249 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.OpenButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 250 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 250 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AppendButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 251 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 251 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SaveButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 252 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 252 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SaveasButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 253 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 253 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ValidateButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 254 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 254 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ExportButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 255 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 255 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.DuplicateButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 256 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 256 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AddButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 257 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 257 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.RemoveButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 258 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 258 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.HelpButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 259 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 259 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SettingsButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 261 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.MainDataGrid_PreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 261 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.MainDataGrid_BeginningEdit);
            
            #line default
            #line hidden
            
            #line 261 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.MainDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 261 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.MainDataGrid_KeyUp);
            
            #line default
            #line hidden
            
            #line 261 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainDataGrid_Loaded);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 15:
            
            #line 381 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MasterIDComboBox_Loaded);
            
            #line default
            #line hidden
            
            #line 381 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).DropDownOpened += new System.EventHandler(this.MasterIDComboBox_OpenDropbox);
            
            #line default
            #line hidden
            break;
            case 16:
            
            #line 418 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.EngineTypeCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 17:
            
            #line 572 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 572 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AddGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 18:
            
            #line 573 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 573 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.RemoveGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 19:
            
            #line 577 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 20:
            
            #line 582 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.PropellantCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 21:
            
            #line 725 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.PlumeCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 22:
            
            #line 869 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.TechComboBox_PreviewKeyUp);
            
            #line default
            #line hidden
            break;
            case 23:
            
            #line 1026 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 1026 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AddGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 24:
            
            #line 1027 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 1027 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.RemoveGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 25:
            
            #line 1028 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 1028 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SortThrustCurve_MouseUp);
            
            #line default
            #line hidden
            break;
            case 26:
            
            #line 1029 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 27:
            
            #line 1114 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.TanksVolumeInput_PrewiewKeyDown);
            
            #line default
            #line hidden
            break;
            case 28:
            
            #line 1119 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 1119 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AddGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 29:
            
            #line 1120 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RegisterMouseDown);
            
            #line default
            #line hidden
            
            #line 1120 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.RemoveGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 30:
            
            #line 1122 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 31:
            
            #line 1127 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.PropellantCombo_Loaded);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

