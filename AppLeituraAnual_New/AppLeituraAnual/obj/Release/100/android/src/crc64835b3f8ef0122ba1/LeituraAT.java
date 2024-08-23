package crc64835b3f8ef0122ba1;


public class LeituraAT
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
		mono.android.Runtime.register ("AppBibliaNVI.LeituraAT, AppBibliaNVI", LeituraAT.class, __md_methods);
	}


	public LeituraAT ()
	{
		super ();
		if (getClass () == LeituraAT.class)
			mono.android.TypeManager.Activate ("AppBibliaNVI.LeituraAT, AppBibliaNVI", "", this, new java.lang.Object[] {  });
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
