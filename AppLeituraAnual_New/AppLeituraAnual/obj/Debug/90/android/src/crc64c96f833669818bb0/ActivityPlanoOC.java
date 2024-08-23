package crc64c96f833669818bb0;


public class ActivityPlanoOC
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_btnFecharClicked_Click:(Landroid/view/View;)V:__export__\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("AppLeituraAnual.ActivityPlanoOC, AppLeituraAnual", ActivityPlanoOC.class, __md_methods);
	}


	public ActivityPlanoOC ()
	{
		super ();
		if (getClass () == ActivityPlanoOC.class)
			mono.android.TypeManager.Activate ("AppLeituraAnual.ActivityPlanoOC, AppLeituraAnual", "", this, new java.lang.Object[] {  });
	}


	public void btnSalvarClicked (android.view.View p0)
	{
		n_btnFecharClicked_Click (p0);
	}

	private native void n_btnFecharClicked_Click (android.view.View p0);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
