﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Visa.Resources.uk_UA {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class uk_UA {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal uk_UA() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Visa.Resources.uk_UA.uk-UA", typeof(uk_UA).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Випевніть капчу і натисніть ТУТ..
        /// </summary>
        public static string FillCaptchaAndPress {
            get {
                return ResourceManager.GetString("FillCaptchaAndPress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Результати пошуку.
        /// </summary>
        public static string SearchResult {
            get {
                return ResourceManager.GetString("SearchResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сервер не відповідає. Щоб поторити спробу запустіть перевірку знову!.
        /// </summary>
        public static string ServerError {
            get {
                return ResourceManager.GetString("ServerError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Будь-ласка випевніть візовий ценрт і візову категорію.
        /// </summary>
        public static string ValidationError_Message {
            get {
                return ResourceManager.GetString("ValidationError_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Валідація не пройдена.
        /// </summary>
        public static string ValidationError_Title {
            get {
                return ResourceManager.GetString("ValidationError_Title", resourceCulture);
            }
        }
    }
}