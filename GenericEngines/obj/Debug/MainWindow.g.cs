﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "188DF652D97D6B3AB4E7AA6A1157CA08FCE870C6"
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
            ((GenericEngines.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.mainWindow_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 168 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 168 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.newButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 169 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 169 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.openButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 170 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 170 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.appendButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 171 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 171 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.saveButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 172 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 172 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.saveasButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 173 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 173 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.exportButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 174 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 174 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.duplicateButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 175 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 175 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 176 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 176 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removeButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 177 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 177 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.helpButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 178 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 178 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.settingsButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 179 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.mainDataGrid_BeginningEdit);
            
            #line default
            #line hidden
            
            #line 179 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.mainDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 179 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.mainDataGrid_KeyUp);
            
            #line default
            #line hidden
            
            #line 179 "..\..\MainWindow.xaml"
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
            case 14:
            
            #line 256 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.engineTypeCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 15:
            
            #line 399 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 399 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 16:
            
            #line 400 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 400 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removeGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 17:
            
            #line 404 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 18:
            
            #line 409 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.propellantCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 19:
            
            #line 509 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.plumeCombo_Loaded);
            
            #line default
            #line hidden
            break;
            case 20:
            
            #line 610 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).PreviewKeyUp += new System.Windows.Input.KeyEventHandler(this.techComboBox_PreviewKeyUp);
            
            #line default
            #line hidden
            break;
            case 21:
            
            #line 729 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 729 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 22:
            
            #line 730 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 730 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removeGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 23:
            
            #line 731 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 731 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.sortThrustCurve_MouseUp);
            
            #line default
            #line hidden
            break;
            case 24:
            
            #line 732 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 25:
            
            #line 789 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 789 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.addGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 26:
            
            #line 790 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.registerMouseDown);
            
            #line default
            #line hidden
            
            #line 790 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.removeGD_MouseUp);
            
            #line default
            #line hidden
            break;
            case 27:
            
            #line 794 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.DataGrid)(target)).Loaded += new System.Windows.RoutedEventHandler(this.GD_Loaded);
            
            #line default
            #line hidden
            break;
            case 28:
            
            #line 799 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.ComboBox)(target)).Loaded += new System.Windows.RoutedEventHandler(this.propellantCombo_Loaded);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

