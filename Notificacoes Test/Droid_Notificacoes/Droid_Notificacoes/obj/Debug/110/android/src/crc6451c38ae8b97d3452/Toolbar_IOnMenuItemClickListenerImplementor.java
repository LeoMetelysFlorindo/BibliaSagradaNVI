package crc6451c38ae8b97d3452;


public class Toolbar_IOnMenuItemClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc6451c38ae8b97d3452.Toolbar_IOnMenuItemClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMenuItemClick:(Landroid/view/MenuItem;)Z:GetOnMenuItemClick_Landroid_view_MenuItem_Handler:Android.Support.V7.Widget.Toolbar/IOnMenuItemClickListenerInvoker, Xamarin.Android.Support.v7.AppCompat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V7.Widget.Toolbar+IOnMenuItemClickListenerImplementor, Xamarin.Android.Support.v7.AppCompat", Toolbar_IOnMenuItemClickListenerImplementor.class, __md_methods);
	}


	public Toolbar_IOnMenuItemClickListenerImplementor ()
	{
		super ();
		if (getClass () == Toolbar_IOnMenuItemClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V7.Widget.Toolbar+IOnMenuItemClickListenerImplementor, Xamarin.Android.Support.v7.AppCompat", "", this, new java.lang.Object[] {  });
	}


	public boolean onMenuItemClick (android.view.MenuItem p0)
	{
		return n_onMenuItemClick (p0);
	}

	private native boolean n_onMenuItemClick (android.view.MenuItem p0);

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
