package crc643216196a6070c252;


public class ActionBar_IOnNavigationListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc643216196a6070c252.ActionBar_IOnNavigationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNavigationItemSelected:(IJ)Z:GetOnNavigationItemSelected_IJHandler:Android.Support.V7.App.ActionBar/IOnNavigationListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V7.App.ActionBar+IOnNavigationListenerImplementor, Xamarin.Android.Support.v7.AppCompat", ActionBar_IOnNavigationListenerImplementor.class, __md_methods);
	}


	public ActionBar_IOnNavigationListenerImplementor ()
	{
		super ();
		if (getClass () == ActionBar_IOnNavigationListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V7.App.ActionBar+IOnNavigationListenerImplementor, Xamarin.Android.Support.v7.AppCompat", "", this, new java.lang.Object[] {  });
	}


	public boolean onNavigationItemSelected (int p0, long p1)
	{
		return n_onNavigationItemSelected (p0, p1);
	}

	private native boolean n_onNavigationItemSelected (int p0, long p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
