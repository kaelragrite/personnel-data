﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonnelData.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SharedResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SharedResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PersonnelData.Resources.SharedResources", typeof(SharedResources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified City can&apos;t be found in the database..
        /// </summary>
        internal static string CityNotFound {
            get {
                return ResourceManager.GetString("CityNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} field length must be exactly {1} symbols..
        /// </summary>
        internal static string ExactStringLength {
            get {
                return ResourceManager.GetString("ExactStringLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} field can&apos;t contain letters from Georgian and English alphabet at the same time..
        /// </summary>
        internal static string GeoLatinRestriction {
            get {
                return ResourceManager.GetString("GeoLatinRestriction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} field value must be at least 18 years old..
        /// </summary>
        internal static string MinimumAge {
            get {
                return ResourceManager.GetString("MinimumAge", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Specified Person can&apos;t be found in the database..
        /// </summary>
        internal static string PersonNotFound {
            get {
                return ResourceManager.GetString("PersonNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Related Person can&apos;t be found in the database..
        /// </summary>
        internal static string RelatedPersonNotFound {
            get {
                return ResourceManager.GetString("RelatedPersonNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Person relation can&apos;t be found in the database..
        /// </summary>
        internal static string RelationNotFound {
            get {
                return ResourceManager.GetString("RelationNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} field is required..
        /// </summary>
        internal static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} field length must be between {2}-{1} symbols..
        /// </summary>
        internal static string StringLength {
            get {
                return ResourceManager.GetString("StringLength", resourceCulture);
            }
        }
    }
}