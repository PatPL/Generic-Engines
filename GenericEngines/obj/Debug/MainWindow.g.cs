﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2BD3547FBC33916662DA41A9F20B805553DC5251"
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
            
            #line 29 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 29 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 30 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 30 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removeButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 31 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 31 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.saveButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 32 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 32 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.saveasButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 33 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 33 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.openButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 34 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.exportButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.mainDataGrid_BeginningEdit);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.mainDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.mainDataGrid_KeyUp);
            
            #line default
            #line hidden
            
            #line 35 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.mainDataGrid_Loaded);
            
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
            case 8:
            
            #line 127 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 127 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addPropellantButton_MouseUp);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 128 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 128 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removePropellantButton_MouseUp);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 129 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.propellentDataGrid_BeginningEdit);
            
            #line default
            #line hidden
            
            #line 129 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.propellentDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 129 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.propellentDataGrid_KeyUp);
            
            #line default
            #line hidden
            
            #line 129 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.propellentDataGrid_Loaded);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 134 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.propellantCombo_Loaded);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

