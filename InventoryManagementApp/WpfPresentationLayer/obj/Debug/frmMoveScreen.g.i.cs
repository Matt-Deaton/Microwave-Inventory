﻿#pragma checksum "..\..\frmMoveScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3DB26D246091808EB3447E44530308C6735E87F2DD64F64B97D4686865D7D604"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfPresentationLayer;


namespace WpfPresentationLayer {
    
    
    /// <summary>
    /// frmMoveScreen
    /// </summary>
    public partial class frmMoveScreen : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPartNumber;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPartName;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtLocationFrom;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantityAvaiable;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantityMoving;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNewLocation;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\frmMoveScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfPresentationLayer;component/frmmovescreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmMoveScreen.xaml"
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
            
            #line 8 "..\..\frmMoveScreen.xaml"
            ((WpfPresentationLayer.frmMoveScreen)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtPartNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtPartName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtLocationFrom = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtQuantityAvaiable = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtQuantityMoving = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\frmMoveScreen.xaml"
            this.txtQuantityMoving.GotKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.TxtQuantityMoving_GotKeyboardFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtNewLocation = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\frmMoveScreen.xaml"
            this.txtNewLocation.GotKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.TxtNewLocation_GotKeyboardFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\frmMoveScreen.xaml"
            this.btnConfirm.Click += new System.Windows.RoutedEventHandler(this.BtnConfirm_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\frmMoveScreen.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.BtnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

