package crc64a8ec59975fb7360a;


public class AsyncLayoutInflater_IOnInflateFinishedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc64a8ec59975fb7360a.AsyncLayoutInflater_IOnInflateFinishedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInflateFinished:(Landroid/view/View;ILandroid/view/ViewGroup;)V:GetOnInflateFinished_Landroid_view_View_ILandroid_view_ViewGroup_Handler:Android.Support.V4.View.AsyncLayoutInflater/IOnInflateFinishedListenerInvoker, Xamarin.Android.Support.AsyncLayoutInflater\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V4.View.AsyncLayoutInflater+IOnInflateFinishedListenerImplementor, Xamarin.Android.Support.AsyncLayoutInflater", AsyncLayoutInflater_IOnInflateFinishedListenerImplementor.class, __md_methods);
	}


	public AsyncLayoutInflater_IOnInflateFinishedListenerImplementor ()
	{
		super ();
		if (getClass () == AsyncLayoutInflater_IOnInflateFinishedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V4.View.AsyncLayoutInflater+IOnInflateFinishedListenerImplementor, Xamarin.Android.Support.AsyncLayoutInflater", "", this, new java.lang.Object[] {  });
	}


	public void onInflateFinished (android.view.View p0, int p1, android.view.ViewGroup p2)
	{
		n_onInflateFinished (p0, p1, p2);
	}

	private native void n_onInflateFinished (android.view.View p0, int p1, android.view.ViewGroup p2);

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
