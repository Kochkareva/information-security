﻿#pragma checksum "..\..\..\CreateUserWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64CEDFF39C591300440AD44F9A3BD57A9C88CEF5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace IB_LW_1 {
    
    
    /// <summary>
    /// CreateUserWindow
    /// </summary>
    public partial class CreateUserWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxLogin;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxMinPasswordLength;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxPasswordExpiration;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxBlocking;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxLowercaseLetters;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxUppercaseLetters;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxNumbers;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\CreateUserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxPunctuationMarks;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IB_LW_1;component/createuserwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CreateUserWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextBoxLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TextBoxMinPasswordLength = ((System.Windows.Controls.TextBox)(target));
            
            #line 46 "..\..\..\CreateUserWindow.xaml"
            this.TextBoxMinPasswordLength.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextBoxPasswordExpiration = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\..\CreateUserWindow.xaml"
            this.TextBoxPasswordExpiration.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.checkBoxBlocking = ((System.Windows.Controls.CheckBox)(target));
            
            #line 68 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxBlocking.Unchecked += new System.Windows.RoutedEventHandler(this.checkBoxBlocking_Unchecked);
            
            #line default
            #line hidden
            
            #line 68 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxBlocking.Checked += new System.Windows.RoutedEventHandler(this.checkBoxBlocking_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.checkBoxLowercaseLetters = ((System.Windows.Controls.CheckBox)(target));
            
            #line 73 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxLowercaseLetters.Unchecked += new System.Windows.RoutedEventHandler(this.checkBoxLowercaseLetters_Unchecked);
            
            #line default
            #line hidden
            
            #line 73 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxLowercaseLetters.Checked += new System.Windows.RoutedEventHandler(this.checkBoxLowercaseLetters_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.checkBoxUppercaseLetters = ((System.Windows.Controls.CheckBox)(target));
            
            #line 78 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxUppercaseLetters.Unchecked += new System.Windows.RoutedEventHandler(this.checkBoxUppercaseLetters_Unchecked);
            
            #line default
            #line hidden
            
            #line 78 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxUppercaseLetters.Checked += new System.Windows.RoutedEventHandler(this.checkBoxUppercaseLetters_Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.checkBoxNumbers = ((System.Windows.Controls.CheckBox)(target));
            
            #line 83 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxNumbers.Unchecked += new System.Windows.RoutedEventHandler(this.checkBoxNumbers_Unchecked);
            
            #line default
            #line hidden
            
            #line 83 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxNumbers.Checked += new System.Windows.RoutedEventHandler(this.checkBoxNumbers_Checked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.checkBoxPunctuationMarks = ((System.Windows.Controls.CheckBox)(target));
            
            #line 88 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxPunctuationMarks.Unchecked += new System.Windows.RoutedEventHandler(this.checkBoxPunctuationMarks_Unchecked);
            
            #line default
            #line hidden
            
            #line 88 "..\..\..\CreateUserWindow.xaml"
            this.checkBoxPunctuationMarks.Checked += new System.Windows.RoutedEventHandler(this.checkBoxPunctuationMarks_Checked);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 91 "..\..\..\CreateUserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSave_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 114 "..\..\..\CreateUserWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

