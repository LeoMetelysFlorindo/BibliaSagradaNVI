package crc643a80db4d007afaf0;


public class ActionProvider_ISubUiVisibilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc643a80db4d007afaf0.ActionProvider_ISubUiVisibilityListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSubUiVisibilityChanged:(Z)V:GetOnSubUiVisibilityChanged_ZHandler:Android.Support.V4.View.ActionProvider/ISubUiVisibilityListenerInvoker, Xamarin.Android.Support.Compat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V4.View.ActionProvider+ISubUiVisibilityListenerImplementor, Xamarin.Android.Support.Compat", ActionProvider_ISubUiVisibilityListenerImplementor.class, __md_methods);
	}


	public ActionProvider_ISubUiVisibilityListenerImplementor ()
	{
		super ();
		if (getClass () == ActionProvider_ISubUiVisibilityListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V4.View.ActionProvider+ISubUiVisibilityListenerImplementor, Xamarin.Android.Support.Compat", "", this, new java.lang.Object[] {  });
	}


	public void onSubUiVisibilityChanged (boolean p0)
	{
		n_onSubUiVisibilityChanged (p0);
	}

	private native void n_onSubUiVisibilityChanged (boolean p0);

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
