﻿#pragma checksum "..\..\MainPageManager.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "34657952AB23179E5D40B090AC273AD7D934FD7CF9A975229C429B0655B8EB01"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
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


namespace UsersApp {
    
    
    /// <summary>
    /// AuthWindow
    /// </summary>
    public partial class AuthWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 92 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnStatistics;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProjects;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTasks;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnProfile;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEmployee;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ContentGrid;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\MainPageManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl MainContentControl;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/UsersApp;component/mainpagemanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainPageManager.xaml"
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
            
            #line 47 "..\..\MainPageManager.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BorderMouse);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 57 "..\..\MainPageManager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnStatistics = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\MainPageManager.xaml"
            this.btnStatistics.Click += new System.Windows.RoutedEventHandler(this.BtnStatistics_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnProjects = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\MainPageManager.xaml"
            this.btnProjects.Click += new System.Windows.RoutedEventHandler(this.BtnProjects_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnTasks = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\MainPageManager.xaml"
            this.btnTasks.Click += new System.Windows.RoutedEventHandler(this.BtnTasks_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnProfile = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\MainPageManager.xaml"
            this.btnProfile.Click += new System.Windows.RoutedEventHandler(this.BtnProfile_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnEmployee = ((System.Windows.Controls.Button)(target));
            
            #line 119 "..\..\MainPageManager.xaml"
            this.btnEmployee.Click += new System.Windows.RoutedEventHandler(this.BtnProfile_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ContentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.MainContentControl = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

