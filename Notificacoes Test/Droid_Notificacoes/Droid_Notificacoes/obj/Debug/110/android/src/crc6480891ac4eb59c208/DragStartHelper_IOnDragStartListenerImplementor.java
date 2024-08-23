package crc6480891ac4eb59c208;


public class DragStartHelper_IOnDragStartListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		crc6480891ac4eb59c208.DragStartHelper_IOnDragStartListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDragStart:(Landroid/view/View;Landroid/support/v13/view/DragStartHelper;)Z:GetOnDragStart_Landroid_view_View_Landroid_support_v13_view_DragStartHelper_Handler:Android.Support.V13.View.DragStartHelper/IOnDragStartListenerInvoker, Xamarin.Android.Support.Compat\n" +
			"";
		mono.android.Runtime.register ("Android.Support.V13.View.DragStartHelper+IOnDragStartListenerImplementor, Xamarin.Android.Support.Compat", DragStartHelper_IOnDragStartListenerImplementor.class, __md_methods);
	}


	public DragStartHelper_IOnDragStartListenerImplementor ()
	{
		super ();
		if (getClass () == DragStartHelper_IOnDragStartListenerImplementor.class)
			mono.android.TypeManager.Activate ("Android.Support.V13.View.DragStartHelper+IOnDragStartListenerImplementor, Xamarin.Android.Support.Compat", "", this, new java.lang.Object[] {  });
	}


	public boolean onDragStart (android.view.View p0, android.support.v13.view.DragStartHelper p1)
	{
		return n_onDragStart (p0, p1);
	}

	private native boolean n_onDragStart (android.view.View p0, android.support.v13.view.DragStartHelper p1);

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
