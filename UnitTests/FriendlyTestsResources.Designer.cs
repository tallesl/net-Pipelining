﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PipeliningLibrary.UnitTests {
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
    internal class FriendlyTestsResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FriendlyTestsResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PipeliningLibrary.UnitTests.FriendlyTestsResources", typeof(FriendlyTestsResources).Assembly);
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
        ///   Looks up a localized string similar to {
        ///  &quot;Id&quot;: null,
        ///  &quot;Output&quot;: null,
        ///  &quot;Success&quot;: false,
        ///  &quot;ElapsedTime&quot;: &quot;00:00:00&quot;,
        ///  &quot;Pipes&quot;: []
        ///}.
        /// </summary>
        internal static string Empty {
            get {
                return ResourceManager.GetString("Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Id&quot;: &quot;some_id&quot;,
        ///  &quot;Output&quot;: &quot;The object is not seriazable (System.Type.IsSerializable).&quot;,
        ///  &quot;Success&quot;: true,
        ///  &quot;ElapsedTime&quot;: &quot;00:00:01&quot;,
        ///  &quot;Pipes&quot;: [
        ///    {
        ///      &quot;Pipe&quot;: &quot;PipeliningLibrary.UnitTests.BubbleSortPipe&quot;,
        ///      &quot;Started&quot;: &quot;2017-06-05T15:16:17&quot;,
        ///      &quot;Ended&quot;: &quot;2017-06-05T15:16:18&quot;,
        ///      &quot;Exception&quot;: null,
        ///      &quot;Position&quot;: 0
        ///    }
        ///  ]
        ///}.
        /// </summary>
        internal static string NotSerializable {
            get {
                return ResourceManager.GetString("NotSerializable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Id&quot;: &quot;some_id&quot;,
        ///  &quot;Output&quot;: {
        ///    &quot;Serializable&quot;: 123,
        ///    &quot;NotSerializable&quot;: &quot;The object is not seriazable (System.Type.IsSerializable).&quot;
        ///  },
        ///  &quot;Success&quot;: true,
        ///  &quot;ElapsedTime&quot;: &quot;00:00:01&quot;,
        ///  &quot;Pipes&quot;: [
        ///    {
        ///      &quot;Pipe&quot;: &quot;PipeliningLibrary.UnitTests.BubbleSortPipe&quot;,
        ///      &quot;Started&quot;: &quot;2017-06-05T15:16:17&quot;,
        ///      &quot;Ended&quot;: &quot;2017-06-05T15:16:18&quot;,
        ///      &quot;Exception&quot;: null,
        ///      &quot;Position&quot;: 0
        ///    }
        ///  ]
        ///}.
        /// </summary>
        internal static string NotSerializableExpando {
            get {
                return ResourceManager.GetString("NotSerializableExpando", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;Id&quot;: &quot;some_id&quot;,
        ///  &quot;Output&quot;: [
        ///    1,
        ///    2,
        ///    3
        ///  ],
        ///  &quot;Success&quot;: true,
        ///  &quot;ElapsedTime&quot;: &quot;00:00:01&quot;,
        ///  &quot;Pipes&quot;: [
        ///    {
        ///      &quot;Pipe&quot;: &quot;PipeliningLibrary.UnitTests.BubbleSortPipe&quot;,
        ///      &quot;Started&quot;: &quot;2017-06-05T15:16:17&quot;,
        ///      &quot;Ended&quot;: &quot;2017-06-05T15:16:18&quot;,
        ///      &quot;Exception&quot;: null,
        ///      &quot;Position&quot;: 0
        ///    }
        ///  ]
        ///}.
        /// </summary>
        internal static string Serializable {
            get {
                return ResourceManager.GetString("Serializable", resourceCulture);
            }
        }
    }
}