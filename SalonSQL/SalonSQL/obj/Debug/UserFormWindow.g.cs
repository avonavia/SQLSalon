﻿#pragma checksum "..\..\UserFormWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F97FB8A9CB8F69E02B9E5B696C08DFD13AF3FA737B5E53464AEE3630348F9FFA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using SalonSQL;
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


namespace SalonSQL {
    
    
    /// <summary>
    /// UserFormWindow
    /// </summary>
    public partial class UserFormWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox First_name_box;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Last_name_box;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Enter_button;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Gender_button_m;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton Gender_button_zh;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Back_button;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\UserFormWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Surname_box;
        
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
            System.Uri resourceLocater = new System.Uri("/SalonSQL;component/userformwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserFormWindow.xaml"
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
            this.First_name_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.Last_name_box = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Enter_button = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\UserFormWindow.xaml"
            this.Enter_button.Click += new System.Windows.RoutedEventHandler(this.Enter_button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Gender_button_m = ((System.Windows.Controls.RadioButton)(target));
            
            #line 18 "..\..\UserFormWindow.xaml"
            this.Gender_button_m.Checked += new System.Windows.RoutedEventHandler(this.Gender_button_m_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Gender_button_zh = ((System.Windows.Controls.RadioButton)(target));
            
            #line 19 "..\..\UserFormWindow.xaml"
            this.Gender_button_zh.Checked += new System.Windows.RoutedEventHandler(this.Gender_button_zh_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Back_button = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\UserFormWindow.xaml"
            this.Back_button.Click += new System.Windows.RoutedEventHandler(this.Back_button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Surname_box = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
