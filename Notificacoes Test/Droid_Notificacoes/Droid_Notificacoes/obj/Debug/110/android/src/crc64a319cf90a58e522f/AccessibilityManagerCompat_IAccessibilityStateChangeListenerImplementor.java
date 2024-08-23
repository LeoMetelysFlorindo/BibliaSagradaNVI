package crc64a319cf90a58e522f;


public class AccessibilityManagerCompat_IAccessibilityStateChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc64a319cf90a58e522f.AccessibilityManagerCompat_IAccessibilityStateChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAccessibilityStateChanged:(Z)V:GetOnAccessibilityStateChanged_ZHandler:Android.Support.V4.View.Accessibility.AccessibilityManagerCompat/IAccessibilityStateChangeListenerInvoker, Xamarin.Android.Support.Compat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V4.View.Accessibility.AccessibilityManagerCompat+IAccessibilityStateChangeListenerImplementor, Xamarin.Android.Support.Compat", AccessibilityManagerCompat_IAccessibilityStateChangeListenerImplementor.class, __md_methods);
	}


	public AccessibilityManagerCompat_IAccessibilityStateChangeListenerImplementor ()
	{
		super ();
		if (getClass () == AccessibilityManagerCompat_IAccessibilityStateChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V4.View.Accessibility.AccessibilityManagerCompat+IAccessibilityStateChangeListenerImplementor, Xamarin.Android.Support.Compat", "", this, new java.lang.Object[] {  });
	}


	public void onAccessibilityStateChanged (boolean p0)
	{
		n_onAccessibilityStateChanged (p0);
	}

	private native void n_onAccessibilityStateChanged (boolean p0);

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
