using System;
using SitePoint.Cookbook.Configuration;

namespace SitePoint.Cookbook.Configuration
{
	/// <summary>
	/// Example of a settings class that can reload itself when 
	/// the config file changes.
	/// </summary>
public class MySettings
{
	public float Foo
	{
		get { return this.foo; }
		set { this.foo = value; }
	}

	float foo;

	public string Bar
	{
		get { return this.bar; }
		set { this.bar = value; }
	}

	string bar;

		#region Optional Static Helper
		//This section is optional, but I often do something like this 
		//as a convenience. It makes accessing the settings easy. Ex...
		//	MyStuff.Settings.Bar
		//Make sure the section name in the config matches with the name used here.
		const string SECTION_NAME = "MyStuff";
		static MySettings _settings = (MySettings)System.Configuration.ConfigurationManager.GetSection("MySettings");
		
		public static MySettings Settings
		{
			get
			{
				return _settings;
			}
		}
		#endregion
	}

}
