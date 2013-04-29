namespace CodePlex.Elmah
{
	#region Imports

	using System;

	using BaseDebug = System.Diagnostics.Debug;
    using ConditionalAttribute = System.Diagnostics.ConditionalAttribute;

	#endregion

    /// <summary>
    /// Provides methods for assertions and debugging help that is mostly 
    /// applicable during development.
    /// </summary>
	
    internal sealed class Debug
	{
        [ Conditional("DEBUG") ]
        public static void Assert(bool condition)
        {
            BaseDebug.Assert(condition);
        }

        [ Conditional("DEBUG") ]
        public static void AssertStringNotEmpty(string s)
        {
            BaseDebug.Assert(StringEtc.MaskNull(s).Length != 0);
        }
        
        private Debug() {}
	}
}
