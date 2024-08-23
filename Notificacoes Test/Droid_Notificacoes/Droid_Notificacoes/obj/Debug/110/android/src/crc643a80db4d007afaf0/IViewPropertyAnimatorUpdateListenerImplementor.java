package crc643a80db4d007afaf0;


public class IViewPropertyAnimatorUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc643a80db4d007afaf0.IViewPropertyAnimatorUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationUpdate:(Landroid/view/View;)V:GetOnAnimationUpdate_Landroid_view_View_Handler:Android.Support.V4.View.IViewPropertyAnimatorUpdateListenerInvoker, Xamarin.Android.Support.Compat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V4.View.IViewPropertyAnimatorUpdateListenerImplementor, Xamarin.Android.Support.Compat", IViewPropertyAnimatorUpdateListenerImplementor.class, __md_methods);
	}


	public IViewPropertyAnimatorUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == IViewPropertyAnimatorUpdateListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V4.View.IViewPropertyAnimatorUpdateListenerImplementor, Xamarin.Android.Support.Compat", "", this, new java.lang.Object[] {  });
	}


	public void onAnimationUpdate (android.view.View p0)
	{
		n_onAnimationUpdate (p0);
	}

	private native void n_onAnimationUpdate (android.view.View p0);

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
